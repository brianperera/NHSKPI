USE [NHSKPI]
GO

/****** Object:  StoredProcedure [dbo].[GetIncompleteSpecialtyKPI]    Script Date: 12/4/2014 1:59:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Get incomplete Speciality KPI
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[GetIncompleteSpecialtyKPI]
@TargetId int,
@HospitalId int	
AS
BEGIN

SELECT * FROM 
(SELECT * FROM ( SELECT Id AS SpecialtyID,Specialty,SpecialtyCode FROM  [dbo].[tblSpecialty]) AS T
CROSS JOIN (SELECT Id AS KPIId,KPIDescription FROM [dbo].[tblKPI] WHERE TargetApplyFor = @TargetId) AS T1) AS TAll
WHERE NOT EXISTS (
    SELECT 1 from (SELECT SpecialtyID,KPIId,TargetMonth FROM [dbo].[tblKPISpecialtyMonthlyData]
WHERE HospitalId = @HospitalId  AND DATEPART(MONTH,TargetMonth) = DATEPART(MONTH, DATEADD(month, -1, GETDATE()))) TCurrent 
	WHERE TAll.KPIId = TCurrent.KPIId and TAll.SpecialtyID = TCurrent.SpecialtyId
)
  
END

GO

/*************************************************************************************************************/

USE [NHSKPI]
GO

/****** Object:  StoredProcedure [dbo].[GetIncompleteWardKPI]    Script Date: 12/4/2014 1:59:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Get incomplete ward KPI
Author:		Sampath
***************************************************************************/

CREATE PROCEDURE [dbo].[GetIncompleteWardKPI]
@TargetId int,
@HospitalId int	
AS
BEGIN

SELECT * FROM 
(SELECT * FROM ( SELECT Id AS WardID,WardCode,WardName FROM [dbo].[tblWard] WHERE HospitalId = @HospitalId ) AS T
CROSS JOIN (SELECT Id AS KPIId,KPIDescription FROM [dbo].[tblKPI] WHERE TargetApplyFor = @TargetId) AS T1) AS TAll
WHERE NOT EXISTS (
    SELECT 1 from (SELECT WardId,KPIId,TargetMonth FROM [dbo].[tblKPIWardMonthlyData] 
WHERE HospitalId = @HospitalId  AND DATEPART(MONTH,TargetMonth) = DATEPART(MONTH, DATEADD(month, -1, GETDATE()))) TCurrent 
	WHERE TAll.KPIId = TCurrent.KPIId and TAll.WardID = TCurrent.WardId
)
  
END

GO

