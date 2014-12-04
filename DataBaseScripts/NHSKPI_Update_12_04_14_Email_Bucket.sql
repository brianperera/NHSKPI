USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspEmailInsert]    Script Date: 12/04/2014 22:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add Email
Author:		Brian Perera
***************************************************************************/
ALTER PROCEDURE [dbo].[uspEmailInsert] 
	
	@Id				int,	
	@Email			varchar(200),
	@Description	varchar(200),
	@HospitalId		int
	
	
AS
BEGIN
	
	SET NOCOUNT ON;
    INSERT INTO [dbo].[tblEmailBucket]
    (    
    Email,
    [Description],
    HospitalId
    )
    
    VALUES
    (
    @Email,
    @Description,
    @HospitalId
    )
    
END

GO
/****** Object:  StoredProcedure [dbo].[uspEmailView]    Script Date: 12/04/2014 22:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	View emails on hospital level
Author:		Brian Perera
***************************************************************************/
CREATE PROCEDURE [dbo].[uspEmailView]
	@Id				int,
	@Email			varchar(200) output,
	@Description	varchar(200) output,
	@HospitalId		int output
AS
BEGIN	
SELECT
	
	@Id				= Id,
	@Email			= Email,
	@Description	= [Description],
	@HospitalId		= HospitalId	
	
FROM [dbo].[tblEmailBucket]
WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[uspEmailDelete]    Script Date: 12/04/2014 22:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	View emails on hospital level
Author:		Brian Perera
***************************************************************************/
CREATE PROCEDURE [dbo].[uspEmailDelete]
	@Id				int
AS
BEGIN
DELETE 
FROM [dbo].[tblEmailBucket]
WHERE Id = @Id
END