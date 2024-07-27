using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.DBGround.DBModels.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ERPFastTrack.DBGround.Context;

public class ERPFastTrackUIContext : IdentityDbContext<UserData>
{
    public bool UseIntProperty { get; set; }
    public string Schema { get; }
    public ERPFastTrackUIContext(DbContextOptions<ERPFastTrackUIContext> options)
        : base(options)
    {
        var schema = Environment.GetEnvironmentVariable("ERPFT_DB_SCHEMA");
        if (string.IsNullOrWhiteSpace(schema))
            Schema = "dbo";
        else Schema = schema;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ReplaceService<IModelCacheKeyFactory, DynamicSchemaModelCacheKeyFactory>();
    }

    private static string GetConnectionString()
    {        
        return Environment.GetEnvironmentVariable("MY_CONNECTION_STRING");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        // Set the default schema for all entities
        builder.HasDefaultSchema(Schema);

        builder.Entity<Organization>()
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.Entity<OrgUsersRelationship>()
            .HasKey(ab => new { ab.Id });

        builder.Entity<OrgUsersRelationship>()
            .HasOne(ab => ab.UserData)
            .WithMany(a => a.Organizations)
            .HasForeignKey(ab => ab.UserId);

        builder.Entity<OrgUsersRelationship>()
            .HasOne(ab => ab.Organization)
            .WithMany(b => b.OrgUsers)
            .HasForeignKey(ab => ab.OrgId);

        builder.Entity<OrgRole>()
            .HasMany(a => a.OrgUsersRelationship)
            .WithOne(ad => ad.Role)
            .HasForeignKey(ad => ad.RoleId);

        builder.Entity<UserData>().Navigation(e => e.Organizations).AutoInclude();
        builder.Entity<OrgUsersRelationship>().Navigation(e => e.Role).AutoInclude();
        builder.Entity<OrgUsersRelationship>().Navigation(e => e.UserData).AutoInclude();

        builder.Entity<SalesforceConnection>()
            .HasOne(a => a.Organization)
            .WithMany(a => a.SalesforceConfigurations)
            .HasForeignKey(ad => ad.OrgId);

        builder.Entity<DatabaseConnection>()
            .HasOne(a => a.Organization)
            .WithMany(a => a.DatabaseConnections)
            .HasForeignKey(ad => ad.OrgId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<QueryConfiguration>()
            .HasOne(a => a.DatabaseConnection)
            .WithMany(a => a.QueryConfigurations)
            .HasForeignKey(ad => ad.DbConnId);

        builder.Entity<QueryConfiguration>()
            .HasOne(a => a.Organization)
            .WithMany(a => a.QueryConfigurations)
            .HasForeignKey(ad => ad.OrgId);

        builder.Entity<Project>()
            .HasOne(a => a.ProjectType)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.ProjectTypeId);

        builder.Entity<Project>()
            .HasOne(a => a.DestinationType)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.DestinationTypeId);


        builder.Entity<Project>()
            .HasOne(a => a.SalesforceConnection)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.SfConnId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Project>()
            .HasOne(a => a.DatabaseConnection)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.DbConnId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Project>()
            .HasOne(a => a.FileSourceConnection)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.FsConnId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Project>()
            .HasOne(a => a.Organization)
            .WithMany(a => a.Projects)
            .HasForeignKey(ad => ad.OrgId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Job>()
            .HasOne(a => a.Organization)
            .WithMany(a => a.Jobs)
            .HasForeignKey(ad => ad.OrgId);

        builder.Entity<Job>()
            .HasOne(a => a.FileSourceDetails)
            .WithMany(a => a.Jobs)
            .HasForeignKey(ad => ad.FileSourceDetailId);

        builder.Entity<Job>()
            .HasOne(a => a.QueryConfiguration)
            .WithMany(a => a.Jobs)
            .HasForeignKey(ad => ad.QueryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Job>()
            .HasOne(a => a.Project)
            .WithMany(a => a.Jobs)
            .HasForeignKey(ad => ad.PId);

        builder.Entity<OrgRole>().HasData(
            new OrgRole { Id = 1, RoleName = "SUPERADMINISTRATOR" },
            new OrgRole { Id = 2, RoleName = "ADMINISTRATOR" },
            new OrgRole { Id = 3, RoleName = "EDITOR" },
            new OrgRole { Id = 4, RoleName = "EXECUTOR" }
            );

        builder.Entity<ProjectType>().HasData(
            new ProjectType { Id = 1, Name = "DATABASE" },
            new ProjectType { Id = 2, Name = "FILESOURCE" }
        );

        builder.Entity<DestinationType>().HasData(
            new DestinationType { Id = 1, Name = "SALESFORCE" },
            new DestinationType { Id = 2, Name = "CSV" }
        );

        builder.Entity<FileSourceConnection>()
            .HasOne(ab => ab.Organization)
            .WithMany(a => a.FileSourceConnections)
            .HasForeignKey(ab => ab.OrgId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<FileSourceDetails>()
            .HasOne(ab => ab.FileSourceConnection)
            .WithMany(a => a.FileSourceDetails)
            .HasForeignKey(ab => ab.FsConnId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Scheduling>()
            .HasOne(ab => ab.Organization)
            .WithMany(b => b.Schedulings)
            .HasForeignKey(ab => ab.OrgId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Scheduling>()
            .HasOne(ab => ab.Project)
            .WithMany(b => b.Schedulings)
            .HasForeignKey(ab => ab.PId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SchedulingDetails>()
            .HasOne(ab => ab.Scheduling)
            .WithMany(b => b.SchedulingDetails)
            .HasForeignKey(ab => ab.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SchedulingDetails>()
            .HasOne(ab => ab.Job)
            .WithMany(b => b.SchedulingDetails)
            .HasForeignKey(ab => ab.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Execution>()
            .HasOne(ab => ab.Organization)
            .WithMany(b => b.Executions)
            .HasForeignKey(ab => ab.OrgId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ExecutionDetails>()
            .HasOne(ab => ab.Job)
            .WithMany(b => b.ExecutionDetails)
            .HasForeignKey(ab => ab.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Execution>()
            .HasOne(ab => ab.Scheduling)
            .WithMany(b => b.Executions)
            .HasForeignKey(ab => ab.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrgRole> OrgRoles { get; set; }
    public DbSet<OrgUsersRelationship> OrgUsersRelationships { get; set; }
    public DbSet<SalesforceConnection> SalesforceConnections { get; set; }
    public DbSet<DatabaseConnection> DatabaseConnections { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<DestinationType> DestinationTypes { get; set; }
    public DbSet<QueryConfiguration> QueryConfigurations { get; set; }
    public DbSet<FileSourceConnection> FileSourceConnections { get; set; }
    public DbSet<FileSourceDetails> FileSourceDetails { get; set; }
    public DbSet<Scheduling> Schedulings { get; set; }
    public DbSet<SchedulingDetails> SchedulingDetails { get; set; }
    public DbSet<Execution> Executions { get; set; }
    public DbSet<ExecutionDetails> ExecutionDetails { get; set; }
    public DbSet<License> Licenses { get; set; }

    public DbSet<ExecutionDetailedHistory> ExecutionDetailedHistories { get; set; }

}
