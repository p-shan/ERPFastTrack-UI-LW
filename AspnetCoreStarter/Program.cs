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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSalesforceProcessors(builder.Configuration);
builder.Services.AddDBProcessors(builder.Configuration);
builder.Services.AddSourceProcessors(builder.Configuration);
builder.Services.AddProcessorFactory();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
string connectionString = string.Empty;
string schema = "dbo";
if (environment != "Development")
{
    connectionString = Environment.GetEnvironmentVariable("ERPFT_CONNECTION_STRING");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new Exception("Connection String Not Provided");

    schema = Environment.GetEnvironmentVariable("ERPFT_DB_SCHEMA");
    if (string.IsNullOrWhiteSpace(schema))
        schema = "dbo";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Add services to the container.
builder.Services.AddDbContext<ERPFastTrackUIContext>(options =>
  options.UseSqlServer(connectionString, x=> x.MigrationsHistoryTable("__MyMigrationsHistory", schema)));
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
