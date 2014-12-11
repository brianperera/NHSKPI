using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Util
{
    public class Constant
    {
        #region Common constant
        public static readonly string HC = "BUCKS";
        public static readonly string CommentType = "Comment Type 1,Comment Type 2,Comment Type 3";
        public static readonly string CMN_String_Dynamic = "dynamic";
        public static readonly string CMN_String_Static = "static";
        public static readonly string CMN_String_Ward_Id = "WardId";
        public static readonly string CMN_String_Specialty_Id = "SpecialtyId";
        public static readonly string CMN_String_KPI_Id = "KpiId";
        public static readonly string CMN_String_Financail_Year = "FinancailYear";
        public static readonly string CMN_String_Hospital_Id = "HospitalId";
        
        #endregion

        #region Data Base related Constant

        public static readonly string NHS_Database_Connection_Name  = "NHSKPI";
        public static readonly string SP_Hospital_Insert            = "uspHospitalInsert";
        public static readonly string SP_Hospital_GetId             = "uspHospitalGetId";
        public static readonly string SP_Hospital_Update            = "uspHospitalUpdate";
        public static readonly string SP_Hospital_Search            = "uspHospitalSearch";
        public static readonly string SP_Hospital_ViewAll           = "uspHospitalViewAll";
        public static readonly string SP_Hospital_View              = "uspHopitalView";
        public static readonly string SP_WardGroup_Insert           = "uspWardGroupInsert";
        public static readonly string SP_WardGroup_Update           = "uspWardGroupUpdate";
        public static readonly string SP_WardGroup_Search           = "uspWardGroupSearch";
        public static readonly string SP_WardGroup_View             = "uspWardGroupView";
        public static readonly string SP_Ward_Initial_Data          = "uspGetWardInitialData";
        public static readonly string SP_Ward_Insert                = "uspWardInsert";
        public static readonly string SP_Ward_Update                = "uspWardUpdate";
        public static readonly string SP_Ward_Search                = "uspWardSearch";
        public static readonly string SP_Ward_View                  = "uspWardView";
        public static readonly string SP_Ward_View_All              = "uspWardViewAll";
        public static readonly string SP_KPI_View_All               = "uspKPIViewAll";      
        public static readonly string SP_KPI_Group_Insert           = "uspKPIGroupInsert";
        public static readonly string SP_KPI_Group_Update           = "uspKPIGroupUpdate";
        public static readonly string SP_KPI_Group_Search           = "uspKPIGroupSearch";
        public static readonly string SP_KPI_Group_View             = "uspKPIGroupView";
        public static readonly string SP_KPI_Insert                 = "uspKPIInsert";
        public static readonly string SP_KPI_Update                 = "uspKPIUpdate";
        public static readonly string SP_KPI_Search                 = "uspKPISearch";
        public static readonly string SP_KPI_View                   = "uspKPIView";
        public static readonly string SP_Get_All_TargetApplyFor     = "uspGetTargetApplyFor";
        public static readonly string SP_KPIDetail_Insert           = "uspKPIDetailsInsert";
        public static readonly string SP_KPIDetail_Update           = "uspKPIDetailsUpdate";
        public static readonly string SP_KPIDetail_View             = "uspKPIDetailsView";
        public static readonly string SP_Get_All_KPI_No             = "uspGetAllKpiNo";
        public static readonly string SP_Get_Auto_KPI_No            = "uspGetAutoKpiNo";
        public static readonly string SP_Specialty_Insert           = "uspSpecialtyInsert";
        public static readonly string SP_Specialty_Update           = "uspSpecialtyUpdate";
        public static readonly string SP_Specialty_Search           = "uspSpecialtySearch";
        public static readonly string SP_Specialty_View             = "uspSpecialtyView";   
        public static readonly string SP_User_Insert                = "uspUserInsert";
        public static readonly string SP_Get_All_UserRole           = "GetAllUserRole";
        public static readonly string SP_User_View                  = "uspUserView";
        public static readonly string SP_User_Update                = "uspUserUpdate";
        public static readonly string SP_User_Search                = "uspUserSearch";
        public static readonly string SP_User_Login                 = "uspUserLogin";
        public static readonly string SP_User_Initial_Data          = "uspGetUserInitialData";
        public static readonly string SP_Get_Ward_Data              = "uspGetWardData";
        public static readonly string SP_Get_Specialty_Data         = "uspGetSpecialtyData";

        public static readonly string SP_KPIWardMonthlyTarget_Insert        = "uspKPIWardMonthlyTargetInsert";
        public static readonly string SP_Ward_KPI_Target_View               = "uspKPIWardMonthlyTargetView";
        public static readonly string SP_Hospital_KPI_YTD_Target_View       = "uspHospitalKPIYTDTargetView";
        public static readonly string SP_Hospital_KPI_YTD_Target_Insert     = "uspHospitalKPIYTDTargetInsert";
        public static readonly string SP_KPIWardMonthlyTarget_Update        = "uspKPIWardMonthlyTargetUpdate";
        public static readonly string SP_Ward_KPI_Data_View                 = "uspKPIWardMonthlyDataView";
        public static readonly string SP_KPIWardMonthlyData_Insert          = "uspKPIWardMonthlyDataInsert";
        public static readonly string SP_KPIBulkWardData_Delete             = "uspKPIBulkWardDataDelete";
        public static readonly string SP_KPIBulkSpecialtyData_Delete = "uspKPIBulkSpecialtyDataDelete";
        public static readonly string SP_BulkSpecialtyKPIData_Delete = "uspBulkSpecialtyKPIDataDelete";
        public static readonly string SP_KPIBulkWardTarget_Delete = "uspKPIBulkWardTargetDelete";
        public static readonly string SP_KPIBulkSpecialtyTarget_Delete = "uspKPIBulkSpecialtyTargetDelete";
        public static readonly string SP_BulkKPISpecialtyTarget_Delete = "uspBulkKPISpecialtyTargetDelete";
        public static readonly string SP_KPIBulkKPITarget_Delete = "uspKPIBulkKPITargetDelete";
        public static readonly string SP_KPIBulkKPIData_Delete             = "uspKPIBulkKPIDataDelete";
        public static readonly string SP_KPIWardMonthlyData_Update          = "uspKPIWardMonthlyDataUpdate";
        public static readonly string SP_Hospital_KPI_YTD_Target_Update     = "uspHospitalKPIYTDTargetUpdate";
        public static readonly string SP_Hospital_Level_KPI_Initial_Data    = "uspGetHospitalLevelKPIInitialData";
        public static readonly string SP_Hospital_Level_KPI_Search          = "uspHospitalLevelKPISearch";
        public static readonly string SP_Ward_Level_KPI_Initial_Data        = "uspGetWardLevelKPIInitialData";
        public static readonly string SP_Ward_Level_KPI_Search              = "uspWardLevelKPISearch";

        public static readonly string SP_Hospital_YTD_KPI_Data_View         = "uspHospitalYearToDateDataView";
        public static readonly string SP_KPIHospitalYTDData_Insert          = "uspHospitalYearToDateDataInsert";
        public static readonly string SP_KPIHospitlaYTDData_Update          = "uspHospitalYearToDateDataUpdate";

        public static readonly string SP_Password_Change                    = "uspChangePassword";
        public static readonly string SP_KPI_WardTarget_Level_Data          = "GetKPIForWardTargetLevel";
        public static readonly string SP_KPI_WardData_Level_Data            = "GetKPIForWardDataLevel";
        public static readonly string SP_Specialty_Level_Target_Initial_Data = "uspGetSpecialtyLevelTargetInitialData";
        public static readonly string SP_Specialty_KPI_Target_View = "uspKPISpecialtyMonthlyTargetView";
        public static readonly string SP_Specialty_KPI_Monthly_Target_Insert = "uspKPISpecialtyMonthlyTargetInsert";
        public static readonly string SP_Specialty_KPI_Monthly_Target_Update = "uspKPISpecialtyMonthlyTargetUpdate";
        public static readonly string SP_Specialty_KPI_Data_View = "uspKPISpecialtyMonthlyDataView";
        public static readonly string SP_Specialty_KPI_Monthly_Data_Insert = "uspKPISpecialtyMonthlyDataInsert";
        public static readonly string SP_Specialty_KPI_Monthly_Data_Update = "uspKPISpecialtyMonthlyDataUpdate";
        public static readonly string SP_Specialty_Level_KPI_Initial_Data = "uspGetSpecialtyLevelKPIInitialData";
        public static readonly string SP_Specialty_Level_KPI_Search = "uspSpecialtyLevelKPISearch";
        public static readonly string SP_Specialty_Level_YTD_Target_Initial_Data = "uspGetSpecialtyLevelYTDTargetInitialData";
        public static readonly string SP_Specialty_KPI_YTD_Target_View = "uspSpecialtyKPIYTDTargetView";
        public static readonly string SP_Specialty_KPI_YTD_Target_Insert = "uspSpecialtyKPIYTDTargetInsert";
        public static readonly string SP_Specialty_KPI_YTD_Target_Update = "uspSpecialtyKPIYTDTargetUpdate";
        public static readonly string SP_Specialty_YTD_KPI_Data_View = "uspSpecialtyYearToDateDataView";
        public static readonly string SP_Specialty_YTD_KPI_Data_Update = "uspSpecialtyYearToDateDataUpdate";
        public static readonly string SP_Specialty_YTD_KPI_Data_Insert = "uspSpecialtyYearToDateDataInsert";
        public static readonly string SP_Specialty_YTD_KPI_Data_Search = "uspSpecialtyLevelYTDKPISearch";
        public static readonly string SP_Comment_Insert = "uspCommentInsert";
        public static readonly string SP_Comment_Search = "uspCommentSearch";
        public static readonly string SP_Comment_Delete = "uspCommentDelete";
        public static readonly string SP_Comment_View = "uspCommentView";
        public static readonly string SP_Get_Comment_Users = "uspGetCommentUsers";

        public static readonly string SP_Ward_Level_BulkWard_Search = "uspWardLevelBulkWardSearch";
        public static readonly string SP_Specialty_Level_BulkSpecialty_Search = "uspSpecialtyLevelBulkSpecialtySearch";
        public static readonly string SP_Specialty_Level_BulkKPI_Search = "uspSpecialtyLevelBulkKPISearch";
        public static readonly string SP_Get_Dash_Board_Data = "uspDashBoardView";
        public static readonly string SP_Get_Dash_Board_Specialty_Data = "uspDashBoardSpecialtyView";

        public static readonly string SP_Set_Update_Department_Head = "uspDepartmentHeadInsertUpdate";

        public static readonly string SP_Insert_KPINews = "uspKPINewsInsert";
        public static readonly string SP_Update_KPINews = "uspKPINewsUpdate";
        public static readonly string SP_Insert_KPIHospitalNews = "uspKPIHospitalNewsInsert";
        public static readonly string SP_Update_KPIHospitalNews = "uspKPIHospitalNewsUpdate";
        public static readonly string SP_Search_KPINews = "uspKPINewsSearch";
        public static readonly string SP_Search_KPIHospitalNews = "uspKPIHospitalNewsSearch";
        #endregion

        #region Messages related Constant
        public static readonly string MSG_Hospital_Success_Add              = "Hospital details successfully added";
        public static readonly string MSG_Hospital_Success_Update           = "Hospital details successfully updated";
        public static readonly string MSG_WardGroup_Success_Add             = "Ward group details successfully added ";
        public static readonly string MSG_WardGroup_Success_Update          = "Ward group details successfully updated ";
        public static readonly string MSG_Ward_Success_Add                  = "Ward details successfully added";
        public static readonly string MSG_Ward_Success_Update               = "Ward details successfully updated";
        public static readonly string MSG_KpiGroup_Success_Add              = "KPI group details successfully added";
        public static readonly string MSG_KpiGroup_Success_Update           = "KPI group details successfully updated ";
         public static readonly string MSG_Ward_KPI_Target_Success_Add      = "Ward KPI Target successfully added";
         public static readonly string MSG_Ward_KPI_Target_Fail_Add         = "Could not add ward KPI target";
         public static readonly string MSG_Ward_KPI_Target_Already_Exist    = "Target has been already set for selected KPI";
         public static readonly string MSG_KPI_YTD_Target_Success_Add       = "KPI YTD target successfully added";
         public static readonly string MSG_Specialty_Success_Add            = "Successfully added specialty";
         public static readonly string MSG_Specialty_Success_Update         = "Successfully updated specialty";
         public static readonly string MSG_Ward_KPI_Target_Success_Update   = "Ward KPI target successfully updated";
     
         public static readonly string MSG_Ward_KPI_Data_Success_Add        = "KPI Data successfully added";
         public static readonly string MSG_Ward_KPI_Data_Already_Exist      = "Data has been already added for selected KPI";
         public static readonly string MSG_Ward_KPI_Data_Success_Update     = "KPI Data successfully updated";
         public static readonly string MSG_YTD_KPI_Target_Success_Update    = "YTD KPI Target successfully updated";
         public static readonly string MSG_YTD_KPI_Target_Already_Exist     = "Target has been already set for selected KPI";
         public static readonly string MSG_KPI_Success_Add                  = "KPI successfully added ";
         public static readonly string MSG_KPI_Success_Update               = "KPI successfully updated ";
         public static readonly string MSG_User_Success_Add                 = "User successfully added ";
         public static readonly string MSG_User_Success_Update              = "User successfully updated ";        

         public static readonly string MSG_User_Exist                       = "User Name is already in use";
         public static readonly string MSG_Hospital_Exist                   = "Hospital Name is already in use";
         public static readonly string MSG_KPI_Exist                        = "KPI No is already in use";
         public static readonly string MSG_KPIGroup_Exist                   = "KPI Group Name is already in use";
         public static readonly string MSG_KPIGroup_Empty                   = "KPI Group Name is empty";
         public static readonly string MSG_Specialty_Exist                  = "Specialty Name is already in use";
         public static readonly string MSG_Ward_Exist                       = "Ward Code is already in use";
         public static readonly string MSG_WardGroup_Exist                  = "Ward Group Name is already in use";

         public static readonly string MSG_KPIDetails_Success_Add           = "KPI Details successfully added ";
         public static readonly string MSG_KPIDetails_Success_Update        = "KPI Details successfully updated ";

         public static readonly string MSG_Password_Change_Success          = "Your Password has been changed ";         
         public static readonly string MSG_Password_Incorrect               = "Your current password was incorrect";

         public static readonly string MSG_Hospital_YTD_KPI_Data_Success_Add = "Hospital YTD Data successfully added";
         public static readonly string MSG_Hospital_YTD_KPI_Data_Already_Exist = "Data has been already added for selected KPI";
         public static readonly string MSG_Hospital_YTD_KPI_Data_Success_Update = "Hospital YTD Data successfully updated";

         public static readonly string MSG_Specialty_KPI_Target_Success_Add = "Specialty KPI Target successfully added";
         public static readonly string MSG_Specialty_KPI_Target_Success_Update = "Specialty KPI Target successfully updated";
         public static readonly string MSG_Specialty_KPI_Data_Success_Add = "Specialty KPI Data successfully added";
         public static readonly string MSG_Specialty_KPI_Data_Already_Exist = "Data has been already added for selected KPI";
         public static readonly string MSG_Specialty_KPI_Data_Success_Update = "Specialty KPI Data successfully updated";

         public static readonly string MSG_Specialty_KPI_YTD_Target_Success_Add = "Specialty KPI YTD target successfully added";
         public static readonly string MSG_Specialty_KPI_YTD_Target_Success_Update = "Specialty YTD KPI Target successfully updated";
         public static readonly string MSG_Specialty_YTD_KPI_Data_Success_Add = "Specialty YTD Data successfully added";
         public static readonly string MSG_Specialty_YTD_KPI_Data_Success_Update = "Specialty YTD Data successfully updated";
         public static readonly string MSG_Comment_Parameter_Invalid = "KPI No or User Name is invalid";

         public static readonly string MSG_Configuration_Success_Update = "Configurations Successfully updated";
         public static readonly string MSG_Dialog_Bullet_Points = "<span class='ui-icon ui-icon-circle-check' style='float:left; margin:0 7px 50px 0;'></span>";
         public static readonly string MSG_Hospital_Name_Exist = "This hospital has alreasy registerd with NHSKPI.<BR><BR>(1) If you are not the system administrator for HOSPITAL_NAME, please neter your email so we can send you who to contact in order to create you an account.<BR><BR>(2) If you the system administrator and forgotton your credentials please press \"Resend Credentilas\" button to receive the login details again.<BR><BR>(3) If you need to create a new account due any other reason please contact us via email and will be in touch with you within 24 hours of your request.";

         public static readonly string EmailFacilities = "EmailFacilities";
         public static readonly string Reminders = "Reminders";
         public static readonly string DownloadDataSets = "DownloadDataSets";
         public static readonly string BenchMarkingModule = "BenchMarkingModule";

        #endregion

        #region Page Redirect

         public static readonly string Page_Redirect_Hospital_Insert    = "~/Views/Hospital/HospitalUpdate.aspx";
         public static readonly string Page_Redirect_WardGroup_Insert   = "~/Views/Ward/WardGroupUpdate.aspx";
         public static readonly string Page_Redirect_Ward_Insert        = "~/Views/Ward/WardUpdate.aspx";
         public static readonly string Page_Redirect_KPIDetail_Insert   = "~/Views/KPI/KPIDetailUpdate.aspx";
         public static readonly string Page_Redirect_KPI_Insert         = "~/Views/KPI/KPIUpdate.aspx";
         public static readonly string Page_Redirect_Dashboard          = "~/Views/Dashboard/Dashboard.aspx";
         public static readonly string Page_Redirect_DashboardSpecialty = "~/Views/Dashboard/DashboardSpecialty.aspx";
         public static readonly string Page_Redirect_Specialty_Insert   = "~/Views/Specialty/SpecialtyUpdate.aspx";
        
        #endregion      

        #region Session Related

         public static readonly string SSN_Current_Hospital_Id = "CurrentHospitalId";
         public static readonly string SSN_Current_Targets = "CurrentTargets";
         public static readonly string SSN_Current_Datas = "CurrentDatas";
        #endregion
    }
}
