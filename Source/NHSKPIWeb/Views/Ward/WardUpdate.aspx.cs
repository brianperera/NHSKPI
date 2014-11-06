using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;

public partial class Views_Ward_WardUpdate : System.Web.UI.Page
{
    #region Private Variable

    private WardController wardController = null;
    private NHSKPIDataService.Models.Ward ward = null;
    private int wardIds;   
   
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
    public int WardIds
    {
        get {
            
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                wardIds = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                wardIds = 0;
            }
            return wardIds; 
             }
        set { wardIds = value; }
    }
    #endregion 

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadWardInitialData();
            if (Request.QueryString["Id"] != null)
            {
                this.ward = WardController.ViewWards(int.Parse(Request.QueryString["Id"]));
                GetWard();
                btnUpdate.Visible = true;

            }
            else
            {
                btnSave.Visible = true;

            }
        }
        else
        {
            ClientScript.RegisterHiddenField("isPostBack", "1");
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
        ListItem itemWardGroup = new ListItem("--Select Ward Group--", "");
        ddlWardGroupName.Items.Insert(0, itemWardGroup);       
    }

    #endregion

    #region Set Ward
    public void SetWard()
    {
        Ward.WardId         = WardIds;
        Ward.WardCode       = txtWardCode.Text;
        ward.WardName       = txtWardName.Text;
        Ward.HospitalId     = Convert.ToInt32(ddlHospitalName.SelectedItem.Value.ToString());
        Ward.WardGroupId    = Convert.ToInt32(ddlWardGroupName.SelectedItem.Value.ToString());       
        Ward.IsActiveWard   = chkIsActive.Checked;
    }
    #endregion

    #region Get Ward
    private void GetWard()
    {
        txtWardCode.Text = Ward.WardCode;
        txtWardName.Text = Ward.WardName;        
        ddlHospitalName.SelectedValue = Ward.HospitalId.ToString();
        ddlWardGroupName.SelectedValue = Ward.WardGroupId.ToString();        
        chkIsActive.Checked = Ward.IsActiveWard;

    }
    #endregion

    #region Clear Controls
    private void ClearControls()
    {
        txtWardName.Text                = string.Empty;
        txtWardCode.Text                = string.Empty;
        ddlHospitalName.SelectedIndex   = 0;
        ddlWardGroupName.SelectedIndex  = 0;       
    }
    #endregion

    #region Save Button Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {   SetWard();
        if (WardController.AddWard(Ward) < 0)
        {
            lblAddMessage.Text = Constant.MSG_Ward_Exist;
            lblAddMessage.CssClass = "alert-danger";
        }
        else
        {
            lblAddMessage.Text = Constant.MSG_Ward_Success_Add;
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
            SetWard();
            if (WardController.UpdateWard(Ward))
            {
                lblAddMessage.Text = Constant.MSG_Ward_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_Ward_Exist;
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
