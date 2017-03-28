USE [School]
GO

/****** Object:  Table [dbo].[Detention]    Script Date: 3/27/2017 10:29:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Detention](
	[Id] [int] NOT NULL  IDENTITY(1,1),
	[Name] [varchar](50) NOT NULL,
	[PenaltyInMinutes] [int] NOT NULL,
 CONSTRAINT [PK_Detention] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[DetentionAction](
	[Id] [int] NOT NULL  IDENTITY(1,1),
	[Name] [varchar](50) NOT NULL,
	[ProcessorType] [varchar](50) NULL,
 CONSTRAINT [PK_DetentionAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




CREATE TABLE [dbo].[School](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[ContactNo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_School_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[ParentName] [varchar](50) NOT NULL,
	[ParentContact] [varchar](50) NOT NULL,
	[ParentEmail] [varchar](50) NULL,
	[Class] [varchar](2) NULL,
	[Division] [varchar](2) NULL,
	[JoinDate] [datetime] NULL,
	[SchoolId] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_School] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[School] ([Id])
GO


CREATE TABLE [dbo].[StudentDetention](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[StudentId] [int] NOT NULL ,
	[DetentionId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL ,
	[Duration] [int] NOT NULL,
	[Remarks] [varchar](100) not NULL
)
GO 

CREATE TABLE [dbo].[DetentionFilter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilterName] [varchar](50) NOT NULL,
	[FilterProcessor] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DetentionFilter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

