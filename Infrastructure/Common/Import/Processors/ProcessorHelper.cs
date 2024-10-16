using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Infrastructure.Import.Model;

namespace Infrastructure.Import.Processors
{
    public static class ProcessorHelper
    {
        public static void CheckMapping(WorksheetMapping mapping, WorksheetModel model, bool ignoreBlankSheet = true)
        {
            if (mapping == null || mapping.ColumnMappings == null || mapping.ColumnMappings.Count == 0)
                throw new Exception($"Mapping not found for worksheet {(model == null ? string.Empty : model.Name)}");

            if (model == null)
                throw new Exception($"Worksheet not found for {mapping.WorksheetName}");

            if (ignoreBlankSheet == false && (model.Rows == null || model.Rows.Count == 0))
                throw new Exception($"Worksheet model {model.Name} has no data");

            if (model.Headers == null || model.Headers.Count < mapping.ColumnMappings.Count)
            {
                throw new Exception($"Worksheet model {model.Name} missing header columns");
            }
        }

        public static T MatchReferenceData<T>(WorksheetCell worksheetCell, Dictionary<string, T> data, bool cleanValue = true) where T : class
        {
            if (data == null || !data.Any() || worksheetCell == null)
            {
                return null;
            }
            var key = worksheetCell.GetStringValueOrNull();
            if (string.IsNullOrEmpty(key))
                return null;

            if (cleanValue)
            {
                key = Regex.Replace(key, @"\s+", " ").Trim();
            }
            if (!data.ContainsKey(key))
            {
                throw new Exception(string.Format($"Unable to match {typeof(T).Name} reference value with range key {key}"));
            }
            return data[key];
        }

    }
}
