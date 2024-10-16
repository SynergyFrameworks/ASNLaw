using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Mapping;
using Word;

namespace Infrastructure.Common.DataFile
{
    public class WordTransformer : AbstractDataFileTransformer
    {
        public IWordManager WordManager { get; set; }

        public override Stream TransformToFile(Mapping.Mapping mapping, IList<Dictionary<string, object>> data, IDictionary<string, object> parameters = null)
        {
            var document = parameters != null && parameters.ContainsKey("template") 
                ? WordManager.OpenDocument((string)parameters["template"])
                : WordManager.OpenDocument(Path.GetTempFileName());

            WriteProperties(document,mapping.PropertyMappings,data);

            

            var path = Path.GetTempFileName();

            document.SaveAs(path);
            return new FileStream(path,FileMode.Open);
        }



        private void WriteProperties(IDocument document, IList<PropertyMapping> mappings, IList<Dictionary<string,object>>  data)
        {
            foreach (var mapping in mappings)
            {
                foreach (var value in from item in data where item.ContainsKey(mapping.PropertyName) select item[mapping.PropertyName])
                {
                    if (value is IList) //only try to map list objects as tables
                    {
                        if (mapping.PropertyMappings.Count == 0)
                            continue;
                        WriteTable(document,mapping,(IList<Dictionary<string,object>>)value);
                    }
                    else
                    {
                        document.SetPropertyValue(mapping.MappingInfo, value.ToString());
                    }
                }
            }
        }

        private void WriteTable(IDocument document, PropertyMapping tableMapping, IList<Dictionary<string, object>> data)
        {
            try
            {
                var tables = document.GetTables();
                var tableIndex = int.Parse(tableMapping.MappingInfo);
                if (tableIndex> tables.Count)
                    return;
                var table = tables[tableIndex];

                var rows = table.GetRows();
                var index = rows.Count > 1 ?  2 : 1;
                var copyIndex = rows.Count > 1 ? 1 : 0;
                foreach (var item in data)
                {
                    var newRow = table.CopyRowToIndex(copyIndex, index);
                    var cells = newRow.GetCells();
                    foreach (var mapping in tableMapping.PropertyMappings)
                    {
                        if (!item.ContainsKey(mapping.PropertyName))
                            continue;
                        var value = FormatOutputString(item[mapping.PropertyName]);

                        var cellIndex = int.Parse(mapping.MappingInfo);                        
                        var currCellValue = cells[cellIndex].Value;

                        //replace leading tab - no idea why it's being added
                        if (currCellValue.IndexOf("\t") == 0)
                            currCellValue = currCellValue.Substring(1);
                        var propertyString = String.Format("###{0}.{1}", tableMapping.PropertyName,
                            mapping.PropertyName);
                        cells[cellIndex].Value = currCellValue.Replace(propertyString, value);
                        //temp align right for some auto align left issue
                        decimal test;
                        if(decimal.TryParse(value, out test))
                            cells[cellIndex].AlignRight();
                    }
                    index++;
                }
                table.RemoveRow(copyIndex);
                
            }
            catch (Exception ex)
            {
                //log
            }
        }

        private string FormatOutputString(object obj)
        {
            if (obj is decimal)
            {
                return ((decimal)obj).ToString("N");
            }
            if (obj is int)
            {
                return ((int) obj).ToString("N1");
            }
            if (obj is DateTime)
            {
                return ((DateTime) obj).ToString("d");
            }
            return obj.ToString();
        }
    }
}
