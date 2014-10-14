using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.IO;

public partial class Views_Hospital_HospitalSearch : System.Web.UI.Page
{
    #region Private Variables

    private HospitalController hospitalController = null;
    private NHSKPIDataService.Models.Hospital hospital = null;
    
    #endregion

    #region Public Properties

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

    public NHSKPIDataService.Models.Hospital Hospital
    {
        get
        {
            if (hospital == null)
            {
                hospital = new NHSKPIDataService.Models.Hospital();
            }
            return hospital;
        }
        set
        {
            hospital = value;
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    #region Load Search Result

    private void LoadSearchResult()
    {
        gvSearchResult.DataSource = HospitalController.SearchHospital(txtHospitalName.Text, txtHospitalCode.Text, chkIsActive.Checked,0).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion
}
