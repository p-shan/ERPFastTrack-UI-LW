/****** Object:  Table [lwdbo].[__MyMigrationsHistory]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[__MyMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___MyMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetRoleClaims]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetRoles]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetUserClaims]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetUserLogins]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetUserRoles]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetUsers]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[AspNetUserTokens]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[DatabaseConnections]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[DatabaseConnections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ConnectionString] [nvarchar](max) NOT NULL,
	[OrgId] [int] NOT NULL,
 CONSTRAINT [PK_DatabaseConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[DestinationTypes]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[DestinationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DestinationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[ExecutionDetailedHistories]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[ExecutionDetailedHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExecutionDetailId] [int] NOT NULL,
	[SalesforceId] [nvarchar](max) NOT NULL,
	[SObjectName] [nvarchar](max) NOT NULL,
	[QueryName] [nvarchar](max) NOT NULL,
	[ExternalIdName] [nvarchar](max) NOT NULL,
	[ExternalIdValue] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[MessageCode] [nvarchar](max) NOT NULL,
	[SchemaFailure] [bit] NOT NULL,
	[Created] [bit] NOT NULL,
	[JsonReq] [nvarchar](max) NOT NULL,
	[JsonRes] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ExecutionDetailedHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[ExecutionDetails]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[ExecutionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExecutionId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[StartTime] [datetime2](7) NULL,
	[CompletionTime] [datetime2](7) NULL,
	[Observations] [nvarchar](max) NULL,
	[ProcessedRecords] [int] NOT NULL,
	[FailedRecords] [int] NOT NULL,
	[TotalRecords] [int] NOT NULL,
 CONSTRAINT [PK_ExecutionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Executions]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Executions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ScheduledAt] [datetime2](7) NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[FailedReason] [nvarchar](max) NULL,
	[StartTime] [datetime2](7) NULL,
	[CompletionTime] [datetime2](7) NULL,
 CONSTRAINT [PK_Executions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[FileSourceConnections]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[FileSourceConnections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[FileLocation] [nvarchar](max) NOT NULL,
	[ArchiveLocation] [nvarchar](max) NOT NULL,
	[OrgId] [int] NOT NULL,
 CONSTRAINT [PK_FileSourceConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[FileSourceDetails]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[FileSourceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileSourceDetailName] [nvarchar](max) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[Delimiter] [nvarchar](max) NOT NULL,
	[DateFieldFormat] [nvarchar](max) NOT NULL,
	[TimeFieldFormat] [nvarchar](max) NOT NULL,
	[ArchiveFileName] [nvarchar](max) NOT NULL,
	[HasHeader] [bit] NOT NULL,
	[FsConnId] [int] NOT NULL,
 CONSTRAINT [PK_FileSourceDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Jobs]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Jobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PId] [int] NOT NULL,
	[QueryId] [int] NULL,
	[SObjectName] [nvarchar](max) NULL,
	[Mapping] [nvarchar](max) NOT NULL,
	[OrgId] [int] NOT NULL,
	[FileSourceDetailId] [int] NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Licenses]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Licenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgId] [int] NOT NULL,
	[LicenseCode] [nvarchar](max) NOT NULL,
	[IsValid] [bit] NOT NULL,
	[LastValidation] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Organizations]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Organizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[CreatedByUserId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[OrgRoles]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[OrgRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OrgRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[OrgUsersRelationships]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[OrgUsersRelationships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[OrgId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_OrgUsersRelationships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Projects]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[DestinationTypeId] [int] NOT NULL,
	[OrgId] [int] NOT NULL,
	[DbConnId] [int] NULL,
	[SfConnId] [int] NULL,
	[FsConnId] [int] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[ProjectTypes]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[ProjectTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProjectTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[QueryConfigurations]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[QueryConfigurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QueryName] [nvarchar](max) NOT NULL,
	[QueryDetails] [nvarchar](max) NOT NULL,
	[OrgId] [int] NOT NULL,
	[DbConnId] [int] NOT NULL,
 CONSTRAINT [PK_QueryConfigurations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[SalesforceConnections]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[SalesforceConnections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[ClientId] [nvarchar](max) NULL,
	[ClientSecret] [nvarchar](max) NULL,
	[TokenEndpoint] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[TokenExpiry] [datetime2](7) NULL,
	[OrgId] [int] NOT NULL,
 CONSTRAINT [PK_SalesforceConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[SchedulingDetails]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[SchedulingDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Priority] [int] NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_SchedulingDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lwdbo].[Schedulings]    Script Date: 22-07-2024 18:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lwdbo].[Schedulings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PId] [int] NOT NULL,
	[OrgId] [int] NOT NULL,
	[ScheduleType] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[HourlyMinutes] [int] NULL,
	[MonthDay] [int] NULL,
	[MonthTime] [time](7) NULL,
	[DailyTime] [time](7) NULL,
 CONSTRAINT [PK_Schedulings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [lwdbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 22-07-2024 18:48:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [lwdbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [lwdbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [lwdbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [lwdbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [lwdbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 22-07-2024 18:48:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [lwdbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DatabaseConnections_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_DatabaseConnections_OrgId] ON [lwdbo].[DatabaseConnections]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExecutionDetailedHistories_ExecutionDetailId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_ExecutionDetailedHistories_ExecutionDetailId] ON [lwdbo].[ExecutionDetailedHistories]
(
	[ExecutionDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExecutionDetails_ExecutionId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_ExecutionDetails_ExecutionId] ON [lwdbo].[ExecutionDetails]
(
	[ExecutionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExecutionDetails_JobId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_ExecutionDetails_JobId] ON [lwdbo].[ExecutionDetails]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Executions_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Executions_OrgId] ON [lwdbo].[Executions]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Executions_ScheduleId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Executions_ScheduleId] ON [lwdbo].[Executions]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FileSourceConnections_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_FileSourceConnections_OrgId] ON [lwdbo].[FileSourceConnections]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FileSourceDetails_FsConnId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_FileSourceDetails_FsConnId] ON [lwdbo].[FileSourceDetails]
(
	[FsConnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jobs_FileSourceDetailId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Jobs_FileSourceDetailId] ON [lwdbo].[Jobs]
(
	[FileSourceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jobs_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Jobs_OrgId] ON [lwdbo].[Jobs]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jobs_PId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Jobs_PId] ON [lwdbo].[Jobs]
(
	[PId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jobs_QueryId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Jobs_QueryId] ON [lwdbo].[Jobs]
(
	[QueryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Organizations_Name]    Script Date: 22-07-2024 18:48:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Organizations_Name] ON [lwdbo].[Organizations]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrgUsersRelationships_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_OrgUsersRelationships_OrgId] ON [lwdbo].[OrgUsersRelationships]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrgUsersRelationships_RoleId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_OrgUsersRelationships_RoleId] ON [lwdbo].[OrgUsersRelationships]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrgUsersRelationships_UserId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_OrgUsersRelationships_UserId] ON [lwdbo].[OrgUsersRelationships]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_DbConnId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_DbConnId] ON [lwdbo].[Projects]
(
	[DbConnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_DestinationTypeId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_DestinationTypeId] ON [lwdbo].[Projects]
(
	[DestinationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_FsConnId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_FsConnId] ON [lwdbo].[Projects]
(
	[FsConnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_OrgId] ON [lwdbo].[Projects]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_ProjectTypeId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_ProjectTypeId] ON [lwdbo].[Projects]
(
	[ProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_SfConnId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Projects_SfConnId] ON [lwdbo].[Projects]
(
	[SfConnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QueryConfigurations_DbConnId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_QueryConfigurations_DbConnId] ON [lwdbo].[QueryConfigurations]
(
	[DbConnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QueryConfigurations_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_QueryConfigurations_OrgId] ON [lwdbo].[QueryConfigurations]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesforceConnections_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_SalesforceConnections_OrgId] ON [lwdbo].[SalesforceConnections]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SchedulingDetails_JobId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_SchedulingDetails_JobId] ON [lwdbo].[SchedulingDetails]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SchedulingDetails_ScheduleId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_SchedulingDetails_ScheduleId] ON [lwdbo].[SchedulingDetails]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedulings_OrgId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Schedulings_OrgId] ON [lwdbo].[Schedulings]
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedulings_PId]    Script Date: 22-07-2024 18:48:00 ******/
CREATE NONCLUSTERED INDEX [IX_Schedulings_PId] ON [lwdbo].[Schedulings]
(
	[PId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [lwdbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [lwdbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [lwdbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [lwdbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [lwdbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [lwdbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [lwdbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [lwdbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [lwdbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [lwdbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [lwdbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [lwdbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [lwdbo].[DatabaseConnections]  WITH CHECK ADD  CONSTRAINT [FK_DatabaseConnections_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
GO
ALTER TABLE [lwdbo].[DatabaseConnections] CHECK CONSTRAINT [FK_DatabaseConnections_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[ExecutionDetailedHistories]  WITH CHECK ADD  CONSTRAINT [FK_ExecutionDetailedHistories_ExecutionDetails_ExecutionDetailId] FOREIGN KEY([ExecutionDetailId])
REFERENCES [lwdbo].[ExecutionDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[ExecutionDetailedHistories] CHECK CONSTRAINT [FK_ExecutionDetailedHistories_ExecutionDetails_ExecutionDetailId]
GO
ALTER TABLE [lwdbo].[ExecutionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ExecutionDetails_Executions_ExecutionId] FOREIGN KEY([ExecutionId])
REFERENCES [lwdbo].[Executions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[ExecutionDetails] CHECK CONSTRAINT [FK_ExecutionDetails_Executions_ExecutionId]
GO
ALTER TABLE [lwdbo].[ExecutionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ExecutionDetails_Jobs_JobId] FOREIGN KEY([JobId])
REFERENCES [lwdbo].[Jobs] ([Id])
GO
ALTER TABLE [lwdbo].[ExecutionDetails] CHECK CONSTRAINT [FK_ExecutionDetails_Jobs_JobId]
GO
ALTER TABLE [lwdbo].[Executions]  WITH CHECK ADD  CONSTRAINT [FK_Executions_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
GO
ALTER TABLE [lwdbo].[Executions] CHECK CONSTRAINT [FK_Executions_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[Executions]  WITH CHECK ADD  CONSTRAINT [FK_Executions_Schedulings_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [lwdbo].[Schedulings] ([Id])
GO
ALTER TABLE [lwdbo].[Executions] CHECK CONSTRAINT [FK_Executions_Schedulings_ScheduleId]
GO
ALTER TABLE [lwdbo].[FileSourceConnections]  WITH CHECK ADD  CONSTRAINT [FK_FileSourceConnections_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
GO
ALTER TABLE [lwdbo].[FileSourceConnections] CHECK CONSTRAINT [FK_FileSourceConnections_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[FileSourceDetails]  WITH CHECK ADD  CONSTRAINT [FK_FileSourceDetails_FileSourceConnections_FsConnId] FOREIGN KEY([FsConnId])
REFERENCES [lwdbo].[FileSourceConnections] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[FileSourceDetails] CHECK CONSTRAINT [FK_FileSourceDetails_FileSourceConnections_FsConnId]
GO
ALTER TABLE [lwdbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_FileSourceDetails_FileSourceDetailId] FOREIGN KEY([FileSourceDetailId])
REFERENCES [lwdbo].[FileSourceDetails] ([Id])
GO
ALTER TABLE [lwdbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_FileSourceDetails_FileSourceDetailId]
GO
ALTER TABLE [lwdbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_Projects_PId] FOREIGN KEY([PId])
REFERENCES [lwdbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_Projects_PId]
GO
ALTER TABLE [lwdbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_QueryConfigurations_QueryId] FOREIGN KEY([QueryId])
REFERENCES [lwdbo].[QueryConfigurations] ([Id])
GO
ALTER TABLE [lwdbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_QueryConfigurations_QueryId]
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships]  WITH CHECK ADD  CONSTRAINT [FK_OrgUsersRelationships_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [lwdbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships] CHECK CONSTRAINT [FK_OrgUsersRelationships_AspNetUsers_UserId]
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships]  WITH CHECK ADD  CONSTRAINT [FK_OrgUsersRelationships_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships] CHECK CONSTRAINT [FK_OrgUsersRelationships_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships]  WITH CHECK ADD  CONSTRAINT [FK_OrgUsersRelationships_OrgRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [lwdbo].[OrgRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[OrgUsersRelationships] CHECK CONSTRAINT [FK_OrgUsersRelationships_OrgRoles_RoleId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_DatabaseConnections_DbConnId] FOREIGN KEY([DbConnId])
REFERENCES [lwdbo].[DatabaseConnections] ([Id])
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_DatabaseConnections_DbConnId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_DestinationTypes_DestinationTypeId] FOREIGN KEY([DestinationTypeId])
REFERENCES [lwdbo].[DestinationTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_DestinationTypes_DestinationTypeId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_FileSourceConnections_FsConnId] FOREIGN KEY([FsConnId])
REFERENCES [lwdbo].[FileSourceConnections] ([Id])
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_FileSourceConnections_FsConnId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTypes_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [lwdbo].[ProjectTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectTypes_ProjectTypeId]
GO
ALTER TABLE [lwdbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_SalesforceConnections_SfConnId] FOREIGN KEY([SfConnId])
REFERENCES [lwdbo].[SalesforceConnections] ([Id])
GO
ALTER TABLE [lwdbo].[Projects] CHECK CONSTRAINT [FK_Projects_SalesforceConnections_SfConnId]
GO
ALTER TABLE [lwdbo].[QueryConfigurations]  WITH CHECK ADD  CONSTRAINT [FK_QueryConfigurations_DatabaseConnections_DbConnId] FOREIGN KEY([DbConnId])
REFERENCES [lwdbo].[DatabaseConnections] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[QueryConfigurations] CHECK CONSTRAINT [FK_QueryConfigurations_DatabaseConnections_DbConnId]
GO
ALTER TABLE [lwdbo].[QueryConfigurations]  WITH CHECK ADD  CONSTRAINT [FK_QueryConfigurations_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[QueryConfigurations] CHECK CONSTRAINT [FK_QueryConfigurations_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[SalesforceConnections]  WITH CHECK ADD  CONSTRAINT [FK_SalesforceConnections_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [lwdbo].[SalesforceConnections] CHECK CONSTRAINT [FK_SalesforceConnections_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[SchedulingDetails]  WITH CHECK ADD  CONSTRAINT [FK_SchedulingDetails_Jobs_JobId] FOREIGN KEY([JobId])
REFERENCES [lwdbo].[Jobs] ([Id])
GO
ALTER TABLE [lwdbo].[SchedulingDetails] CHECK CONSTRAINT [FK_SchedulingDetails_Jobs_JobId]
GO
ALTER TABLE [lwdbo].[SchedulingDetails]  WITH CHECK ADD  CONSTRAINT [FK_SchedulingDetails_Schedulings_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [lwdbo].[Schedulings] ([Id])
GO
ALTER TABLE [lwdbo].[SchedulingDetails] CHECK CONSTRAINT [FK_SchedulingDetails_Schedulings_ScheduleId]
GO
ALTER TABLE [lwdbo].[Schedulings]  WITH CHECK ADD  CONSTRAINT [FK_Schedulings_Organizations_OrgId] FOREIGN KEY([OrgId])
REFERENCES [lwdbo].[Organizations] ([Id])
GO
ALTER TABLE [lwdbo].[Schedulings] CHECK CONSTRAINT [FK_Schedulings_Organizations_OrgId]
GO
ALTER TABLE [lwdbo].[Schedulings]  WITH CHECK ADD  CONSTRAINT [FK_Schedulings_Projects_PId] FOREIGN KEY([PId])
REFERENCES [lwdbo].[Projects] ([Id])
GO
ALTER TABLE [lwdbo].[Schedulings] CHECK CONSTRAINT [FK_Schedulings_Projects_PId]
GO

SET IDENTITY_INSERT [lwdbo].[DestinationTypes] ON 
GO
INSERT [lwdbo].[DestinationTypes] ([Id], [Name]) VALUES (1, N'SALESFORCE')
GO
INSERT [lwdbo].[DestinationTypes] ([Id], [Name]) VALUES (2, N'CSV')
GO
SET IDENTITY_INSERT [lwdbo].[DestinationTypes] OFF
GO
SET IDENTITY_INSERT [lwdbo].[OrgRoles] ON 
GO
INSERT [lwdbo].[OrgRoles] ([Id], [RoleName]) VALUES (1, N'SUPERADMINISTRATOR')
GO
INSERT [lwdbo].[OrgRoles] ([Id], [RoleName]) VALUES (2, N'ADMINISTRATOR')
GO
INSERT [lwdbo].[OrgRoles] ([Id], [RoleName]) VALUES (3, N'EDITOR')
GO
INSERT [lwdbo].[OrgRoles] ([Id], [RoleName]) VALUES (4, N'EXECUTOR')
GO
SET IDENTITY_INSERT [lwdbo].[OrgRoles] OFF
GO
SET IDENTITY_INSERT [lwdbo].[ProjectTypes] ON 
GO
INSERT [lwdbo].[ProjectTypes] ([Id], [Name]) VALUES (1, N'DATABASE')
GO
INSERT [lwdbo].[ProjectTypes] ([Id], [Name]) VALUES (2, N'FILESOURCE')
GO
SET IDENTITY_INSERT [lwdbo].[ProjectTypes] OFF
GO

