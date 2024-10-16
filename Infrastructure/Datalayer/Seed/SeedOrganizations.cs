using Datalayer.Domain;
using Datalayer.Domain.Demographics;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Datalayer.Seed
{
    public static class SeedOrganizations
    {
        public static void EnsureOrganizationSeeded(this Context.ASNDbContext context)
        {
            if (!context.Organizations.Any())
            {

                var SeriesId = Guid.NewGuid();
                var clientId = Guid.NewGuid();
                var groupId = Guid.NewGuid();
                var ProjectIds = new Guid[] {
                    Guid.Parse("93255B69-3014-4101-9E46-EF88D326B111"),
                    Guid.Parse("42A945A9-2763-4C57-B232-8E0C63445212"),
                      };

       
                context.Organizations.AddRange(new List<Organization>() {

                new Organization {
                    Id = Guid.Parse("88717994-5DE6-4DE5-B7BD-FE12D14F6C37"),
                    Addresses = new List<Address>() {
                        new Address{
                            AddressLine1 = "7799 Leesburg Pike",
                            AddressLine2 = "Suite 300 North",
                            AddressType = "Street",
                            City = "Falls Church",
                            StateCode = "VA ",
                            CountryCode = "US",
                            PostalCode = "22043-2408",
                            Id =  Guid.NewGuid(),
                            CreatedBy = "System",
                            CreatedDate =  DateTime.UtcNow
                        }
                    },
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "system",
                    Name = "PAE",
                    ImageUrl = "https://www.pae.com/sites/default/files/PAE-Logo-Transparent_1.gif",
                    Phones = new List<Phone>() {
                        new Phone {
                            Id = Guid.NewGuid(),
                            CountryPrefix = "1",
                            CreatedBy = "system",
                            CreatedDate = DateTime.UtcNow,
                            PhoneNumber = "888-526-5416",
                            PhoneType = "Phone"
                        },
                    },
                    WebUrl = "https://www.pae.com/",
                    Clients = new List<ASNClient>()  {
                        new ASNClient {
                        Id = clientId,
                        ClientNo = "46549",
                        Name = "Business Process Solutions",
                        OrganizationId = Guid.Parse("88717994-5DE6-4DE5-B7BD-FE12D14F6C37"),
                        Addresses = new List<Address>() {
                        new Address{
                            AddressLine1 = "7799 Leesburg Pike",
                            AddressLine2 = "Suite 300 North",
                            AddressType = "Street",
                            City = "Falls Church",
                            StateCode = "VA ",
                            CountryCode = "US",
                            PostalCode = "22043-2408",
                            Id =  Guid.NewGuid(),
                            CreatedBy = "System",
                            CreatedDate =  DateTime.UtcNow
                        } },
                        Phones = new List<Phone>() {
                        new Phone {
                            Id = Guid.NewGuid(),
                            CountryPrefix = "1",
                            CreatedBy = "system",
                            CreatedDate = DateTime.UtcNow,
                            PhoneNumber = "888-526-5416",
                            PhoneType = "Main",
                            

                        } },
                        Description = "PAE's Business Process Solutions services make government business transactions more efficient and effective. They support personnel security, immigration identity and verification, records and mail management, litigation support, claims processing and shared services for contact centers.",
                        WebUrl = "https://www.pae.com/",
                        ImageUrl = "https://www.pae.com/sites/default/files/PAE-Logo-Transparent_1.gif",
                          
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow,
                        Teams = new List<Team>()
                          {
                               new Team
                               {
                                  Id = Guid.NewGuid(),
                                  Name = "Red Team",
                                  ClientId = clientId,
                                  Description = "The Red Team",
                                  CreatedBy = "System",
                                  CreatedDate =  DateTime.UtcNow,
                                  Groups = new List<ASNGroup>()
                                  {
                                       new ASNGroup
                                       {
                                            Id =  Guid.NewGuid(),
                                            Name = "Assesment",
                                            Description = "Project Test Workgroup",
                                            ImageUrl = "https://www.pae.com/sites/default/files/PAE-Logo-Transparent_1.gif",
                                            CreatedBy = "System",
                                            CreatedDate = DateTime.UtcNow,
                                            Projects = new List<Project>()
                                              {
                                                 new Project
                                                  {
                                                    Id = Guid.NewGuid(),
                                                    Name = "CLAIMS PROCESSING",
                                                    Description = "PAE provides health and insurance eligibility services for government customers as part of our information optimization capabilities.",
                                                    ImageUrl = "",
                                                    GroupId = groupId,

                                        
                                                    CreatedBy = "System",
                                                    CreatedDate =  DateTime.UtcNow


                                                  },
                                                 new Project
                                                  {
                                                    Id = Guid.NewGuid(),
                                                    Name = "IMMIGRATION, IDENTITY AND VERIFICATION",
                                                    Description = "PAE has over 2,000 employees providing fee collection, application preparation, vetting and biometric capture for U.S. civilian agencies.",
                                                    ImageUrl = "",
                                                    GroupId = groupId,
                                                   
                                                    CreatedBy = "System",
                                                    CreatedDate =  DateTime.UtcNow

                                                  },
                                                 new Project
                                                  {
                                                    Id = ProjectIds[0],
                                                    Name = "LITIGATION SUPPORT",
                                                    Description = "PAE litigation support services include web hosting, eDiscovery, technology-assisted review as well as other litigation staffing services that bring efficiencies to the workload of U.S. government lawyers. Government agencies can access these services via the DOJ MEGA IV IDIQ or our GSA Schedule.",
                                                    ImageUrl = "",
                                                    GroupId = groupId,

                                                    
                                                    CreatedBy = "System",
                                                    CreatedDate =  DateTime.UtcNow


                                                  },
                                                 new Project
                                                  {

                                                    Id = ProjectIds[1],
                                                    Name = "RECORDS HANDLING AND MAIL MANAGEMENT",
                                                    Description = "PAE provides records handling and mail management services supporting the U.S. government in over 100 StateCodeside locations. ",
                                                    ImageUrl = "",
                                                    GroupId = groupId,
                                                   
                                                     CreatedBy = "System",
                                                     CreatedDate =  DateTime.UtcNow,
                                                     Series = new List<Series>()
                                                {
                                                     new Series{
                                                      Id = SeriesId,
                                                      Name = "Series Test",
                                                      Description = "Test Series",
                                                      CreatedBy = "System",
                                                      CreatedDate = DateTime.UtcNow,
                                                      Project = new Project
                                                  {
                                                    Id = Guid.NewGuid(),
                                                    Name = "IMMIGRATION, IDENTITY AND VERIFICATION",
                                                    Description = "PAE has over 2,000 employees providing fee collection, application preparation, vetting and biometric capture for U.S. civilian agencies.",
                                                    ImageUrl = "http://test.com",
                                                    GroupId = groupId,
                                                   
                                                    CreatedBy = "System",
                                                    CreatedDate =  DateTime.UtcNow

                                                  },
                                                      Tasks = new List<ASNTask>(){

                                                     new ASNTask{
                                                         Id = Guid.NewGuid(),
                                                         Name = "Parse",
                                                         ShortName = "TParse",
                                                         ProjectId = ProjectIds[1],
                                                         Description = "Testing Parse Task",
                                                         ModuleId = Guid.Parse("26F1B54F-FC8B-480D-A0A0-592A63F8B9B5"),
                                                         CreatedBy = "System",
                                                         CreatedDate = DateTime.UtcNow,
                                                      },

                                                     new ASNTask{
                                                         Id = Guid.NewGuid(),
                                                         Name = "Matrix",
                                                         Description = "Testing Matrix Task",
                                                         ShortName = "TESTM",
                                                         ProjectId = ProjectIds[0],
                                                         ModuleId = Guid.Parse("26F1B54F-FC8B-480D-A0A0-592A63F8B9B5"),
                                                         CreatedBy = "System",
                                                         CreatedDate = DateTime.UtcNow,

                                                      },


                                                      }


                                                     }

                                                },
                                                  },
                                              },
                                            Resources = new List<Resource>
                                               {
                                                   new Resource
                                                   {
                                                        Id = Guid.NewGuid(),
                                                        ResourceType = "Test",
                                                        JsonValues = Newtonsoft.Json.JsonConvert.SerializeObject(new {Level = "one", IsEnabled = true}),
                                                        CreatedBy = "System",
                                                        CreatedDate = DateTime.UtcNow,


                                                   }

                                               },
                                            Modules = new List<Module>()
                                               {
                                                    new Module{
                                                     Id = Guid.Parse("26F1B54F-FC8B-480D-A0A0-592A63F8B9B5"),
                                                     Title = "Module Test",
                                                     Notes = "Test Module Notes",
                                                     Description = "Market Test Module",
                                                     Version = "V1",
                                                     RequireLicenseAcceptance = true,
                                                     CreatedBy = "System",
                                                     CreatedDate =  DateTime.UtcNow,
                                                      


                                                    }
                                                },


                                       }

                                  }

                               }

                          }





                        }

                    },
                    MailBoxes = new List<MailBox>()
                     {
                         new MailBox(){
                     Server = "google Mail Server",
                     FromAddress = "admin@scionanalytics.com",
                     AdminEmail = "admin@scionanalytics.com",
                     ServerUserName = "Google",
                     ServerPassword = "Pa$$word",
                     ConnectionSecurity = "SSL",
                     CreatedBy = "System",
                     CreatedDate =  DateTime.UtcNow,

                         },

                               new MailBox(){
                      Server = "yahoo Mail Server",
                     FromAddress = "admin@scionanalytics.com",
                     AdminEmail = "admin@scionanalytics.com",
                     ServerUserName = "Yahoo",
                     ServerPassword = "Pa$$word",
                     ConnectionSecurity = "TLS",
                     CreatedBy = "System",
                     CreatedDate =  DateTime.UtcNow,

                         },



                           new MailBox(){
                     Server = "Azure Mail Server",
                     FromAddress = "admin@scionanalytics.com",
                     AdminEmail = "admin@scionanalytics.com",
                     ServerUserName = "Azure",
                     ServerPassword = "Pa$$word",
                     ConnectionSecurity = "PGP",
                     CreatedBy = "System",
                     CreatedDate =  DateTime.UtcNow,

                         },







                     },
                     
                    #region  Domain.Security.User to save for later
                   

                    //User = new Domain.Security.User {
                    //    IdentityUserId = Guid.Parse("694DAE48-8CD1-4D09-9DB1-A9DC9FA95597"),
                    //    CreatedBy = "system",
                    //    CreatedDate = DateTime.UtcNow,
                    //    Email = "user@pae.com",
                    //    IsActive = true,
                    //    Name = "Main PAE User",
                    //    UserName = "paeuser",
                    //    ImageUrl = "https://www.pae.com/sites/default/files/PAE-Logo-Transparent_1.gif",
                    //    Subscriptions = new List<UserSubscription> {
                    //        new UserSubscription {
                    //            CreatedBy = "system",
                    //            CreatedDate = DateTime.UtcNow,
                    //            Subscription = new Subscription {
                    //                Key = "47885",
                    //                CreatedBy = "system",
                    //                CreatedDate = DateTime.UtcNow,
                    //                Name = "Test Subscription",
                    //                SubLevel = 1,
                    //                ServiceContract = new ServiceContract {
                    //                    Amount = 0,
                    //                    ContractType = "Demo",
                    //                    CreatedBy = "system",
                    //                    CreatedDate = DateTime.UtcNow,
                    //                    ExpirationDate = DateTime.UtcNow.AddDays(30),
                    //                    StartDate = DateTime.UtcNow,
                    //                },
                    //                Module = new Module {
                    //                    Authors = "Scion",
                    //                    CreatedBy = "system",
                    //                    CreatedDate = DateTime.UtcNow,
                    //                    IsInstalled = true,
                    //                    IsRemovable = true,
                    //                    LicenseUrl = "",
                    //                    Notes = "The Test Environment.",
                    //                    Owners = "Scion Analytics",
                    //                    Version = "Demo-1.0.0.0",
                    //                    VersionTag = "Trial",
                    //                    ProjectUrl = "https://dev.azure.com/scionanalytics/PDA%20Web%20Services",
                    //                    Description = "Trial Module for Acme Analytics.",
                    //                    RequireLicenseAcceptance = false,
                    //                    Tags = "Trial;Demo",
                    //                    Title = "Demo Module",
                    //                }
                    //            }
                    //        }
                    //    },
                    //    Groups = new List<ASNGroup>{
                    //        new ASNGroup {
                    //            Team = new Team {

                    //                CreatedBy = "system",
                    //                CreatedDate = DateTime.UtcNow,
                    //                Description = "Trial for Scion Analytics",

                    //                Id = Guid.Parse("B781DD6B-FCDA-420D-96CD-3417C97E5E5D"),
                    //                Name = "Acme Analytics Demo",
                    //                ClientId = Guid.Parse("be61b7da-b961-48e5-858c-c4f83b4c7b2c")

                    //            },
                    //            CreatedBy = "system",
                    //            CreatedDate = DateTime.UtcNow,
                    //            Description = "ASNGroup Trial",
                    //            ImageUrl = "http://myimage.com/test-acme-group-demo.jpg",
                    //            Name = "ASNGroup Demo",
                    //        }
                    //    }
                    //}
                    
                    
                    
                    #endregion

                 }

                });

                context.SaveChanges();
            }
        }
    }
}
