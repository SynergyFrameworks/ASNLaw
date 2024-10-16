using Microsoft.EntityFrameworkCore;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Datalayer.Domain.Security;
using Datalayer.Domain.Storage;
using Datalayer.Domain.Group;
using Datalayer.Extensions;
using Datalayer.Seed;

namespace Datalayer.Context
{
    public class ASNDbContext : ConfigConnectionContextBase
    {
        private const string _CONNECTION_KEY = "ASNDbConnection";
        public ASNDbContext() : base(_CONNECTION_KEY)
        {
        }

        public ASNDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        //Demographics domain
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }

        //ASNGroup domain
        public DbSet<ASNAction> Actions { get; set; }
        public DbSet<ActionOption> ActionOptions { get; set; }
        public DbSet<ASNGroup> Groups { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ASNTask> Tasks { get; set; }
        public DbSet<TaskStep> TaskSteps { get; set; }

        //Organization
        public DbSet<ASNClient> Clients { get; set; }
        public DbSet<MailBox> MailBoxes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ServiceContract> ServiceContracts { get; set; }
        public DbSet<Team> Teams { get; set; }

        //Security
        public DbSet<AuthenticationProvider> AuthenticationProviders { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        //Storage
        public DbSet<DocumentSource> DocumentSources { get; set; }
        public DbSet<StorageProvider> StorageProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SetupColumnId<Address>();
            modelBuilder.SetupColumnId<Phone>();

            modelBuilder.SetupColumnId<ASNAction>();
            modelBuilder.SetupColumnId<ActionOption>();
            modelBuilder.SetupColumnId<ASNGroup>();
            modelBuilder.SetupColumnId<Module>();
            modelBuilder.SetupColumnId<OptionValue>();
            modelBuilder.SetupColumnId<Project>();
            modelBuilder.SetupColumnId<ProjectDocument>();
            modelBuilder.SetupColumnId<Resource>();
            modelBuilder.SetupColumnId<Series>();
            modelBuilder.SetupColumnId<ASNTask>();
            modelBuilder.SetupColumnId<TaskStep>();

            modelBuilder.SetupColumnId<ASNClient>();
            modelBuilder.SetupColumnId<MailBox>();
            modelBuilder.SetupColumnId<Organization>();
            modelBuilder.SetupColumnId<ServiceContract>();
            modelBuilder.SetupColumnId<Team>();

            modelBuilder.SetupColumnId<AuthenticationProvider>();
            modelBuilder.SetupColumnId<GroupPermission>();
            modelBuilder.SetupColumnId<Permission>();
            modelBuilder.SetupColumnId<Subscription>();
            modelBuilder.SetupColumnId<User>();
            modelBuilder.SetupColumnId<UserSubscription>();

            modelBuilder.SetupColumnId<DocumentSource>();
            modelBuilder.SetupColumnId<StorageProvider>();

            modelBuilder.Entity<Organization>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("SuperUserId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StorageProvider>()
                .HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ASNTask>()
                .HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ASNTask>()
                .HasOne(e => e.Module)
                .WithMany()
                .HasForeignKey("ModuleId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectDocument>()
                .HasOne(e => e.Project)
                .WithMany() 
                .HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectDocument>()
                .HasOne(e => e.DocumentSource)
                .WithMany()
                .HasForeignKey("DocumentSourceId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}

