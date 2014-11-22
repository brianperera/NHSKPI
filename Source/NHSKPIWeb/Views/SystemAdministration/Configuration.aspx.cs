using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;
using NHSKPIDataService.Models;

public partial class Views_SystemAdministration_Configuration : System.Web.UI.Page
{
    #region Private Variable

    private UserController userController = null;
    private NHSKPIDataService.Models.Configuration configuration = null;
    private NHSKPIDataService.Models.HospitalConfigurations hospitalConfigurations = null;
    public User nhsUser = null;

    #endregion

    #region Public Variable

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

    public NHSKPIDataService.Models.Configuration Configuration
    {
        get 
        {
            if (configuration == null)
            {
                configuration = new NHSKPIDataService.Models.Configuration();
            }
            return configuration; 
        }
        set 
        { 
            configuration = value; 
        }
    }

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

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialData();
        }
    }

    #endregion

    #region Load Initial Data();

    private void LoadInitialData()
    {
        Configuration = UserController.ViewConfiguration();
        rdoModule.Items.FindByValue(Configuration.TargetApply).Selected = true;

        //Check if the hopital has configurations, otherwise add configurations
        //As per the requirement all the configurations will be enabled at start
        //Render the hospital configurations
        HospitalConfigurations.EmailFacilities = true;
        HospitalConfigurations.Reminders = true;
        HospitalConfigurations.DownloadDataSets = true;
        HospitalConfigurations.BenchMarkingModule = true;
        HospitalConfigurations.HospitalId = NHSUser.HospitalId;
        UserController.HospitalConfigurationsAdd(HospitalConfigurations);

        //Get the hospital information
        //Need to move this to the master page, so that the navigation menu items are validated.
        HospitalConfigurations = UserController.HospitalConfigurationsView(HospitalConfigurations);

        otherModules.Items.FindByValue(Constant.EmailFacilities).Selected = HospitalConfigurations.EmailFacilities;
        otherModules.Items.FindByValue(Constant.Reminders).Selected = HospitalConfigurations.Reminders;
        otherModules.Items.FindByValue(Constant.DownloadDataSets).Selected = HospitalConfigurations.DownloadDataSets;
        otherModules.Items.FindByValue(Constant.BenchMarkingModule).Selected = HospitalConfigurations.BenchMarkingModule;
    }

    #endregion

    #region Update Button Click

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Configuration.TargetApply = rdoModule.SelectedValue;
        UserController.UpdateConfiguration(Configuration);

        HospitalConfigurations.EmailFacilities = otherModules.Items.FindByValue(Constant.EmailFacilities).Selected;
        HospitalConfigurations.Reminders = otherModules.Items.FindByValue(Constant.Reminders).Selected;
        HospitalConfigurations.DownloadDataSets = otherModules.Items.FindByValue(Constant.DownloadDataSets).Selected;
        HospitalConfigurations.BenchMarkingModule = otherModules.Items.FindByValue(Constant.BenchMarkingModule).Selected;
        HospitalConfigurations.HospitalId = NHSUser.HospitalId;
        UserController.UpdateHospitalConfiguration(HospitalConfigurations);

        lblAddMessage.Text = Constant.MSG_Configuration_Success_Update;
        lblAddMessage.CssClass = "alert-success";
    }

    #endregion
}