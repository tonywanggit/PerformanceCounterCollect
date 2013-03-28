USE [PerfmonCounter]
GO
/****** Object:  Table [dbo].[service_counter_snapshots]    Script Date: 01/25/2013 22:40:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[service_counter_snapshots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCounterId] [int] NOT NULL,
	[SnapshotMachineName] [varchar](100) NULL,
	[CreationTimeUtc] [datetime] NOT NULL,
	[ServiceCounterValue] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[services]    Script Date: 01/25/2013 22:40:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[services](
	[Name] [varchar](100) NOT NULL,
	[DisplayName] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[service_counters]    Script Date: 01/25/2013 22:40:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[service_counters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](100) NOT NULL,
	[MachineIP] [varchar](50) NULL,
	[MachineName] [varchar](100) NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[CounterName] [varchar](100) NOT NULL,
	[InstanceName] [varchar](100) NULL,
	[DisplayName] [varchar](1000) NULL,
	[DisplayType] [varchar](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__service_c__Displ__08EA5793]    Script Date: 01/25/2013 22:40:20 ******/
ALTER TABLE [dbo].[service_counters] ADD  DEFAULT ('table') FOR [DisplayType]
GO
/****** Object:  ForeignKey [FK__service_c__Servi__09DE7BCC]    Script Date: 01/25/2013 22:40:20 ******/
ALTER TABLE [dbo].[service_counters]  WITH CHECK ADD FOREIGN KEY([ServiceName])
REFERENCES [dbo].[services] ([Name])
GO
