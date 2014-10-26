USE [NHSKPI]

GO
/****** Object:  Table [dbo].[tblDepartmentHead]    Script Date: 10/26/2014 12:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDepartmentHead](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](150) NULL,
	[JobTile] [nchar](150) NULL,
	[MobileNo] [nchar](150) NULL,
	[Email] [nchar](150) NULL,
	[ApprovedUserId] [int] NULL,
 CONSTRAINT [PK_DepartmentHead] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_tblDepartmentHead_tblUser]    Script Date: 10/26/2014 12:53:07 ******/
ALTER TABLE [dbo].[tblDepartmentHead]  WITH CHECK ADD  CONSTRAINT [FK_tblDepartmentHead_tblUser] FOREIGN KEY([ApprovedUserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblDepartmentHead] CHECK CONSTRAINT [FK_tblDepartmentHead_tblUser]
GO


GO
/****** Object:  StoredProcedure [dbo].[uspDepartmentHeadInsertUpdate]    Script Date: 10/25/2014 18:57:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add User Details
Author:		Brian Perera
***************************************************************************/

CREATE PROCEDURE [dbo].[uspDepartmentHeadInsertUpdate]
	@Id						int output,
	@Name					varchar(150),
	@JobTile				varchar(150),
	@MobileNo				varchar(150),
	@Email					varchar(150),
	@ApprovedUserId			int
AS
BEGIN
	
	SET NOCOUNT ON;	
	IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentHead WHERE (ApprovedUserId = @ApprovedUserId))
		BEGIN
			INSERT INTO [dbo].[tblDepartmentHead]
			(Name, JobTile, MobileNo, Email, ApprovedUserId)
			VALUES(
			@Name,
			@JobTile,
			@MobileNo,
			@Email,	
			@ApprovedUserId	
			)
			Set @Id = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[tblDepartmentHead]
			SET 
			Name=@Name,
			JobTile=@JobTile,
			MobileNo=@MobileNo,
			Email=@Email
			WHERE ApprovedUserId=@ApprovedUserId
			
			BEGIN
				SELECT @Id = Id
				FROM [dbo].[tblDepartmentHead]
				WHERE ApprovedUserId=@ApprovedUserId 
			END
			
		END
END


