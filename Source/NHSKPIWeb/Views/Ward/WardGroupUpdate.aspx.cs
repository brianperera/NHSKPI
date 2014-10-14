using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;

public partial class Views_Ward_WardGroupUpdate : System.Web.UI.Page
{
    #region Private Variable

    private WardGroupController wardGroupController = null;
    private NHSKPIDataService.Models.WardGroup wardGroup = null;
    private int wardGroupId;
   
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
    public int WardGroupId
    {
        get {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                wardGroupId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                wardGroupId = 0;
            }
            return wardGroupId;          
                    
           }
        set 
        {
            wardGroupId = value; 
        }
    } 

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            FillHospitalList();
            if (Request.QueryString["Id"] != null)
            {
                this.wardGroup = WardGroupController.ViewWardGroup(int.Parse(Request.QueryString["Id"]));
                GetWardGroup();
                btnUpdate.Visible = true;
                
            }
            else
            {
                btnSave.Visible = true;
                
            }
        }

    }
    #endregion   
    
    #region Set Ward Group
    public void SetWardGroup()
    {
        WardGroup.Id = WardGroupId;
        WardGroup.WardGroupName = txtWardGroupName.Text;
        WardGroup.Description = txtDescription.Text;
        WardGroup.HospitalId = Convert.ToInt32(ddlHospitalName.SelectedItem.Value.ToString());
        WardGroup.IsActive = chkIsActive.Checked;  
    }
    #endregion

    #region Get Ward Group
    private void GetWardGroup()
    {
        txtWardGroupName.Text = WardGroup.WardGroupName;
        txtDescription.Text = WardGroup.Description;
        ddlHospitalName.SelectedValue = WardGroup.HospitalId.ToString();
        chkIsActive.Checked = WardGroup.IsActive;
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

    #region Clear Controls
    private void ClearControls()
    {
        txtDescription.Text = string.Empty;
        txtWardGroupName.Text = string.Empty;
        ddlHospitalName.SelectedIndex = 0;
        chkIsActive.Checked = true;
    }
    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SetWardGroup();
            if (WardGroupController.AddWardGroup(WardGroup) < 0)
            {
                lblAddMessage.Text = Constant.MSG_WardGroup_Exist;
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_WardGroup_Success_Add;
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
            SetWardGroup();
            if (WardGroupController.UpdateWardGroup(WardGroup))
            {
                lblAddMessage.Text = Constant.MSG_WardGroup_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_WardGroup_Exist;
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
