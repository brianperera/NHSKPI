USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspAddUpdateWardData]    Script Date: 10/27/2014 17:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Perera
-- Create date: 27-10-2014
-- Description:	Bulk upload ward data
-- =============================================
CREATE PROCEDURE [dbo].[uspAddUpdateWardData] 
	@Id int output,
	@HospitalId int,
	@WardCode varchar(100),
	@WardName varchar(100),
	@WardGroupId int,
	@WardGroupName varchar(100),
	@Description varchar(100),
	@IsActive bit,
	@WardIdentityId int = 0
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.tblWardGroup wdg WHERE wdg.Id = @WardGroupId AND wdg.HospitalId = @HospitalId)
	BEGIN
		--Insert into the ward group
		INSERT INTO dbo.tblWardGroup
		(
			WardGroupName, 
			[Description], 
			HospitalId,
			IsActive
		)
		VALUES
		(
			@WardGroupName,
			@Description,
			@HospitalId,
			@IsActive
		)
		Set @Id = SCOPE_IDENTITY()	
	END
	ELSE
	BEGIN
		UPDATE dbo.tblWardGroup 
		SET
			WardGroupName = @WardGroupName,
			[Description] = @Description,
			HospitalId = @HospitalId,
			IsActive = @IsActive
		WHERE Id = @WardGroupId
		
		Set @Id = @WardGroupId
	END
		--Commit transaction
END		

BEGIN		
	--Insert into the ward	
	
	SELECT @WardIdentityId=wd.id FROM dbo.tblWard wd WHERE wd.WardCode = @WardCode AND  wd.HospitalId = @HospitalId
	IF @WardIdentityId = 0
	BEGIN
		-- insert
		INSERT INTO dbo.tblWard
		(
			WardCode, 
			WardName, 
			HospitalId, 
			WardGroupId,
			IsActive
		)
		VALUES
		(
			@WardCode,
			@WardName,
			@HospitalId,
			@Id,
			@IsActive
		)
		Set @Id = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		-- update
		UPDATE dbo.tblWard 
		SET
			WardCode = @WardCode,
			WardName = @WardName,
			HospitalId = @HospitalId,
			WardGroupId = @Id,
			IsActive = @IsActive
		WHERE Id = @WardIdentityId
		
		Set @Id = @WardIdentityId
	END
	
END
