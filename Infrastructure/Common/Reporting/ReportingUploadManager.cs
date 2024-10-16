using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Common.Persistence;
using Infrastructure.Common.Persistence.Cache;
using log4net;
using Excel;
using Infrastructure.Exceptions;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Domain.Performance;
using Infrastructure.Common.Domain.Reporting;
using Infrastructure.Common;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Reporting
{
    public class ReportingUploadManager : IReportingUploadManager
    {
        private ILog Log = LogManager.GetLogger(typeof (ReportingUploadManager));
        public IExcelImportManager ExcelImportManager { get; set; }

        public IUploadHistoryManager UploadHistoryManager { get; set; }
      
        public IEntityManager EntityManager { get; set; }

        public string CheckForDuplicateDataConflict(Guid instanceGuid)
        {
            return null;
        }

        public void ClearCache(Guid instanceGuid)
        {
            throw new NotImplementedException();
        }




        //[UpdateClient("progressBarHandler", "Data loaded from file", 40)]
        public Guid LoadFromUpload(DataUpload upload, Dictionary<string, object> additionalParams = null)
        {            
            try
            {
                if (string.IsNullOrEmpty(upload.Type))
                    upload.Type = "PSF";
                var instanceGuid = Guid.NewGuid();
                var fileData = ExcelImportManager.ParseExcelFile<FileData>(upload.Stream);
                FirmCache.AddItemFixed(GetCacheString(instanceGuid.ToString(),"reporting"),fileData,DateTime.UtcNow.AddMinutes(15d));
                FirmCache.AddItemFixed(GetCacheString(instanceGuid.ToString(),"metadata"),upload,DateTime.UtcNow.AddMinutes(15d));
                return instanceGuid;
            }
            catch (Exception ex)
            {
                upload.Status = "Failed";
                upload.Description = ex.Message;
                UploadHistoryManager.Save(upload);
                Log.ErrorFormat("Error loading and saving reporting data {0}",ex);
                throw new UploadDataException(ex.Message, ex);
            }
        }

        
        private string GetCacheString(string id,string type)
        {
            return Thread.CurrentThread.GetAssignedTenantId() + id + type + Thread.CurrentThread.GetUserId();
        }

        private void SaveReportingPeriod(ReportingPeriod period)
        {                        
            EntityManager.BatchCreateOrUpdate(new List<ReportingPeriod> {period});
        }

        public ReportingPeriod GetLatestReportingPeriod()
        {
            var parameters = new List<TableQueryParameters>
                {
                    new TableQueryParameters
                        {
                            Comparator = TableQueryParameters.Comparators.EqualTo,
                            PropertyName = "PartitionKey",
                            Value =
                                Thread.CurrentThread.GetAssignedTenantId()
                        }
                };
            var periods =  EntityManager.FindAll<ReportingPeriod>(parameters);

            return periods.OrderByDescending(p => p.PeriodEndDate).FirstOrDefault();
        }

        public IList<ReportingPeriod> GetReportingPeriods()
        {
            var parameters = new List<TableQueryParameters>
                {
                    new TableQueryParameters
                        {
                            Comparator = TableQueryParameters.Comparators.EqualTo,
                            PropertyName = "PartitionKey",
                            Value =
                                Thread.CurrentThread.GetAssignedTenantId()
                        }
                };
            return EntityManager.FindAll<ReportingPeriod>(parameters);            
        }

        public UploadDataUpdate SaveData(Guid instanceGuid, bool overwrite = true, bool saveUploadData = true)
        {
            throw new NotImplementedException();
        }
    }
}
