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

public partial class Views_KPI_SpecialtyLevelTargetUpdate : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;

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

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SpecialtyLevelKPIInitialData"] = null;
            LoadInitialData();
            LoadSearchResult();
        }
    }

    #endregion

    #region Load Initial data

    private void LoadInitialData()
    {

        DataSet dsData = KPIController.GetSpecialtyLevelKPIInitialData(Master.NHSUser.HospitalId);
        Session["SpecialtyLevelKPIInitialData"] = dsData;
        

        ddlKPI.DataSource = dsData.Tables[2];
        ddlKPI.DataTextField = "KPIDescription";
        ddlKPI.DataValueField = "Id";
        ddlKPI.DataBind();
        ListItem KPIItem = new ListItem("", "0");
        ddlKPI.Items.Insert(0, KPIItem);

        if (Master.NHSUser.HospitalId != 0)
        {
            DataView dvSpecialty = new DataView(dsData.Tables[1]);


            ddlSpecialty.DataSource = dvSpecialty;
            ddlSpecialty.DataTextField = "Specialty";
            ddlSpecialty.DataValueField = "Id";
            ddlSpecialty.DataBind();
        }
        ListItem itemSpecialty = new ListItem("", "0");
        ddlSpecialty.Items.Insert(0, itemSpecialty);



        string nextFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

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
        DataSet dsData = KPIController.SpecialtyLevelKPISearch(Master.NHSUser.HospitalId, int.Parse(ddlSpecialty.SelectedValue), int.Parse(ddlKPI.SelectedValue), new DateTime(int.Parse(lblCurentFinancialYear.Text.Substring(0, 4)), 4, 1));
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

    #region Search Result Row Data Bound

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
                if (gvSearchResult.DataKeys[e.Row.RowIndex]["ManuallyEntered"].ToString().ToLower() == "false")
                {
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[4].Text = string.Empty;
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
