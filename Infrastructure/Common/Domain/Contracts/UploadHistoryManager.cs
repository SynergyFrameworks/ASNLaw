using Infrastructure.Common.Extensions;
using Infrastructure.Common.Domain.Performance;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace Infrastructure.Common.Business.Performance
{
    public class UploadHistoryManager : IUploadHistoryManager
    {

        public IKeyValueManager AzureBlobManager { get; set; }
        public IEntityManager AzureTableManager { get; set; }
        public IDictionary<string, string> TemplateDic { get; set; }

        public IList<DataUpload> GetAllUploads()
        {
            try
            {

                DataUpload upload = new DataUpload();
                //upload.PartitionKey = Thread.CurrentThread.GetAssignedTenantId().ToString();
                return AzureTableManager.FindAll<DataUpload>(upload.PartitionKey);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public DataUpload GetUpload(string uploadId)
        {
            try
            {
                if (uploadId != null)
                {
                    DataUpload upload = AzureTableManager.Find<DataUpload>(new DataUpload
                    {
                        //PartitionKey = Thread.CurrentThread.GetAssignedTenantId().ToString(),
                        RowKey = uploadId
                    });
                    if (upload == null)
                    {
                        return null;
                    }
                    if (upload.HasAttachment)
                    {
                        upload.Content = ((MemoryStream)AzureBlobManager.GetValue(upload.BlobFileId.ToString())).ToArray();
                    }
                    return upload;
                }
                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }



        public DataUpload Save(DataUpload upload)
        {
            try
            {

                if (upload.Stream != null)
                {
                    upload.BlobFileId = Guid.NewGuid();
                    MemoryStream memoryStream = new MemoryStream(upload.Content) { Position = 0 };
                    AzureBlobManager.CreateOrUpdate(upload.BlobFileId.ToString(), memoryStream);
                }
                else
                {
                    if (upload.Status != "Failed")
                    {

                        upload.Status = "Warning";
                    }
                }

                try
                {
                    // upload.PartitionKey = Thread.CurrentThread.GetAssignedTenantId().ToString();
                    upload.RowKey = Guid.NewGuid().ToString();
                    // AzureTableManager.CreateOrUpdate(upload);
                    return upload;
                }
                catch (Exception e)
                {
                    if (upload.Stream != null)
                    {
                        try
                        {
                            AzureBlobManager.Delete((upload.BlobFileId.ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(
                                String.Format(
                                    "Error saving metadata:{0},followed by error removing blob that was just added:{1}",
                                    e.Message, ex.Message));
                        }

                    }
                    throw new Exception(String.Format("Error saving metadata:{0}", e.Message), e);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid DeleteUpload(DataUpload upload)
        {
            upload.PartitionKey = Thread.CurrentThread.GetAssignedTenantId().ToString();
            try
            {
                DataUpload tempUpload = AzureTableManager.Find<DataUpload>(upload);
                AzureBlobManager.Delete(tempUpload.BlobFileId.ToString());
                AzureTableManager.Delete(upload);
                return upload.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateTemplate(PmTemplate template)
        {
            try
            {
                Stream data = AzureBlobManager.GetValue(template.BlobFileId);
                if (data == null || data.Length == 0)
                    SaveTemplate(template);
                AzureTableManager.CreateOrUpdate(template);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<PmTemplate> GetTemplates(Guid? applicationId)
        {
            try
            {
                List<TableQueryParameters> parametrs = new List<TableQueryParameters>
                {
                    new TableQueryParameters
                    {
                        Comparator = TableQueryParameters.Comparators.StartsWith,
                        PropertyName = "PartitionKey",
                        Value =
                            Thread.CurrentThread.GetAssignedTenantId() + ":" +
                            (applicationId.HasValue ? applicationId.Value.ToString() : "")
                    }
                };
                return AzureTableManager.FindAll<PmTemplate>(parametrs);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public PmTemplate SaveTemplate(PmTemplate template)
        {
            try
            {
                //template.BlobFileId = Thread.CurrentThread.GetAssignedTenantId() + ":" + template.fileType;
                //template.PartitionKey = Thread.CurrentThread.GetAssignedTenantId() + ":" + template.ApplicationId;
                template.RowKey = template.fileType;
                if (template.Content != null && template.Content.Length > 0)
                {
                    AzureBlobManager.CreateOrUpdate(template.BlobFileId, new MemoryStream(template.Content));
                    try
                    {
                        AzureTableManager.CreateOrUpdate(template);
                        return template;
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            AzureBlobManager.Delete(template.BlobFileId);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(
                                String.Format(
                                    "Error saving metadata:{0},followed by error removing blob that was just added:{1}",
                                    e.Message, ex.Message));
                        }
                    }
                }
                else
                {
                    AzureTableManager.CreateOrUpdate(template);

                    return template;
                }
                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public PmTemplate GetTemplate(string fileType, Guid applicationId)
        {
            try
            {
                if (fileType != null)
                {
                    PmTemplate template = AzureTableManager.Find<PmTemplate>(new PmTemplate
                    {
                        //PartitionKey = Thread.CurrentThread.GetAssignedTenantId() + ":" + applicationId,
                        RowKey = fileType
                    });
                    if (template == null)
                    {
                        return null;
                    }
                    template.Content =
                        ((MemoryStream)AzureBlobManager.GetValue(template.BlobFileId)).ToArray();
                    return template;
                }
                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }


        public PmTemplate RecoverOriginTemplate(string fileType, Guid applicationId)
        {
            PmTemplate existingTemplate = GetTemplate(fileType, applicationId);
            string templatePath;
            MemoryStream ms = new MemoryStream();

            if (existingTemplate != null)
            {
                string templateName = TemplateDic.ContainsKey(fileType) ? TemplateDic[fileType] : null;
                if (templateName != null)
                {
                    templatePath = AppDomain.CurrentDomain.BaseDirectory + "Resource\\PmDataTemplates\\" +
                                   templateName;
                    using (FileStream fs = File.OpenRead(templatePath))
                    {
                        fs.CopyTo(ms);
                    }
                    ms.Position = 0;
                    existingTemplate.Name = Path.GetFileName(templatePath);
                    AzureTableManager.CreateOrUpdate(existingTemplate);
                    AzureBlobManager.CreateOrUpdate(existingTemplate.BlobFileId, ms);
                    return existingTemplate;
                }
                else
                {
                    throw new Exception("Original template not found");
                }
            }
            else
            {
                throw new Exception("Template type not found");
            }
        }
    }
}
