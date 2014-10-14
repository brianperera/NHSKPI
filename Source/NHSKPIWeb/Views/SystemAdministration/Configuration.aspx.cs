using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;

public partial class Views_SystemAdministration_Configuration : System.Web.UI.Page
{
    #region Private Variable

    private UserController userController = null;
    private NHSKPIDataService.Models.Configuration configuration = null;    

    #endregion

    #region Public Variable

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
    }

    #endregion

    #region Update Button Click

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Configuration.TargetApply = rdoModule.SelectedValue;
        UserController.UpdateConfiguration(Configuration);
        lblAddMessage.Text = Constant.MSG_Configuration_Success_Update;
        lblAddMessage.CssClass = "alert-success";
    }

    #endregion
}