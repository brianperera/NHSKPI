using NHSKPIBusinessControllers;
using NHSKPIDataService.Models;
using NHSKPIDataService.Services;
using NHSKPIDataService.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_Notification_NotificationConfig : System.Web.UI.Page
{
    private UtilController utilController = null;
    private User nhsUser = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadHospitalLevelEmailList();

        if (!IsPostBack)
        {
            LoadEmailConfig();
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

    public void LoadHospitalLevelEmailList()
    {
        //READ
        utilController = new UtilController();
        lbEmailList.DataTextField = "EmailAddress";
        lbEmailList.DataValueField = "Id";
        lbEmailList.DataSource = utilController.GetEmailList(NHSUser.HospitalId);
        lbEmailList.DataBind();
    }

    protected void btnAddEmail_Click(object sender, EventArgs e)
    {
        Email currentEmail = new Email();
        currentEmail.EmailAddress = txtEmail.Text;
        currentEmail.Id = 0;
        currentEmail.Description = txtEmail.Text;
        currentEmail.HospitalId = NHSUser.HospitalId;

        utilController = new UtilController();
        utilController.InsertEmailToBucket(currentEmail);

        LoadHospitalLevelEmailList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmailNotificationService service = new EmailNotificationService();

        int intVal;

        EmailNotification notification = new EmailNotification();

        notification.HospitalId = NHSUser.HospitalId;
        notification.ReminderEmail = txtReminderEmailAddress.Text;
        notification.EscalationEmail = txtEscalationEmailAddress.Text;

        if (int.TryParse(ddlReminder1.SelectedValue, out intVal))
            notification.Reminder1 = intVal;
        if (int.TryParse(ddlReminder2.SelectedValue, out intVal))
            notification.Reminder2 = intVal;
        if (int.TryParse(ddlEscalation.SelectedValue, out intVal))
            notification.ManagerEscalation = intVal;

        service.InsertEmailNotification(notification);
    }

    private void LoadEmailConfig()
    {
        EmailNotificationService service = new EmailNotificationService();
        EmailNotification notification = service.SearchEmailNotification(NHSUser.HospitalId);

        if (notification != null)
        {
            txtReminderEmailAddress.Text = notification.ReminderEmail;
            txtEscalationEmailAddress.Text = notification.EscalationEmail;
            ddlReminder1.SelectedValue = notification.Reminder1.ToString();
            ddlReminder2.SelectedValue = notification.Reminder2.ToString();
            ddlEscalation.SelectedValue = notification.ManagerEscalation.ToString();
        }
    }
}