using Infrastructure.Import.Model;
using Infrastructure.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;
using Infrastructure.Common.Extensions;
using System;

namespace Infrastructure.Import.Processors
{
    public abstract class AbstractProcessor : IProcessor
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AbstractProcessor));
        public virtual object Process(WorksheetModel worksheetModel) {
            throw new System.NotImplementedException();
        }

        public virtual object Process(WorksheetModel worksheetModel, Dictionary<string, object> additionalParameters)
        {
            throw new System.NotImplementedException();
        }

        protected bool IsEmptyWorksheet(WorksheetModel worksheetModel)
        {
            if (worksheetModel.HasNoDataRows())
                Log.Info($"Now rows found for type {worksheetModel.Name}.");
            return worksheetModel.HasNoDataRows();
        }

        public T LookupReferenceData<T>(string key, IDictionary<string, T> existingReferenceDataValues, bool IsNullable = true) where T : CodeDescription
        {

            if (key == null)
                key = "";

            key = key.CollapseWhiteSpace().Trim();
            if (existingReferenceDataValues != null && existingReferenceDataValues.Any())
            {
                var existingValuesList = existingReferenceDataValues.Keys.Aggregate("", (current, nextKey) => nextKey + ", " + current);
                existingValuesList.Remove(existingValuesList.Length - 2);
                var matchedValue = existingReferenceDataValues.Select(_value => _value.Value).FirstOrDefault(_value => _value.Code == key || _value.Description == key);
                if (matchedValue == null && !IsNullable)
                {
                    Log.WarnFormat($"Unable to match {typeof(T).Name} reference value with range key {key}; options are: {existingValuesList}");
                    throw new Exception(string.Format($"Unable to match {typeof(T).Name} reference value with range key {key}; options are: {existingValuesList}"));
                }

                return matchedValue;
            }
            else
            {
                Log.WarnFormat($"Unable to find any existing {typeof(T).Name} reference values in our database.");
                throw new Exception(string.Format($"Unable to find any existing {typeof(T).Name} reference values in our database."));
            }
        }
    }
}
