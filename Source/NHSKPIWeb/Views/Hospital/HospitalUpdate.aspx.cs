using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.IO;

public partial class Views_Hospital_HospitalUpdate : System.Web.UI.Page
{
    #region Private Variables
    private HospitalController hospitalController = null;
    private NHSKPIDataService.Models.Hospital hospital = null;
    private int hospitalId;   

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
    public int HospitalId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                hospitalId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                hospitalId = 0;
            }
            return hospitalId; 
        }
        set 
        { 
            hospitalId = value; 
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
                this.hospital = HospitalController.ViewHospital(int.Parse(Request.QueryString["Id"]));
                GetHospital();
                btnUpdate.Visible = true;                
            }            
            else
            {
                btnSave.Visible = true;                
            }
        }
    }
    #endregion

    #region Set Hospital
    private void SetHospital()
    {
        Hospital.Id             = HospitalId;
        Hospital.HospitalName   = txtHospitalName.Text;
        Hospital.HospitalCode   = txtHospitalCode.Text;
        Hospital.PhoneNumber    = txtPhoneNumber.Text;       
        Hospital.HospitalType   = ddlType.SelectedValue.ToString();
        Hospital.Address        = txtAddress.Text;
        Hospital.IsActive       = chkIsActive.Checked;
    }
    #endregion

    #region Get Hospital
    private void GetHospital()
    {
        txtHospitalName.Text  = Hospital.HospitalName;
        txtHospitalCode.Text  = Hospital.HospitalCode;
        txtPhoneNumber.Text   = Hospital.PhoneNumber.ToString(); 
        ddlType.SelectedValue = Hospital.HospitalType.ToString();    
        txtAddress.Text       = Hospital.Address;
        chkIsActive.Checked   = Hospital.IsActive;
    }

    #endregion

    #region Clear Controls
    private void ClearControls()
    {
        txtHospitalName.Text = string.Empty;
        txtHospitalCode.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        txtAddress.Text = string.Empty;    
    }
    #endregion

    #region Save Button Click Event

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SetHospital();
            if (HospitalController.AddHospital(Hospital) < 0)
            {
                lblAddMessage.Text = Constant.MSG_Hospital_Exist;
                lblAddMessage.CssClass = "alert-danger";
            }
            else
            {

                lblAddMessage.Text = Constant.MSG_Hospital_Success_Add;
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

    #region Update Button Click Event

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SetHospital();
            if (HospitalController.UpdateHospital(Hospital))
            {
                lblAddMessage.Text = Constant.MSG_Hospital_Success_Update;
                lblAddMessage.CssClass = "alert-success";
            }
            else
            {
                lblAddMessage.Text = Constant.MSG_Hospital_Exist;
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
