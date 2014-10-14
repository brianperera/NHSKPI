using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Models;
using NHSKPIDataService.Util;

public partial class Views_KPI_KPIDetailUpdate : System.Web.UI.Page
{
    #region Private Variables
    private KPIController kpiController = null;
    private NHSKPIDataService.Models.KPIDetails kpiDetails = null;
    private User nhsUser = null;
    private int kpiId;
    private int kpiDetailId;
     
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
    public NHSKPIDataService.Models.KPIDetails KpiDetails
    {
        get
        {
            if (kpiDetails == null)
            {
                kpiDetails = new NHSKPIDataService.Models.KPIDetails();
            }
            return kpiDetails; 
        }
        set 
        { 
            kpiDetails = value; 
        }
    }
    public int KpiId
    {
        get 
        {
            if (Request.QueryString["KPIId"] != null && int.Parse(Request.QueryString["KPIId"].ToString()) > 0)
            {
                kpiId = int.Parse(Request.QueryString["KPIId"].ToString());
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

    public int KpiDetailsId
    {
        get
        {
            if (int.Parse(hdnId.Value) > 0)
            {
                kpiDetailId = int.Parse(hdnId.Value);
            }
            else
            {
                kpiDetailId = 0;
            }
            return kpiDetailId;
        }
        set
        {
            kpiDetailId = value;
        }
    }  

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetKPIName();
            if (Request.QueryString["KPIId"] != null)
            {
                this.kpiDetails = KpiController.ViewKPIDetails(int.Parse(Request.QueryString["KPIId"]));
                if (this.kpiDetails.IsExist)
                {
                    GetKPIDetails();
                    btnUpdate.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                }
                               
            }
            
        }
    }
    #endregion   

    #region Set KPI Name 
    /// <summary>
    /// Set KPI Name
    /// </summary>
    private void SetKPIName()
    {
        //Need to set KPI Name
        lblKPIName.Text = string.Empty;
    }
    #endregion

    #region Set KPI Details
    /// <summary>
    /// Assign fields values to KpiDetails object
    /// </summary>
    private void SetKPIDetail()
    {
        KpiDetails.Id = KpiDetailsId;
        KpiDetails.KpiId = KpiId;
        KpiDetails.Weight = Convert.ToInt32(txtWeight.Text);
        KpiDetails.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
        KpiDetails.RangeTarget = chkRangeTarget.Checked;
        KpiDetails.FormatCode = txtFormatCode.Text;
        KpiDetails.HigherTheBetterFlag = chkHigherTheBetterFlag.Checked;
        KpiDetails.ThresholdDetails = txtThresholdDetails.Text;
        KpiDetails.Visibility = chkVisibility.Checked;
        KpiDetails.CanSummariesFlag = chkCanSummariesFlag.Checked;
        KpiDetails.IndicatorLead = txtIndicatorLead.Text;
        KpiDetails.UserId = NHSUser.Id;
        KpiDetails.CommentsLead = txtCommentsLead.Text;
        KpiDetails.ResponsibleDivision = txtResponsibleDivision.Text;
        KpiDetails.ManuallyEntered = chkManuallyEntered.Checked;
        KpiDetails.SeparateYTDFigure = chkSeparateYTDFigure.Checked;
        KpiDetails.AverageYTDFigure = chkAverageYTDFigure.Checked;


    }
    #endregion

    #region Get KPI Details
    /// <summary>
    /// Get KpiDetails object values and assign to the Controler
    /// </summary>
    private void GetKPIDetails()
    {
        
        hdnId.Value = KpiDetails.Id.ToString();
        txtWeight.Text = KpiDetails.Weight.ToString();
        txtDisplayOrder.Text = KpiDetails.DisplayOrder.ToString();
        chkRangeTarget.Checked = KpiDetails.RangeTarget;
        txtFormatCode.Text = KpiDetails.FormatCode;
        chkHigherTheBetterFlag.Checked = KpiDetails.HigherTheBetterFlag;
        txtThresholdDetails.Text = KpiDetails.ThresholdDetails;
        chkVisibility.Checked = KpiDetails.Visibility;
        chkCanSummariesFlag.Checked = KpiDetails.CanSummariesFlag;
        txtIndicatorLead.Text = KpiDetails.IndicatorLead;
        //user not assign
        txtCommentsLead.Text = KpiDetails.CommentsLead;
        txtResponsibleDivision.Text = KpiDetails.ResponsibleDivision;
        chkManuallyEntered.Checked = KpiDetails.ManuallyEntered;
        chkSeparateYTDFigure.Checked = KpiDetails.SeparateYTDFigure;
        chkAverageYTDFigure.Checked = KpiDetails.AverageYTDFigure;

    }
    #endregion

    #region Clear Controls
    private void ClearControls()
    {
        txtWeight.Text = string.Empty;
        txtThresholdDetails.Text = string.Empty;
        txtResponsibleDivision.Text = string.Empty;
        txtIndicatorLead.Text = string.Empty;
        txtFormatCode.Text = string.Empty;
        txtDisplayOrder.Text = string.Empty;
        txtCommentsLead.Text = string.Empty;
             
       
    }

    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetKPIDetail();
        if (KpiController.AddKPIDetails(KpiDetails) > 0)
        {
            lblAddMessage.Text = Constant.MSG_KPIDetails_Success_Add;
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
            SetKPIDetail();
            if (KpiController.UpdateKPIDetails(KpiDetails))
            {
                lblAddMessage.Text = Constant.MSG_KPIDetails_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    #endregion
    
}
