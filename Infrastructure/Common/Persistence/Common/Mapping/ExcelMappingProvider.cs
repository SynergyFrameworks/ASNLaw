using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Excel;
using Infrastructure.Common.Persistence.Common.Mapping;

namespace Infrastructure.Common.Mapping
{
    /// <summary>
    /// Mapping provider for excel files. It will read in an Excel file and look for mapped fields by looking for a configurable token (default is ###)
    /// The property info will be whatever is after the token, and the mapping info will be the cell address. 
    /// The address includes sheet information, so this supports mapping to multiple worksheets. 
    /// </summary>
    public class ExcelMappingProvider : IMappingProvider
    {
        public string MappingSource { get; set; } //Assume to be a directory for the time being. 
        private string _mappingToken;
        public String MappingToken
        {
            get
            {
                if (string.IsNullOrEmpty(_mappingToken))
                    _mappingToken = "###";
                return _mappingToken;
            }
            set { _mappingToken = value; }
        }
        public IExcelManager ExcelManager { get; set; }

        public IList<Mapping> GenerateMappings()
        {
            if (MappingSource == "assembly")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var files = assembly.GetManifestResourceNames().Where(f => f.Contains(".xls"));
                return files.Select(f => GenerateMapping(assembly.GetManifestResourceStream(f), f)).ToList();
            }
            var dir = new DirectoryInfo(MappingSource);

            var excelFiles = dir.GetFiles("*.xls");
            return excelFiles.Select(e => GenerateMapping(e)).ToList();
        }

        public Mapping GenerateMapping(FileInfo file, IDictionary<string, object> parameters = null)
        {
            return GenerateMapping(ExcelManager.OpenWorkbook(file), file.Name, parameters);
        }

        public Mapping GenerateMapping(Stream file, IDictionary<string, object> parameters = null)
        {
            return GenerateMapping(ExcelManager.OpenWorkbook(file), "", parameters);
        }

        public Mapping GenerateMapping(FileInfo file, IList<HeaderMetadata> metadata, IDictionary<string, object> parameters = null)
        {
            var workbook = ExcelManager.OpenWorkbook(file);
            var mappingCells = GetMappingCells(workbook, parameters);

            var currentMetadataCellPair = mappingCells.FirstOrDefault(c => c.Value.RangeValue == MappingToken + "Data.Metadata");
            var currentDataCellPair = mappingCells.FirstOrDefault(c => c.Value.RangeValue == MappingToken + "Data.Data");

            var mappings = mappingCells.Where(i => !i.Value.RangeValue.Contains("Data")).Select(mappingCell => new PropertyMapping
            {
                PropertyName = mappingCell.Value.RangeValue.Replace(MappingToken, ""),
                MappingInfo = mappingCell.Key
            }).ToList();

            if (string.IsNullOrEmpty(currentDataCellPair.Key) || string.IsNullOrEmpty(currentMetadataCellPair.Key))
                return new Mapping { PropertyMappings = mappings };

            var currentMetadataCell = currentMetadataCellPair.Key;
            var currentDataCell = currentDataCellPair.Key;
            var dataMappings = new PropertyMapping
            {
                PropertyMappings = new List<PropertyMapping>(),
                PropertyName = "Data"
            };
            mappings.Add(dataMappings);
            foreach (var header in metadata.Where(m => m.IsVisible))
            {
                var newMetadataMapping = new PropertyMapping
                {
                    PropertyName = header.DataKey,
                    HeaderName = header.DisplayValue,
                    MappingInfo = currentMetadataCell,
                    IsHeaderMapping = true
                };
                mappings.Add(newMetadataMapping);
                currentMetadataCell = GetNextCellHorizontal(newMetadataMapping);

                var newDataMapping = new PropertyMapping
                {
                    PropertyName = header.DataKey,
                    HeaderName = header.DisplayValue,
                    MappingInfo = currentDataCell,
                    Format = header.Type
                };
                dataMappings.PropertyMappings.Add(newDataMapping);
                currentDataCell = GetNextCellHorizontal(newDataMapping);
            }

            return new Mapping
            {
                PropertyMappings = mappings
            };
        }

