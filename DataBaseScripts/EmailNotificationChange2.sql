USE [NHSKPI]
GO

/****** Object:  Table [dbo].[tblEmailNotifications]    Script Date: 12/4/2014 11:11:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblEmailNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HospitalId] [int] NOT NULL,
	[Reminder1] [int] NULL,
	[Reminder2] [int] NULL,
	[ManagerEscalation] [int] NULL,
	[ReminderEmail] [varchar](100) NULL,
	[ManagerEscalationEmail] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  StoredProcedure [dbo].[uspEmailNotificationDelete]    Script Date: 12/4/2014 11:12:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Delete Email Notification
Author:		Sampath
***************************************************************************/

Create PROCEDURE [dbo].[uspEmailNotificationDelete]
	@HospitalId   	int		
AS
BEGIN
	
	SET NOCOUNT ON;
	IF EXISTS(SELECT 1 FROM [dbo].[tblEmailNotifications] WHERE (HospitalId = @HospitalId)) 
	BEGIN
		DELETE FROM [dbo].[tblEmailNotifications]
		  WHERE (HospitalId = @HospitalId)
	END	
  
END
GO

/****** Object:  StoredProcedure [dbo].[uspEmailNotificationInsert]    Script Date: 12/4/2014 11:12:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/**************************************************************************
Purpose:	Add Email Notification
Author:		Sampath
***************************************************************************/

Create PROCEDURE [dbo].[uspEmailNotificationInsert]

	@HospitalId   	int,
	@Reminder1		int,
	@Reminder2		int,
	@ManagerEscalation		int,
	@ReminderEmail		varchar(100),
	@ManagerEscalationEmail	varchar(100)
		
AS
BEGIN
	
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT 1 FROM [dbo].[tblEmailNotifications] WHERE (HospitalId = @HospitalId)) 
	BEGIN

	INSERT INTO [dbo].[tblEmailNotifications]
           ([HospitalId]
           ,[Reminder1]
           ,[Reminder2]
           ,[ManagerEscalation]
           ,[ReminderEmail]
           ,[ManagerEscalationEmail])
     VALUES
           (@HospitalId ,
	@Reminder1,
	@Reminder2,
	@ManagerEscalation,
	@ReminderEmail,
	@ManagerEscalationEmail)

	END	
	ELSE
	BEGIN
	UPDATE [dbo].[tblEmailNotifications]
	   SET [Reminder1] = @Reminder1
		  ,[Reminder2] = @Reminder2
		  ,[ManagerEscalation] = @ManagerEscalation
		  ,[ReminderEmail] = @ReminderEmail
		  ,[ManagerEscalationEmail] = @ManagerEscalationEmail
	 WHERE HospitalId = @HospitalId
	END   
END
GO

/****** Object:  StoredProcedure [dbo].[uspEmailNotificationSelect]    Script Date: 12/4/2014 11:57:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**************************************************************************
Purpose:	Select Email Notification
Author:		Sampath
***************************************************************************/

Create PROCEDURE [dbo].[uspEmailNotificationSelect]
	@HospitalId   	int		
AS
BEGIN
SET NOCOUNT ON;  
IF(@HospitalId < 0)
	SELECT [Id]
		  ,[HospitalId]
		  ,[Reminder1]
		  ,[Reminder2]
		  ,[ManagerEscalation]
		  ,[ReminderEmail]
		  ,[ManagerEscalationEmail]
	  FROM [dbo].[tblEmailNotifications]
ELSE
	SELECT [Id]
		  ,[HospitalId]
		  ,[Reminder1]
		  ,[Reminder2]
		  ,[ManagerEscalation]
		  ,[ReminderEmail]
		  ,[ManagerEscalationEmail]
	  FROM [dbo].[tblEmailNotifications]
			  WHERE (HospitalId = @HospitalId)
END
