using Excel;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Domain.Performance;
using Infrastructure.Common.Domain.Reference;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Excel.Transformers
{
    public class CustomValueTransformer : AbstractTransformer
    {
        private IEntityManager EntityManager { get; set; }
        private IList<CustomProperty> _customProperties;

        public override object ParseExcelFile(Stream file, Dictionary<string, object> data = null)
        {
            if (file == null || file.Length == 0)
                throw new InvalidDataException("No File Provided");

            List<CustomValue> result = new List<CustomValue>();
            List<CustomProperty> properties = new List<CustomProperty>();
            IWorkbook workbook = ExcelManager.OpenWorkbook(file);
            IWorksheet worksheet = workbook.Worksheets[1];
            string firstColVal = worksheet.Cells[1, 1].Value.SafeToString();
            int rowNum = 1;
            int colNum = 1;
            string relatedObjectType = "";
            while (!string.IsNullOrEmpty(firstColVal))
            {
                if (!properties.Any())
                {
                    relatedObjectType = firstColVal;
                    BuildPropertyList(worksheet, rowNum, properties);
                    rowNum++;
                    continue;
                }
                Guid relatedObjectId = GetRelatedObjectId(relatedObjectType, firstColVal);
                colNum++;
                foreach (CustomProperty property in properties)
                {
                    result.Add(new CustomValue
                    {
                        RelatedObjectId = relatedObjectId,
                        CustomProperty = property,
                        Value = worksheet.Cells[rowNum, colNum].Value.SafeToString() ?? ""
                    });
                    colNum++;
                }

                rowNum++;
                colNum = 1;
                firstColVal = workbook.Worksheets[1].Cells[rowNum, colNum].Value.SafeToString();
            }


            return result;
        }

        private void BuildPropertyList(IWorksheet worksheet, int rowNum, IList<CustomProperty> properties)
        {
            if (_customProperties == null || !_customProperties.Any())
                _customProperties = EntityManager.FindAll<CustomProperty>();
            int colNum = 2;
            string headerVal = worksheet.Cells[rowNum, colNum].Value.SafeToString();
            while (!string.IsNullOrEmpty(headerVal))
            {
                var existing = _customProperties.FirstOrDefault(f => f.Code == headerVal.Replace(" ", "") || f.Description == headerVal);
                if (existing != null)
                {
                    properties.Add(existing);
                }
                else
                {
                    CustomProperty prop = new CustomProperty
                    {
                        Code = headerVal.Replace(" ", ""),
                        Description = headerVal,
                        DataType = "Decimal",//TODO dynamically figure this out
                        DisplayOrder = colNum - 1
                    };
                    EntityManager.Create(prop);
                    properties.Add(prop);
                }
                colNum++;
                headerVal = worksheet.Cells[rowNum, colNum].Value.SafeToString();
            }
        }
        private Guid GetRelatedObjectId(string objectType, string value)
        {
            //if (objectType == "Task")
            //{
            //    if (_task == null || !_task.Any())
            //        _task = EntityManager.FindAll<Task>();
            //    var existing = _task.FirstOrDefault(f => f.Name == value || f.LegalName == value || f.Code == value);
            //    if (existing != null)
            //        return existing.Id;
            //    var newFund = new Task
            //    {
            //        Name = value,
            //        LegalName = value,
            //        Code = value
            //    };
            //    TaskManager.SaveFund(newFund);
            //    return newFund.Id;
            //}

            throw new InvalidDataException("Invalid object type specififed: " + objectType);
        }

    }
}
