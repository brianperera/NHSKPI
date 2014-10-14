using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService.Models;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;

public partial class Views_User_UserUpdate : System.Web.UI.Page
{
    #region Private Members
    private UserController userController = null;    
    private NHSKPIDataService.Models.User user = null;
    private int userId;
    private User nhsUser = null;
    #endregion

    #region Public Members
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
    public int UserId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                userId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                userId = 0;
            }
            return userId; 
        }
        set 
        { 
            userId = value; 
        }
    }   
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {      

            LoadUserInitialData();

            //if (NHSUser.RoleId == (int)Structures.Role.SuperUser)
            //{
            //    rfvHospitalName.Visible = false;
            //}

            if (Request.QueryString["Id"] != null)
            {
                this.user = UserController.ViewUser(int.Parse(Request.QueryString["Id"]));
                GetUser();
                btnUpdate.Visible = true;
                txtUserName.Enabled = true;
                HidePasswordField(false);
            }
            else
            {
                btnSave.Visible = true;
                HidePasswordField(true);

            }
        }
    }
    #endregion    

    #region Set User
    private void SetUser()
    {
        User.Id         = UserId;
        User.UserName   = txtUserName.Text;
        User.Password   = txtPassWord.Text;
        User.FirstName  = txtFirstName.Text;
        User.LastName   = txtLastName.Text;        
        User.Email      = txtEmail.Text;
        User.MobileNo   = txtMobileNo.Text;
        User.LastLogDate = DateTime.UtcNow;
        User.RoleId     = Convert.ToInt32(ddlRoleName.SelectedItem.Value.ToString());
        User.HospitalId = Convert.ToInt32(ddlHospitalName.SelectedItem.Value.ToString());
        User.IsActive   = chkIsActive.Checked;
        User.IsActiveDirectoryUser = chkIsActiveDirectoryUser.Checked;
        User.CreatedDate = DateTime.UtcNow;
        User.CreatedBy = NHSUser.Id;
    }
    #endregion

    #region Get User
    private void GetUser()
    {
        txtUserName.Text = User.UserName;
        txtPassWord.Text = User.Password;
        txtFirstName.Text = User.FirstName;
        txtLastName.Text = User.LastName;        
        txtEmail.Text = User.Email;
        txtMobileNo.Text = User.MobileNo;
        ddlRoleName.SelectedValue = User.RoleId.ToString();
        ddlHospitalName.SelectedValue = User.HospitalId.ToString();
        chkIsActive.Checked = User.IsActive;
        chkIsActiveDirectoryUser.Checked = User.IsActiveDirectoryUser;

        if (User.RoleId == (int)NHSKPIDataService.Util.Structures.Role.SuperUser)
        {

            rfvHospitalName.Visible = false;
        }
        else
        {
            rfvHospitalName.Visible = true;
        }

    }
    #endregion
        
    #region Load User Initial Data
    private void LoadUserInitialData()
    {
        ddlHospitalName.Enabled = true;
        DataSet dsUserInitialData = UserController.LoadUserInitialData(Master.NHSUser.RoleId,Master.NHSUser.HospitalId);
        ddlHospitalName.DataTextField = "Name";
        ddlHospitalName.DataValueField = "Id";
        ddlHospitalName.DataSource = dsUserInitialData.Tables[1];
        ddlHospitalName.DataBind();
        if (Master.NHSUser.HospitalId == 0)
        {
            ListItem itemHospital = new ListItem("--Select Hospital--", "0");
            ddlHospitalName.Items.Insert(0, itemHospital);
            
        }
        ddlHospitalName.Items.FindByValue(Master.NHSUser.HospitalId.ToString()).Selected = true;

        ddlRoleName.DataSource = dsUserInitialData.Tables[0];
        ddlRoleName.DataTextField = "RoleName";
        ddlRoleName.DataValueField = "Id";
        ddlRoleName.DataBind();

        ListItem li = new ListItem("--Select User Role--", string.Empty);
        ddlRoleName.Items.Insert(0, li);
        
    }
    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetUser();
        if (UserController.AddUser(User) < 0)
        {
            lblAddMessage.Text = Constant.MSG_User_Exist;
            lblAddMessage.CssClass = "alert-danger";
        }

        else 
        {
            lblAddMessage.Text = Constant.MSG_User_Success_Add;
            lblAddMessage.CssClass = "alert-success";
            ClearControls();
        }
    }
    #endregion

    #region Update Button Click
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SetUser();
            if (UserController.UpdateUser(User))
            {
                lblAddMessage.Text = Constant.MSG_User_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Hide Password Field

    private void HidePasswordField(bool visible)
    {
        divConfirmPasswordClear.Visible = visible;
        divConfirmPasswordText.Visible = visible;
        divConfirmPasswordLabel.Visible = visible;

        divPasswordClear.Visible = visible;
        divPasswordText.Visible = visible;
        divPasswordLabel.Visible = visible;
    }

    #endregion

    #region Clear Controls
    private void ClearControls()
    {
        txtUserName.Text    = string.Empty;
        txtFirstName.Text   = string.Empty;
        txtLastName.Text    = string.Empty;
        txtMobileNo.Text    = string.Empty;
        txtEmail.Text       = string.Empty;        
        ddlRoleName.SelectedIndex = 0;
        ddlHospitalName.SelectedIndex = 0;
    }
    #endregion

    
   
}
