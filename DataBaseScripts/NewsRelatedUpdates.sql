USE [NHSKPI]
GO

/****** Object:  Table [dbo].[tblKPINews]    Script Date: 11/6/2014 3:03:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblKPINews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](1000) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


GO

/****** Object:  Table [dbo].[tblKPIHospitalNews]    Script Date: 11/6/2014 3:04:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblKPIHospitalNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](1000) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NULL,
	[HospitalId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

GO

/****** Object:  StoredProcedure [dbo].[uspKPIHospitalNewsInsert]    Script Date: 11/6/2014 3:05:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Add KPI Hospital News
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPIHospitalNewsInsert]

	@Title			varchar(1000),
	@Description	varchar (MAX),
	@IsActive   	bit,
	@HospitalId		int
		
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO [dbo].[tblKPIHospitalNews]
           ([Title]
           ,[Description]
           ,[CreatedDate]
           ,[IsActive]
		   ,[HospitalId])
     VALUES
           (@Title,
		   @Description,
		   GETDATE(),
		   @IsActive,
		   @HospitalId)
END

GO

GO

/****** Object:  StoredProcedure [dbo].[uspKPIHospitalNewsSearch]    Script Date: 11/6/2014 3:06:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Select KPI News
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPIHospitalNewsSearch]

	@Id			int,
	@HospitalId int,
	@IsActive   bit	
AS
BEGIN
	
	SET NOCOUNT ON;
IF(@Id < 0)
	SELECT [Id]
		  ,[Title]
		  ,[Description]
		  ,[CreatedDate]
		  ,[IsActive]
	  FROM [dbo].[tblKPIHospitalNews]
	  WHERE [HospitalId] = @HospitalId AND
			(@IsActive IS NULL OR [IsActive] = @IsActive)
ELSE
	SELECT [Id]
		  ,[Title]
		  ,[Description]
		  ,[CreatedDate]
		  ,[IsActive]
	  FROM [dbo].[tblKPIHospitalNews]
	  WHERE [Id] = @Id AND [HospitalId] = @HospitalId AND
			(@IsActive IS NULL OR [IsActive] = @IsActive)
END

GO

GO

/****** Object:  StoredProcedure [dbo].[uspKPINewsInsert]    Script Date: 11/6/2014 3:06:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Add KPI News
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPINewsInsert]

	@Title			varchar(1000),
	@Description	varchar (MAX),
	@IsActive   	bit
		
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO [dbo].[tblKPINews]
           ([Title]
           ,[Description]
           ,[CreatedDate]
           ,[IsActive])
     VALUES
           (@Title,
		   @Description,
		   GETDATE(),
		   @IsActive)
END

GO

GO

/****** Object:  StoredProcedure [dbo].[uspKPINewsSearch]    Script Date: 11/6/2014 3:06:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Select KPI News
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPINewsSearch]

	@Id			int,
	@IsActive   bit	
AS
BEGIN
	
	SET NOCOUNT ON;
IF(@Id < 0)
	SELECT [Id]
		  ,[Title]
		  ,[Description]
		  ,[CreatedDate]
		  ,[IsActive]
	  FROM [dbo].[tblKPINews]
	  WHERE (@IsActive IS NULL OR [IsActive] = @IsActive)
ELSE
	SELECT [Id]
		  ,[Title]
		  ,[Description]
		  ,[CreatedDate]
		  ,[IsActive]
	  FROM [dbo].[tblKPINews]
	  WHERE [Id] = @Id  AND
			(@IsActive IS NULL OR [IsActive] = @IsActive)
END

GO