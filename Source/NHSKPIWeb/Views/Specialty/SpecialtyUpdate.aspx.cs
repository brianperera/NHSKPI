using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Data;

public partial class Views_Specialty_Specialty : System.Web.UI.Page
{
    #region Private Variable
    private SpecialtyController specialtyController = null;
    private WardGroupController wardGroupController = null;
    private NHSKPIDataService.Models.Specialty specialty = null;
    private NHSKPIDataService.Models.WardGroup wardGroup = null;
    private int specialtyId;   
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
    public int SpecialtyId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                specialtyId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                specialtyId = 0;
            }
            return specialtyId; 
        }
        set 
        { 
            specialtyId = value; 
        }
    }
    #endregion

    #region Set Specialty
    public void SetSpecialty()
    {
        Specialty.Id = SpecialtyId;
        Specialty.GroupId = int.Parse(ddlSpecialtyGroup.SelectedValue);
        Specialty.SpecialtyName = txtSpecialty.Text;
        Specialty.SpecialtyCode = txtSpecialtyCode.Text;
        Specialty.NationalCode = txtNationalSpecialtyCode.Text;
        Specialty.NationalSpecialty = txtNationalSpecialty.Text;
        Specialty.IsActive = chkIsActive.Checked;
    }
    #endregion

    #region Get Specialty
    private void GetSpecialty()
    {
        txtSpecialty.Text = Specialty.SpecialtyName;
        chkIsActive.Checked = specialty.IsActive;
        txtSpecialtyCode.Text = Specialty.SpecialtyCode;
        txtNationalSpecialtyCode.Text = Specialty.NationalCode;
        txtNationalSpecialty.Text = Specialty.NationalSpecialty;
        if (ddlSpecialtyGroup.Items.FindByValue(Specialty.GroupId.ToString()) != null)
            ddlSpecialtyGroup.Items.FindByValue(Specialty.GroupId.ToString()).Selected = true;

    }
    #endregion

    #region Clear Fields
    /// <summary>
    /// Clear Fields
    /// </summary>
    private void ClearFields()
    {
        txtSpecialty.Text = string.Empty;
    }
    #endregion

    #region Page Load
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialData();
            if (Request.QueryString["Id"] != null)
            {
                this.specialty = SpecialtyController.ViewSpecialty(int.Parse(Request.QueryString["Id"]));
                GetSpecialty();
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

    #region Load Initial Data();

    private void LoadInitialData()
    {
        DataSet dsGroup = WardGroupController.SearchWardGroup(string.Empty, Master.NHSUser.HospitalId, true);
        ddlSpecialtyGroup.DataValueField = "Id";
        ddlSpecialtyGroup.DataTextField = "WardGroupName";
        ddlSpecialtyGroup.DataSource = dsGroup.Tables[0];
        ddlSpecialtyGroup.DataBind();
        ListItem itemWardGroup = new ListItem("", "0");
        ddlSpecialtyGroup.Items.Insert(0, itemWardGroup);
    }

    #endregion

    #region Save Button Click
    /// <summary>
    /// Save Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetSpecialty();

        if (SpecialtyController.AddSpecialty(Specialty) < 0)
        {
            lblAddMessage.Text = Constant.MSG_Specialty_Exist;
            lblAddMessage.CssClass = "alert-danger";           
        }
        else
        {
            lblAddMessage.Text = Constant.MSG_Specialty_Success_Add;
            lblAddMessage.CssClass = "alert-success";
            ClearFields();
        }

    }
    #endregion

    #region Update Button Click
    /// <summary>
    /// Update Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SetSpecialty();
            if (SpecialtyController.UpdateSpecialty(Specialty))
            {
                lblAddMessage.Text = Constant.MSG_Specialty_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_Specialty_Exist;
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
