Create DataBase [Blogstore]
USE [Blogstore]
GO
/****** Object:  Table [Admin]    Script Date: 2020/3/22 15:09:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Admin](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Username] [nchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Remark] [nchar](200) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Blog]    Script Date: 2020/3/22 15:09:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Blog](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Title] [nchar](50) NULL,
	[Body] [ntext] NULL,
	[Body_md] [ntext] NULL,
	[VistitNum] [int] NOT NULL,
	[CaNumber] [nvarchar](64) NULL,
	[CaName] [nvarchar](64) NULL,
	[Remark] [nvarchar](50) NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Category]    Script Date: 2020/3/22 15:09:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CaNme] [nchar](50) NULL,
	[Number] [nvarchar](50) NULL,
	[Pbh] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Admin] ([Id], [CreateDate], [Username], [Password], [Remark]) VALUES (1, CAST(N'2020-03-22T00:00:00.000' AS DateTime), N'admin                                             ', N'123456', NULL)
SET IDENTITY_INSERT [Category] ON 

INSERT [Category] ([Id], [CreateDate], [CaNme], [Number], [Pbh], [Remark]) VALUES (2, CAST(N'2020-03-22T00:00:00.000' AS DateTime), N'ASP.NET                                           ', N'01', NULL, NULL)
INSERT [Category] ([Id], [CreateDate], [CaNme], [Number], [Pbh], [Remark]) VALUES (3, CAST(N'2020-03-23T00:00:00.000' AS DateTime), N'C                                                 ', N'02', NULL, NULL)
SET IDENTITY_INSERT [Category] OFF
ALTER TABLE [Blog] ADD  CONSTRAINT [DF_Blog_VistitNum]  DEFAULT ((0)) FOR [VistitNum]
GO
ALTER TABLE [Blog] ADD  CONSTRAINT [DF_Blog_Sort]  DEFAULT ((0)) FOR [Sort]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin'
GO
