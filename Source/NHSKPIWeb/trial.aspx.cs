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
using System.Data;
using System.Text;


public partial class login : System.Web.UI.Page
{
    #region Private Variables

    private User nhsUser = null;
    private UserController userController = null;

    private HospitalController hospitalController = null;
    private NHSKPIDataService.Models.Hospital hospital = null;
    private UtilController utilController = null;

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

    public DataView AllHospitals
    {
        get
        {
            return HospitalController.GetAllHospitals();
        }
    }

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.PopulateHospitalList();
        }
    }
    #endregion

    #region Set Hospital
    private void SetHospital()
    {
        if (ddlHospitalName.SelectedHospitalCode != "Other")
        {
            Hospital.HospitalName = ddlHospitalName.SelectedHospitalName;
            Hospital.HospitalCode = ddlHospitalName.SelectedHospitalCode;
            Hospital.Id = HospitalController.GetHospitalIdFromCode(Hospital);
        }
        else
        {
            Hospital.HospitalName = ddlHospitalName.SelectedHospitalName;
            Hospital.HospitalCode = string.Empty;
        }

        Hospital.PhoneNumber = txtPhoneName.Text;
        Hospital.HospitalType = "NHS Trust";
        Hospital.Address = string.Empty;
        Hospital.IsActive = true;
    }

    //private void PopulateHospitalList()
    //{
    //    ddlHospitalName.DataSource = AllHospitals;
    //    ddlHospitalName.DataValueField = "Code";
    //    ddlHospitalName.DataTextField = "Name";
    //    ddlHospitalName.DataBind();

    //    //Append the Other value to the datasource, this will allow the user to manualy key-in the hospital name
    //    ddlHospitalName.Items.Insert(ddlHospitalName.Items.Count, new ListItem("Other"));
    //}
    #endregion

    #region Set User
    private void SetUser(int hospitalId)
    {
        
        NHSUser.UserName = string.Empty;
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

    protected void btnTrial_Click(object sender, EventArgs e)
    {
        if (!cbTCAgreement.Checked)
        {
            string msg = "Please agree to the terms and conditions";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
            return;
        }

        SetHospital();
        //int id = HospitalController.AddHospital(Hospital);

        //if (id == -1)
        //{
        //    string msg = "Error! Hospital not added";
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
        //    return;
        //}

        //Hospital.Id = id;
        //Hospital.HospitalCode = id.ToString("0000");
        HospitalController.UpdateHospital(Hospital);

        SetUser(Hospital.Id);
        if (UserController.AddUser(NHSUser) < 0)
        {
            string msg = Constant.MSG_Hospital_Name_Exist.Replace("HOSPITAL_NAME", Hospital.HospitalName);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
        }
        else
        {
            utilController = new UtilController();

            EmailMessage emailMessage = new NHSKPIDataService.Models.EmailMessage
            {
                EmailTo = txtEmailAddress.Text,
                Subject = "KPI Portal for FREE Trail has been approved.",
                Body = "Your KPI Portal free trail has been approved. You can use this trail version for 60 days." + "<br><br>URL:" + ConfigurationManager.AppSettings["SiteURL"].ToString() + "<br><br>User Name:" + txtEmailAddress.Text + "<br><br>Password:" + txtPassword.Text + "<br><br>Note: This is a auto generated message. Please do not reply to this email."
            };

            utilController.SendEmailNotification(emailMessage);

            //Login the new user
            User user = UserController.UserLogin(txtEmailAddress.Text, txtPassword.Text, Hospital.HospitalCode);
            if (user != null)
            {
                Session["NHSUser"] = user;
                Session["NHSConfiguration"] = UserController.ViewConfiguration();
                Response.Redirect("QuickStart.aspx", false);
            }
            else
            {
                string msg = "User login failed";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + msg + "');", true);
            }
        }
    }
}
