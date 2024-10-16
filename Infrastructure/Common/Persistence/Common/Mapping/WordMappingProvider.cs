using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Word;

namespace Infrastructure.Common.Mapping
{
    public class WordMappingProvider : IMappingProvider
    {
        public IWordManager WordManager { get; set; }
        public string MappingSource { get; set; }
        public IList<Mapping> GenerateMappings()
        {
            var di = new DirectoryInfo(MappingSource);
            return
                di.GetFiles()
                    .Where(f => f.Extension == ".docx")
                    .Select(f => GenerateMapping(new FileInfo(f.FullName)))
                    .ToList();
        }

        public Mapping GenerateMapping(FileInfo file, IDictionary<string, object> parameters = null)
        {
            var document = WordManager.OpenDocument(file.FullName);
            var propertyMappings =
                document.GetProperties()
                    .Select(p => new PropertyMapping {MappingInfo = p.Name, PropertyName = p.Value})
                    .ToList();
            
            propertyMappings.AddRange(document.GetTables().Select(GenerateTableMapping));
            var mapping = new Mapping
            {
                ObjectName = file.Name,
                PropertyMappings = propertyMappings
            };           

            
            return mapping;
        }

        public Mapping GenerateMapping(Stream file, IDictionary<string, object> parameters = null)
        {
            throw new NotImplementedException();
        }

        public Mapping GenerateMapping(FileInfo file, IList<HeaderMetadata> metadata, IDictionary<string, object> parameters = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a property mapping for a template table that is in the format of 
        /// -----HEADER ROW------
        /// -----MAPPING ROW------
        /// -----TOTAL ROW--------
        /// Mappings on the mapping row should be in the form of
        /// ###ListPropertyName.PropertyName (i.e. ###Investments.InvestmentName)
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        public PropertyMapping GenerateTableMapping(ITable table, int tableIndex)
        {
            var propertyMapping = new PropertyMapping {MappingInfo = tableIndex.ToString(), PropertyMappings = new List<PropertyMapping>()};
            var rows = table.GetRows();            
            var mappingRow = rows.Count() > 1? rows[1] : rows[0];
            var tablePropertyName = "";
            var mappingIndex = 0;
            foreach (var cellContents in mappingRow.GetCells().Select(cell => cell.Value))
            {
                
                if (string.IsNullOrEmpty(cellContents) || !cellContents.Contains("###"))
                {
                    mappingIndex++;
                    continue;
                }
                var cellMappingTokens = GetTokensFromCell(cellContents); //we can have multiple mapping entries in a single 

                foreach (var token in cellMappingTokens)
                {
                    if (string.IsNullOrEmpty(tablePropertyName))
                    {
                        tablePropertyName = token.Substring(0, token.IndexOf("."));
                    }

                    propertyMapping.PropertyMappings.Add(new PropertyMapping
                    {
                        PropertyName = token.Substring(token.IndexOf(".") + 1),
                        MappingInfo = mappingIndex.ToString()
                    });
                }                
                mappingIndex++;
            }
            propertyMapping.PropertyName = tablePropertyName;
            return propertyMapping;
        }


        private IList<string> GetTokensFromCell(string cellContents)
        {
            cellContents = cellContents.Substring(cellContents.IndexOf("###"));//strip off any leading text that isn't a mapping token
            var tokens = cellContents.Split("###".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);

            return (from t in tokens let spaceIndex = t.IndexOf(" ") select t.Substring(0, spaceIndex > 0 ? spaceIndex : t.Length)).ToList();
        }
    }
}
