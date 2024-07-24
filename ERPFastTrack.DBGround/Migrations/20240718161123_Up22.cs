using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPFastTrack.DBGround.Migrations
{
    /// <inheritdoc />
    public partial class Up22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Schedulings",
                schema: "lwdbo",
                newName: "Schedulings",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SchedulingDetails",
                schema: "lwdbo",
                newName: "SchedulingDetails",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SalesforceConnections",
                schema: "lwdbo",
                newName: "SalesforceConnections",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "QueryConfigurations",
                schema: "lwdbo",
                newName: "QueryConfigurations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProjectTypes",
                schema: "lwdbo",
                newName: "ProjectTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Projects",
                schema: "lwdbo",
                newName: "Projects",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OrgUsersRelationships",
                schema: "lwdbo",
                newName: "OrgUsersRelationships",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OrgRoles",
                schema: "lwdbo",
                newName: "OrgRoles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Organizations",
                schema: "lwdbo",
                newName: "Organizations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Licenses",
                schema: "lwdbo",
                newName: "Licenses",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Jobs",
                schema: "lwdbo",
                newName: "Jobs",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "FileSourceDetails",
                schema: "lwdbo",
                newName: "FileSourceDetails",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "FileSourceConnections",
                schema: "lwdbo",
                newName: "FileSourceConnections",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Executions",
                schema: "lwdbo",
                newName: "Executions",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExecutionDetails",
                schema: "lwdbo",
                newName: "ExecutionDetails",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExecutionDetailedHistories",
                schema: "lwdbo",
                newName: "ExecutionDetailedHistories",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DestinationTypes",
                schema: "lwdbo",
                newName: "DestinationTypes",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DatabaseConnections",
                schema: "lwdbo",
                newName: "DatabaseConnections",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "lwdbo",
                newName: "AspNetUserTokens",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "lwdbo",
                newName: "AspNetUsers",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "lwdbo",
                newName: "AspNetUserRoles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "lwdbo",
                newName: "AspNetUserLogins",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "lwdbo",
                newName: "AspNetUserClaims",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "lwdbo",
                newName: "AspNetRoles",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "lwdbo",
                newName: "AspNetRoleClaims",
                newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Schedulings",
                schema: "dbo",
                newName: "Schedulings",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "SchedulingDetails",
                schema: "dbo",
                newName: "SchedulingDetails",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "SalesforceConnections",
                schema: "dbo",
                newName: "SalesforceConnections",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "QueryConfigurations",
                schema: "dbo",
                newName: "QueryConfigurations",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "ProjectTypes",
                schema: "dbo",
                newName: "ProjectTypes",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Projects",
                schema: "dbo",
                newName: "Projects",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "OrgUsersRelationships",
                schema: "dbo",
                newName: "OrgUsersRelationships",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "OrgRoles",
                schema: "dbo",
                newName: "OrgRoles",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Organizations",
                schema: "dbo",
                newName: "Organizations",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Licenses",
                schema: "dbo",
                newName: "Licenses",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Jobs",
                schema: "dbo",
                newName: "Jobs",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "FileSourceDetails",
                schema: "dbo",
                newName: "FileSourceDetails",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "FileSourceConnections",
                schema: "dbo",
                newName: "FileSourceConnections",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "Executions",
                schema: "dbo",
                newName: "Executions",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "ExecutionDetails",
                schema: "dbo",
                newName: "ExecutionDetails",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "ExecutionDetailedHistories",
                schema: "dbo",
                newName: "ExecutionDetailedHistories",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "DestinationTypes",
                schema: "dbo",
                newName: "DestinationTypes",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "DatabaseConnections",
                schema: "dbo",
                newName: "DatabaseConnections",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "dbo",
                newName: "AspNetUserTokens",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "dbo",
                newName: "AspNetUsers",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "dbo",
                newName: "AspNetUserRoles",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                newName: "AspNetUserLogins",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "dbo",
                newName: "AspNetUserClaims",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "dbo",
                newName: "AspNetRoles",
                newSchema: "lwdbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "dbo",
                newName: "AspNetRoleClaims",
                newSchema: "lwdbo");
        }
    }
}
