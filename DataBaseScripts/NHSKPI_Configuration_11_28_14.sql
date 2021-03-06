USE [NHSKPI]
GO
/****** Object:  Table [dbo].[tblHospitalConfigurations]    Script Date: 11/28/2014 01:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHospitalConfigurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailFacilities] [bit] NULL,
	[Reminders] [bit] NULL,
	[DownloadDataSets] [bit] NULL,
	[BenchMarkingModule] [bit] NULL,
	[HospitalId] [int] NULL,
 CONSTRAINT [PK_tblHospitalConfigurations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_HospitalConfiguration_Hospital]    Script Date: 11/28/2014 01:12:28 ******/
ALTER TABLE [dbo].[tblHospitalConfigurations]  WITH CHECK ADD  CONSTRAINT [FK_HospitalConfiguration_Hospital] FOREIGN KEY([HospitalId])
REFERENCES [dbo].[tblHospital] ([Id])
GO
ALTER TABLE [dbo].[tblHospitalConfigurations] CHECK CONSTRAINT [FK_HospitalConfiguration_Hospital]
GO
