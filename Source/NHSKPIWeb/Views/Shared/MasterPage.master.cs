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
    
    #endregion

    #region Properties

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
        if (!IsPostBack)
        {
            SetMenuPermission();
            lblNHSUser.Text = NHSUser.FirstName + " " + NHSUser.LastName;
            if(NHSUser.HospitalId != 0)
            {
            lblHospitalName.Text = NHSUser.HospitalName;
            lblHospitalType.Text = NHSUser.HospitalType;
            }
            SetDashboard();
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
        }
        else if (NHSUser.RoleId == (int)Structures.Role.DataEntryOperator)
        {
            lnkManualData.Visible = true;
            
        }

        SetConfiguration();
    }

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
