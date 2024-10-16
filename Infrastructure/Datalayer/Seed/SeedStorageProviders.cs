using Datalayer.Domain.Storage;
using System;
using System.Linq;

namespace Datalayer.Seed
{
    public static class SeedStorageProviders
    {
        public static void EnsureStorageProvidersSeeded(this Context.ASNDbContext context)
        {
            if (!context.StorageProviders.Any())
            {
                var firstClient = context.Clients.Where(c => c.Name == "Business Process Solutions" && c.ClientNo == "46549").First();
                var firstTeam = context.Teams.Where(t => t.ClientId == firstClient.Id).Select(t => t.Id).First();
                var group = context.Groups.Where(g => g.TeamId == firstTeam).First();

                var storageProvidersIds = new Guid[] {
                    Guid.Parse("03255B56-3014-4101-9E46-EF88D326B194"),
                    Guid.Parse("52A945B9-2763-4C57-B232-8E0C6344527A"),
                };

                context.StorageProviders.AddRange(new StorageProvider[] {

                    new StorageProvider {
                            Id =  storageProvidersIds[0],
                            ClientId = firstClient.Id,
                            CreatedBy = "system",
                            CreatedDate = DateTime.UtcNow,
                            DisplayName = $"{firstClient.Name}'s One Drive",
                            GroupId = group.Id,
                            ProviderName = "One Drive",
                            ProviderUser = "test-one-drive",
                            ProviderPassword = "password needs encryption",
                            ClientConnection = "https://onedrive.live.com",
                    },

                    new StorageProvider {
                            Id = storageProvidersIds[1],
                            ClientId = firstClient.Id,
                            CreatedBy = "system",
                            CreatedDate = DateTime.UtcNow,
                            DisplayName = $"{firstClient.Name}'s Google Drive",
                            GroupId = group.Id,
                            ProviderName = "Google Drive",
                            ProviderUser = "test-google-drive",
                            ProviderPassword = "password needs encryption",
                            ClientConnection = "https://www.google.com/intl/en/drive/"
                    }
                });

                context.DocumentSources.AddRange(new DocumentSource[] {
                    new DocumentSource {
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Aviation Proposal sources",
                        InputFolder = "Proposal/Aviation/Input",
                        OutputFolder = "Proposal/Aviation/Revised",
                        StorageProviderId = storageProvidersIds[0],
                    },
                    new DocumentSource {
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Automobile Proposal sources",
                        InputFolder = "Proposal/Auto/Input",
                        OutputFolder = "Proposal/Auto/Revised",
                        StorageProviderId = storageProvidersIds[0],
                    },
                    new DocumentSource {
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "Prototypes Proposal sources",
                        InputFolder = "Prototypes/Input",
                        OutputFolder = "Prototypes/Revised",
                        StorageProviderId = storageProvidersIds[1],
                    }
                });

                context.SaveChanges();

            }
        }
    }
}
