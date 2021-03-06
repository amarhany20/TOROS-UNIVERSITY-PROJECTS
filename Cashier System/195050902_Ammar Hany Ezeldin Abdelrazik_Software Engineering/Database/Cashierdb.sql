USE [cashier]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 08/02/2022 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](100) NULL,
	[Barcode] [nvarchar](100) NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[Stock] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Show_Item] [bit] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Details]    Script Date: 08/02/2022 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Order_ID] [int] NOT NULL,
	[Item_ID] [int] NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total_Item_Price] [decimal](19, 4) NOT NULL,
 CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 08/02/2022 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Total] [decimal](19, 4) NULL,
	[Payment_Method] [nvarchar](50) NULL,
	[IsOrderCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08/02/2022 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[Birthday] [date] NULL,
	[CreationDate] [datetime] NULL,
	[Activated] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (1, N'Hayat Water', N'Water', N'9874648648', CAST(1.3500 AS Decimal(19, 4)), 9, N'Mineral Water', 1)
INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (2, N'Nestle Water', N'Water', N'13251351359871235', CAST(1.5000 AS Decimal(19, 4)), 10, N'Mineral Water', 1)
INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (4, N'Hot Wheels Yellow Car', N'Toys', N'123987641369578 513698', CAST(21.0000 AS Decimal(19, 4)), 10, N'From HotWheels Company', 1)
INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (5, N'Carrefour Spaghetti 500 Gram', N'Spaghetti', N'13056871', CAST(5.5000 AS Decimal(19, 4)), 10, N'', 1)
INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (7, N'Cars 2 Movie', N'Movies', N'1346871643781643...1354', CAST(20.5000 AS Decimal(19, 4)), 10, N'From Disney', 1)
INSERT [dbo].[Items] ([ID], [Name], [Category], [Barcode], [Price], [Stock], [Description], [Show_Item]) VALUES (8, N'chicken strips', N'Frozen Food', N'12357813876', CAST(50.2500 AS Decimal(19, 4)), 10, N'', 0)
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Details] ON 

INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1022, 1022, 1, CAST(5.0000 AS Decimal(19, 4)), 15, CAST(75.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1023, 1022, 4, CAST(21.0000 AS Decimal(19, 4)), 1, CAST(21.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1024, 1022, 5, CAST(5.5000 AS Decimal(19, 4)), 1, CAST(5.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1026, 1022, 2, CAST(1.5000 AS Decimal(19, 4)), 1, CAST(1.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1027, 1022, 2, CAST(1.5000 AS Decimal(19, 4)), 1, CAST(1.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1028, 1022, 2, CAST(3.0000 AS Decimal(19, 4)), 1, CAST(3.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1030, 1022, 5, CAST(5.5000 AS Decimal(19, 4)), 1, CAST(5.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1031, 1025, 4, CAST(21.0000 AS Decimal(19, 4)), 1, CAST(21.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1034, 1026, 8, CAST(50.2500 AS Decimal(19, 4)), 10, CAST(502.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1035, 1026, 8, CAST(50.2500 AS Decimal(19, 4)), 1, CAST(50.2500 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1036, 1026, 8, CAST(50.2500 AS Decimal(19, 4)), 1, CAST(50.2500 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1038, 1026, 4, CAST(21.0000 AS Decimal(19, 4)), 1, CAST(21.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1041, 1030, 1, CAST(1.3500 AS Decimal(19, 4)), 1, CAST(1.3500 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1042, 1030, 5, CAST(5.5000 AS Decimal(19, 4)), 30, CAST(165.0000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1044, 1030, 7, CAST(20.5000 AS Decimal(19, 4)), 1, CAST(20.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1045, 1030, 5, CAST(5.5000 AS Decimal(19, 4)), 1, CAST(5.5000 AS Decimal(19, 4)))
INSERT [dbo].[Order_Details] ([ID], [Order_ID], [Item_ID], [Price], [Quantity], [Total_Item_Price]) VALUES (1046, 1030, 2, CAST(1.5000 AS Decimal(19, 4)), 1, CAST(1.5000 AS Decimal(19, 4)))
SET IDENTITY_INSERT [dbo].[Order_Details] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID], [User_ID], [Date], [Total], [Payment_Method], [IsOrderCompleted]) VALUES (1022, 1, CAST(N'2022-02-06T03:26:11.020' AS DateTime), CAST(113.0000 AS Decimal(19, 4)), N'Credit Card', 1)
INSERT [dbo].[Orders] ([ID], [User_ID], [Date], [Total], [Payment_Method], [IsOrderCompleted]) VALUES (1025, 1, CAST(N'2022-02-07T10:17:39.943' AS DateTime), CAST(21.0000 AS Decimal(19, 4)), N'Credit Card', 1)
INSERT [dbo].[Orders] ([ID], [User_ID], [Date], [Total], [Payment_Method], [IsOrderCompleted]) VALUES (1026, 4, CAST(N'2022-02-07T10:18:34.563' AS DateTime), CAST(624.0000 AS Decimal(19, 4)), N'Credit Card', 1)
INSERT [dbo].[Orders] ([ID], [User_ID], [Date], [Total], [Payment_Method], [IsOrderCompleted]) VALUES (1030, 5, CAST(N'2022-02-08T01:14:46.057' AS DateTime), CAST(193.8500 AS Decimal(19, 4)), N'Cash', 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Password], [IsAdmin], [Gender], [Birthday], [CreationDate], [Activated]) VALUES (1, N'admin', N'admin', 1, N'male', CAST(N'2010-02-25' AS Date), CAST(N'2022-02-05T12:32:24.993' AS DateTime), 1)
INSERT [dbo].[Users] ([ID], [Username], [Password], [IsAdmin], [Gender], [Birthday], [CreationDate], [Activated]) VALUES (4, N'Ammar1', N'ammar', 1, N'male', CAST(N'2000-02-25' AS Date), CAST(N'2022-02-07T09:37:52.283' AS DateTime), 0)
INSERT [dbo].[Users] ([ID], [Username], [Password], [IsAdmin], [Gender], [Birthday], [CreationDate], [Activated]) VALUES (5, N'Ammar', N'ammar', 1, N'male', CAST(N'2000-02-25' AS Date), CAST(N'2022-02-07T09:38:10.053' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_Show_Item]  DEFAULT ((0)) FOR [Show_Item]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Table_1_isAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Activated]  DEFAULT ((0)) FOR [Activated]
GO
ALTER TABLE [dbo].[Order_Details]  WITH CHECK ADD  CONSTRAINT [FK_Order_Details_Items] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[Items] ([ID])
GO
ALTER TABLE [dbo].[Order_Details] CHECK CONSTRAINT [FK_Order_Details_Items]
GO
ALTER TABLE [dbo].[Order_Details]  WITH CHECK ADD  CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY([Order_ID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[Order_Details] CHECK CONSTRAINT [FK_Order_Details_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
