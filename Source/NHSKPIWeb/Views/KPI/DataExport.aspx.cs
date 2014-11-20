using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;

public partial class Views_KPI_DataExport : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private NHSKPIDataService.Models.KPI kpi = null;
    private int kpiId;
    private User nhsUser = null;

    #endregion

    #region Public Properties

    public KPIController KPIController
    {
        get
        {
            if (kpiController == null)
            {
                kpiController = new KPIController();
            }

            return kpiController;
        }
        set
        {
            kpiController = value;
        }
    }
    public NHSKPIDataService.Models.KPI Kpi
    {
        get 
        {
            if (kpi == null)
            {
                kpi = new NHSKPIDataService.Models.KPI();
            }
            return kpi; 
        }
        set 
        { 
            kpi = value; 
        }
    }
    public int KpiId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                kpiId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                kpiId = 0;
            }
            return kpiId; 
        }
        set 
        { 
            kpiId = value; 
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

 

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    #endregion   
   
}
