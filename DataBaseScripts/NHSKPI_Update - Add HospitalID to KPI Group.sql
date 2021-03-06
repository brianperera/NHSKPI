USE [NHSKPI]

ALTER TABLE [dbo].[tblKPIGroup] ADD HospitalID INT NULL;

GO
/****** Object:  StoredProcedure [dbo].[uspKPIGroupInsert]    Script Date: 10/21/2014 12:10:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add KPI Group Details
Author:		Zayan Safwan
***************************************************************************/
ALTER PROCEDURE [dbo].[uspKPIGroupInsert] 
		
	@KPIGroupName		varchar(100),
	@IsActive			bit,
	@HospitalID			int,
	@Id					int output
	
	
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT 1 FROM[dbo].[tblKPIGroup] WHERE ([KPIGroupName] = @KPIGroupName)) 
	BEGIN
    INSERT INTO [dbo].[tblKPIGroup]
    (    
    [KPIGroupName],
    [IsActive],
	[HospitalID]
    )
    
    VALUES
    (
    @KPIGroupName,
    @IsActive,
	@HospitalID
    )
  Set @Id = SCOPE_IDENTITY() 
  END
  
  ELSE
  BEGIN
   Set @Id = -1;
  END 
END
