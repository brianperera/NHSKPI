using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;

public partial class Views_KPI_KPIUpdate : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private NHSKPIDataService.Models.KPI kpi = null;
    private int kpiId;
    private User nhsUser = null;

    #endregion

    #region Public Properties

    public KPIController KPIController
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
    public NHSKPIDataService.Models.KPI Kpi
    {
        get 
        {
            if (kpi == null)
            {
                kpi = new NHSKPIDataService.Models.KPI();
            }
            return kpi; 
        }
        set 
        { 
            kpi = value; 
        }
    }
    public int KpiId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                kpiId = int.Parse(Request.QueryString["Id"].ToString());
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

 

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillKPIGroupList();
            FillTargetApplyForList();

            if (Request.QueryString["Id"] != null)
            {
                this.kpi = KPIController.ViewKPI(int.Parse(Request.QueryString["Id"]));
                GetKPI();
                btnUpdate.Visible = true;
                
            }
            else
            {
                LoadAutoKPINo();
                btnSave.Visible = true;
                
            }
        }
    }
    #endregion   

    #region KPIGroupList
    /// <summary>
    /// List All KPI Group Name
    /// </summary>
    private void FillKPIGroupList()
    {
        ddlKPIGroupName.DataSource = KPIController.SearchKPIGroup(string.Empty, true);
        ddlKPIGroupName.DataTextField = "KPIGroupName";
        ddlKPIGroupName.DataValueField = "Id";
        ddlKPIGroupName.DataBind();

        ListItem li = new ListItem("--Select KPI Group--", string.Empty);
        ddlKPIGroupName.Items.Insert(0, li);
    }
    #endregion

    #region KPI Target Level List
    /// <summary>
    /// List All Apply Target Levels
    /// </summary>
    private void FillTargetApplyForList()
    {
        ddlTargetApplyFor.DataSource        = KPIController.GetKPIApplyFor();
        ddlTargetApplyFor.DataTextField     = "KPIApplyFor";
        ddlTargetApplyFor.DataValueField    = "Id";
        ddlTargetApplyFor.DataBind();

        ListItem li = new ListItem("--Select Target--", string.Empty);
        ddlTargetApplyFor.Items.Insert(0, li);

    }
    #endregion

    #region Load Auto KPI No

    private void LoadAutoKPINo()
    {
        txtKPINo.Text = KPIController.GetAutoKPINo().ToString();
    }

    #endregion

    #region Set KPI
    /// <summary>
    /// Assign fields values to Kpi object
    /// </summary>
    private void SetKPI()
    {        
         Kpi.Id                     = KpiId;
         Kpi.KPIDescription         = txtKPIDescription.Text;
         Kpi.KPINo                  = txtKPINo.Text;
         Kpi.GroupId                = Convert.ToInt32(ddlKPIGroupName.SelectedItem.Value.ToString());
         Kpi.TargetApplyFor         = Convert.ToInt32(ddlTargetApplyFor.SelectedItem.Value.ToString());
         Kpi.Weight                 = ((txtWeight.Text) == string.Empty) ? int.MinValue :(Convert.ToInt32(txtWeight.Text));
         Kpi.DisplayOrder           = ((txtDisplayOrder.Text) == string.Empty) ? int.MinValue : (Convert.ToInt32(txtDisplayOrder.Text));
         Kpi.RangeTarget            = chkRangeTarget.Checked;
         Kpi.FormatCode             = txtFormatCode.Text;
         Kpi.HigherTheBetterFlag    = chkHigherTheBetterFlag.Checked;
         Kpi.ThresholdDetails       = txtThresholdDetails.Text;
         Kpi.Visibilty              = chkVisibility.Checked;
         Kpi.CanSummeriseFlag       = chkCanSummariesFlag.Checked;
         Kpi.IndicatorLead          = txtIndicatorLead.Text;
         Kpi.UserId                 = NHSUser.Id;
         Kpi.CommentsLead           = txtCommentsLead.Text;
         Kpi.ResponsibleDivision    = txtResponsibleDivision.Text;
         Kpi.ManuallyEntered        = chkManuallyEntered.Checked;
         Kpi.SeparateYTDFigure      = chkSeparateYTDFigure.Checked;
         Kpi.AverageYTDFigure       = chkAverageYTDFigure.Checked;       
         Kpi.StaticTarget           = chkStaticTarget.Checked;
         Kpi.NumeratorOnlyFlag      = chkNumeratorOnlyFlag.Checked;
         Kpi.IsActive               = chkIsActive.Checked;
         Kpi.NumeratorDescription   = txtNumeratorDescription.Text;
         Kpi.DenominatorDescription = txtDenominatorDescription.Text;
         Kpi.YTDValueDescription    = txtYTDValueDescription.Text;

    }
    #endregion

    #region Get KPI
    /// <summary>
    /// Get Kpi object values and assign to the Controler
    /// </summary>
    private void GetKPI()
    {
        txtKPIDescription.Text          = Kpi.KPIDescription;
        txtKPINo.Text                   = Kpi.KPINo;
        ddlKPIGroupName.SelectedValue   = Kpi.GroupId.ToString();
        ddlTargetApplyFor.SelectedValue = Kpi.TargetApplyFor.ToString();
        chkStaticTarget.Checked         = Kpi.StaticTarget;
        chkNumeratorOnlyFlag.Checked    = Kpi.NumeratorOnlyFlag;
        txtWeight.Text                  = (Kpi.Weight == int.MinValue) ? string.Empty:Kpi.Weight.ToString();
        txtDisplayOrder.Text            = (Kpi.DisplayOrder == int.MinValue) ? string.Empty : Kpi.DisplayOrder.ToString();
        chkRangeTarget.Checked          = Kpi.RangeTarget;
        txtFormatCode.Text              = Kpi.FormatCode;
        chkHigherTheBetterFlag.Checked  = Kpi.HigherTheBetterFlag;
        txtThresholdDetails.Text        = Kpi.ThresholdDetails;
        chkVisibility.Checked           = Kpi.Visibilty;
        chkCanSummariesFlag.Checked     = Kpi.CanSummeriseFlag;
        txtIndicatorLead.Text           = Kpi.IndicatorLead;
        //user not assign
        txtCommentsLead.Text = Kpi.CommentsLead;
        txtResponsibleDivision.Text = Kpi.ResponsibleDivision;
        chkManuallyEntered.Checked = Kpi.ManuallyEntered;
        chkSeparateYTDFigure.Checked = Kpi.SeparateYTDFigure;
        chkAverageYTDFigure.Checked = Kpi.AverageYTDFigure;
        chkIsActive.Checked             = Kpi.IsActive;
        txtNumeratorDescription.Text = Kpi.NumeratorDescription;
        txtDenominatorDescription.Text = Kpi.DenominatorDescription;
        txtYTDValueDescription.Text = Kpi.YTDValueDescription;

    }
    #endregion 

    #region Clear Controls
    private void ClearControls()
    {
        txtKPIDescription.Text = string.Empty;
        txtKPINo.Text = string.Empty;
        ddlTargetApplyFor.SelectedIndex = 0;
        ddlKPIGroupName.SelectedIndex = 0;
        txtWeight.Text = string.Empty; ;
        txtDisplayOrder.Text = string.Empty;
        txtFormatCode.Text = string.Empty;
        txtThresholdDetails.Text = string.Empty;
        txtIndicatorLead.Text = string.Empty;
        txtCommentsLead.Text = string.Empty;
        txtResponsibleDivision.Text = string.Empty;
        chkNumeratorOnlyFlag.Checked = false;
        chkSeparateYTDFigure.Checked = false;
        chkRangeTarget.Checked = false;
        chkAverageYTDFigure.Checked = false;
        chkManuallyEntered.Checked = false;
    }
    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetKPI();
        try
        {
            if (KPIController.AddKPI(Kpi) < 0)
            {
                lblAddMessage.Text = Constant.MSG_KPI_Exist;
                lblAddMessage.CssClass = "alert-danger";
                
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_KPI_Success_Add;
                lblAddMessage.CssClass = "alert-success";
                ClearControls();
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    #endregion

    #region Update Button Click
     protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SetKPI();
            if (KPIController.UpdateKPI(Kpi))
            {
                lblAddMessage.Text = Constant.MSG_KPI_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_KPI_Exist;
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
