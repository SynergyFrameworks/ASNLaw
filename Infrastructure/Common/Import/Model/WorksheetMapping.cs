using System;
using System.Collections.Generic;

namespace Infrastructure.Import.Model
{
    public class WorksheetMapping
    {
        public string WorksheetName { get; set; }
        public List<WorksheetColumnMapping> ColumnMappings { get; set; }
        public int StartRowNumber { get; set; }
        public int EndRowNumber { get; set; }

        public WorksheetMapping(string worksheetName)
        {
            WorksheetName = worksheetName;
            ColumnMappings = new List<WorksheetColumnMapping>();
        }

        public void AddColumnMapping(WorksheetColumnMapping columnMapping)
        {
            ColumnMappings.Add(columnMapping);
        }

        public WorksheetColumnMapping GetColumnMapping(int columnNumber)
        {
            if (IsMappingsEmpty())
                return null;
            return ColumnMappings.Find(mapping => mapping.ColumnNumber == columnNumber);
        }

        public WorksheetColumnMapping GetColumnMapping(string propertyName)
        {
            if (IsMappingsEmpty())
                return null;
            return ColumnMappings.Find(mapping => mapping.PropertyName == propertyName);
        }

        // Define the indexer to allow use of [] notation.
        public int this[string propertyName]
        {
            get
            {
                var columnMapping = ColumnMappings.Find(mapping => mapping.PropertyName == propertyName);
                if (columnMapping == null)
                {
                    throw new Exception($"Error: Accessing Mapping {WorksheetName} property {propertyName} failed");
                }
                return columnMapping.ColumnNumber;
            }
        }

        public Dictionary<string, int> GetDictionaryFormatMappings()
        {
            if (IsMappingsEmpty())
                return null;
            var mappings = new Dictionary<string, int>();
            foreach (WorksheetColumnMapping columnMapping in ColumnMappings)
            {
                mappings.Add(columnMapping.PropertyName, columnMapping.ColumnNumber);
            }
            return mappings;
        }

        private bool IsMappingsEmpty()
        {
            return ColumnMappings == null || ColumnMappings.Count == 0;
        }
    }
}
