using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;
using NHSKPIBusinessControllers;
using System.Configuration;
using NHSKPIDataService.Services;

public partial class login : System.Web.UI.Page
{
    #region Private Variables

    private User nhsUser = null;
    private UserController userController = null;

    #endregion

    #region Properties

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

    public User NHSUser
    {
        get 
        {
            if (nhsUser == null)
            {
                nhsUser = new User();
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
        if (!IsPostBack)
        {
            lblMessage.Visible = false;
            LoadNews();
        }
    }

    private void LoadNews()
    {
        NewsService newsService = new NewsService();
        LstVwKPINews.DataSource = newsService.SearchKPINews(new KPINews(), -1, true);
        LstVwKPINews.DataBind();
    }

    #endregion

    #region Login Button Click Event

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        User user = LogIn();
        if (user != null)
        {
            Session["NHSUser"] = user;
            Session["NHSConfiguration"] = UserController.ViewConfiguration();
            NHSKPIDataService.Models.Configuration con = (NHSKPIDataService.Models.Configuration)Session["NHSConfiguration"];
            if (con.TargetApply == "1")
            {
                Response.Redirect(Constant.Page_Redirect_Dashboard, false);
            }
            else if (con.TargetApply == "2")
            {
                Response.Redirect(Constant.Page_Redirect_DashboardSpecialty, false);
            }
            else if (con.TargetApply == "3")
            {
                Response.Redirect(Constant.Page_Redirect_Dashboard, false);
            }
            
        }
        else
        {
            lblMessage.Text = "Invalid User credential.";
            lblMessage.Visible = true;
        }
    }

    #endregion    

    #region Log in 

    private User LogIn()
    {
        return UserController.UserLogin(txtUserName.Text, txtPassword.Text, null);        
    }

    #endregion
}
