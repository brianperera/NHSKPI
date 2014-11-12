using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.IO;
using NHSKPIDataService.Services;
using NHSKPIDataService.Models;

public partial class Views_News_KPINewsSearch : System.Web.UI.Page
{
    #region Private Variables

    private User nhsUser;

    #endregion

    #region Public Properties

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
        LoadNews();
    }

    private void LoadNews()
    {
        NewsService newsService = new NewsService();
        List<KPINews> hospitalNews = new List<KPINews>();
        
        List<KPIHospitalNews> activeHospitalNews = newsService.SearchKPIHospitalNews(new KPIHospitalNews(), -1, NHSUser.HospitalId, null);

        if (activeHospitalNews != null)
        {
            hospitalNews.AddRange(activeHospitalNews);
        }

        List<KPINews> activeKpiNews = newsService.SearchKPINews(new KPINews(), -1, null);

        if (activeKpiNews != null)
        {
            hospitalNews.AddRange(activeKpiNews);
        }

        gvSearchResult.DataSource = hospitalNews;
        gvSearchResult.DataBind();
    }

    #endregion
}
