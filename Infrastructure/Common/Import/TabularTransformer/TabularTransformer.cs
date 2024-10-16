
using Excel;
using Infrastructure.Import.Model;
using Infrastructure.Common.Domain.Performance;

using Infrastructure.Common.Domain;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.Import.TabularTransformer
{
    public class TabularTransformer : ITabularTransformer
    {
        public IExcelManager ExcelManager { get; set; }
        public IUploadHistoryManager UploadHistoryManager { get; set; }
       


        /// <summary>
        /// This method parses through a given dataupload and return a workbookModel containing the worksheet data of the file. 
        /// <param name="headerColumnCount">Provides a manual way to overwrite how many columns we read, in case the first row is shorter than any rows below</param>
        /// </summary>
        public WorkbookModel Transform(DataUpload upload, int? headerColumnCount = null)
        {
            try
            {
                Stream file = upload.Stream;
                var workbook = ExcelManager.OpenWorkbook(file);

                var workbookModel = new WorkbookModel
                {
                    HeaderRowNumber = upload.HeaderRowNumber,
                    DataRowStartNumber = upload.DataRowStartNumber,
                    FileType = upload.Type,
                    Name = upload.Name,
                };

                if (workbook == null || workbook.Worksheets == null)
                {
                    workbookModel.WorkbookErrors.Add(new ParseError
                    {
                        ErrorMessage = "Error: Cannot read workbook and worksheets"
                    });
                    return CheckAndReturnWorkbookModel(workbookModel);
                }

                if (workbook.Worksheets.Count == 0)
                {
                    workbookModel.WorkbookErrors.Add(new ParseError
                    {
                        ErrorMessage = "Error: No worksheets found in workbook"
                    });
                    return CheckAndReturnWorkbookModel(workbookModel);
                }

                if (workbookModel.DataRowStartNumber <= workbookModel.HeaderRowNumber)
                {
                    workbookModel.WorkbookErrors.Add(new ParseError
                    {
                        ErrorMessage = "Error: Data row starting number should be larger than header row number"
                    });
                    return CheckAndReturnWorkbookModel(workbookModel);
                }

                for (int i = 1; i <= workbook.Worksheets.Count; i++)
                {
                    var sheet = TransformWorksheet(workbook.Worksheets[i], i, workbookModel.HeaderRowNumber, workbookModel.DataRowStartNumber, headerColumnCount);
                    if (sheet!= null)
                    {
                        workbookModel.Worksheets.Add(sheet);
                    }
                }

                return CheckAndReturnWorkbookModel(workbookModel);
            }
            catch (Exception ex)
            {
                upload.Status = "Failed";
                upload.Description = ex.Message;
                UploadHistoryManager.Save(upload);
               
                throw;
            }
        }

        /// <summary>
        /// Generate Ref Data from the workbook based on Summary types and data types
        /// </summary>
        /// <typeparam name="T">SummaryType</typeparam>
        /// <typeparam name="U">DataType</typeparam>
        /// <returns></returns>
        public Dictionary<string, IList<CodeDescription>> GenerateLookUpDataFromWorkbookModel<T, U>(WorkbookModel workbook)
        {
            var refData = new Dictionary<string, IList<CodeDescription>>();
            var summaryType = typeof(T);
            var dataType = typeof(U);
            var objectTypePropertyList = summaryType.GetProperties().Concat(dataType.GetProperties());
            foreach (var property in objectTypePropertyList)
            {
                var refDataAttribute = property.GetCustomAttribute<ReferenceDataAttribute>();
                if (refDataAttribute != null)
                {
                    var refType = refDataAttribute.RefDataType;
                    //var refTypeName = refType.Name; // worksheet name cannot be longer than 31 chars
                    var refTypeName = refType.Name.Length > 31 ? refType.Name.Substring(0, 31) : refType.Name; // worksheet name cannot be longer than 31 chars

                    //we only want distinct ref types
                    if (!refData.ContainsKey(refTypeName))
                    {
                        var refDataSheet = workbook.GetWorksheet(refTypeName);
                        if (refDataSheet != null && !refDataSheet.HasNoDataRows())
                        {
                            var codeDescriptions = new List<CodeDescription>();
                            foreach (var row in refDataSheet.Rows)
                            {
                                //description, Id
                                var id = row.GetCell(2).GetStringValueOrNull();
                                codeDescriptions.Add(new CodeDescription
                                {
                                    Description = row.GetCell(1).GetStringValueOrNull(),
                                    Id = id == null ? Guid.Empty : Guid.Parse(id)
                                });
                            }
                            refData.Add(refTypeName, codeDescriptions);
                        }
                    }
                }
            }
            return refData;
        }

        private WorksheetModel TransformWorksheet(IWorksheet worksheet, int worksheetNumber, int headerRowNumber, int dataRowStartNumber, int? headerColumnNumber)
        {
            try
            {
                var worksheetModel = new WorksheetModel
                {
                    WorksheetNumber = worksheetNumber,
                    HeaderRowNumber = headerRowNumber,
                    DataRowStartNumber = dataRowStartNumber,
                    Name = worksheet.Name,
                };
                var columnIndex = 1;

                //Scan header row
                while (!string.IsNullOrEmpty((string)worksheet.Cells[headerRowNumber, columnIndex].Value))
                {
                    worksheetModel.Headers.Add(new HeaderCell
                    {
                        ColumnName = (string)worksheet.Cells[headerRowNumber, columnIndex].Value,
                        ColumnNumber = columnIndex,
                    });
                    columnIndex++;
                }

                int headerCount = worksheetModel.Headers.Count;
                if (headerCount == 0)
                {
                    //skip sheets without header rows
                   
                    return null;
                }

                //manual overwrite 
                if(headerColumnNumber != null)
                {
                    headerCount = (int) headerColumnNumber;
                }

                //set column types
                worksheetModel = SetColumnTypes(worksheet, worksheetModel);

                //Scan data rows
                int dataRowIndex = dataRowStartNumber;
                bool rowIsEmpty = false;//If the whole row is empty, this will be marked true, thus the row will not be added and the loop will terminate
                while (!rowIsEmpty)
                {
                    rowIsEmpty = true;
                    var row = new WorksheetRow(dataRowIndex);
                    //iterrate through cells in this row
                    for (int i = 1; i <= headerCount; i++)
                    {
                        var cellValue = worksheet.Cells[dataRowIndex, i].Value;
                        if (rowIsEmpty && cellValue != null)
                        {
                                rowIsEmpty = cellValue is string ? string.IsNullOrEmpty(cellValue.ToString()): false;
                        }
                        row.Cells.Add(new WorksheetCell
                        {
                            RowNumber = row.RowNumber,
                            ColumnNumber = i,
                            CellValue = cellValue,
                        });
                    }

                    //the whole row is finished. moving to next row
                    if (!rowIsEmpty)
                        worksheetModel.Rows.Add(row);
                    dataRowIndex++;
                }

                return CheckAndReturnWorksheetModel(worksheetModel);
            }
            catch (Exception ex)
            {
              
                throw;
            }
        }

        private WorksheetModel SetColumnTypes(IWorksheet worksheet, WorksheetModel worksheetModel)
        {
            //check and set the column type for each column
            var headerCount = worksheetModel.Headers.Count;
            int dataRowIndex = worksheetModel.DataRowStartNumber;
            bool rowIsEmpty = false;//If the whole row is empty, this will be marked true, thus the row will not be added and the loop will terminate
                                    //read <5 rows to decide
            int dataRow5Index = dataRowIndex + 5 - 1;
            while (!rowIsEmpty && dataRowIndex <= dataRow5Index)
            {
                rowIsEmpty = true;
                var row = new WorksheetRow(dataRowIndex);
                //iterrate through cells in this row
                for (int i = 1; i <= headerCount; i++)
                {
                    if (worksheet.Cells[dataRowIndex, i].Value != null)
                        rowIsEmpty = false;//if there's value to any cell of this row, this row is not empty
                    row.Cells.Add(new WorksheetCell
                    {
                        RowNumber = row.RowNumber,
                        ColumnNumber = i,
                        CellValue = worksheet.Cells[dataRowIndex, i].Value,
                    });
                }

                //the whole row is finished. moving to next row
                if (!rowIsEmpty)
                    worksheetModel.Rows.Add(row);
                dataRowIndex++;
            }

            //if no data rows found, throw 
            if (worksheetModel.Rows.Count == 0)
            {
             
            }

            worksheetModel.ReinforceColumnType();
            worksheetModel.Rows.Clear();

            return worksheetModel;
        }

        private WorkbookModel CheckAndReturnWorkbookModel(WorkbookModel workbookModel)
        {
            if (workbookModel.WorkbookErrors != null && workbookModel.WorkbookErrors.Count > 0)
            {
               
                string errorList = string.Join("\n", workbookModel.WorkbookErrors.Select(e => e.ErrorMessage));
                throw new Exception($"Following error(s) occured while parsing the workbook: {errorList}");
            }

            return workbookModel;
        }

        private WorksheetModel CheckAndReturnWorksheetModel(WorksheetModel worksheetModel)
        {
            if (worksheetModel.WorksheetErrors != null && worksheetModel.WorksheetErrors.Count > 0)
            {
               
                string errorList = string.Join("\n", worksheetModel.WorksheetErrors.Select(e => e.ErrorMessage));
                throw new Exception($"Following error(s) occured while parsing the workbook: {errorList}");
            }
            return worksheetModel;
        }
    }
}
