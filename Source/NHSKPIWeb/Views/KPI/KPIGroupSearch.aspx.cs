using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;


public partial class Views_KPI_KPIGroupSearch : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;
    private NHSKPIDataService.Models.KPIGroup kpiGroup = null;

    #endregion

    #region Public Variable

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
    public NHSKPIDataService.Models.KPIGroup KPIGroup
    {
        get
        {
            if (kpiGroup == null)
            {
                kpiGroup = new NHSKPIDataService.Models.KPIGroup();
            }
            return kpiGroup;
        }
        set
        {
            kpiGroup = value;
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadKPIGroupResult();
        }
    }

    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadKPIGroupResult();
    }

    #endregion

    #region Load KPI Group Result

    private void LoadKPIGroupResult()
    {
        gvSearchResult.DataSource = KPIController.SearchKPIGroup(txtKPIGroupName.Text, chkIsActive.Checked).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion
}
