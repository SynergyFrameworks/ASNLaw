using Datalayer.Domain.Group;
using Datalayer.Domain.Storage;
using System;
using System.Linq;

namespace Datalayer.Seed
{
    public static class SeedDocuments
    {
        public static void EnsureDocumentSeeded(this Context.ASNDbContext context)
        {
            if (!context.ProjectDocuments.Any())
            {
                StorageProvider firstStorageProvider = context.StorageProviders.Where(c => c.ProviderName == "Google Drive" && c.ProviderUser == "test-google-drive").First();
                Guid firstStorageProviderId = context.StorageProviders.Where(c => c.ProviderName == "Google Drive" && c.ProviderUser == "test-google-drive").Select(t => t.Id).First(); ;
                Guid firstDocumentSourcesId = context.DocumentSources.Where(t => t.StorageProviderId == firstStorageProvider.Id).Select(t => t.Id).First();
                Guid firstProjectId = context.Projects.Select(t => t.Id).First();

                Guid[] documentSourceIds = new Guid[] {
                    Guid.Parse("03255B56-3014-4101-9E46-EF88D326B194"),
                    Guid.Parse("52A945B9-2763-4C57-B232-8E0C6344527A"),
                };


                context.ProjectDocuments.AddRange(new ProjectDocument[] {

                  new ProjectDocument
                                     {
                                          Id = Guid.NewGuid(),
                                          Name = "IDENTITY_VERIFICATION_Document",
                                          Extension = "docx",
                                          Url = "http://drive.test.com",
                                          Description = "The description of the lvetting and biometric capture for U.S. civilian agencies",
                                          IsOutput = true,
                                          Size = 3223,
                                          DocumentSourceId = firstDocumentSourcesId,
                                          ProjectId  =  firstProjectId,
                                          CreatedBy = "System",
                                          CreatedDate = DateTime.UtcNow,
                                     },
                  new ProjectDocument
                                     {
                                          Id = Guid.NewGuid(),
                                          Name = "LITIGATION_SUPPORT_Document",
                                          Extension = "docx",
                                          Url = "http://drive.test.com",
                                          Description = "The description of the litigation support services",
                                          IsOutput = true,
                                          Size = 3223,
                                          DocumentSourceId = firstDocumentSourcesId,
                                          ProjectId  =  firstProjectId,
                                          CreatedBy = "System",
                                          CreatedDate = DateTime.UtcNow,
                                     },
                  new ProjectDocument
                                     {
                                          Id = Guid.NewGuid(),
                                          Name = "LITIGATION_SUPPORT_Document",
                                          Extension = "docx",
                                          Url = "http://drive.test.com",
                                          DocumentSourceId = firstDocumentSourcesId,
                                          ProjectId  =  firstProjectId,
                                          Description = "The description of the litigation support services",
                                          IsOutput = true,
                                          Size = 3223,
                                          CreatedBy = "System",
                                          CreatedDate = DateTime.UtcNow

                                     },
                  new ProjectDocument{

                                          Id = Guid.NewGuid(),
                                          Name = "Immigration_Identity_Document",
                                          Extension = "docx",
                                          Url = "http://drive.test.com",
                                          Description = "The description of the immigration book",
                                          IsOutput = true,
                                          Size = 3223,
                                          DocumentSourceId = firstDocumentSourcesId,
                                          ProjectId  =  firstProjectId,
                                          CreatedBy = "System",
                                          CreatedDate = DateTime.UtcNow

                                   }

                });


                context.SaveChanges();

            }
        }
    }
}