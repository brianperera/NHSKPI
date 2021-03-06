USE [NHSKPI]
GO
/****** Object:  Table [dbo].[tblEmailBucket]    Script Date: 12/14/2014 21:35:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmailBucket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nchar](200) NULL,
	[Description] [nchar](200) NULL,
	[HospitalId] [int] NULL,
 CONSTRAINT [PK_tblEmailBucket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_EmailBucket_Hospital]    Script Date: 12/14/2014 21:35:19 ******/
ALTER TABLE [dbo].[tblEmailBucket]  WITH CHECK ADD  CONSTRAINT [FK_EmailBucket_Hospital] FOREIGN KEY([HospitalId])
REFERENCES [dbo].[tblHospital] ([Id])
GO
ALTER TABLE [dbo].[tblEmailBucket] CHECK CONSTRAINT [FK_EmailBucket_Hospital]
GO
