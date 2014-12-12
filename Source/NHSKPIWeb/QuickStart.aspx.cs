using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;
using NHSKPIBusinessControllers;
using System.Configuration;
using System.Data;
using CSVReader;
using System.IO;

public partial class QuickStart : System.Web.UI.Page
{
    #region Fields

    private KPIController kPIController = null;
    private UserController userController = null;
    private WardController wardController = null;
    private SpecialtyController specialtyController = null;
    private string errorMessage = string.Empty;

    #endregion

    #region Properties

    public User nhsUser = null;
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
        UpdateUserList();

        if (!IsPostBack)
        {
            LoadUserInitialData();
            UpdateKPIGroupResult();
        }
    }

    protected void UserListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView drv = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string role = e.Row.Cells[3].Text;

            //TODO Hard coded switch statement needs to be removed
            switch (role)
            {
                case "1": role = "Super User";
                    break;
                case "2": role = "Admin";
                    break;
                case "3": role = "Data Entry Operator";
                    break;
                case "4": role = "View User";
                    break;
                default:
                    break;
            }

            e.Row.Cells[3].Text = role;
        } 
    }

    #endregion

    #region Events

    //Wizard 2nd step
    protected void btnAddKpiGroup_Click(object sender, EventArgs e)
    {
        KPIController kpiController = new KPIController();
        KPIGroup kpiGroup = new KPIGroup
        {
            HospitalID = NHSUser.HospitalId,
            IsActive = true,
            KpiGroupName = txtKpiGroupName.Text
        };

        if (NHSUser.HospitalId > 0 && !string.IsNullOrEmpty(kpiGroup.KpiGroupName))
        {
            if (kpiController.AddKPIGroup(kpiGroup) < 0)
            {
                lblAddKpiGroupMessage.Text = Constant.MSG_KPIGroup_Exist;
                lblAddKpiGroupMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddKpiGroupMessage.Text = Constant.MSG_KpiGroup_Success_Add;
                lblAddKpiGroupMessage.CssClass = "alert-success";
                UpdateKPIGroupResult();
                txtKpiGroupName.Text = string.Empty;
            }
        }
        else
        {
            lblAddKpiGroupMessage.Text = Constant.MSG_KPIGroup_Empty;
            lblAddKpiGroupMessage.CssClass = "alert-danger";
        }

    }

    protected void btnAddUpdateManagerDetails_Click(object sender, EventArgs e)
    {
        userController = new UserController();

        DepartmentHead departmentHead = new DepartmentHead
        {
            Id = 0,
            Name = txbManagerName.Text,
            MobileNo = txbManagerContactDetails.Text,
            JobTitle = txbManagerJobTitle.Text,
            Email = txbManagerEmail.Text,
            ApprovedUserId = NHSUser.Id
        };

        if (userController.InsertUpdateDepartmentHead(departmentHead))
        {
            lbAddUpdateManagerDetailsMessage.Text = "Department Head's details successfully added";
            lbAddUpdateManagerDetailsMessage.CssClass = "alert-success";

            // Send an email to dep head
            UtilController utilController = new UtilController();
            EmailMessage emailMessage = new NHSKPIDataService.Models.EmailMessage
            {
                EmailTo = txbManagerEmail.Text,
                Subject = "You've been added as a department head",
                Body = "This is a notification"
            };

            utilController.SendEmailNotification(emailMessage);
        }
        else
        {
            lbAddUpdateManagerDetailsMessage.Text = "Error occured, record not added";
            lbAddUpdateManagerDetailsMessage.CssClass = "alert-danger";            
        }
    }

    protected void btnAddUsers_Click(object sender, EventArgs e)
    {
        userController = new UserController();

        // Get hospitals user count
        DataSet currentUsers = userController.SearchUser(string.Empty, string.Empty, 0, NHSUser.HospitalId, true);

        if (currentUsers.Tables[0].Rows.Count > 4)
        {
            lbAddUserMessage.Text = "Hospital can only have 4 users";
            lbAddUserMessage.CssClass = "alert-danger";
            return;
        }

        int defaultUserRoleId = 4;

        int.TryParse(ddlRoleName.SelectedItem.Value, out defaultUserRoleId);

        if (defaultUserRoleId == 0)
        {
            lbAddUserMessage.Text = "User Role not selected";
            lbAddUserMessage.CssClass = "alert-danger";
            return;
        }

        User user = new NHSKPIDataService.Models.User
        {
             UserName = txbUsername.Text,
             Password = ConfigurationManager.AppSettings["DefaultUserPassword"].ToString(), 
             FirstName = txbFirstName.Text,
             LastName = txbLastName.Text,
             Email = txbEmail.Text,
             MobileNo = txbMobile.Text,
             LastLogDate = DateTime.UtcNow,
             RoleId = defaultUserRoleId,
             HospitalId = NHSUser.HospitalId,
             IsActive = true,
             CreatedDate = DateTime.UtcNow,
             CreatedBy = NHSUser.Id
        };

        if (userController.AddUser(user, true) > 0)
        {
            lbAddUserMessage.Text = "User Successfully added";
            lbAddUserMessage.CssClass = "alert-success";

            UpdateUserList();
        }
        else
        {
            lbAddUserMessage.Text = "Error occured, record not added";
            lbAddUserMessage.CssClass = "alert-danger";
        }
    }

    protected void btnRemoveKpiGroup_Click(object sender, EventArgs e)
    {
        kPIController = new KPIController();

        if (kPIController.RemoveKPIGroup(int.Parse(lbKpiGroups.SelectedValue)))
        {
            lblAddKpiGroupMessage.Text = Constant.MSG_KpiGroup_Success_Remove;
            lblAddKpiGroupMessage.CssClass = "alert-success";
            UpdateKPIGroupResult();          
        }
        else
        {
            lblAddKpiGroupMessage.Text = Constant.MSG_KpiGroup_Failure_Remove;
            lblAddKpiGroupMessage.CssClass = "alert-danger";
        }

        txtKpiGroupName.Text = string.Empty;
    }

    #endregion

    #region Private methods

    private void UpdateUserList()
    {
        userController = new UserController();
        UserListGridView.DataSource = userController.SearchUser(string.Empty, string.Empty, 0, NHSUser.HospitalId, true).Tables[0];
        UserListGridView.DataBind();
    }

    private void UpdateKPIGroupResult()
    {
        KPIController kpiController = new KPIController();
        lbKpiGroups.DataSource = kpiController.SearchKPIGroup(string.Empty, true).Tables[0];
        lbKpiGroups.DataTextField = "KPIGroupName";
        lbKpiGroups.DataValueField = "Id";
        lbKpiGroups.DataBind();
    }

    private void LoadUserInitialData()
    {
        userController = new UserController();
        DataSet dsUserInitialData = userController.LoadUserInitialData(NHSUser.RoleId, NHSUser.HospitalId);
        
        ddlRoleName.DataSource = dsUserInitialData.Tables[0];
        ddlRoleName.DataTextField = "RoleName";
        ddlRoleName.DataValueField = "Id";
        ddlRoleName.DataBind();

        ListItem li = new ListItem("--Select User Role--", string.Empty);
        ddlRoleName.Items.Insert(0, li);

    }

    #endregion    
}
