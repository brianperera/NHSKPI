using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;

public partial class Views_KPI_KPIGroupUpdate : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private NHSKPIDataService.Models.KPIGroup kpiGroup = null;
    private int kpiGroupId;
   
    #endregion

    #region Public Properties

    public KPIController KpiController
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
    public NHSKPIDataService.Models.KPIGroup KpiGroup
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
    public int KpiGroupId
    {
        get
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                kpiGroupId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                kpiGroupId = 0;
            }
            return kpiGroupId;
        }
        set
        {
            kpiGroupId = value;
        }
    }

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                this.kpiGroup = KpiController.ViewKPIGroup(int.Parse(Request.QueryString["Id"]));
                GetKPIGroup();
                btnUpdate.Visible = true;
                
            }
            else
            {
                btnSave.Visible = true;
                
            }

        }
    }
    #endregion    

    #region Set KPI Group
    /// <summary>
    /// Assign fields values to KpiGroup object
    /// </summary>
    private void SetKPIGroup()
    {
        KpiGroup.Id = KpiGroupId;
        KpiGroup.KpiGroupName = txtKPIGroupName.Text;
        KpiGroup.IsActive = chkIsActive.Checked;

        if (Session["NHSUser"] != null)
        {
            User currentUser = Session["NHSUser"] as User;

            if (currentUser != null)
                KpiGroup.HospitalID = currentUser.HospitalId;
        }
    }
    #endregion  

    #region Get KPI Group
    /// <summary>
    /// Get KpiGroup object values and assign to the Controler
    /// </summary>
    private void GetKPIGroup()
    {
        txtKPIGroupName.Text = KpiGroup.KpiGroupName.ToString();
        chkIsActive.Checked = KpiGroup.IsActive;
    }
    #endregion    

    #region Clear Controls
    private void ClearControls()
    {
        txtKPIGroupName.Text = string.Empty;       
    }
    #endregion

    #region Save Button click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetKPIGroup();

        if (KpiController.AddKPIGroup(KpiGroup) < 0)
        {
            lblAddMessage.Text = Constant.MSG_KPIGroup_Exist;
            lblAddMessage.CssClass = "alert-danger"; 
        }
        else
        {
            lblAddMessage.Text = Constant.MSG_KpiGroup_Success_Add;
            lblAddMessage.CssClass = "alert-success";
            ClearControls();
        }

    }
    #endregion 

    #region Update Button Click
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SetKPIGroup();
            if (KpiController.UpdateKPIGroup(KpiGroup))
            {
                lblAddMessage.Text = Constant.MSG_KpiGroup_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_KPIGroup_Exist;
                lblAddMessage.CssClass = "alert-danger"; 
            }


        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    #endregion   


}
