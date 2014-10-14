using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;

public partial class Views_Ward_WardSearch : System.Web.UI.Page
{
    #region Private Variable

    private WardController wardController = null;
    private NHSKPIDataService.Models.Ward ward = null;    

    #endregion

    #region Public Variable

    public WardController WardController
    {
        get
        {
            if (wardController == null)
            {
                wardController = new WardController();
            }

            return wardController;
        }
        set
        {
            wardController = value;
        }
    }
    public NHSKPIDataService.Models.Ward Ward
    {
        get
        {
            if (ward == null)
            {
                ward = new NHSKPIDataService.Models.Ward();
            }
            return ward;

        }
        set
        {
            ward = value;
        }
    }
    
    #endregion 

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadWardInitialData();
            LoadSearchResult();
        }
    }

    #endregion

    #region Load Ward Initial Data

    private void LoadWardInitialData()
    {
        DataSet dsWardInitialData = WardController.LoadWardInitialData(Master.NHSUser.HospitalId);

        ddlHospitalName.DataTextField = "Name";
        ddlHospitalName.DataValueField = "Id";
        ddlHospitalName.DataSource = dsWardInitialData.Tables[0];
        ddlHospitalName.DataBind();
        ddlHospitalName.Items.FindByValue(Master.NHSUser.HospitalId.ToString()).Selected = true;
        
        ddlWardGroupName.DataTextField = "WardGroupName";
        ddlWardGroupName.DataValueField = "Id";
        ddlWardGroupName.DataSource = dsWardInitialData.Tables[1];
        ddlWardGroupName.DataBind();
        ListItem itemWardGroup = new ListItem("", "0");
        ddlWardGroupName.Items.Insert(0, itemWardGroup);      
    }

    #endregion

    #region Load Search Result

    private void LoadSearchResult()
    {
        gvSearchResult.DataSource = WardController.SearchWard(txtWardName.Text, txtWardCode.Text, int.Parse(ddlHospitalName.SelectedValue), int.Parse(ddlWardGroupName.SelectedValue), chkIsActive.Checked).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }

    #endregion
}
