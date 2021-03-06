USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspUserLogin]    Script Date: 11/30/2014 16:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	User Login (CR | Brian: Requirement 2)
Author:		K.Piragalathan
***************************************************************************/
ALTER PROCEDURE [dbo].[uspUserLogin] 
	@Email varchar(50),
	@Password varchar(50),
	@HospitalCode varchar(50),
	@IsExist bit output,
	@Id int output,
	@RoleId int output,
	@FirstName varchar(50) output,
	@LastName varchar(50) output,
	@HospitalId int output,
	@HospitalName varchar(250) output,
	@HospitalType varchar(20) output
AS
BEGIN
	
	IF EXISTS (SELECT 1 FROM dbo.tblUser WHERE (Email = @Email) AND ([Password] = @Password) AND (IsActive = 1))
	BEGIN
		SELECT
		@Id = U.Id,
		@RoleId = RoleId,
		@FirstName = FirstName,
		@LastName = LastName,
		@HospitalCode = (Select h.Code from tblHospital h where h.Id = (Select HospitalId from tblUser u where u.Id = 1) AND IsActive = 1),
		@HospitalId = CASE WHEN RoleId = 1 THEN (SELECT Id FROM dbo.tblHospital WHERE IsActive = 1 AND Code = @HospitalCode) ELSE  (SELECT Id FROM dbo.tblHospital WHERE IsActive = 1 AND Id = HospitalId) END,
		@HospitalName = (SELECT Name FROM dbo.tblHospital WHERE IsActive = 1 AND Id = HospitalId),
		@HospitalType = (SELECT Type FROM dbo.tblHospital WHERE IsActive = 1 AND Id = HospitalId)
		FROM dbo.tblUser U
		WHERE
		(Email = @Email) AND ([Password] = @Password) AND U.IsActive = 1
		SET @IsExist = 1
	END
	ELSE
	BEGIN
		SET @IsExist = 0
	END
END

