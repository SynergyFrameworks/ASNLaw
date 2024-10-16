using Datalayer.Domain.Security;
using System;
using System.Linq;

namespace Datalayer.Seed
{
    public static class SeedPermissions
    {
        public static void EnsurePermissionsSeeded(this Context.ASNDbContext context)
        {
            if (!context.Permissions.Any())
            {
                context.Permissions.AddRange(AddSuperAdminPermissions());
                context.Permissions.AddRange(AddAdminPermissions());

                context.Permissions.AddRange(new Permission[] {
                     //Manager
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Action",
                    }
                    ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ActionOptions",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Address",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "User",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Client",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "DocumentSource",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ASNGroup",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Mailbox",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Module",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "OptionValues",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Organization",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = false, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Permision",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Phone",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Project",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ProjectDocument",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Resource",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Series",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "StorageProvider",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Task",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "Manager",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Team",
                    }

                     //User
                                          ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Action",
                    }
                    ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ActionOptions",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Address",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = false, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "User",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Client",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "DocumentSource",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ASNGroup",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Mailbox",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Module",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "OptionValues",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Organization",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Permision",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Phone",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Project",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "ProjectDocument",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Resource",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Series",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "StorageProvider",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = true, //Will turn into UserSettings_CanCreate
                        CanDelete = true, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = true, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Task",
                    }
                                         ,
                     new Permission {
                        AspNetRole = "User",
                        CanCreate = false, //Will turn into UserSettings_CanCreate
                        CanDelete = false, //Will turn into UserSettings_CanDelete
                        CanRead = true, //Will turn into UserSettings_CanRead
                        CanWrite = false, //Will turn into UserSettings_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Description = "",
                        Name = "Team",
                    }
                });

                context.SaveChanges();
            }
        }


        private static Permission[] AddSuperAdminPermissions()
        {
            return new Permission[] {
                    new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Action",
                    },
                    new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ActionOptions",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Address",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "User",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true, //Will turn into Client_CanCreate
                        CanDelete = true, //Will turn into Client_CanDelete
                        CanRead = true, //Will turn into Client_CanRead
                        CanWrite = true, //Will turn into Client_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Client",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "DocumentSource",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ASNGroup",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true, //Will turn into Mailbox_CanCreate
                        CanDelete = true, //Will turn into Mailbox_CanDelete
                        CanRead = true, //Will turn into Mailbox_CanRead
                        CanWrite = true, //Will turn into Mailbox_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Mailbox",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Module",
                    },
                      new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "OptionValues",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true, //Will turn into Organization_CanCreate
                        CanDelete = true, //Will turn into Organization_CanDelete
                        CanRead = true, //Will turn into Organization_CanRead
                        CanWrite = true, //Will turn into Organization_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Organization",
                    },
                      new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Permission",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Phone",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Project",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ProjectDocument",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Resource",
                    },
                      new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Series",
                    },
                      new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "StorageProvider",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Task",
                    },
                     new Permission {
                        AspNetRole = "SuperAdmin",
                        CanCreate = true, //Will turn into Team_CanCreate
                        CanDelete = true, //Will turn into Team_CanDelete
                        CanRead = true, //Will turn into Team_CanRead
                        CanWrite = true, //Will turn into Team_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Team",
                    }
            };
        }

        private static Permission[] AddAdminPermissions()
        {
            return new Permission[] {
                    new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Action",
                    },
                    new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ActionOptions",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Address",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "User",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true, //Will turn into Client_CanCreate
                        CanDelete = true, //Will turn into Client_CanDelete
                        CanRead = true, //Will turn into Client_CanRead
                        CanWrite = true, //Will turn into Client_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Client",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "DocumentSource",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ASNGroup",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true, //Will turn into Mailbox_CanCreate
                        CanDelete = true, //Will turn into Mailbox_CanDelete
                        CanRead = true, //Will turn into Mailbox_CanRead
                        CanWrite = true, //Will turn into Mailbox_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Mailbox",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Module",
                    },
                      new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "OptionValues",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = false, //Will turn into Organization_CanCreate
                        CanDelete = false, //Will turn into Organization_CanDelete
                        CanRead = true, //Will turn into Organization_CanRead
                        CanWrite = true, //Will turn into Organization_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Organization",
                    },
                      new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Permission",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Phone",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Project",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "ProjectDocument",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Resource",
                    },
                      new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Series",
                    },
                      new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "StorageProvider",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true,
                        CanDelete = true,
                        CanRead = true,
                        CanWrite = true,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Task",
                    },
                     new Permission {
                        AspNetRole = "Admin",
                        CanCreate = true, //Will turn into Team_CanCreate
                        CanDelete = true, //Will turn into Team_CanDelete
                        CanRead = true, //Will turn into Team_CanRead
                        CanWrite = true, //Will turn into Team_CanWrite
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        Name = "Team",
                    }
            };
        }
    }
}
