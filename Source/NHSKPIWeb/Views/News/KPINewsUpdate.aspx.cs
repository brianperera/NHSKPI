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

public partial class Views_News_KPINewsUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateHospitalList();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        NewsService newsService = new NewsService();

        if (NHSUser.RoleId == (int)Structures.Role.SuperUser && ddlHospital.SelectedValue == "*")
        {
            KPINews news = new KPINews();
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = true;

            newsService.InserKPINews(news);
        }
        else
        {
            KPIHospitalNews news = new KPIHospitalNews();

            int hospitalID;
            int.TryParse(ddlHospital.SelectedValue, out hospitalID);

            news.HospitalId = hospitalID;
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = true;

            newsService.InserKPIHospitalNews(news);
        }
    }

    HospitalController hospitalController;
    User nhsUser;

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

    public DataView AllHospitals
    {
        get
        {
            return HospitalController.GetAllHospitals();
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

    private void PopulateHospitalList()
    {
        if (NHSUser.RoleId == (int)Structures.Role.SuperUser)
        {
            ddlHospital.DataSource = AllHospitals;
            ddlHospital.DataValueField = "Code";
            ddlHospital.DataTextField = "Name";
            ddlHospital.DataBind();

            ddlHospital.Items.Insert(0, new ListItem("All", "*"));
        }
        else
        {
            ddlHospital.Items.Insert(0, new ListItem(NHSUser.HospitalName, NHSUser.HospitalId.ToString()));
        }
    }
}