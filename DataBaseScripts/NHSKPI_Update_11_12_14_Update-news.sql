USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspKPINewsUpdate]    Script Date: 11/12/2014 23:22:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Update KPI News
Author:		Brian
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPINewsUpdate]
	
	@Id				int,
	@Title			varchar(1000),
	@Description	varchar (MAX),
	@IsActive   	bit
		
AS
BEGIN
	
	SET NOCOUNT ON;
	
	UPDATE [dbo].[tblKPINews]
	SET [Title] = @Title,
		[Description] = @Description,
		[CreatedDate] = GETDATE(),
		[IsActive] = @IsActive
	WHERE [Id] = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[uspKPIHospitalNewsUpdate]    Script Date: 11/12/2014 23:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Update KPI Hospital News
Author:		Brian
***************************************************************************/

CREATE PROCEDURE [dbo].[uspKPIHospitalNewsUpdate]

	@Id				int,
	@Title			varchar(1000),
	@Description	varchar (MAX),
	@IsActive   	bit,
	@HospitalId		int
		
AS
BEGIN
	
	SET NOCOUNT ON;
	
	UPDATE [dbo].[tblKPIHospitalNews]
	SET [Title] = @Title,
		[Description] = @Description,
		[CreatedDate] = GETDATE(),
		[IsActive] = @IsActive,
		[HospitalId] = @HospitalId
	WHERE [Id] = @Id
END

