using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;
using NHSKPIBusinessControllers;
using System.Configuration;
using System.Net.Mail;


public partial class login : System.Web.UI.Page
{
    #region Private Variables

    private User nhsUser = null;
    private UserController userController = null;

    private HospitalController hospitalController = null;
    private NHSKPIDataService.Models.Hospital hospital = null;

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

    public HospitalController HospitalController
    {
        get
        {
            if (hospitalController == null)
            {
                hospitalController = new HospitalController();
            }

            return hospitalController;
        }
        set
        {
            hospitalController = value;
        }
    }
    public NHSKPIDataService.Models.Hospital Hospital
    {
        get
        {
            if (hospital == null)
            {
                hospital = new NHSKPIDataService.Models.Hospital();
            }
            return hospital;
        }
        set
        {
            hospital = value;
        }
    }

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    #endregion

    #region Set Hospital
    private void SetHospital()
    {
        Hospital.HospitalName = txtCompanyName.Text;
        Hospital.HospitalCode = string.Empty;
        Hospital.PhoneNumber = txtPhoneName.Text;
        Hospital.HospitalType = "NHS Trust";
        Hospital.Address = string.Empty;
        Hospital.IsActive = true;
    }
    #endregion

    #region Set User
    private void SetUser(int hospitalId)
    {
        
        NHSUser.UserName = txtUserName.Text;
        NHSUser.Password = txtPassword.Text;
        NHSUser.FirstName = txtName.Text;
        NHSUser.LastName = string.Empty;
        NHSUser.Email = txtEmailAddress.Text;
        NHSUser.MobileNo = txtPhoneName.Text;
        NHSUser.LastLogDate = DateTime.UtcNow;
        NHSUser.RoleId = (int)NHSKPIDataService.Util.Structures.Role.Admin;
        NHSUser.HospitalId = hospitalId;
        NHSUser.IsActive = true;
        NHSUser.IsActiveDirectoryUser = true;
        NHSUser.CreatedDate = DateTime.UtcNow;
        NHSUser.CreatedBy = 1;
    }
    #endregion

    #region Send Email Notification

    private void SendEmailNotification()
    {
        // Command line argument must the the SMTP host.
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.Port = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["Port"].ToString());
        client.Host = System.Web.Configuration.WebConfigurationManager.AppSettings["Host"].ToString();
        client.EnableSsl = true;
        client.Timeout = 10000;
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(System.Web.Configuration.WebConfigurationManager.AppSettings["NetworkCredentialUserName"].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings["NetworkCredentialPassword"].ToString());



        MailMessage mm = new MailMessage(System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFrom"].ToString(), txtEmailAddress.Text, "KPI Portal for FREE Trail has been approved.", "Your KPI Portal free trail has been approved. You can use this trail version for 60 days." + "<br><br>URL:" + System.Web.Configuration.WebConfigurationManager.AppSettings["SiteURL"].ToString() + "<br><br>User Name:" + txtUserName.Text + "<br><br>Password:"+ txtPassword.Text + "<br><br>Note: This is a auto generated message. Please do not reply to this email.");
        mm.IsBodyHtml = true;

        mm.BodyEncoding = System.Text.UTF8Encoding.UTF8;
        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        client.Send(mm);
    }

    #endregion

    protected void btnTrial_Click(object sender, EventArgs e)
    {
        SetHospital();
        int id = HospitalController.AddHospital(Hospital);
        Hospital.Id = id;
        Hospital.HospitalCode = id.ToString("0000");
        HospitalController.UpdateHospital(Hospital);

        SetUser(id);
        if (UserController.AddUser(NHSUser) < 0)
        {
            lblAddMessage.Text = Constant.MSG_User_Exist;
            lblAddMessage.CssClass = "alert-danger";
        }
        else
        {
            SendEmailNotification();
            Response.Redirect("login.aspx",false);
        }
    }
}
