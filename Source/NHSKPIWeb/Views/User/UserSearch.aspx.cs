using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;


public partial class Views_User_UserSearch : System.Web.UI.Page
{
    #region Private Variable

    private UserController userController = null;
    private NHSKPIDataService.Models.User user = null;

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
    public NHSKPIDataService.Models.User User
    {
        get
        {
            if (user == null)
            {
                user = new NHSKPIDataService.Models.User();
            }
            return user;
        }
        set
        {
            user = value;
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUserInitialData();
            LoadUserResult();
        }
    }

    #endregion

    #region Load User Initial Data
    private void LoadUserInitialData()
    {
        ddlHospitalName.Enabled = true;
        DataSet dsUserInitialData = UserController.LoadUserInitialData(Master.NHSUser.RoleId, Master.NHSUser.HospitalId);
        ddlHospitalName.DataTextField = "Name";
        ddlHospitalName.DataValueField = "Id";
        ddlHospitalName.DataSource = dsUserInitialData.Tables[1];
        ddlHospitalName.DataBind();
        ddlHospitalName.Items.FindByValue(Master.NHSUser.HospitalId.ToString()).Selected = true;

        ddlRoleName.DataSource = dsUserInitialData.Tables[0];
        ddlRoleName.DataTextField = "RoleName";
        ddlRoleName.DataValueField = "Id";
        ddlRoleName.DataBind();

        ListItem li = new ListItem("--Select User Role--", "0");
        ddlRoleName.Items.Insert(0, li);

    }
    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadUserResult();
    }

    #endregion

    #region Load User Result

    private void LoadUserResult()
    {
        gvSearchResult.DataSource = UserController.SearchUser(txtUserName.Text,txtEmail.Text ,int.Parse(ddlRoleName.SelectedValue),int.Parse(ddlHospitalName.SelectedValue),chkIsActive.Checked).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion
}
