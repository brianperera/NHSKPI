using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;

public partial class Views_KPI_SpecialtyYTDTargetUpdate : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;
    private UtilController utilController = null;

    #endregion

    #region Properties

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

    public UtilController UtilController
    {
        get
        {
            if (utilController == null)
            {
                utilController = new UtilController();
            }
            return utilController;
        }
        set
        {
            utilController = value;
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialData();
            LoadSearchResult();
            
        }
    }

    #endregion

    #region Load Initial data

    private void LoadInitialData()
    {

        DataSet dsData = UtilController.GetSpecialtyLevelYTDTargetInitialData();
        ddlSpecialty.DataSource = dsData.Tables[0];
        ddlSpecialty.DataTextField = "Specialty";
        ddlSpecialty.DataValueField = "Id";
        ddlSpecialty.DataBind();
        ListItem item = new ListItem("", "0");
        ddlSpecialty.Items.Insert(0, item);

        ddlKPI.DataSource = dsData.Tables[1];
        ddlKPI.DataTextField = "KPIDescription";
        ddlKPI.DataValueField = "Id";
        ddlKPI.DataBind();
        ListItem KPIItem = new ListItem("", "0");
        ddlKPI.Items.Insert(0, KPIItem);

        string nextFinYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();

        lblCurentFinancialYear.Text = nextFinYear;
    }

    #endregion

    #region Search Button Click

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }

    #endregion

    #region Load Search Result

    private void LoadSearchResult()
    {
        DataSet dsData = KPIController.SpecialtyLevelYTDKPISearch(int.Parse(ddlSpecialty.SelectedValue), int.Parse(ddlKPI.SelectedValue), new DateTime(int.Parse(lblCurentFinancialYear.Text.Substring(0, 4)), 4, 1));
        gvSearchResult.DataSource = dsData.Tables[0];
        gvSearchResult.DataBind();

    }

    #endregion

    #region Previous Button Click Event

    protected void imgBtnPrevoius_Click(object sender, ImageClickEventArgs e)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');
        int nextfist = Convert.ToInt32(years[0]) - 1;
        int nextsecond = Convert.ToInt32(years[1]) - 1;

        string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

        lblCurentFinancialYear.Text = nextFinYear;
    }

    #endregion

    #region Next Button Click Event

    protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');
        int nextfist = Convert.ToInt32(years[0]) + 1;
        int nextsecond = Convert.ToInt32(years[1]) + 1;

        string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

        lblCurentFinancialYear.Text = nextFinYear;
    }

    #endregion

    #region Grid Search Result Data Bound

    protected void gvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(lblCurentFinancialYear.Text.Split('-')[0]) > DateTime.Now.Date.Year)
                {
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[5].Text = string.Empty;
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}
