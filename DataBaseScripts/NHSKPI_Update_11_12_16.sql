USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspHospitalGetId]    Script Date: 11/16/2014 20:29:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Get Hospital Id
Author:		Brian Perera
***************************************************************************/

CREATE PROCEDURE [dbo].[uspHospitalGetId]

	@Code			varchar (10),
	@Id				int output
		
AS
BEGIN
	SELECT @Id = hs.Id FROM[dbo].[tblHospital] hs WHERE ([Code] = @Code)
END
