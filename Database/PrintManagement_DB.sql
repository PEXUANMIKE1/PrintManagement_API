USE [PrintManagement_API]
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillName] [nvarchar](max) NOT NULL,
	[BillStatus] [int] NOT NULL,
	[TotalMoney] [decimal](18, 2) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TradingCode] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfirmEmails]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfirmEmails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConfirmCode] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExpiryTime] [datetime2](7) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
 CONSTRAINT [PK_ConfirmEmails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFeedBacks]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFeedBacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FeedBackContent] [nvarchar](max) NOT NULL,
	[ResponseByCompany] [nvarchar](max) NOT NULL,
	[UserFeedBackId] [int] NOT NULL,
	[FeedBackTime] [datetime2](7) NOT NULL,
	[ResponseTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CustomerFeedBacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deliveries]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deliveries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShippingMethodId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[DeliverId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DeliveryAddress] [nvarchar](max) NOT NULL,
	[EstimateDeliveryTime] [datetime2](7) NOT NULL,
	[ActualDeliveryTime] [datetime2](7) NULL,
	[DeliveryStatus] [int] NOT NULL,
 CONSTRAINT [PK_Deliveries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Designs]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DesginerId] [int] NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[DesignTime] [datetime2](7) NOT NULL,
	[DesignStatus] [int] NOT NULL,
	[ApproverId] [int] NOT NULL,
 CONSTRAINT [PK_Designs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportCoupons]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportCoupons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalMoney] [decimal](18, 2) NOT NULL,
	[ResourcePropertyDetailId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TradingCode] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_ImportCoupons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeyPerformanceIndicators]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyPerformanceIndicators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[IndicatorName] [nvarchar](max) NOT NULL,
	[Target] [int] NOT NULL,
	[ActuallyAchieved] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[AchieveKPI] [bit] NOT NULL,
 CONSTRAINT [PK_KeyPerformanceIndicators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsSeen] [bit] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrintJobs]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintJobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignId] [int] NOT NULL,
	[PrintJobStatus] [int] NOT NULL,
 CONSTRAINT [PK_PrintJobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](max) NOT NULL,
	[RequestDescriptionFromCustomer] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ExpectedEndDate] [datetime2](7) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ProjectStatus] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExpiryTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceForPrintJobs]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceForPrintJobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourcePropertyDetailId] [int] NOT NULL,
	[PrintJobsId] [int] NOT NULL,
 CONSTRAINT [PK_ResourceForPrintJobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceProperties]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceProperties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourcePropertyName] [nvarchar](max) NOT NULL,
	[ResourcesId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ResourceProperties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourcePropertyDetails]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourcePropertyDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourcePropertyId] [int] NOT NULL,
	[PropertyDetailName] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ResourcePropertyDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceName] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[AvailableQuantity] [int] NOT NULL,
	[ResourceStatus] [int] NOT NULL,
	[ResourceType] [int] NOT NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [nvarchar](max) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShippingMethods]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingMethods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShippingMethodName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ShippingMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[NumberOfMember] [int] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
	[ManagerId] [int] NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/2/2024 11:21:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NULL,
	[TeamId] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (1, N'Bìa truyện tranh Doremon', 0, CAST(74000.00 AS Decimal(18, 2)), 7, 9, N'3aff9426-65f2-4258-a87f-bf77afdf8a5c', CAST(N'2024-07-09T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-24T00:53:27.4186335' AS DateTime2), 2)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (2, N'Tạp chí xe hơi tháng 8', 1, CAST(86000.00 AS Decimal(18, 2)), 5, 7, N'73dfc6bd-2ef6-4d2d-a567-459535a796cd', CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-24T15:54:42.4437457' AS DateTime2), 2)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (3, N'Banner VF8', 0, CAST(0.00 AS Decimal(18, 2)), 16, 18, N'034225ac-854c-4f1b-a09d-569883cd3068', CAST(N'2024-07-21T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-25T18:39:02.6169803' AS DateTime2), 1018)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (4, N'Poster Phim Fast Of Furious', 1, CAST(24000.00 AS Decimal(18, 2)), 9, 11, N'b92baa7d-0981-440a-80f9-b48ba7c6dc9c', CAST(N'2024-07-17T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-25T18:40:34.1759529' AS DateTime2), 1018)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (5, N'lịch 2025', 1, CAST(99000.00 AS Decimal(18, 2)), 20, 22, N'3a2eabbd-3248-4c07-8768-6a59d3a307fc', CAST(N'2024-07-31T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-25T22:27:49.8290305' AS DateTime2), 2)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (6, N'Tài liệu Javascript', 1, CAST(24000.00 AS Decimal(18, 2)), 19, 21, N'759593e8-386e-4edf-8656-e2ef52d96114', CAST(N'2024-07-23T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-26T15:13:52.3021449' AS DateTime2), 2)
INSERT [dbo].[Bills] ([Id], [BillName], [BillStatus], [TotalMoney], [ProjectId], [CustomerId], [TradingCode], [CreateTime], [UpdateTime], [EmployeeId]) VALUES (7, N'Bìa truyện Conan tập 100', 1, CAST(300000.00 AS Decimal(18, 2)), 11, 13, N'a9bb00ac-efc8-406f-a717-13a2d0b65bc0', CAST(N'2024-07-18T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-27T12:28:51.9090155' AS DateTime2), 1018)
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
SET IDENTITY_INSERT [dbo].[ConfirmEmails] ON 

INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1, N'code_638564756631486414', 1, CAST(N'2024-07-13T13:54:23.1488408' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (2, N'code_638564758318514320', 2, CAST(N'2024-07-13T13:57:11.8514383' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (3, N'code_638564760925960428', 3, CAST(N'2024-07-13T14:01:32.5960471' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (4, N'code_638564762876096053', 4, CAST(N'2024-07-13T14:04:47.6096095' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (5, N'code_638564778759511434', 5, CAST(N'2024-07-13T14:31:15.9513067' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1004, N'code_123456', 1018, CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1005, N'code_638572033312194258', 1022, CAST(N'2024-07-22T00:02:11.2196677' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1006, N'code_123456553534534535', 1013, CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1007, N'code_123785231441244156', 1017, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1008, N'code_412421412564326721', 1012, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1010, N'code_412515535131414144', 1014, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[ConfirmEmails] ([Id], [ConfirmCode], [UserId], [ExpiryTime], [IsConfirmed]) VALUES (1011, N'code_673141673788854522', 1010, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[ConfirmEmails] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (7, N'Phùng Thanh Độ', N'0987654321', N'hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (8, N'Vũ Thị Tuyết Măng', N'0999998883', N'Hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (9, N'Trịnh Đình Quang', N'0987654321', N'TPHCM', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (10, N'Vương Xuân Tuấn', N'034567532', N'Quốc Oai - Hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (11, N'Phạm Ngọc An', N'0987564243', N'hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (12, N'Hoàng Văn Mạnh', N'0362785498', N'hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (13, N'Phạm Đình Huy', N'09764512411', N'Thái Bình', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (18, N'Phạm Nhật Vượng', N'099999999', N'Hà Nội', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (19, N'Nguyễn Thái Đinh', N'0978512344', N'Bình Định', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (20, N'Bùi Đức Tuyển', N'0986452341', N'Hải Dương', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (21, N'Bùi Đức Tuyển', N'0945124151', N'Hải Dương', N'pexuanmike@gmail.com')
INSERT [dbo].[Customers] ([Id], [FullName], [PhoneNumber], [Address], [Email]) VALUES (22, N'Nguyễn Văn A', N'09412556362', N'Thái Nguyên', N'pexuanmike@gmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Deliveries] ON 

INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (4, 1, 9, 1012, 7, N'TPHCM', CAST(N'2024-07-29T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-27T12:26:50.2669032' AS DateTime2), 4)
INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (5, 1, 11, 1012, 9, N'hà Nội', CAST(N'2024-07-29T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-25T19:05:46.4271663' AS DateTime2), 3)
INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (6, 1, 7, 1012, 5, N'hà Nội', CAST(N'2024-07-29T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-27T12:28:03.2973594' AS DateTime2), 5)
INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (7, 1, 21, 1012, 19, N'Hải Dương', CAST(N'2024-07-29T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-26T15:16:56.7739554' AS DateTime2), 3)
INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (8, 1, 22, 1012, 20, N'Thái Nguyên', CAST(N'2024-07-30T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-27T12:27:58.1273427' AS DateTime2), 5)
INSERT [dbo].[Deliveries] ([Id], [ShippingMethodId], [CustomerId], [DeliverId], [ProjectId], [DeliveryAddress], [EstimateDeliveryTime], [ActualDeliveryTime], [DeliveryStatus]) VALUES (9, 1, 13, 1014, 11, N'Thái Bình', CAST(N'2024-08-02T17:00:00.0000000' AS DateTime2), CAST(N'2024-07-31T23:16:19.0581416' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Deliveries] OFF
GO
SET IDENTITY_INSERT [dbo].[Designs] ON 

INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (5, 5, 5, N'IMG_638572840142387077.jpg', CAST(N'2024-07-22T22:26:54.2533198' AS DateTime2), 2, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (6, 5, 5, N'IMG_638572841237853425.jpg', CAST(N'2024-07-22T22:28:43.7887505' AS DateTime2), 2, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (7, 5, 5, N'IMG_638572888846907590.jpg', CAST(N'2024-07-22T23:48:04.6975850' AS DateTime2), 2, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (8, 5, 5, N'IMG_638572889280194115.jpg', CAST(N'2024-07-22T23:48:48.0208488' AS DateTime2), 3, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (10, 5, 5, N'IMG_638572898414391355.jpg', CAST(N'2024-07-23T00:04:01.4411586' AS DateTime2), 4, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (11, 7, 5, N'IMG_638572972162945996.jpg', CAST(N'2024-07-23T02:06:56.3082388' AS DateTime2), 3, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (12, 16, 1013, N'IMG_638573367159623680.jpg', CAST(N'2024-07-23T13:05:15.9758939' AS DateTime2), 3, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (13, 9, 1013, N'IMG_638573376432101822.jpg', CAST(N'2024-07-23T13:20:43.2194173' AS DateTime2), 3, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (14, 9, 1013, N'IMG_638573376510930205.jpg', CAST(N'2024-07-23T13:20:51.0984973' AS DateTime2), 4, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (15, 11, 5, N'IMG_638574659718944070.jpg', CAST(N'2024-07-25T00:59:31.9076684' AS DateTime2), 3, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (16, 19, 5, N'IMG_638575405216826002.jpg', CAST(N'2024-07-25T21:42:01.6881498' AS DateTime2), 4, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (17, 19, 5, N'IMG_638575426889318527.jpg', CAST(N'2024-07-25T22:18:08.9378828' AS DateTime2), 2, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (18, 20, 5, N'IMG_638575427013741163.jpg', CAST(N'2024-07-25T22:18:21.3794611' AS DateTime2), 3, 2)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (19, 10, 5, N'IMG_638575430177666410.jpg', CAST(N'2024-07-25T22:23:37.7678598' AS DateTime2), 3, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (20, 10, 1013, N'IMG_638575444435077826.jpg', CAST(N'2024-07-25T22:47:23.5094415' AS DateTime2), 2, 1018)
INSERT [dbo].[Designs] ([Id], [ProjectId], [DesginerId], [FilePath], [DesignTime], [DesignStatus], [ApproverId]) VALUES (21, 19, 5, N'IMG_638576035671346966.jpg', CAST(N'2024-07-26T15:12:47.1417649' AS DateTime2), 3, 2)
SET IDENTITY_INSERT [dbo].[Designs] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (1, 1013, N'Thiết kế của bạn đã bị từ chối', N'/projects-design/16', CAST(N'2024-07-23T13:16:18.2616901' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (2, 1013, N'Thiết kế của bạn đã bị từ chối', N'/projects-design/9', CAST(N'2024-07-23T13:21:14.7632414' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (3, 1013, N'Thiết kế của bạn đã được duyệt', N'/projects-design/9', CAST(N'2024-07-23T13:21:23.4544531' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (4, 5, N'Thiết kế của bạn đã được duyệt', N'/projects-design/11', CAST(N'2024-07-25T01:00:02.4665683' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (7, 5, N'Thiết kế của bạn đã bị từ chối', N'/projects-design/19', CAST(N'2024-07-25T21:46:10.2732929' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (8, 5, N'Thiết kế của bạn đã bị từ chối', N'/projects-design/19', CAST(N'2024-07-25T21:46:21.2366588' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (9, 5, N'Thiết kế của bạn đã được duyệt', N'/projects-design/20', CAST(N'2024-07-25T22:25:29.2638527' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (10, 1017, N'Đơn hàng đã giao thành công', N'/shipping-management', CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (12, 2, N'Đơn hàng đã giao thành công', N'/shipping-management', CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (13, 5, N'Thiết kế của bạn đã được duyệt', N'/projects-design/19', CAST(N'2024-07-26T15:13:15.6935995' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (14, 1017, N'Giao hàng thành công.', N'/shipping-management', CAST(N'2024-07-26T15:16:56.7899960' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (15, 2, N'Giao hàng thành công.', N'/shipping-management', CAST(N'2024-07-26T15:16:56.8023054' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (16, 1017, N'Giao hàng không thành công. Khách hàng từ chối nhận đơn!', N'/shipping-management', CAST(N'2024-07-27T12:26:50.2993651' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (17, 2, N'Giao hàng không thành công. Khách hàng từ chối nhận đơn!', N'/shipping-management', CAST(N'2024-07-27T12:26:50.3202901' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (18, 1017, N'Giao hàng không thành công. Khách hàng trả lại đơn!', N'/shipping-management', CAST(N'2024-07-27T12:27:58.1336661' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (19, 2, N'Giao hàng không thành công. Khách hàng trả lại đơn!', N'/shipping-management', CAST(N'2024-07-27T12:27:58.1408046' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (20, 1017, N'Giao hàng không thành công. Khách hàng trả lại đơn!', N'/shipping-management', CAST(N'2024-07-27T12:28:03.3415215' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (21, 2, N'Giao hàng không thành công. Khách hàng trả lại đơn!', N'/shipping-management', CAST(N'2024-07-27T12:28:03.3507715' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (22, 5, N'Thiết kế của bạn đã được duyệt', N'/projects-design/10', CAST(N'2024-07-31T21:54:07.6859031' AS DateTime2), 0)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (23, 1017, N'Đã có đơn hàng được tạo hãy chuyển đơn cho nhân viên vận chuyển', N'/projects-delivery/11', CAST(N'2024-07-31T22:51:56.3706934' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (24, 1017, N'Giao hàng thành công.', N'/shipping-management', CAST(N'2024-07-31T23:16:19.1130784' AS DateTime2), 1)
INSERT [dbo].[Notifications] ([Id], [UserId], [Content], [Link], [CreateTime], [IsSeen]) VALUES (25, 1018, N'Giao hàng thành công.', N'/shipping-management', CAST(N'2024-07-31T23:16:19.1242752' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (2, 2, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (3, 3, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (4, 4, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (5, 5, 2)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1002, 1018, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1004, 1022, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1005, 1013, 2)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1006, 1012, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1007, 1020, 2)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1008, 1017, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1009, 1019, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1010, 1015, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1011, 1016, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1012, 1014, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1013, 1011, 8)
INSERT [dbo].[Permissions] ([Id], [UserId], [RoleId]) VALUES (1014, 1010, 8)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[PrintJobs] ON 

INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (1, 11, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (2, 8, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (3, 12, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (4, 13, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (5, 18, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (6, 21, 1)
INSERT [dbo].[PrintJobs] ([Id], [DesignId], [PrintJobStatus]) VALUES (7, 15, 1)
SET IDENTITY_INSERT [dbo].[PrintJobs] OFF
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (5, N'Tạp chí xe hơi tháng 8', N'Làm gấp', CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), 2, CAST(N'2024-08-01T00:00:00.0000000' AS DateTime2), 7, 5)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (7, N'Bìa truyện tranh Doremon', N'Làm nhanh', CAST(N'2024-07-09T17:00:00.0000000' AS DateTime2), 2, CAST(N'2024-07-18T17:00:00.0000000' AS DateTime2), 9, 5)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (9, N'Poster Phim Fast Of Furious', N'Làm dễ nhìn', CAST(N'2024-07-17T17:00:00.0000000' AS DateTime2), 1018, CAST(N'2024-07-25T17:00:00.0000000' AS DateTime2), 11, 5)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (10, N'Sách tiếng việt lớp 1', N'Làm giấy thường', CAST(N'2024-07-18T17:00:00.0000000' AS DateTime2), 1018, CAST(N'2024-07-25T17:00:00.0000000' AS DateTime2), 12, 2)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (11, N'Bìa truyện Conan tập 100', N'làm bằng giấy xịn', CAST(N'2024-07-18T17:00:00.0000000' AS DateTime2), 1018, CAST(N'2024-07-24T17:00:00.0000000' AS DateTime2), 13, 5)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (16, N'Banner VF8', N'Thu hút người xem', CAST(N'2024-07-21T17:00:00.0000000' AS DateTime2), 1018, CAST(N'2024-07-30T17:00:00.0000000' AS DateTime2), 18, 3)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (19, N'Tài liệu Javascript', N'Mai lấy', CAST(N'2024-07-23T17:00:00.0000000' AS DateTime2), 2, CAST(N'2024-07-24T17:00:00.0000000' AS DateTime2), 21, 5)
INSERT [dbo].[Projects] ([Id], [ProjectName], [RequestDescriptionFromCustomer], [StartDate], [EmployeeId], [ExpectedEndDate], [CustomerId], [ProjectStatus]) VALUES (20, N'lịch 2025', N'Chọn màu phù hợp', CAST(N'2024-07-31T17:00:00.0000000' AS DateTime2), 2, CAST(N'2024-08-30T17:00:00.0000000' AS DateTime2), 22, 5)
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[RefreshTokens] ON 

INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1098, N'vOnINR0h1jamaKlpCOl4Bz/JZBr3dQyvTwoF8OyEwXWZTxTWCPj2JXSnqjUasj4ce9vJk/vhYnM2kjQUztO/IQ==', 5, CAST(N'2024-07-21T16:30:01.0267395' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1099, N'LPzvkKV4l2fdhynebu4j7QEbXVmf4WgN/Aua88bBTEVRHNHGSqzCxA9da3vocelrIIvh17ADOuE2kc42UZ8QNg==', 5, CAST(N'2024-07-21T16:37:42.4409911' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1100, N'h4KaTPIq7Zxar2oxo9JSvud/kj+ypNpsDkdgrGWEHw40zCTPGTj5RAAXQEUInZuRZLClKwQdlQN/1bWvEkBThw==', 5, CAST(N'2024-07-21T16:50:45.2318844' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1101, N'FSEoS9sI84lnaxBheokXD0iImOe2r5quhzu5tR5LvqiOggToCrO/4NuPmpf5cEnSfR46ktdp1hp8YW5xztn3BQ==', 1, CAST(N'2024-07-21T17:07:31.3807493' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1102, N'Nvly0MGlmHBIJZH2NhjIHXHzqCyY6XDFKF+ZU8Xs2yxIJIGF/qO22oOJBLLWwDgA6D1hLlwFrTp5ims0Z29LeQ==', 5, CAST(N'2024-07-21T17:11:53.0742909' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1103, N'w/daXunPx7sKH7dVNI5/S0TmzPiRpd0DHD9MNGCJd7ZKaPSRjJtyLMVjiLIN6WyLru6nQb7MWjswTjIzHhmPgQ==', 1, CAST(N'2024-07-21T17:59:38.6308617' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1104, N'70M5hhXSx5IY2CyC2g8rN4OwJZSPNlNcSxPkRYT6lobW1/Sj3Bglk+tp7+ne+ZwlVP1E109fFygGL77zGr4T7Q==', 5, CAST(N'2024-07-21T18:04:21.4136403' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1105, N'IEzGch6vhR3eWMnpVbqEixNjmFTglqtnleU+sPatCzxvlv1cHUxCbXa/RhYiAGcgF3SMuZl+FZ42vus7cC66/A==', 2, CAST(N'2024-07-21T18:04:27.2945589' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1106, N'/Gj5/JlDfJb7K4j4mHxX9YwLz2zU2xpNy6g0BRpkcsJWaTxiVG5UvLJN0+zA53OMhB7GuqEw7UbBmaMx6VB7AA==', 5, CAST(N'2024-07-21T18:07:50.2425938' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1107, N'q9SMFBxs7l8jDMX2MlD/0X2zZOkiwe93WIHDvnjZllAc6hZcraxX2IFR2TuiCfgsQaPiORSVgjFDS4XsakWr1w==', 1, CAST(N'2024-07-21T18:22:50.9551870' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1108, N'AM4+8+8lsHpyWxpU9msN7s83qijY6OpiA/r3p4FhOWbdUSlbItQ91ZFpnZeRNDoMC51CwslCnRiwks4nMYufug==', 1018, CAST(N'2024-07-21T18:26:13.4248604' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1109, N'haxuJmrK/p7XqfqGvV+UxPsFMlCybWeispvOSCi+n8WWpjRlMyniTJF14m6fEjuHlbs34/ZeeyiqKVqGzpO/iA==', 5, CAST(N'2024-07-21T21:10:09.2110760' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1110, N'D8t5h30QoXkfaq1PhVqIcYmg7AUXa28qXabJmzGMcjtfsCh/5fOdtEuVKz6MTglIxTntc7osZsb6WpBjYbrqdA==', 5, CAST(N'2024-07-21T21:47:30.0679218' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1111, N'LDvR0VlTyINTrkLYnhZ8fxVOvTWhqPc4vpkfkcO+XEs6jObs8jY2Qfk5EQ1uA+UcNHELc99dt5AfButMXGCtLg==', 5, CAST(N'2024-07-21T21:53:42.9337761' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1112, N'kOlCImfmKdkzcMklgaWUviAEyOO93D4DfPWcO/pgFBuKDR4Ni/wpdVA1eBekSaMaOPPoJVd3hUoNOyGgB609Ww==', 1, CAST(N'2024-07-21T22:15:16.4150931' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1113, N'ZmU0K/ytl7oTYdwpurxizpSPr3t6h6BwnREedyRc+EbVJ3WgHpOXAEFhUIaICLowZeRkOCoDHP3S354T7cQc/A==', 1018, CAST(N'2024-07-21T22:38:50.4426477' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1114, N'QZZOC5NSa177MF/HNsMIkFjk1lLe8MsMjflShkYIdg/bhzPPsx0n4k4TJkMk5je0fkXHPIc8dC9PBCq8AWsL7A==', 1, CAST(N'2024-07-21T22:41:30.6936329' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1115, N'vs9DjTUOLI2ckHhZ1xK51DP4U8nWkCP7IkTy3tTzOzMjd84ae0UdPECkxsOmolKWU43YhVoGHVBJIqpyXQD2Lw==', 5, CAST(N'2024-07-21T22:42:09.0306848' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1116, N'tsISoVBTvOXrMx5dRI+Ci8KYcBhfKJPtNcqobU9Cv1UigglMJcSKYwHPiZFrKjDXlRhEF1QjrRXJ81uyVXVZmQ==', 5, CAST(N'2024-07-21T22:43:50.7612120' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1117, N'3rZYqjuC5TNKk8g5WdJl+ljT084mKs73bmyyyT9xPPZLh7DTw/4s/BTD6EvQlBt75CJbIY/7HralJwqboBjN/g==', 1018, CAST(N'2024-07-21T22:44:00.4761559' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1118, N'XFBhpXQP6S+RC/JPxTCYsVTXwKLJIVd/9QlTD5MxMZBZA9XEkX1DkLrzCmEb+EPRbNQQp9PaojPYHLmberBeCg==', 1022, CAST(N'2024-07-22T00:03:22.0196386' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1119, N'qL5ndlrdTzczT1zQRRocx1WRJTDves4MPmzjGJcE2lcdeo+B37t0WSpsWCGXcHMxo4UIThkfLYA/zRRpdTWWZg==', 1, CAST(N'2024-07-22T00:10:19.9242405' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1120, N'8IPg7fl6sPnSGTwvU8qWs74nPW9+My4AwHWQAwA1xyAI0rp/4F9AY1wltT05Blx6rmVpql+KGFtl8PRfVU2+Vw==', 1, CAST(N'2024-07-22T15:03:56.8698257' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1121, N'xdQNDMagMy/dSGpIyl1imxqBlIdM4kAJ0B9znJveLGtoqPJnOGaGPksiLkGl/SqNxectOq3Iw0+wcP/mFNLsMA==', 1018, CAST(N'2024-07-22T15:49:37.6258356' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1122, N'gdxSOmhYCwc+nVTO5loiGj1TQx9GEoIXq4GH5J5V0CVPi7qmyrEGzVrI+F7wMiRhISg7YieKadOv43ABMWVh5g==', 1, CAST(N'2024-07-22T16:51:18.1186815' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1123, N'3lklEDzfyBAHEtCuy8plkG1PbgHVlqE1VT9UjdDJKytzpRJMMXzhZLobujcyixXc3yDlSspvpDuFFvcHrwPcNg==', 2, CAST(N'2024-07-22T18:39:03.5648736' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1124, N'A8wMPy8SvHm+0gSLgU3VZc2MJc2oHr+oLxHg/5x/GBPCbfT9NznHRohpmuZGQskOzQJ8HgK0uZ6v8nF7RQgKZQ==', 1, CAST(N'2024-07-22T21:25:53.0290962' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1125, N'HqIOBJ5MYCb2KVqd7g3pRLXI8FVI0M6FtDErofXB0W+AAabsvXM5PD8nVpgC7M9rgP+rY6iTo8F4KiLFtZdDCw==', 1, CAST(N'2024-07-22T21:33:58.8906434' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1126, N'jnEViIVeCMKZKMwA0jTvUi38iO+YSvUJ6ZRIpEi+ZSQ7usLaCzbIHShlKl8IxtTuDgZvNgOL7nm4RgLFWGgkTQ==', 2, CAST(N'2024-07-22T21:39:28.0828102' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1127, N'X2093VOwpUkM7YJAz/WrBLJOdq13dFMlTHtbzihsYYmIUQn+6LCJNBFUlcRjV1QusTwBedosG3TiR7+ePIBQew==', 1, CAST(N'2024-07-22T22:12:41.0747255' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1128, N'5tT+dbNTf7CA071q7NjvfEjIA4GW6SRgnRX8HDEetLquHa/iZKRItzhDJgRZ8OZkuHhctktwgE4AAXWwgkmiJQ==', 2, CAST(N'2024-07-22T22:24:20.4052012' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1129, N'Kwxz7yQJ2xwnn6BY5FUFhHyZ6dvMqUXItu5mchH7OoC6ZtCIpsPNmeXw4OpucdpdEfZbN4yM6LRPCZ/cNS7wKQ==', 2, CAST(N'2024-07-22T22:26:17.0710717' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1130, N'zuDuD2NUVUCE5LvFRVw98EH4dnU8vkiwubHsQvx5gVkGWRb8qbxjnT046rz/mptsg+SW04dGxpDt9GZlqOdkyQ==', 5, CAST(N'2024-07-22T22:26:28.0782786' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1131, N'zvx2TjirhJmKvR51jCIy1t+SqBPv566U9xKqmXsFsXyh+hi9Kzm2sYdyDvkq2Qij5mS+Fu8aNLfwyifEv8DXtg==', 5, CAST(N'2024-07-22T22:26:42.1422826' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1132, N'PvfATzS1plmgVCYBNmm2CSh+jBy1beGvoLv6fLc6Y/lFk1STfENggGZBt7KawtrRQLc/pjENMONrBOaNs4dIjg==', 5, CAST(N'2024-07-22T22:48:23.0095269' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1133, N'GzpFFTbWqKy4qPjDHnKN17roQC+T6Wy5xv91/2ptN0bKdZe9BVcxEpFGOk3FJV8w7RTIynSat7aSEP0DsPdjxQ==', 5, CAST(N'2024-07-22T22:48:51.9304246' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1134, N'dHN/5ZrtXrkBQVUNhhbr5cCzYr90MLfNBcSRBIDQ5T4FbyjtEntYJ1SHz+o3f+qAxomYrmhMYQ2+DM0zl/0/bQ==', 5, CAST(N'2024-07-22T22:51:48.2201160' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1135, N'XBfdO8JyZ0wKhzTeY677Y6J6XVm+kjmYy3XluCQgmYT3HNhQ0Z9Hponqen6AjK8nx/fL9lriCxl8UwNqsb4IMg==', 2, CAST(N'2024-07-23T01:02:24.4868992' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1136, N'7UzQvXZY0l9DdvDxWTQMIXu/quWjMHjPy97DsTZidcclvAeBd2SRTwQv41XjYi6hnLbLfQXSy1q9U/rDImDsNQ==', 1, CAST(N'2024-07-23T01:16:22.0040053' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1137, N'+YXVgO5Hb6i81LX2NjC7DJqNZQ1qbi/B1ja3J5aMbH7V9qcjteYGR9ZyDMyQYoPArEgCK+3P8YX1FG3a3ihCnQ==', 5, CAST(N'2024-07-23T01:31:52.7195876' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1138, N'D4sek+g54bprJlLDkzVM2THeg4YlK9SfCa+LLLssyfnkUorziNkc8HAmVtUmwVwlRDSP28SjnIvYoVFi4Kmrjg==', 2, CAST(N'2024-07-23T01:35:40.9885287' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1139, N'MuA7tPiEEUzo6yArlGkor9D+qO+Dur7pBfYo94b4d10kdGPoxHbg8xaLJmTWL5QD9AN4om+hNf4U/R+4hLvqig==', 5, CAST(N'2024-07-23T02:06:19.1849466' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1140, N'Sig/A8NjmzO0ULN6AefHOe+hfiHrQtVuxJKNvz0O1vqQY6T39pOVJJgY8aOQVD/geH0ycPIswU2mBsq6P2UrVw==', 2, CAST(N'2024-07-23T02:07:21.6593409' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1141, N'nu5LnqTopTDPHodym+o8xEIFlSxiCoFK0zjeTjb/H0YpqHKOUnKA15zXv7mf6EWNZJjRTETMxsYkB1Bsxs7ZIg==', 1018, CAST(N'2024-07-23T02:09:16.4575066' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1142, N'0tS+nhLaGjIBKpslgiyLnMjvSrt4NSzVqhIzOkaB9gfZQ7id45w+tsWKcv6ixiTqkeXO1cfTZL2JnMhBvFk9Ow==', 1, CAST(N'2024-07-23T02:09:24.2073863' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1143, N'v/faTe0eYHfABOVoQyoBX6mxdcKXfJOwpAjEINYw6TiUuTDsLwFSJMC+2fKj26oGN0zs6RlNeB/c6vJXuaM+1Q==', 1018, CAST(N'2024-07-23T12:55:12.0389219' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1144, N'WLi7Z5hY0qviy2m243ob6nQFD1xIn/MmiZlJOgQM4I4i74QHOiyD7gnPmBvVWyQV6qpkWK0j6ssemDlFhXeB8Q==', 1, CAST(N'2024-07-23T12:58:56.0240326' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1145, N'lTuus1Sdn4ykkYo1x3+gmS7p8U2ICS3s8JzZFfkbr397OcKtiWCLh6hDwf3F4U2vX+vzPJkHH3RZ3Neus1+WEA==', 1013, CAST(N'2024-07-23T13:03:01.9823395' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1146, N'HRbMnX9DwiFfjZDSeupaVB8MPi91MmNCyle09U1Ji4iZt8AQkH/zjRXIQnYfE/COGntoyN8BlAGi8isTl5hbJg==', 1, CAST(N'2024-07-23T13:03:16.5808874' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1147, N'vXpTakjizadrgIl+TVCaY0rE1jqqwpRLVfJPyGgisoR3wvk+74wlCnH42KSy2rz2tRt6XyEHKiJDC1pIzoR7vw==', 1013, CAST(N'2024-07-23T13:04:05.2959402' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1148, N'4sSEG6RkhtCzdoHG9/bYMLGtuv8PeDL/w8Gw6lrCHeOklHJetgfwEykZQCZ1ofkMKCxjgpuwl7/a0oqw3KV9bw==', 1018, CAST(N'2024-07-23T13:05:29.0024600' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1149, N'pdoIUgzyrGAi7tSI5EZyJwHHLNf1Xg1kEksqKYXctvEgSRYiCDMGV6E5KsZ3YKkI+82viJEaMIHTODMPwWoTIA==', 1018, CAST(N'2024-07-23T13:07:40.4893337' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1150, N'TInNfx5s5fJ7fjbU1S3tdAqD2Fj+7nSxFrc+Dfcn6OFD8+KLHifdgT8dLB5aB0UZb7j/qnORSdkeIb5mRfU/cQ==', 1013, CAST(N'2024-07-23T13:19:28.2211874' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1151, N'xxmcidu7QCKc6zXYhpM/oQaXoP3eSoMeo4HNTg9F/O+IcxCVzFn2UcqXr6n8PGFUs6CCvCMNVkrqooneNxdEsA==', 1018, CAST(N'2024-07-23T13:21:05.0339964' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1152, N'y6aj+kr5r6bYVR15IBlx4ACJS85fqopmclWYob6i1nGldd1XucyBWKQBVAd3qH5qg9jg9tdsmyfmU4czt6cVcA==', 1018, CAST(N'2024-07-23T15:25:05.6023458' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1153, N'U+ZbMrrCUwLNx4Roy/TI/njWU2PbV+Fgpzl00uMUNNGPZRHH/OtgpA7pAfHAc/n6KkUcwk7M9/wS0iPWyKnLcw==', 1018, CAST(N'2024-07-23T22:08:58.9848889' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1154, N'jyIayCfq7EHqeuHaUgY83CB974y4hHoWZ8QgJ9HpNmakoQ0Sdr/f7zPfx8etOomah3sT7A0K9l6sSgGwLQdaTA==', 1, CAST(N'2024-07-23T22:22:39.0404168' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1155, N'2VyhfHQt0Bk0KyOOmBV1eanRF3PrwSQ8pr/x9Ebl+Kl7DkKkXJD0HzbpgdZoPjsarWWc0s01+eJpJXHcaK3aVw==', 2, CAST(N'2024-07-23T22:30:24.6223275' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1156, N'auKEIwW8Q/EueROJXcpQeYHL6t3OV4Lz0XC8CrKnQznbSAh3SydrAvCyTObFtODQIphXPrNsknAREk0uWXtnag==', 1013, CAST(N'2024-07-23T22:43:37.3872139' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1157, N'zr3fiIutBwBOSGyxRFG/WdhErZYTXpWeP+j0dLflNFxGKavxGWtbvlFt4ZB4f3dSUT2RdXIdRzThXtCJvZqrlQ==', 2, CAST(N'2024-07-23T22:43:58.8311653' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1158, N'k25/Pkj+9SsaBH4v+bpBrQILITtA6LvoUpJrAdF5hFtq17vtaNQ4AhrhWjgGoPrqR1DM7QGlk7JV7BDffJitFQ==', 1, CAST(N'2024-07-23T23:31:24.2274320' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1159, N'dqdrF7iK08ZhhxEbd0l3x3mYP+H+4jooagy43qCbqzyj/ME3oU7/bGCrnq998LPmnqJGrLinRrl+r8gfVIAy3g==', 1018, CAST(N'2024-07-23T23:31:54.8174558' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1160, N'dSILrf/Oeh3Yek0Q6UqMFmSYbw3D2etsoGkcAqGRSx9nChgxWYSWvn0R+1ZEpeSWwVFE8u7sJV6QU0T/7bugKw==', 2, CAST(N'2024-07-23T23:33:10.6014632' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1161, N'9R3rEneuaG6DGQSzYRi17UmBQcmXcNeToMLKpwGYoPqeJx4aeyzWRkUe+/ReY7ZmzcOISUpHLuJB+fDaqB5qdQ==', 2, CAST(N'2024-07-24T00:49:14.6120265' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1162, N'RZHIu8d0VkAUYZ9gRtfvIgGejHqi2mwSylIkhWRvyOVv7emDyUJ+Ui9xoYxRo5IZPhIeuit1UBgZK+OVSLE9cg==', 2, CAST(N'2024-07-24T12:26:57.5215942' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1163, N'EplM65cw03W6OGPvVKULmt1X3vOAnT/x5WiUrnw/MTXLCiTjV6ts5pysUQHHdkrF75MxRyb13qtfczJRzGn0SQ==', 2, CAST(N'2024-07-24T14:27:01.1834407' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1164, N'7zUQLmLWAnIm1XTACJRMSodhPBbIBgrQl+9Z1bla07hSbyzhOecmB628s1vQK1YcDDfiGUhcqobQs5s2/kJdrg==', 2, CAST(N'2024-07-24T14:46:30.7730150' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1165, N'ELuI1QLMrX0We2i9QDoG/L1c5GzJfFk7VokIcWRWXJu7jQ0kPKyLkxFN3TRqdTzjhyUn2rvdRGMOcQwOh31RDQ==', 2, CAST(N'2024-07-24T17:15:09.1338122' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1166, N'+38TZ/fUeKv1zoKPgbFVomff3EkvmM9YPNOxx6aEKP1cHRvYm1RGTeum9x2UBJfARcbHjEeveMjzAsCYSWNbyg==', 2, CAST(N'2024-07-24T20:24:01.0130716' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1167, N'PQdMaZ2p6+hL2nBh2SwSet2CrEAYXcY6NBv0GB2N4wDKTHfHblv/35A1Vtzgm0ogR478aliKxHAEUJN4XN098g==', 2, CAST(N'2024-07-24T22:26:30.6259824' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1168, N'JqX7hk/Po24T29fh9CKdLxXl+bfaMgk7YN+R/Qr2VzhyZkAx4WtSF78PB68XL0g6eWRpUpzLF9RUt78PS9//9w==', 1, CAST(N'2024-07-24T23:17:16.3853753' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1169, N'vHdbHFL1OMoq5kwkxj2LEUKnBgnWCtWMoGDLZspmaQqDihXc9qA5jITw/Aiff9sKEZ9Cl1eIdKTJd3SlROHv5g==', 2, CAST(N'2024-07-24T23:32:36.4534736' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1170, N'WlpN5N6yz2YsGREI1vUqC61PsuYHchKENIJdx7/Rf9qtULOq4axeF8TB3zMw/wSUJ6VxEvsecNeWw3G9Hb0AeQ==', 1, CAST(N'2024-07-24T23:49:31.8964388' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1171, N'16GHbp88woPfaI49kr/JS3cOI8a7GrkwrPfarUW2qawmgz7k4N4npbs9nr05/W/9iFCKFMoDZ/KFq+dXjwlF5g==', 1013, CAST(N'2024-07-24T23:54:10.9664959' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1172, N'YSegtGV1c+jXCbxGWm3NZaU4Za78t0r80vfLzUjn5DHuj9LLIK53hnqngnUhZEfEwBUBu0lpWtqnLmDMKLAtvg==', 2, CAST(N'2024-07-24T23:56:13.4811469' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1173, N'OHGafTycHoq2wZQNSBxP05TufDFvmfDrn1JMLZoK/tEthwzWS3THMMCygWFh4qksN5+nqwmCd1G18rkSYY8xaw==', 1017, CAST(N'2024-07-25T00:04:14.7666901' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1174, N'8Dw5YqfdSz157/EbTq+QUDRDJj2pmQB2Xj0jrKfWOUkLYO584BlXNcX5tx8K7jxVTMr01Z3meKio8QHZISeTzQ==', 1017, CAST(N'2024-07-25T00:54:27.2747224' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1175, N'AVC+/hfhPg/W91+a/KSPmEH6d1yuV53XsMPIKperhlsZh/YWWcsy545EcE1Q6bumZ2R8PEHiAtStJVFoYhao2g==', 1017, CAST(N'2024-07-25T00:56:03.6878057' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1176, N'B7G8KM1mDGwjyPFwmUNgjGfQrXJFPplCnLQeNB8n4m5KiaYvmsk8q/w4h2NEF+fWkfJ1RhxTOvrdeG/36+beAA==', 1, CAST(N'2024-07-25T00:56:09.2251642' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1177, N'AiRI7qhh8FUjmcy3vDZht/SJliwSxxbfI74ucC48A4kFuH5JmBgeP+E5kzU8RRQVN70ECO7SEVLvL6cxwmltlw==', 2, CAST(N'2024-07-25T00:56:30.4656247' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1178, N'rDfajBM4g07z9hZsHlPxCA5x5ULS6ZceJgale7sZffZWS582pIZCYniF08gkDbld8mUpFe2WJr/KVT/0TaHcfg==', 5, CAST(N'2024-07-25T00:58:07.8824836' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1179, N'Oe08U/sMLuvxRkdYalA2RG5O3FKYAMIJ77+7AO93ny3+YOXWgSfDncGiKz5xM8IMwOi4sSXLHGru07UQ9EIFaw==', 1018, CAST(N'2024-07-25T00:59:50.8256167' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1180, N'1SD/S9nPO3FrjXkG8bkfjRFA8zK8UVBpi4HXWDfOmeXA5CcvL3EYwVLyPsX8rkyMX0e4n7pHCm/oTH4Vwl6QAQ==', 2, CAST(N'2024-07-25T01:04:25.7315040' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1181, N'0VCjVCsktDgcIvgw8HgEjpFadJTRKj/9yO7slX8PyZDKj5+TCESblRUGnEsSeP6k4ACKhSVe9xXQpnzDke117g==', 1017, CAST(N'2024-07-25T01:04:33.5369235' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1182, N'8xf35V9EPFkpTdopobBtQiefVB71p+ePrqRrw1P5h+GS9QxdzXNLkwPBl6+dYFSmt8OHcPFZW7B6OFtoFHQhFQ==', 1017, CAST(N'2024-07-25T14:24:04.1824582' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1183, N'uUa/Rp5IxtG3fzua+PvSpIaMz61H9Q5DYO5BiBGg7b2I45VNDFg/84qxFieLcJxrKMaJsdr2jyMmADdyxXJZ5A==', 1017, CAST(N'2024-07-25T15:00:51.6249986' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1184, N'Et9rmXZWCXbc4dhndI+JbOZWpH/92uVzniOSAAn9xZIENAx2+kAnbrc2iM0yb1KF0UiE4BTM1LzGJ3j/z56Q4Q==', 1017, CAST(N'2024-07-25T15:06:50.9150569' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1185, N'Y/TR4bj4qj7e/lCfjMh+TBVtRxF+DO116RkHHs/8KfxZdNDT93Pzfr1xQ2B+0w04twIOZaC88c+92mhr9F0t4w==', 1, CAST(N'2024-07-25T15:41:08.2078989' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1186, N'Oz9TCFO+0nwMuNEtr8ZA62PkcF1XMKzgiP32vckqnf9ERhKLqKIdNrHGLqUU7fYBa8kJ7h1Qb6Gsv4poeB9GvQ==', 1, CAST(N'2024-07-25T15:41:56.6430985' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1187, N'p3CwbwWn2MLPQZb3qnc82Ggvw+GA0rT20J6vR26jq5ZQN8hGJay10GBERxdBL3/qZ8YrpIlof6IAC/DrAMIWoA==', 1, CAST(N'2024-07-25T15:42:20.7599822' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1188, N'Ly+02HCStZynaBSYrDkAEJrqHCQcbQB/ilvBkqbzvboiGdpaz2nOQjLdEEgP61YULcZ7D57zvcCaES6wDCzN2A==', 1, CAST(N'2024-07-25T15:43:44.8888301' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1189, N'ii/Y+LoVxNuItOfJxDFVDGLCNm+DIPv/EI1SOkx0H835uZzBOiiAhguaV90TBeKmWPFce4tV2jEMu9VSVB2nBw==', 2, CAST(N'2024-07-25T15:45:01.0124938' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1190, N'spMt6TsHcHPGIvRt731GtIcWAdRNbs2T0NynyFCISZtbNGdV+vJNfwFie43GpnklAN2SpfMHw6vPVkjt5fx1cw==', 2, CAST(N'2024-07-25T15:46:29.8689461' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1191, N'm7fuh9Hl03IjIxHjh7fo9EkvmYxLAoNpI/hXanbra90apL8QO/ztjndkeK0MVaIWt8wiM/NooMPBXnBPaOqxww==', 2, CAST(N'2024-07-25T15:50:10.8690392' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1192, N'qA95kfMjcvIHZiGODNc4u/vOprXnRF+Gr7UwRnigqLXu3VraI7lVLDsm1VOyCYZ9AQpPcmTm5Crl4qLmYbOuyw==', 2, CAST(N'2024-07-25T15:50:47.1479298' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1193, N'TvGjwaBDepcIBqqjp5QXQvZ5HYRvSJ5aRQIurQbIHLa6mPXfhUT0qnnuWwb9OQlN3s8wN6oueveYSmX5xYwiHQ==', 2, CAST(N'2024-07-25T15:50:52.2485757' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1194, N'M0oFo4J9VhrLYsE8f998KDd2nsMcD0VX3OALZAZzhttp4Zlw6ewyxFNeYzmFoAMyogXaEdluhxk0RQRlt5hRaA==', 1, CAST(N'2024-07-25T15:52:18.9653208' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1195, N'KhT/2sTcAOUSLKlAFUYKTfDQqvE7W5WK8HWYegeG9GgVDGQsdfDwdH+5ZvCmSYXlvna2KxwoBTTL/9OzzxItHg==', 1017, CAST(N'2024-07-25T18:10:51.3815255' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1196, N'OkZg0+GP1JIC70M2CeMunYEPI9ozintu0LIYjce1y+uj5iWhsrobVbgdunkjfDTKlS4aFatdRZ3xaogDYPRM+w==', 1, CAST(N'2024-07-25T18:19:50.2512282' AS DateTime2))
GO
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1197, N'4ooNRu828XWWXvE7oK1uIspO9byKSH8VTun3gr/D4MB9SEHpBS4fdsjuT51FjXhXfD6pCGZvCA7KUSOFiuY+4Q==', 1022, CAST(N'2024-07-25T18:20:09.1844330' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1198, N'mD9DNdiYiXNRwVr1HrIbUKGFw6/tP0d2FvEPTyx6BWdKENpy33KycUDlLAEDurfRL6atDPW+Y9FqHr/HgLsBqg==', 1, CAST(N'2024-07-25T18:20:20.9984090' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1199, N'SHqhOx8L3lZ2OA+JR6AZe4QC735gMXug4DbFjeo2XV9sYMPNDS0JBeNgCwTZqOqtpsNh9E0Yif1YLK/dTNaZVQ==', 1022, CAST(N'2024-07-25T18:23:21.2215512' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1200, N'nahxvvv+fUbHp2gpAyVwTHaEBmDImqRcB8VVPhnpvcFXzdvhbvTxg5urHenntHmaMprE1NMcWEsaPMce9QtSQg==', 1012, CAST(N'2024-07-25T18:23:39.2062286' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1201, N'2sGAjJySMmXmwm9e4TTq/UNB2U4Gph/92uv/HEM4ByfrGQiV6q0RAVPkgo64yaKX/mXNUmrhxWpjJggWNklgoQ==', 1018, CAST(N'2024-07-25T18:38:44.4516115' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1202, N'reDFqPKJINblmLTQ0JylLKi/D8+eN7CI1DxYhwZn3vxyujEqESTQeCkiwbDBEokv52/GUM3lucjbnKPHs93dcQ==', 2, CAST(N'2024-07-25T18:39:56.9630496' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1203, N'qO6EGsnzcxYyUkUJxaB8C3nAbOCbiW4iqWD6S5XRPQ9BowATfiHaygDs3pyH3A9oUtdXmHHbTKMw4BqvshZoOw==', 1018, CAST(N'2024-07-25T18:40:21.4489576' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1204, N'zsyBou2jAuUMckNPiM3Hx9MaT0padji9sli2vAZ9Lqg6QU5zSYXweh54FILfozwERlOdr4CPIpurfIgQM2b/nA==', 1017, CAST(N'2024-07-25T18:47:00.5510598' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1205, N'YZFWeCxspYvyDqDUhoJXiphTxUEfXY4JHBs7o6o6y3Ho6IB1mH52ISwJvaNM8a/T0xYEXfTYgBg7UQBxP8JTLQ==', 1012, CAST(N'2024-07-25T18:50:07.8998749' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1206, N'qGsvK15GtvYBdTEp7OuNkJVCB5Rbe9ij1b9rFJv9YcPrq4pBHx5thf/ourf5bfd+Dv4FVcBwDFEL1Jaa+iNtAA==', 1017, CAST(N'2024-07-25T18:57:41.1602070' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1207, N'vBBEoCaV50rnfcVFsKcql43sANeWO1J6FJtTUd6z46Eul7ulLU7vgFRDqvndpvaXVe0ZOs0cvLDvzofm2PNm7w==', 1012, CAST(N'2024-07-25T19:03:16.2093206' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1208, N'XGHTQ6bH5JZ6k9FcTTFVBkLlMY9YSeeBGDmm+lpgVqxbH1abRGcu/iTrVbYXDxsyQh85mZy+DpR6YV03Y1kYIA==', 1017, CAST(N'2024-07-25T21:08:32.7236620' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1209, N'LUaktC6ujBqcpaDK/QIeHH3/f9rVxz5fpqDrXrVVt0rftmKevJywEsqoqX9El9zgAu4fgEyIB7oXulhXTrd3iQ==', 1018, CAST(N'2024-07-25T21:10:09.4420772' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1210, N'fuTnenC+UPrd/D2LsRklchvMB4Iw+keZ6VBSywOUc34yiI9/OCMJZ9Boxa79ijwUZjUlSHN9D8ol9fj6HaJ1gA==', 2, CAST(N'2024-07-25T21:11:12.3214803' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1211, N'1dP7t5DTD2xKsGwVYm8R6U5An6Ds4VmtSVO3LnAv83h66uid1kBpeYKAlxSZmNtV7HQKlXd6dBvP0IhvoBiruQ==', 2, CAST(N'2024-07-25T21:23:20.4996433' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1212, N'SfGkG2myr8BdJ3MgFVHOdMeORGnltMODxJ363mpFk7Z16j+5Sa0chjGX7mdfRINrq3mG0FgoEqocomoVG0crig==', 2, CAST(N'2024-07-25T21:28:01.8604220' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1213, N'CbFKc2QiR2pLXUVRT5W0ydMjqQ4qsqe+o+v75Y5of/MsGsksGdYUIRQ2hqyAcT0mWRJCsWcOf8zIJaU4vMqArg==', 1012, CAST(N'2024-07-25T21:38:44.4274966' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1214, N'Mj2KEZcLVzAmNSGyLGRatLg2AgIAd7U/4iIWdsKAs7zl5lWRnD3Ci3dllOIRLZwpNpSjUjfvhEVKunQZo72fEA==', 5, CAST(N'2024-07-25T21:38:53.1002417' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1215, N'C6YeIhIfDPpRdaFLtnw5fgykWSpFN4OcqO5XMiuesAiZ0FrKr0l/al2/mpSiQ0MCkbKHXstUaGNEq0pfJhtzMA==', 2, CAST(N'2024-07-25T21:45:53.7247434' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1216, N'Kr2nG6OkB8fDghd6hghHhW5i10250g96vs3ZaEx/2Fny6m0Bj7KnYoPyhH6dGVMTgbCoFHKJ9m31nx2B8SrRZA==', 5, CAST(N'2024-07-25T21:47:49.4635696' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1217, N'OUdmHcV14sgubJhK9lAW+a2zrcQBgLdvmUwTVR4Elc7cu9m6oEkYbVNBiaQo5UOpKipcso6RoGvuUSHA0FpIWA==', 1018, CAST(N'2024-07-25T22:24:00.2399287' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1218, N'iBiP3xb6yh7o1oGSYyYzfUB4WORHej+oArt9F5fbt4cTS+1pYWbTShYvIJRTNq0ZoJlMKO7iO1xfYWEsQd3ACQ==', 2, CAST(N'2024-07-25T22:25:20.5209731' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1219, N'VYUeCWh0wz7+KchcFrxI5GRaDrF4peWKQsj68m+l22tBqwG9Tx/dmI2ap4lMqSOpDCePASD2OjaFVkg++11srg==', 1017, CAST(N'2024-07-25T22:25:58.5815961' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1220, N'sncfEE+OwDymB+wWuzpkx7pKzHUxj7OQzABTn1P7jE8aH/vLB9H9I+NVtq5qj+IGyQoGzFrFpqwocYgF/HZC8Q==', 1012, CAST(N'2024-07-25T22:26:54.5825291' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1221, N'NYvU2X9QqumDnj817hfD/XP1jLFLbePiPueA874mjod9Bk/wQMGtC9Lc3zVtlwslnFMXmDVm/i4qilTnCvRSlQ==', 2, CAST(N'2024-07-25T22:27:38.6223527' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1222, N'CDO+r8ccIRuFiGd51D+SPVEPqRf+LVweUhsqJwhZXzVBaKDEKbQj6kPNrBibyIX2aoNVRNIClt6Hb8CHmy8QsA==', 5, CAST(N'2024-07-25T22:33:09.6867569' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1223, N'iFCKd8P6RUzbTdXGL2S71QvJAX7LUIRFI4bYaq0oaLC4w9SqK6qOVGgcoHc37kwbODBDASjtqhrs9GSVUkJnqQ==', 1, CAST(N'2024-07-25T22:34:02.9104068' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1224, N'uLQV+H5kbU/KPaO86z+bYtCmKXyAuVzkfZr5ft8DZG+diT3c9OoSTXYU0chXomi/Zv1dBrT1C5ycxzoG5yMrrg==', 2, CAST(N'2024-07-25T22:37:27.7836724' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1225, N'c+cmMfMXcmAbS1X9uClSkW7mm022AJab7ma1KUyhbBN7/0AMhQc4toK7O9oDxBRtcHBuTl3W04e7d8S5ahwaXA==', 1013, CAST(N'2024-07-25T22:44:11.8799923' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1226, N'kDAI2+QFq/xdfS3BPizZ7GAeGdRlTS4zQq8lGv2Q57rJp0X//elzlwaxw+yrVclCDzlQpknRUMbobKuZony7kg==', 2, CAST(N'2024-07-25T22:51:33.0546255' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1227, N'ILIw45Ddsuq/0JUswluw8abU6nQ1yoCOnG11bVn1JWfj/NJGZLuikZJWLpmxzjKMun2iKljV1pEbES2tJSLfjQ==', 5, CAST(N'2024-07-26T15:01:44.9806644' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1228, N'uOyX0eO46+2Eg8P6dOC+w7+l6RFTk3I4ydtlGfw2OOdiVhDDJ/2K20DVnASVEGJk3PevvzX8Jairq8GY+EfrrQ==', 2, CAST(N'2024-07-26T15:13:03.7210840' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1229, N'YzxIt/kXoUF2zcd2+pmDibF9n1NmaMh+DERtA43lve0Yj4BqbSKqtf8Jfrrw8dtP42ft28Yq1BduLDlikK+4xA==', 5, CAST(N'2024-07-26T15:13:26.2263404' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1230, N'VzDAFLx2maR3rSQyV00MKeTPLbPTcD+eLRy3XSHnyh0md7H+5/4gzL3z6VrUU/5Yme5U1cwxBYFjV1jbhxAq2Q==', 2, CAST(N'2024-07-26T15:13:38.8442607' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1231, N'TONQzv0tM7IfyOMbd6iujYwudVFHb7289X0ZrFqieX6kDw9fUjzlnuFEVkU1qfoySWpp9dWmk4SISWj6OK022g==', 1017, CAST(N'2024-07-26T15:15:48.7119589' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1232, N'lvxlJu1GEQ+A6pxij4yrGVxEQaH22kmN2NlgleJBJwIo6Y4dvqZp8soQCJMZ93b2PwpjgRZPMZp9GiqJ+KwtcQ==', 1012, CAST(N'2024-07-26T15:16:25.9941836' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1233, N'JE60ZJftx0Ksb03ORbS+jFwgxJApept6BMzGXwuWDKGvq1duZhgrozOfLMx9VCOylIAlO9xsxlWlv9T5ivmhVg==', 1017, CAST(N'2024-07-26T15:17:07.5792505' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1234, N'EzycLrW+ILkv5LFxwn8L26EGh4qRLzXfDWhRbcyVUk0NFZz1mEU8tH1krukjXqOL95jK6vxsB0uikbhYjVtOaA==', 5, CAST(N'2024-07-26T15:19:56.7567040' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1235, N'Z3hqA69Sy7tDjjI/ib6h+jfOpP3SAwcqWlXfa1gTZy3RQB1gbgwPB0xEGlnqiT5Q6kfFa4vOHYblE+dmffPv3A==', 1017, CAST(N'2024-07-26T15:21:22.6146924' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1236, N'Vn6AiBbKn1ESiUj8d+RfOiCtJvQGGc9c6+Gdlk3RUcLoq9p5DIFkQZjf6v9uvx/Rw0u3pZjVFDcfgrNUarz78g==', 1012, CAST(N'2024-07-26T15:22:16.9657100' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1237, N'auKPLMCuZkFMUfh2Jsuc2RwYeCY1Jt20VIMpWAVCU7akgFxko3l5tzDC77KOdCiPhmAFnZJI9dX3RFAYP7yICA==', 5, CAST(N'2024-07-26T15:23:14.6375813' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1238, N'9eDPdl8IEzbGQIPQD8sjPPah4uFzm2kMclnmcRKzjmWmZS5yW0Hk87cj7g49HmvXRYCnq+V9Qv3q7HMBDIyYYg==', 1, CAST(N'2024-07-27T09:37:24.5911983' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1239, N'NG3RCUDu+llOCUtokLoLYQZUICellDybfQdgrIVgeE36QuRVaw2yGQw3HoBEZuiu/E2ijtayhkhRG+3LFDrYrw==', 1, CAST(N'2024-07-27T11:38:10.2767604' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1240, N'OJLADqaYXdKGDaS8wp+YG9wjn8w2sSN53W2dXptC67AgI/b3/rQLwDogpsJGqPEjrdIc5RXWusBsImGfq1X6VA==', 1012, CAST(N'2024-07-27T12:26:37.0702120' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1241, N'VuAarHfsLAfeLP/3YouVMYfwGqgDgjvm+6sAfA80tt6yg7awrL2JvHnzz2E35gwFSBpsXHg20QXGlk3L+y/ZMQ==', 1018, CAST(N'2024-07-27T12:28:40.0838959' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1242, N'ZJ6Si8h8h3xirWXrsPitEFHlNvjYsQUkVBuR/eWTytKeDdN7YT5ZgmvK2Obksomcv35KKA7OHBQlVnMyPLkfkA==', 2, CAST(N'2024-07-27T12:29:01.4204290' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1243, N'ZJ+41Xxc2aoC6zGtPpfDQCXdfVgqaBVyvtxPiPktYb70Y8iG8uEPn90+tpUmAq3PrmmOMdX2mRBU3Mk+HNIPqQ==', 1017, CAST(N'2024-07-27T12:29:34.6985865' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1244, N'59ogAvYFAEBERpNg7B4QrM1419Qm2FtVfW96BoYUrAaLhwMwrCPWm+El9n90V72pZLYyZFoJgbOTrz3oZHIcwg==', 1, CAST(N'2024-07-30T14:14:08.0371787' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1245, N'VgsNFkqtkBfGART3m4Qm210neuaKYiWBiv0/ibjBNq1FnvdqInkVtHtNCl/qftSZHPR3IStG63gFSW5mKKESvg==', 2, CAST(N'2024-07-30T16:48:49.8527298' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1246, N'EGvmPXXmEJrt+S9/7ipUfyUIEktK7MHl5Fr1UjUSXqWP4G0PaC8WSz6lSp9U5evC01I+t1h6DfuepCh6uD48cQ==', 1012, CAST(N'2024-07-30T18:24:14.6108790' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1247, N'GyTUwlXEIR10l+HA6S9UPPD2fGmNPwBSSErda8rBnaXjYsX6CPn8T/JWGWkGNTM6uRdB6UXhiHL6vbNU2ucO4w==', 2, CAST(N'2024-07-31T21:48:50.7824012' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1248, N'I3gKdTwtEcPSo7bCXXZoiv/JZqEEG6EqaMZhi2KGyFcNEXsAH6fdK3lsHh6Asdgtiz4BvtNsVF8Kb4rg2I3f/A==', 1, CAST(N'2024-07-31T21:49:21.2436662' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1249, N'6SkExB9QGjKfhPSxXaIIj4KltlQx+pcKGcl5PlCy6DUQ3Fcsp4rQ0mh0QWi1AC8iRjmsl1LheFoeRy8zXDFADg==', 1018, CAST(N'2024-07-31T21:53:46.2356942' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1250, N'9014us3jR5m7trvBNmwSTahlC+elcKBXymTS61/uFOqTv7zkqxATSwT1ASbqadPNBiOibI/PP3RCKRyM6j4Dqw==', 1017, CAST(N'2024-07-31T22:39:13.8163044' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1251, N'vIjQ+m9xtCjtbw5flTv9XayQj5bVaNzJUSkB9lZtWfnnwr2doGOazVEg2hF3hTpOAkGdReCZasXoNpO72wNH4g==', 1012, CAST(N'2024-07-31T22:40:14.5560512' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1252, N'4RV0YLBylXn8P3V3oCJxEODm885IlCOk3FYyVvla4dF4Ik/PlJcOvuulIR8mKYL6jes04k/EcC7xXxzWNiIBfg==', 5, CAST(N'2024-07-31T22:40:30.5441165' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1253, N'mBVC8FNr2w6D8YnDxBtpEDpcBK9XvD3At82Ikfah3mQSp0DsxoY8PBJhvlMvZUqO8ZuLRzmFz3oLlwo2nI29+Q==', 1, CAST(N'2024-07-31T22:41:27.7115077' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1254, N'6biR1wLy5+k4u+CyrB7feOcCFt20cB8bsbDWPB1xbxPP70sreKBLbpZGz3XN7g+AeI96OLqMuXShhKZwZdV6Qw==', 1017, CAST(N'2024-07-31T22:42:58.6069960' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1255, N'fiD3i1D4qirWvmZ3F50taPMvr9hO+A++7ZupRScMy25w4ahC4hVTnh9Jpi9/1FCdDhlZLS4rOQ8UaptPkMKz1w==', 1018, CAST(N'2024-07-31T22:44:13.8331135' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1256, N'TUcdfLpXeBwN6hEHkyw7Brh6Y0Bf6cw3Dkd0ZmLPBrKD2XbnQLT2QDAXI+4NWz44nK+cLAK2ub3VRRqwl+zCHw==', 1017, CAST(N'2024-07-31T22:54:58.2105098' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1257, N'c5yitLKpznREg3PcvaLzH7YYsgU1yqPl5cpHY8f9YpyGj7/LUqCuvSv5gW6UrDQ7sY9czTwywFq0+t+5rv0EiQ==', 1014, CAST(N'2024-07-31T23:00:04.1359330' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1258, N'QXEkyfLFtLA2OFhrJry8yO0R5H49ZgnCSVSmNeD3PSbywGO5Ayw7xtpK9lQaEvBYFcIY9rW4G4dgFTfqDf0qXw==', 1010, CAST(N'2024-07-31T23:04:00.6568461' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1259, N'hdOf3ZQkQs6AYRmrI8moM9Xc8YdZieJB8M3NBYUsu5smSSqpaLSaOBXaV65ceAFnUAUUsN2NismRHZcXJxMKVA==', 1017, CAST(N'2024-07-31T23:04:28.3584507' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1260, N'Is0SF+oxwruHQVI49rObBkEWeEfGeLNqixg1u+raS9bOw8xxgZyNp3Cq14YvUtdqdbJ39dxzxhPIUvmasKeMcA==', 1017, CAST(N'2024-07-31T23:15:41.6569220' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1261, N'vcGsW8CAcGQjwc+3shOBTBXm+i0Oz+oL328BagV2/a/UD8uTk2h5ngZnrWWYUsRtDuFvfqUbcGMsa0nVmr9DFQ==', 1012, CAST(N'2024-07-31T23:16:03.3337505' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1262, N'taSkyIhG7uRY75FLVnrfVpkO0ws5+hRr2gK75RPzwgulSdk/JEybpCFuE9V6TagEBIpB74aW82OlgMXaxvUjBQ==', 1014, CAST(N'2024-07-31T23:16:12.7717756' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [Token], [UserId], [ExpiryTime]) VALUES (1263, N'lqKpMuaOzOWcIIqiCu7ykhhKsSGdsWopj3xendpqeXamJhHLNHZp7hva/OObhCgw5k1N5DK7Z0g+7iVRhwFzdw==', 1017, CAST(N'2024-07-31T23:16:30.9963400' AS DateTime2))
SET IDENTITY_INSERT [dbo].[RefreshTokens] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceForPrintJobs] ON 

INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (1, 1, 1)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (2, 7, 1)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (3, 9, 1)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (4, 11, 1)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (5, 17, 1)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (6, 1, 4)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (7, 3, 4)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (8, 8, 4)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (9, 1, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (10, 2, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (11, 3, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (12, 4, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (13, 5, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (14, 6, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (15, 16, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (16, 17, 2)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (17, 3, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (18, 5, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (19, 7, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (20, 9, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (21, 11, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (22, 16, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (23, 17, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (24, 2, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (25, 3, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (26, 5, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (27, 7, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (28, 9, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (29, 11, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (30, 16, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (31, 17, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (32, 3, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (33, 5, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (34, 7, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (35, 9, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (36, 11, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (37, 16, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (38, 17, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (39, 3, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (40, 6, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (41, 9, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (42, 11, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (43, 16, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (44, 17, 5)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (45, 1, 6)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (46, 2, 6)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (47, 7, 6)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (48, 1, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (49, 2, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (50, 3, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (51, 4, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (52, 5, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (53, 7, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (54, 11, 7)
INSERT [dbo].[ResourceForPrintJobs] ([Id], [ResourcePropertyDetailId], [PrintJobsId]) VALUES (55, 17, 7)
SET IDENTITY_INSERT [dbo].[ResourceForPrintJobs] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceProperties] ON 

INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (3, N'Giấy', 2, 2000)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (5, N'Ghim', 5, 250)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (6, N'Máy in', 9, 3)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (7, N'Máy cắt', 9, 2)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (8, N'Mực', 5, 100)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (9, N'Băng dính', 5, 100)
INSERT [dbo].[ResourceProperties] ([Id], [ResourcePropertyName], [ResourcesId], [Quantity]) VALUES (10, N'Màng phủ', 5, 100)
SET IDENTITY_INSERT [dbo].[ResourceProperties] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourcePropertyDetails] ON 

INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (1, 3, N'Giấy A3', N'a3.jpg', CAST(5000.00 AS Decimal(18, 2)), 1646)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (2, 3, N'Giấy A2', N'a2.jpg', CAST(10000.00 AS Decimal(18, 2)), 2813)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (3, 3, N'Giấy A1', N'a1.jpg', CAST(20000.00 AS Decimal(18, 2)), 5113)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (4, 5, N'Ghim giấy', N'paper_clip.jpg', CAST(5000.00 AS Decimal(18, 2)), 1838)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (5, 5, N'Ghim sắt', N'staple.jpg', CAST(15000.00 AS Decimal(18, 2)), 1495)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (6, 6, N'Máy in laser', N'print_laser.jpg', CAST(200000.00 AS Decimal(18, 2)), 99)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (7, 6, N'Máy in phun màu', N'print_color.jpg', CAST(200000.00 AS Decimal(18, 2)), 99)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (8, 6, N'Máy in 3D', N'print_3d.jpg', CAST(200000.00 AS Decimal(18, 2)), 100)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (9, 3, N'Bìa cứng màu', N'bia_cung.jpg', CAST(35000.00 AS Decimal(18, 2)), 1785)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (10, 8, N'Mực in laser', N'paper_clip.jpg', CAST(50000.00 AS Decimal(18, 2)), 5000)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (11, 8, N'Mực in phun', N'staple.jpg', CAST(30000.00 AS Decimal(18, 2)), 4439)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (16, 9, N'Băng dính 2 mặt', N'tape.jpg', CAST(25000.00 AS Decimal(18, 2)), 480)
INSERT [dbo].[ResourcePropertyDetails] ([Id], [ResourcePropertyId], [PropertyDetailName], [Image], [Price], [Quantity]) VALUES (17, 9, N'Băng dính 1 mặt', N'tape.jpg', CAST(15000.00 AS Decimal(18, 2)), 425)
SET IDENTITY_INSERT [dbo].[ResourcePropertyDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Resources] ON 

INSERT [dbo].[Resources] ([Id], [ResourceName], [Image], [AvailableQuantity], [ResourceStatus], [ResourceType]) VALUES (2, N'Paper', N'', 2000, 1, 1)
INSERT [dbo].[Resources] ([Id], [ResourceName], [Image], [AvailableQuantity], [ResourceStatus], [ResourceType]) VALUES (5, N'Supplies', N'', 250, 1, 2)
INSERT [dbo].[Resources] ([Id], [ResourceName], [Image], [AvailableQuantity], [ResourceStatus], [ResourceType]) VALUES (9, N'Equipment', N' ', 3, 1, 3)
SET IDENTITY_INSERT [dbo].[Resources] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleCode], [RoleName]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[Roles] ([Id], [RoleCode], [RoleName]) VALUES (2, N'Designer', N'Designer')
INSERT [dbo].[Roles] ([Id], [RoleCode], [RoleName]) VALUES (7, N'Manager', N'Manager')
INSERT [dbo].[Roles] ([Id], [RoleCode], [RoleName]) VALUES (8, N'Employee', N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[ShippingMethods] ON 

INSERT [dbo].[ShippingMethods] ([Id], [ShippingMethodName]) VALUES (1, N'Chuyển phát nhanh')
INSERT [dbo].[ShippingMethods] ([Id], [ShippingMethodName]) VALUES (2, N'Giao hàng hỏa tốc')
INSERT [dbo].[ShippingMethods] ([Id], [ShippingMethodName]) VALUES (3, N'Giao hàng tiết kiệm')
SET IDENTITY_INSERT [dbo].[ShippingMethods] OFF
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (1, N'Delivery', N'Phòng ban giao hàng', 5, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-18T00:01:17.2586003' AS DateTime2), 1017)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (2, N'Technical', N'Phòng ban kỹ thuật', 4, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-21T01:07:54.5992947' AS DateTime2), 5)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (3, N'Marketing', N'Phòng ban Marketing', 1, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-17T23:19:57.7325005' AS DateTime2), 1016)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (4, N'Sales', N'Phòng ban kinh doanh bán hàng', 4, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-18T21:44:54.8878405' AS DateTime2), 1018)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (7, N'Print Product', N'Phòng ban in ấn', 1, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-19T10:39:27.7026976' AS DateTime2), 1015)
INSERT [dbo].[Teams] ([Id], [Name], [Description], [NumberOfMember], [CreateTime], [UpdateTime], [ManagerId]) VALUES (8, N'Finance', N'Phòng ban kế toán', 1, CAST(N'2024-07-13T13:51:44.3400000' AS DateTime2), CAST(N'2024-07-18T00:01:02.6902539' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1, N'admin', N'$2a$11$ROTurkUn0L0lCgrHv2/3hubcYu8oUGTTGOw6W.Vu1kcNoI5BrsB6y', N'Vũ Phi Trường', CAST(N'2002-10-24T00:00:00.0000000' AS DateTime2), N'IMG_638571785693187819.png', N'admin@gmail.com', N'0325584984', CAST(N'2024-07-13T13:54:22.5281716' AS DateTime2), CAST(N'2024-07-21T17:09:29.3220310' AS DateTime2), NULL, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (2, N'truongvu', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Vũ Phi Trường 3', CAST(N'2024-07-01T17:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'truongvu@gmail.com', N'0987654321', CAST(N'2024-07-13T13:57:11.6451737' AS DateTime2), NULL, 4, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (3, N'a', N'$2a$11$4f3wQ5MlQwcO0Rf5bkz/XepNoC2bYvKkFEi6j6B4RwSEtz.bRzSqC', N'Trịnh Đình Quang', CAST(N'2024-07-02T17:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'sontungktp@gmail.com', N'0999999991', CAST(N'2024-07-13T14:01:32.3821735' AS DateTime2), NULL, 8, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (4, N'vuphitruong', N'$2a$11$Ii9pJML5qvnfjoVaiweR4Och6uoTW7b1zayMEfBl/VX1/9q2iPBt6', N'Vũ Phi Trường 2', CAST(N'2024-07-03T17:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'truongphisky@gmail.com', N'0999999992', CAST(N'2024-07-13T14:04:47.4070509' AS DateTime2), NULL, 2, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (5, N'abc', N'$2a$11$v68USN1J9Dv0rBHqg6k8Ze43Sz1XKPtJBStQGFYMlX6XdG/ImkoWS', N'Nguyễn Công Phượng', CAST(N'2002-10-24T00:00:00.0000000' AS DateTime2), N'IMG_638571774712127424.jpg', N'pexuanmike@gmail.com', N'0325584984', CAST(N'2024-07-13T14:31:15.5149385' AS DateTime2), CAST(N'2024-07-21T16:51:11.2214746' AS DateTime2), 2, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1010, N'abcd', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'abcd', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'abcd@gmail.com', N'0999999993', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), NULL, 1, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1011, N'truong', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Vũ Phi Trường 1', CAST(N'2002-10-24T00:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'truong@gmail.com', N'0999999995', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), NULL, 4, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1012, N'sontung', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Phùng Thanh Tùng', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'sontung@gmail.com', N'0999999996', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), NULL, 1, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1013, N'mixigaming', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Phùng Thanh Độ', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'mixigay@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 2, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1014, N'vinfast', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Phạm Nhật Vượng', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'vinfast@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 1, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1015, N'anpham', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Phạm Ngọc An', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'an@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 7, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1016, N'trucxinh', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Vũ Thị Thanh Trúc', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'truc@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 3, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1017, N'xuantuan', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Vương Xuân Tuấn', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'tuan@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 1, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1018, N'vanviet', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Nguyễn Văn Việt', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'viet@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 4, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1019, N'xuanthanh', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Trịnh Xuân Thanh', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'thanh@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 4, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1020, N'b', N'$2a$11$1Q/ntAspMASG03qhiEBTrOLwgHzp/HnjNGRyw1IGQctx6fRdArflq', N'Nguyễn Văn B', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'b@gmail.com', N'0999999997', CAST(N'2024-07-16T23:13:41.7223349' AS DateTime2), NULL, 2, 1)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [DateOfBirth], [Avatar], [Email], [PhoneNumber], [CreateTime], [UpdateTime], [TeamId], [IsActive]) VALUES (1022, N'test1', N'$2a$11$5.6A/I9XB5Bs7MQWZBtBfOgi0RLQnGbSvOGY6M7p7GWuR/qTK8l8e', N'Account Test 1', CAST(N'2002-10-23T17:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-vector/cute-boy-smiling-cartoon-kawaii-boy-illustration-boy-avatar-happy-kid_1001605-3445.jpg?w=826', N'sontacktp@gmail.com', N'0777777788', CAST(N'2024-07-22T00:02:10.7197891' AS DateTime2), NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ((0)) FOR [EmployeeId]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Users_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Users_EmployeeId]
GO
ALTER TABLE [dbo].[ConfirmEmails]  WITH CHECK ADD  CONSTRAINT [FK_ConfirmEmails_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConfirmEmails] CHECK CONSTRAINT [FK_ConfirmEmails_Users_UserId]
GO
ALTER TABLE [dbo].[CustomerFeedBacks]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFeedBacks_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[CustomerFeedBacks] CHECK CONSTRAINT [FK_CustomerFeedBacks_Customers_CustomerId]
GO
ALTER TABLE [dbo].[CustomerFeedBacks]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFeedBacks_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[CustomerFeedBacks] CHECK CONSTRAINT [FK_CustomerFeedBacks_Projects_ProjectId]
GO
ALTER TABLE [dbo].[CustomerFeedBacks]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFeedBacks_Users_UserFeedBackId] FOREIGN KEY([UserFeedBackId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CustomerFeedBacks] CHECK CONSTRAINT [FK_CustomerFeedBacks_Users_UserFeedBackId]
GO
ALTER TABLE [dbo].[Deliveries]  WITH CHECK ADD  CONSTRAINT [FK_Deliveries_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Deliveries] CHECK CONSTRAINT [FK_Deliveries_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Deliveries]  WITH CHECK ADD  CONSTRAINT [FK_Deliveries_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Deliveries] CHECK CONSTRAINT [FK_Deliveries_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Deliveries]  WITH CHECK ADD  CONSTRAINT [FK_Deliveries_ShippingMethods_ShippingMethodId] FOREIGN KEY([ShippingMethodId])
REFERENCES [dbo].[ShippingMethods] ([Id])
GO
ALTER TABLE [dbo].[Deliveries] CHECK CONSTRAINT [FK_Deliveries_ShippingMethods_ShippingMethodId]
GO
ALTER TABLE [dbo].[Designs]  WITH CHECK ADD  CONSTRAINT [FK_Designs_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Designs] CHECK CONSTRAINT [FK_Designs_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Designs]  WITH CHECK ADD  CONSTRAINT [FK_Designs_Users_DesginerId] FOREIGN KEY([DesginerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Designs] CHECK CONSTRAINT [FK_Designs_Users_DesginerId]
GO
ALTER TABLE [dbo].[ImportCoupons]  WITH CHECK ADD  CONSTRAINT [FK_ImportCoupons_ResourcePropertyDetails_ResourcePropertyDetailId] FOREIGN KEY([ResourcePropertyDetailId])
REFERENCES [dbo].[ResourcePropertyDetails] ([Id])
GO
ALTER TABLE [dbo].[ImportCoupons] CHECK CONSTRAINT [FK_ImportCoupons_ResourcePropertyDetails_ResourcePropertyDetailId]
GO
ALTER TABLE [dbo].[ImportCoupons]  WITH CHECK ADD  CONSTRAINT [FK_ImportCoupons_Users_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ImportCoupons] CHECK CONSTRAINT [FK_ImportCoupons_Users_EmployeeId]
GO
ALTER TABLE [dbo].[KeyPerformanceIndicators]  WITH CHECK ADD  CONSTRAINT [FK_KeyPerformanceIndicators_Users_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[KeyPerformanceIndicators] CHECK CONSTRAINT [FK_KeyPerformanceIndicators_Users_EmployeeId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users_UserId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Roles_RoleId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Users_UserId]
GO
ALTER TABLE [dbo].[PrintJobs]  WITH CHECK ADD  CONSTRAINT [FK_PrintJobs_Designs_DesignId] FOREIGN KEY([DesignId])
REFERENCES [dbo].[Designs] ([Id])
GO
ALTER TABLE [dbo].[PrintJobs] CHECK CONSTRAINT [FK_PrintJobs_Designs_DesignId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Users_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Users_EmployeeId]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users_UserId]
GO
ALTER TABLE [dbo].[ResourceForPrintJobs]  WITH CHECK ADD  CONSTRAINT [FK_ResourceForPrintJobs_PrintJobs_PrintJobsId] FOREIGN KEY([PrintJobsId])
REFERENCES [dbo].[PrintJobs] ([Id])
GO
ALTER TABLE [dbo].[ResourceForPrintJobs] CHECK CONSTRAINT [FK_ResourceForPrintJobs_PrintJobs_PrintJobsId]
GO
ALTER TABLE [dbo].[ResourceForPrintJobs]  WITH CHECK ADD  CONSTRAINT [FK_ResourceForPrintJobs_ResourcePropertyDetails_ResourcePropertyDetailId] FOREIGN KEY([ResourcePropertyDetailId])
REFERENCES [dbo].[ResourcePropertyDetails] ([Id])
GO
ALTER TABLE [dbo].[ResourceForPrintJobs] CHECK CONSTRAINT [FK_ResourceForPrintJobs_ResourcePropertyDetails_ResourcePropertyDetailId]
GO
ALTER TABLE [dbo].[ResourceProperties]  WITH CHECK ADD  CONSTRAINT [FK_ResourceProperties_Resources_ResourcesId] FOREIGN KEY([ResourcesId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[ResourceProperties] CHECK CONSTRAINT [FK_ResourceProperties_Resources_ResourcesId]
GO
ALTER TABLE [dbo].[ResourcePropertyDetails]  WITH CHECK ADD  CONSTRAINT [FK_ResourcePropertyDetails_ResourceProperties_ResourcePropertyId] FOREIGN KEY([ResourcePropertyId])
REFERENCES [dbo].[ResourceProperties] ([Id])
GO
ALTER TABLE [dbo].[ResourcePropertyDetails] CHECK CONSTRAINT [FK_ResourcePropertyDetails_ResourceProperties_ResourcePropertyId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Teams_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Teams_TeamId]
GO
