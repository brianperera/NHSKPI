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
            if (Request.QueryString["Id"] != null)
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnSave.Visible = true;
            }
        }

        if (NHSUser.RoleId == (int)Structures.Role.SuperUser)
        {
            ddlNewsType.Items.Add("KPI News");
            ddlNewsType.Items.Add("Hospital News");
        }
        else
        {
            ddlNewsType.Items.Add("KPI News");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        NewsService newsService = new NewsService();

        if (NHSUser.RoleId == (int)Structures.Role.SuperUser && ddlNewsType.SelectedValue == "KPI News")
        {
            KPINews news = new KPINews();
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = chkIsActive.Checked;

            newsService.InserKPINews(news);
        }
        else
        {
            KPIHospitalNews news = new KPIHospitalNews();

            news.HospitalId = NHSUser.HospitalId;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
}