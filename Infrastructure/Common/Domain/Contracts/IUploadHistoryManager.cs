using System;
using System.Collections.Generic;
using Infrastructure.Common.Domain.Performance;

namespace Infrastructure.Common.Domain.Performance
{
    public interface IUploadHistoryManager
    {
        IList<DataUpload> GetAllUploads(); 
        DataUpload GetUpload(string uploadId);
        DataUpload Save(DataUpload upload);
        Guid DeleteUpload(DataUpload upload);
        void UpdateTemplate(PmTemplate template);
        IList<PmTemplate> GetTemplates(Guid? applicationId);
        PmTemplate GetTemplate(string fileType, Guid applicationId);
        PmTemplate SaveTemplate(PmTemplate template);
        PmTemplate RecoverOriginTemplate(string fileType, Guid applicationId);
    }
}
