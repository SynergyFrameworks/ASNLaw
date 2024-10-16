using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Performance
{
    public interface IUploadDataManager
    {
        String CheckForDuplicateDataConflict(Guid instanceGuid);
        void ClearCache(Guid instanceGuid);
        UploadDataUpdate SaveData(Guid instanceGuid, bool overwrite = true, bool saveUploadData = true);
        Guid LoadFromUpload(DataUpload upload, Dictionary<string, object> additionalParams = null);
    }
}
