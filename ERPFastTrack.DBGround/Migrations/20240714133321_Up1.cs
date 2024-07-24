using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERPFastTrack.DBGround.Migrations
{
    /// <inheritdoc />
    public partial class Up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lwdbo");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationTypes",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    LicenseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    LastValidation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrgRoles",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "lwdbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "lwdbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "lwdbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseConnections",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatabaseConnections_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileSourceConnections",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchiveLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSourceConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSourceConnections_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesforceConnections",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesforceConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesforceConnections_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgUsersRelationships",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUsersRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgUsersRelationships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "lwdbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgUsersRelationships_OrgRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "lwdbo",
                        principalTable: "OrgRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgUsersRelationships_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueryConfigurations",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    DbConnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryConfigurations_DatabaseConnections_DbConnId",
                        column: x => x.DbConnId,
                        principalSchema: "lwdbo",
                        principalTable: "DatabaseConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueryConfigurations_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileSourceDetails",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileSourceDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delimiter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFieldFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeFieldFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchiveFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasHeader = table.Column<bool>(type: "bit", nullable: false),
                    FsConnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSourceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSourceDetails_FileSourceConnections_FsConnId",
                        column: x => x.FsConnId,
                        principalSchema: "lwdbo",
                        principalTable: "FileSourceConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    DestinationTypeId = table.Column<int>(type: "int", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    DbConnId = table.Column<int>(type: "int", nullable: true),
                    SfConnId = table.Column<int>(type: "int", nullable: true),
                    FsConnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_DatabaseConnections_DbConnId",
                        column: x => x.DbConnId,
                        principalSchema: "lwdbo",
                        principalTable: "DatabaseConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_DestinationTypes_DestinationTypeId",
                        column: x => x.DestinationTypeId,
                        principalSchema: "lwdbo",
                        principalTable: "DestinationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_FileSourceConnections_FsConnId",
                        column: x => x.FsConnId,
                        principalSchema: "lwdbo",
                        principalTable: "FileSourceConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalSchema: "lwdbo",
                        principalTable: "ProjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_SalesforceConnections_SfConnId",
                        column: x => x.SfConnId,
                        principalSchema: "lwdbo",
                        principalTable: "SalesforceConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PId = table.Column<int>(type: "int", nullable: false),
                    QueryId = table.Column<int>(type: "int", nullable: true),
                    SObjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mapping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    FileSourceDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_FileSourceDetails_FileSourceDetailId",
                        column: x => x.FileSourceDetailId,
                        principalSchema: "lwdbo",
                        principalTable: "FileSourceDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Projects_PId",
                        column: x => x.PId,
                        principalSchema: "lwdbo",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_QueryConfigurations_QueryId",
                        column: x => x.QueryId,
                        principalSchema: "lwdbo",
                        principalTable: "QueryConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedulings",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PId = table.Column<int>(type: "int", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    ScheduleType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    HourlyMinutes = table.Column<int>(type: "int", nullable: true),
                    MonthDay = table.Column<int>(type: "int", nullable: true),
                    MonthTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DailyTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedulings_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedulings_Projects_PId",
                        column: x => x.PId,
                        principalSchema: "lwdbo",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Executions",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    FailedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Executions_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalSchema: "lwdbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Executions_Schedulings_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "lwdbo",
                        principalTable: "Schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingDetails",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulingDetails_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "lwdbo",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulingDetails_Schedulings_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "lwdbo",
                        principalTable: "Schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionDetails",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessedRecords = table.Column<int>(type: "int", nullable: false),
                    FailedRecords = table.Column<int>(type: "int", nullable: false),
                    TotalRecords = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionDetails_Executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalSchema: "lwdbo",
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutionDetails_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "lwdbo",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionDetailedHistories",
                schema: "lwdbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionDetailId = table.Column<int>(type: "int", nullable: false),
                    SalesforceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SObjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalIdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalIdValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemaFailure = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<bool>(type: "bit", nullable: false),
                    JsonReq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonRes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionDetailedHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionDetailedHistories_ExecutionDetails_ExecutionDetailId",
                        column: x => x.ExecutionDetailId,
                        principalSchema: "lwdbo",
                        principalTable: "ExecutionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "lwdbo",
                table: "DestinationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SALESFORCE" },
                    { 2, "CSV" }
                });

            migrationBuilder.InsertData(
                schema: "lwdbo",
                table: "OrgRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "SUPERADMINISTRATOR" },
                    { 2, "ADMINISTRATOR" },
                    { 3, "EDITOR" },
                    { 4, "EXECUTOR" }
                });

            migrationBuilder.InsertData(
                schema: "lwdbo",
                table: "ProjectTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "DATABASE" },
                    { 2, "FILESOURCE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "lwdbo",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "lwdbo",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "lwdbo",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "lwdbo",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "lwdbo",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "lwdbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "lwdbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnections_OrgId",
                schema: "lwdbo",
                table: "DatabaseConnections",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionDetailedHistories_ExecutionDetailId",
                schema: "lwdbo",
                table: "ExecutionDetailedHistories",
                column: "ExecutionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionDetails_ExecutionId",
                schema: "lwdbo",
                table: "ExecutionDetails",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionDetails_JobId",
                schema: "lwdbo",
                table: "ExecutionDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_OrgId",
                schema: "lwdbo",
                table: "Executions",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_ScheduleId",
                schema: "lwdbo",
                table: "Executions",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSourceConnections_OrgId",
                schema: "lwdbo",
                table: "FileSourceConnections",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSourceDetails_FsConnId",
                schema: "lwdbo",
                table: "FileSourceDetails",
                column: "FsConnId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FileSourceDetailId",
                schema: "lwdbo",
                table: "Jobs",
                column: "FileSourceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OrgId",
                schema: "lwdbo",
                table: "Jobs",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PId",
                schema: "lwdbo",
                table: "Jobs",
                column: "PId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QueryId",
                schema: "lwdbo",
                table: "Jobs",
                column: "QueryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Name",
                schema: "lwdbo",
                table: "Organizations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgUsersRelationships_OrgId",
                schema: "lwdbo",
                table: "OrgUsersRelationships",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUsersRelationships_RoleId",
                schema: "lwdbo",
                table: "OrgUsersRelationships",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUsersRelationships_UserId",
                schema: "lwdbo",
                table: "OrgUsersRelationships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DbConnId",
                schema: "lwdbo",
                table: "Projects",
                column: "DbConnId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DestinationTypeId",
                schema: "lwdbo",
                table: "Projects",
                column: "DestinationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FsConnId",
                schema: "lwdbo",
                table: "Projects",
                column: "FsConnId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrgId",
                schema: "lwdbo",
                table: "Projects",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                schema: "lwdbo",
                table: "Projects",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SfConnId",
                schema: "lwdbo",
                table: "Projects",
                column: "SfConnId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryConfigurations_DbConnId",
                schema: "lwdbo",
                table: "QueryConfigurations",
                column: "DbConnId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryConfigurations_OrgId",
                schema: "lwdbo",
                table: "QueryConfigurations",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesforceConnections_OrgId",
                schema: "lwdbo",
                table: "SalesforceConnections",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingDetails_JobId",
                schema: "lwdbo",
                table: "SchedulingDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingDetails_ScheduleId",
                schema: "lwdbo",
                table: "SchedulingDetails",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_OrgId",
                schema: "lwdbo",
                table: "Schedulings",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_PId",
                schema: "lwdbo",
                table: "Schedulings",
                column: "PId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "ExecutionDetailedHistories",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Licenses",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "OrgUsersRelationships",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "SchedulingDetails",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "ExecutionDetails",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "OrgRoles",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Executions",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Schedulings",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "FileSourceDetails",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "QueryConfigurations",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "DatabaseConnections",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "DestinationTypes",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "FileSourceConnections",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "ProjectTypes",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "SalesforceConnections",
                schema: "lwdbo");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "lwdbo");
        }
    }
}
