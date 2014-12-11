USE [NHSKPI]
GO

/****** Object:  StoredProcedure [dbo].[uspGetWardData]    Script Date: 12/11/2014 07:05:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	ADD SP
Author:		charitha Dissanayake
***************************************************************************/
CREATE PROCEDURE [dbo].[uspGetWardData]
    @HospId	varchar(10)
AS
BEGIN
DECLARE @sqlCommand varchar(1000)
SET @sqlCommand = ' SELECT 
        a.HospitalId,
        b.WardCode,
        b.WardName,
        a.TargetMonth,
        a.Numerator,
        a.Denominator,
        c.*,
        d.MonthlyTargetDescription,
        d.MonthlyTargetGreen,
        d.MonthlyTargetAmber,
        d.YTDTargetDescription,
        d.YTDTargetGreen,
        d.YTDTargetAmber
        
FROM dbo.tblKPIWardMonthlyData a
LEFT JOIN dbo.tblWard b
ON a.WardId = b.Id
LEFT JOIN (select b.KPIGroupName, a.* from dbo.tblKPI a LEFT JOIN
dbo.tblKPIGroup b ON a.KPIGroupId = b.Id) c
ON a.KPIId = c.KPINo
LEFT JOIN dbo.tblKPIWardMonthlyTarget d
ON a.KPIId = d.KPIId AND a.WardId = d.WardId AND a.TargetMonth = d.TargetMonth
WHERE a.HospitalId = ' + @HospId
EXEC (@sqlCommand)
END
GO

