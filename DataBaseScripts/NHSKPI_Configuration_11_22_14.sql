USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspHospitalConfigurationInitialize]    Script Date: 11/22/2014 20:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add User Details
Author:		Brian Perera
***************************************************************************/

CREATE PROCEDURE [dbo].[uspHospitalConfigurationAdd]

	@EmailFacilities				bit output,
	@Reminders						bit output,
	@DownloadDataSets				bit output,
	@BenchMarkingModule				bit output,
	@HospitalId						varchar(150) output,
	@Id								int output
	
AS
BEGIN
	
	SET NOCOUNT ON;	
	
	IF NOT EXISTS (SELECT 1 FROM dbo.tblHospitalConfigurations WHERE (HospitalId = @HospitalId))
	BEGIN
	--If the hopital does not have configuration create them
	INSERT INTO [dbo].[tblHospitalConfigurations]
	(
	EmailFacilities,
	Reminders,
	DownloadDataSets,
	BenchMarkingModule,	
	HospitalId	
	)
	VALUES
	(
	@EmailFacilities,
	@Reminders,
	@DownloadDataSets,
	@BenchMarkingModule,	
	@HospitalId	
	)
	
	SET @Id = 1
    
	END

	ELSE
	BEGIN
	SET @Id = -1
	END
END


GO
/****** Object:  StoredProcedure [dbo].[uspHospitalConfigurationView]    Script Date: 11/22/2014 20:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add User Details
Author:		Brian Perera
***************************************************************************/

CREATE PROCEDURE [dbo].[uspHospitalConfigurationView]

	@EmailFacilities				bit output,
	@Reminders						bit output,
	@DownloadDataSets				bit output,
	@BenchMarkingModule				bit output,
	@HospitalId						int,
	@Id								int output
	
AS
BEGIN
	
	SET NOCOUNT ON;	
	
		--Select newly created configuration set
	BEGIN
	SELECT @EmailFacilities=EmailFacilities, 
	@Reminders = Reminders, @DownloadDataSets = DownloadDataSets, @BenchMarkingModule = BenchMarkingModule
	FROM dbo.tblHospitalConfigurations WHERE (HospitalId = @HospitalId)
	
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[uspHospitalConfigurationUpdate]    Script Date: 11/22/2014 20:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add User Details
Author:		Brian Perera
***************************************************************************/

CREATE PROCEDURE [dbo].[uspHospitalConfigurationUpdate]

	@EmailFacilities				bit,
	@Reminders						bit,
	@DownloadDataSets				bit,
	@BenchMarkingModule				bit,
	@HospitalId						int,
	@Id								int output
	
AS
BEGIN
	
	SET NOCOUNT ON;	
	
		--Select newly created configuration set
	BEGIN
	UPDATE dbo.tblHospitalConfigurations 
	SET 
	EmailFacilities = @EmailFacilities,
	Reminders = @Reminders,
	DownloadDataSets = @DownloadDataSets,
	BenchMarkingModule = @BenchMarkingModule
	FROM dbo.tblHospitalConfigurations WHERE (HospitalId = @HospitalId)
	
	END
	
END