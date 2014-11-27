using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIDataService.Models;
using NHSKPIBusinessControllers;
using NHSKPIDataService.Util;

public partial class Views_Shared_MasterPage : System.Web.UI.MasterPage
{
    #region Private Variables

    private User nhsUser = null;
    private UserController userController = null;
    private Configuration nhsConfiguration = null;
    private NHSKPIDataService.Models.HospitalConfigurations hospitalConfigurations = null;
    
    #endregion

    #region Properties

    public NHSKPIDataService.Models.HospitalConfigurations HospitalConfigurations
    {
        get
        {
            if (hospitalConfigurations == null)
            {
                hospitalConfigurations = new NHSKPIDataService.Models.HospitalConfigurations();
            }
            return hospitalConfigurations;
        }
        set
        {
            hospitalConfigurations = value;
        }
    }

    public User NHSUser
    {
        get 
        {
            if (Session["NHSUser"] != null)
            {
                nhsUser = (User)Session["NHSUser"];
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

            return nhsUser;
        }
        set 
        { 
            nhsUser = value; 
        }
    }

    public Configuration NHSConfiguration
    {
        get
        {
            if (Session["NHSConfiguration"] != null)
            {
                nhsConfiguration = (Configuration)Session["NHSConfiguration"];
            }

            return nhsConfiguration;
        }
        set
        {
            nhsConfiguration = value;
        }
    }

    public UserController UserController
    {
        get
        {
            if (userController == null)
            {
                userController = new UserController();
            }
            return userController;
        }
        set
        {
            userController = value;
        }
    }

    #endregion    

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        SetMenuPermission();

        if (!IsPostBack)
        {
            lblNHSUser.Text = NHSUser.FirstName + " " + NHSUser.LastName;
            if(NHSUser.HospitalId != 0)
            {
            lblHospitalName.Text = NHSUser.HospitalName;
            lblHospitalType.Text = NHSUser.HospitalType;
            }
            SetDashboard();
        }

        
    }

    private void RunNavigationbarRules()
    {
        if (NHSUser.HospitalId > 0)
        {
            HospitalConfigurations.HospitalId = NHSUser.HospitalId;
            HospitalConfigurations = UserController.HospitalConfigurationsView(HospitalConfigurations);
        }
    }

    #endregion

    #region Remove Session

    public void RemoveSession()
    {

    }

    #endregion

    #region Log out click event
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["NHSUser"] = null;
        Session["NHSConfiguration"] = null;
        Response.Redirect("~/login.aspx");
    }
    #endregion

    #region Set Menu Permission

    private void SetMenuPermission()
    {
        //Run the navigation bar rules
        RunNavigationbarRules();

        if (NHSUser.RoleId == (int)Structures.Role.SuperUser)
        { 
            lnkHospital.Visible = true;
            lnkWard.Visible = true;
            lnkManageKPI.Visible = true;
            lnkKPITarget.Visible = true;
            lnkManualData.Visible = true;
            lnkReports.Visible = true;
            lnkAdmin.Visible = true;
            lnkConfiguration.Visible = true;
            lnkBenchMarkReports.Visible = true;
            lnkBenchMarkDataUpload.Visible = true;

        }
        else if (NHSUser.RoleId == (int)Structures.Role.Admin)
        {
            lnkManageKPI.Visible = true;
            lnkKPITarget.Visible = true;
            lnkManualData.Visible = true;
            lnkReports.Visible = true;
            lnkAdmin.Visible = true;
            lnkBenchMarkReports.Visible = true;
            //lnkDataExport.Visible = true;
            //lnkNotificationConfig.Visible = true;
        }
        else if (NHSUser.RoleId == (int)Structures.Role.DataEntryOperator)
        {
            lnkManualData.Visible = true;   
        }

        //lnkDataExport.Visible = HospitalConfigurations.DownloadDataSets;
        //lnkNotificationConfig.Visible = HospitalConfigurations.EmailFacilities;
        //lnkNotificationConfig.Visible = HospitalConfigurations.Reminders;
        lnkBenchMarkDataUpload.Visible = HospitalConfigurations.BenchMarkingModule;

        if (HospitalConfigurations.DownloadDataSets == false)
            HrefDataExportLink = "disabled";
        else
            HrefDataExportLink = string.Empty;

        if (HospitalConfigurations.EmailFacilities == false && HospitalConfigurations.Reminders == false)
            HrefNotificationConfigLink = "disabled";
        else
            HrefNotificationConfigLink = string.Empty;

        if (HospitalConfigurations.BenchMarkingModule == false)
            HrefBenchmarkReportDisable = "disabled";
        else
            HrefBenchmarkReportDisable = string.Empty;
        

        SetConfiguration();
    }

    public string HrefDataExportLink { get; set; }
    public string HrefNotificationConfigLink { get; set; }
    public string HrefBenchmarkReportDisable { get; set; }

    #endregion

    #region Set Configuration

    private void SetConfiguration()
    {
        if (NHSUser.RoleId != (int)Structures.Role.SuperUser)
        {
            if (NHSConfiguration.TargetApply == "1")
            {
                lnkWardLevelTarget.Visible = true;
                lnkWardLevelData.Visible = true;
                lnkWard.Visible = true;
            }
            else if (NHSConfiguration.TargetApply == "2")
            {
                lnkSpecialtyLevelTarget.Visible = true;
                lnkSpecialtyLevelData.Visible = true;
                lnkSpecialty.Visible = true;
            }
            else if (NHSConfiguration.TargetApply == "3")
            {
                lnkWardLevelTarget.Visible = true;
                lnkWardLevelData.Visible = true;
                lnkWard.Visible = true;

                lnkSpecialtyLevelTarget.Visible = true;
                lnkSpecialtyLevelData.Visible = true;
                lnkSpecialty.Visible = true;
            }
        }
        else
        {
            lnkWardLevelTarget.Visible = true;
            lnkWardLevelData.Visible = true;
            lnkWard.Visible = true;

            lnkSpecialtyLevelTarget.Visible = true;
            lnkSpecialtyLevelData.Visible = true;
            lnkSpecialty.Visible = true;
        }

        if (NHSUser.RoleId != (int)Structures.Role.SuperUser || NHSUser.RoleId != (int)Structures.Role.Admin)
        {
            lnkNews.Visible = true;
        }
    }

    #endregion

    #region Set Dashboard

    private void SetDashboard()
    {
        if (NHSConfiguration.TargetApply == "1")
        {
            hrefDashboard.HRef = "../Dashboard/Dashboard.aspx";
        }
        else if (NHSConfiguration.TargetApply == "2")
        {
            hrefDashboard.HRef = "../Dashboard/DashboardSpecialty.aspx";
        }
        else if (NHSConfiguration.TargetApply == "3")
        {
            hrefDashboard.HRef = "../Dashboard/Dashboard.aspx";
        }
        
    }

    #endregion
}