        public Mapping GenerateMapping(IWorkbook workbook, string fileName, IDictionary<string, object> parameters = null)
        {
            var mappingCells = GetMappingCells(workbook, parameters);

            var flatMappings = new List<PropertyMapping>();
            foreach (var cell in mappingCells)
            {
                if (cell.Value.RangeValue.Contains("|"))
                {
                    var mappings = cell.Value.RangeValue.Split('|');
                    flatMappings.AddRange(mappings.Select(mapping => new PropertyMapping
                    {
                        PropertyName = mapping.Replace(MappingToken, ""),
                        MappingInfo = cell.Key,
                        ColumnNumber = cell.Value.ColumnIndex
                    }));
                }
                else
                {
                    flatMappings.Add(new PropertyMapping
                    {
                        PropertyName = cell.Value.RangeValue.Replace(MappingToken, ""),
                        MappingInfo = cell.Key,
                        ColumnNumber = cell.Value.ColumnIndex
                    });
                }
            }

            return new Mapping
            {
                ObjectName = fileName,
                PropertyMappings = flatMappings,
            };
        }

        private List<KeyValuePair<string, RowMetadata>> GetMappingCells(IWorkbook workbook, IDictionary<string, object> parameters)
        {
            var sheets = new List<IWorksheet>();
            if (parameters != null && parameters.ContainsKey("worksheet"))
                sheets.Add(workbook.Worksheets[(string)parameters["worksheet"]]);
            else
                for (var i = 0; i < workbook.Worksheets.Count; i++)
                {
                    sheets.Add(workbook.Worksheets[i]);
                }
            var mappingCells = new List<KeyValuePair<string, RowMetadata>>();
            foreach (var sheet in sheets)
            {
                mappingCells.AddRange(sheet.FindWithValue(MappingToken).Select(valuePair =>
                new KeyValuePair<string, RowMetadata>(valuePair.Key,
                new RowMetadata
                {
                    ColumnIndex = valuePair.Value.Start.Column,
                    RowIndex = valuePair.Value.Rows,
                    RangeValue = valuePair.Value.GetValue<string>()
                })));
            }
            return mappingCells;
        }

        private PropertyMapping GeneratePropertyMapping(string propertyName, IList<PropertyMapping> flatMappings)
        {
            var mapping = new PropertyMapping
            {
                PropertyName = propertyName,
                PropertyMappings = flatMappings.Where(m => !m.PropertyName.Contains(".")).ToList()
            };
            var objects = flatMappings.Where(m => m.PropertyName.Contains(".")).Select(m => m.PropertyName.Substring(0, m.PropertyName.IndexOf(".")));
            foreach (var obj in objects)
            {
                mapping.PropertyMappings.Add(GeneratePropertyMapping(obj, flatMappings.Where(m => m.PropertyName.StartsWith(obj + ".")).Select(m => new PropertyMapping { MappingInfo = m.MappingInfo, PropertyName = m.PropertyName.Substring(m.PropertyName.IndexOf(".") + 1) }).ToList()));
            }
            return mapping;
        }

        private Mapping GenerateMapping(Stream file, string fileName)
        {
            var propertyMappings = new List<PropertyMapping>();
            var workbook = ExcelManager.OpenWorkbook(file);
            foreach (var worksheet in workbook.Worksheets)
            {
                var mappingCells = worksheet.FindWithValue(MappingToken);
                propertyMappings.AddRange(mappingCells.Select(m => new PropertyMapping { MappingInfo = m.Key, PropertyName = m.Value.GetValue<string>().Replace(MappingToken, "") }));
            }
            return new Mapping
            {
                ObjectName = fileName,
                PropertyMappings = propertyMappings
            };
        }

        private string GetNextCellHorizontal(PropertyMapping cell, bool getCellToRight = true)
        {
            var currentCol = ExcelHelper.GetColNumberFromName(cell.Column);
            currentCol = currentCol + (getCellToRight ? 1 : -1);
            return cell.SheetName + "!" + ExcelHelper.GetExcelColumnFromNumber(currentCol) + cell.RowNumber;
        }
    }
}
