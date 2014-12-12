SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian P	
-- Create date: <Create Date,,>
-- Description: Removes a KPI Group
-- =============================================
CREATE PROCEDURE uspKPIGroupDelete 
	-- Add the parameters for the stored procedure here
	@KPIGropuId as INT
AS
BEGIN
	
    -- Insert statements for procedure here
	DELETE tblKPIGroup
	WHERE tblKPIGroup.Id = @KPIGropuId
END
GO
