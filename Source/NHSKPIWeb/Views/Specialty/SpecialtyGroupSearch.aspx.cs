using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;

public partial class Views_Specialty_SpecialtyGroupSearch : System.Web.UI.Page
{
    #region Private Variable

    private WardGroupController wardGroupController = null;
    private NHSKPIDataService.Models.WardGroup wardGroup = null;
    
    #endregion

    #region Public Variable

    public WardGroupController WardGroupController
    {
        get
        {
            if (wardGroupController == null)
            {
                wardGroupController = new WardGroupController();
            }

            return wardGroupController;
        }
        set
        {
            wardGroupController = value;
        }
    }
    public NHSKPIDataService.Models.WardGroup WardGroup
    {
        get
        {
            if (wardGroup == null)
            {
                wardGroup = new NHSKPIDataService.Models.WardGroup();
            }
            return wardGroup;

        }
        set
        {
            wardGroup = value;
        }
    }
    
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillHospitalList();
            LoadSearchResult();
        }
    }

    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }

    #endregion

    #region List Hospital
    /// <summary>
    /// Get Hospital Name List
    /// </summary>
    private void FillHospitalList()
    {
        ddlHospitalName.DataSource = WardGroupController.LoadHospitals(Master.NHSUser.HospitalId).Tables[0];
        ddlHospitalName.DataTextField = "Name";
        ddlHospitalName.DataValueField = "Id";
        ddlHospitalName.DataBind();
        ddlHospitalName.Items.FindByValue(Master.NHSUser.HospitalId.ToString()).Selected = true;

    }
    #endregion

    #region Load Search Result

    private void LoadSearchResult()
    {
        gvSearchResult.DataSource = WardGroupController.SearchWardGroup(txtWardGroupName.Text, int.Parse(ddlHospitalName.SelectedValue), chkIsActive.Checked).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion
}
