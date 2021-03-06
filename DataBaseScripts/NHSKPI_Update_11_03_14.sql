USE [NHSKPI]
GO
-- Alter the Specialty table and add the hospital ID column

ALTER TABLE tblSpecialty
Add HospitalId int

GO

ALTER TABLE tblSpecialty
ADD CONSTRAINT fk_tblSpecialty_pk_tblHospital
FOREIGN KEY (HospitalId)
REFERENCES tblHospital(Id)

GO
/*
ALTER TABLE tblSpecialty
ADD CONSTRAINT fk_tblSpecialty_pk_tblWardGroup
FOREIGN KEY (GroupId)
REFERENCES tblWardGroup(Id)
*/
GO
/****** Object:  StoredProcedure [dbo].[uspAddUpdateSpecialtyData]    Script Date: 11/02/2014 12:49:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Perera
-- Create date: 27-10-2014
-- Description:	Bulk upload specialty data
-- =============================================
CREATE PROCEDURE [dbo].[uspAddUpdateSpecialtyData] 
	@Id int output,
	@HospitalId int,
	@SpecialtyCode varchar(100),
	@Specialty varchar(100),
	@GroupId int,
	@NationalSpecialty varchar(100),
	@NationalCode varchar(100),
	@IsActive bit
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.tblSpecialty sp WHERE sp.SpecialtyCode = @SpecialtyCode AND sp.HospitalId = @HospitalId)
	BEGIN
		--Insert into the ward group
		INSERT INTO dbo.tblSpecialty
		(
			Specialty, 
			SpecialtyCode,
			NationalSpecialty,
			NationalCode,
			GroupId,
			HospitalId,
			IsActive
		)
		VALUES
		(
			@Specialty,
			@SpecialtyCode,
			@NationalSpecialty,
			@NationalCode,
			@GroupId,
			@HospitalId,
			@IsActive
		)
		Set @Id = SCOPE_IDENTITY()	
	END
	ELSE
	BEGIN
		-- update
		UPDATE dbo.tblSpecialty 
		SET
			Specialty = @Specialty,
			SpecialtyCode = @SpecialtyCode,
			NationalSpecialty = @NationalSpecialty,
			NationalCode = @NationalCode,
			GroupId = @GroupId,
			HospitalId = @HospitalId,
			IsActive = @IsActive
		WHERE SpecialtyCode = @SpecialtyCode AND HospitalId = @HospitalId
	END
END
