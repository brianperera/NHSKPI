USE [NHSKPI]
GO

/****** Object:  StoredProcedure [dbo].[uspGetSpecialtyData]    Script Date: 12/11/2014 07:05:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Add SP
Author:		Charitha Dissanayake
***************************************************************************/
CREATE PROCEDURE [dbo].[uspGetSpecialtyData]
    @HospId	varchar(10)
AS
BEGIN
DECLARE @sqlCommand varchar(1000)
SET @sqlCommand = 'SELECT 
        a.Id,
        b.SpecialtyCode,
        b.NationalSpecialty,
        a.TargetMonth,
        a.Numerator,
        a.Denominator,
        c.*,
        d.SpecialtyTargetDescription,
        d.SpecialtyGreen,
        d.SpecialtyAmber,
        d.YTDTargetDescription,
        d.YTDTargetGreen,
        d.YTDTargetAmber
        
FROM dbo.tblKPISpecialtyMonthlyData a
LEFT JOIN dbo.tblSpecialty b
ON a.Id = b.Id
LEFT JOIN (select b.KPIGroupName, a.* from dbo.tblKPI a LEFT JOIN
dbo.tblKPIGroup b ON a.KPIGroupId = b.Id) c
ON a.KPIId = c.KPINo
LEFT JOIN dbo.tblKPISpecialtyMonthlyTarget d
ON a.KPIId = d.KPIId AND a.Id = d.SpecialtyId AND a.TargetMonth = d.TargetMonth
WHERE a.HospitalId = ' + @HospId
EXEC (@sqlCommand)
END
GO

