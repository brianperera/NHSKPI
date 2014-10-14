using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;

public partial class Views_KPI_WardLevelTargetBulkKPIUpdate : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;

    #endregion

    #region Properties
    /// <summary>
    /// Get or set the kpi controller
    /// </summary>
    public KPIController KPIController
    {
        get
        {
            if (kPIController == null)
            {
                kPIController = new KPIController();
            }
            return kPIController;
        }
        set
        {
            kPIController = value;
        }
    }

    #endregion

    #region Page Load
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["WardLevelKPIInitialData"] = null;
                LoadInitialData();
                LoadSearchResult();
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    #endregion

    #region Load Initial data
    /// <summary>
    /// Load the initail data
    /// </summary>
    private void LoadInitialData()
    {

        try
        {
            DataSet dsData = KPIController.GetWardLevelKPIInitialData(Master.NHSUser.HospitalId);
            Session["WardLevelKPIInitialData"] = dsData;

            if (Master.NHSUser.HospitalId != 0)
            {
                DataView dvWards = new DataView(dsData.Tables[1]);
                dvWards.RowFilter = string.Format("HospitalId = {0}", Master.NHSUser.HospitalId);

                ddlWard.DataSource = dvWards;
                ddlWard.DataTextField = "WardName";
                ddlWard.DataValueField = "Id";
                ddlWard.DataBind();
            }

            ListItem itemward = new ListItem("", "0");
            ddlWard.Items.Insert(0, itemward);

            string nextFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

            lblCurentFinancialYear.Text = nextFinYear;
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    #endregion

    #region Search Button Click
    /// <summary>
    /// Search button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            LoadSearchResult();
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    #endregion

    #region Load Search Result
    /// <summary>
    /// Load the search result
    /// </summary>
    private void LoadSearchResult()
    {
        try
        {
            DataSet dsData = KPIController.WardLevelBulkWardSearch(Master.NHSUser.HospitalId, int.Parse(ddlWard.SelectedValue), 0, new DateTime(int.Parse(lblCurentFinancialYear.Text.Substring(0, 4)), 4, 1), 1);
            gvSearchResult.DataSource = dsData.Tables[0];
            gvSearchResult.DataBind();
        }
        catch (Exception ex)
        {            
            throw ex;
        }

    }

    #endregion

    #region Previous Button Click Event
    /// <summary>
    /// Prevoius button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnPrevoius_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) - 1;
            int nextsecond = Convert.ToInt32(years[1]) - 1;

            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

            lblCurentFinancialYear.Text = nextFinYear;
        }
        catch (Exception ex)
        {            
            throw;
        }
    }

    #endregion

    #region Next Button Click Event
    /// <summary>
    /// Next Button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) + 1;
            int nextsecond = Convert.ToInt32(years[1]) + 1;

            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

            lblCurentFinancialYear.Text = nextFinYear;
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    #endregion   
}
