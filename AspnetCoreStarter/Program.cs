using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.Extensions;
using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.Abstraction.Implementations;
using ERPFastTrack.API.Internals.Middlewares;
using ERPFastTrack.API.Internals.Controllers.InternalBase.Data;
using ERPFastTrack.API.Internals.Controllers.InternalBase.Operations;
using ERPFastTrack.SalesforceProcessor.Extensions;
using ERPFastTrack.DBProcessor.Extensions;
using ERPFastTrack.SourceProcessor.Extensions;
using ERPFastTrack.LicenseProcessor.Background;
using ERPFastTrack.LicenseProcessor.Internals;
using ERPFastTrack.DBGround.DBModels.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSalesforceProcessors(builder.Configuration);
    builder.Services.AddDBProcessors(builder.Configuration);
    builder.Services.AddSourceProcessors(builder.Configuration);
    builder.Services.AddProcessorFactory();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    string connectionString = string.Empty;
    string schema = "dbo";
    bool createDb = false;
    if (environment != "Development")
    {
        connectionString = Environment.GetEnvironmentVariable("ERPFT_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("Connection String Not Provided");

        schema = Environment.GetEnvironmentVariable("ERPFT_DB_SCHEMA");
        if (string.IsNullOrWhiteSpace(schema))
            schema = "dbo";

        createDb = string.Equals(Environment.GetEnvironmentVariable("ERPFT_RUNFULLPKG"), "true", StringComparison.InvariantCultureIgnoreCase);
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    }

    Console.WriteLine("APPLICATION INITIATING");

    // Add services to the container.
    builder.Services.AddDbContext<ERPFastTrackUIContext>(options =>
      options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("__MyMigrationsHistory", schema)));
    builder.Services.AddDefaultIdentity<UserData>(options => options.SignIn.RequireConfirmedAccount = false)
      .AddEntityFrameworkStores<ERPFastTrackUIContext>();
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ILicenseAuth, LicenseAuth>();
    builder.Services.AddScoped<OrgRoleManagerAbstract, OrgRoleManager>();
    builder.Services.AddTransient<SalesforceConnectionsBase>();
    builder.Services.AddTransient<DatabaseConnectionsBase>();
    builder.Services.AddTransient<FileSourceConnectionsBase>();
    builder.Services.AddTransient<FileSourceDetailBase>();
    builder.Services.AddTransient<QueryConfigurationBase>();
    builder.Services.AddTransient<ProjectBase>();
    builder.Services.AddTransient<JobBase>();
    builder.Services.AddTransient<SchedulingBase>();
    builder.Services.AddTransient<ExecutionBase>();
    builder.Services.AddTransient<UtilityBase>();
    builder.Services.AddTransient<SourceOperationsBase>();
    builder.Services.AddHostedService<LicenseBgProcessor>();

    var app = builder.Build();
    // Create a logger instance from the app
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    try
    {
        // Write some logs
        logger.LogInformation("Application starting up...");
        if (createDb)
        {
            Console.WriteLine("APPLICATION ENSURING DB");
            logger.LogInformation("APPLICATION ENSURING DB...");
            // Ensure database is created and apply migrations at startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ERPFastTrackUIContext>();

                // Ensure the database is created
                dbContext.Database.EnsureCreated();

                // Check if there are pending migrations
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    var migrator = dbContext.Database.GetService<IMigrator>();
                    foreach (var migration in pendingMigrations)
                    {
                        migrator.Migrate(migration);
                    }
                }
            }
            Console.WriteLine("APPLICATION DB ENSURED");
            logger.LogInformation("APPLICATION DB ENSURED...");
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        };

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<RoleMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();

        Console.WriteLine("APPLICATION HEALTHY");
        logger.LogInformation("APPLICATION HEALTHY...");
    }
    catch (Exception ex)
    {
        Console.WriteLine("APPLICATION_ERROR : " + ex.Message);
        logger.LogInformation("APPLICATION_ERROR : " + ex.Message);
    }

}
catch (Exception ex)
{
    Console.WriteLine("APPLICATION_ERROR : " + ex.Message);
}
