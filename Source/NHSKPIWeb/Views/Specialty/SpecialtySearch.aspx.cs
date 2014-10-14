using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;


public partial class Views_Specialty_SpecialtySearch : System.Web.UI.Page
{
    #region Private Variable

    private SpecialtyController specialtyController = null;
    private NHSKPIDataService.Models.Specialty specialty = null;
    
    #endregion

    #region Public Variable

    public SpecialtyController SpecialtyController
    {
        get
        {
            if (specialtyController == null)
            {
                specialtyController = new SpecialtyController();
            }
            return specialtyController;
        }
        set
        {
            specialtyController = value;
        }
    }
    public NHSKPIDataService.Models.Specialty Specialty
    {
        get
        {
            if (specialty == null)
            {
                specialty = new NHSKPIDataService.Models.Specialty();
            }
            return specialty;
        }
        set
        {
            specialty = value;
        }
    }
    
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSpecialtyResult();
        }
    }

    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSpecialtyResult();
    }

    #endregion

    #region Load Specialty Result

    private void LoadSpecialtyResult()
    {
        gvSearchResult.DataSource = SpecialtyController.SearchSpecialty(txtSpecialty.Text,chkIsActive.Checked).Tables[0];
        gvSearchResult.DataBind();
    }

    #endregion
}
