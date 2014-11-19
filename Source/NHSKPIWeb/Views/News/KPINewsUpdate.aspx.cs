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
    public string KPINews = "KPI News";
    public string HospitalNews = "Hospital News";

    public int NewsArticleId {
        get
        {
            int newsArticleId = 0;

            if (Request.QueryString["Id"] != null)
            {
                int.TryParse(Request.QueryString["Id"], out newsArticleId);
            }

            return newsArticleId;
        }
    }

    public string NewsType
    {
        get
        {
            string newsType = string.Empty;

            if (Request.QueryString["Type"] != null)
            {
                newsType = Request.QueryString["Type"];
            }

            return newsType;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (NHSUser.RoleId == (int)Structures.Role.SuperUser)
            {
                ddlNewsType.Items.Add(KPINews);
                ddlNewsType.Items.Add(HospitalNews);
            }
            else
            {
                ddlNewsType.Items.Add(KPINews);
            }

            if (NewsArticleId > 0 && !string.IsNullOrEmpty(NewsType))
            {
                LoadNewsArticle(NewsArticleId, NewsType);

                btnUpdate.Visible = true;
            }
            else
            {
                btnSave.Visible = true;
            }
        }
    }

    private void LoadNewsArticle(int articleId, string type)
    {
        KPINews news = new KPINews();
        NewsService newsService = new NewsService();

        if (type == "KPI")
        {          
            news = newsService.SearchKPINews(news, articleId, null).SingleOrDefault();

            txtKPINewsTitle.Text = news.Title;
            txtDescription.Text = news.Description;
            chkIsActive.Checked = news.IsActive;
            ddlNewsType.SelectedValue = KPINews;

            //news = newsService.UpdateKPINews(news, articleId, true);
        }
        else if (type == "Hospital")
        {
            news = newsService.SearchKPIHospitalNews(new KPIHospitalNews(), articleId, NHSUser.HospitalId, null).SingleOrDefault();

            txtKPINewsTitle.Text = news.Title;
            txtDescription.Text = news.Description;
            chkIsActive.Checked = news.IsActive;
            ddlNewsType.SelectedValue = HospitalNews;
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

            if (newsService.InserKPINews(news))
            {
                lblAddMessage.Text = "News Not Added";
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddMessage.Text = "News Added";
                lblAddMessage.CssClass = "alert-success";
            }
        }
        else
        {
            KPIHospitalNews news = new KPIHospitalNews();

            news.HospitalId = NHSUser.HospitalId;
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = chkIsActive.Checked;

            if (newsService.InserKPIHospitalNews(news))
            {
                lblAddMessage.Text = "News Not Added";
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddMessage.Text = "News Added";
                lblAddMessage.CssClass = "alert-success";
            }
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
        NewsService newsService = new NewsService();

        if (NHSUser.RoleId == (int)Structures.Role.SuperUser && ddlNewsType.SelectedValue == "KPI News")
        {
            KPINews news = new KPINews();
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = chkIsActive.Checked;
            news.Id = NewsArticleId;

            if (newsService.UpdateKPINews(news))
            {
                lblAddMessage.Text = "News Not Updated";
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddMessage.Text = "News Updated";
                lblAddMessage.CssClass = "alert-success";
            }
        }
        else
        {
            KPIHospitalNews news = new KPIHospitalNews();

            news.HospitalId = NHSUser.HospitalId;
            news.Title = txtKPINewsTitle.Text;
            news.Description = txtDescription.Text;
            news.CreatedDate = DateTime.Today;
            news.IsActive = chkIsActive.Checked;
            news.Id = NewsArticleId;

            if (newsService.UpdateKPIHospitalNews(news))
            {
                lblAddMessage.Text = "News Not Updated";
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddMessage.Text = "News Updated";
                lblAddMessage.CssClass = "alert-success";
            }
        }
    }

    protected void ddlNewsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        targetDeadlineEntryPanel.Visible = string.Equals(HospitalNews, ddlNewsType.SelectedItem.Text) ? true : false;
    }
}