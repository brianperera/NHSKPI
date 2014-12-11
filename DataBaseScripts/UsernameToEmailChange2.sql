USE [NHSKPI]
GO
/****** Object:  StoredProcedure [dbo].[uspUserInsert]    Script Date: 12/11/2014 23:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**************************************************************************
Purpose:	Add User Details
Author:		Zayan Safwan
***************************************************************************/

ALTER PROCEDURE [dbo].[uspUserInsert]

	@UserName				varchar(50),
	@Password				varchar(250),
	@FirstName				varchar(100),
	@LastName				varchar(100),
	@Email					varchar(150),
	@MobileNo				varchar(15),
	@LastLogDate			datetime,
	@RoleId					int,
	@HospitalId				int,
	@IsActive				bit,
	@IsActiveDirectoryUser	bit, 
	@CreatedDate			datetime,
	@CreatedBy				int,
	@Id						int output
	
AS
BEGIN
	
	SET NOCOUNT ON;	
	
	IF NOT EXISTS (SELECT 1 FROM dbo.tblUser WHERE (Email = @Email))
	BEGIN
	INSERT INTO [dbo].[tblUser]
	(
	UserName,
	Password,
	FirstName,
	LastName,	
	Email,
	MobileNo,
	LastLogDate,
	RoleId,
	HospitalId,
	IsActive,
	IsActiveDirectoryUser, 
	CreatedDate,
	CreatedBy	
	)
	VALUES
	(
	@UserName,
	@Password,
	@FirstName,
	@LastName,	
	@Email,
	@MobileNo,
	@LastLogDate,
	@RoleId,
	@HospitalId,
	@IsActive,
	@IsActiveDirectoryUser, 
	@CreatedDate,
	@CreatedBy	
	)
    Set @Id = SCOPE_IDENTITY()
	END

	ELSE
	BEGIN
	SET @Id = -1
	END
END