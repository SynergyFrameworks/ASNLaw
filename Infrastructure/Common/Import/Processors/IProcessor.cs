using Infrastructure.Import.Model;
using Infrastructure.Common.Domain;
using System.Collections.Generic;


namespace Infrastructure.Import.Processors
{
    public interface IProcessor
    {
        object Process(WorksheetModel worksheetModel);

        object Process(WorksheetModel worksheetModel, Dictionary<string, object> additionalParameters);

        T LookupReferenceData<T>(string key, IDictionary<string, T> existingReferenceDataValues, bool IsNullable = true) where T : CodeDescription;
    }
}
