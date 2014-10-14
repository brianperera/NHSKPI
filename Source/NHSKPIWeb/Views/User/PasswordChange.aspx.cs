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

public partial class Views_User_ChangePassword : System.Web.UI.Page
{
    #region Private Members
    private UserController userController = null;
    private NHSKPIDataService.Models.User user = null;
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
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region Set User
    private void SetUser()
    {
        User.Id = NHSUser.Id;       
        User.Password = txtPassword.Text;       
    }
    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCurrentPassword.Text != NHSUser.Password)
            {
                lblAddMessage.Text = Constant.MSG_Password_Incorrect;
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                SetUser();
                if (UserController.ChangePassword(User))
                {
                    lblAddMessage.Text = Constant.MSG_Password_Change_Success;
                    lblAddMessage.CssClass = "alert-success";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


}
