using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using NHSKPIBusinessControllers;
using System.Web.Services;
using NHSKPIDataService.Models;
using NHSKPIDataService.Util;

public partial class Views_KPI_SpecialtyYTDTarget : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kPIController = null;
    private SpecialtyController specialtyController = null;
    private UtilController utilController = null;
    
    #endregion

    #region Properties

    public KPIController KPIController
    {
        get
        {
            if (kPIController == null)
            {
                kPIController = new KPIController();
            }
            return kPIController;
        }
        set
        {
            kPIController = value;
        }
    }

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

    public UtilController UtilController
    {
        get
        {
            if (utilController == null)
            {
                utilController = new UtilController();
            }
            return utilController;
        }
        set
        {
            utilController = value;
        }
    }

    #endregion

    #region Page Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        divStaticTarget.Visible = true;
        divButtons.Visible = true;
        lblMessage.Visible = false;

        
        try
        {
            if (!IsPostBack)
            {
                LoadSpecialtyLevelTargetInitialData();

                if ((Request.QueryString[Constant.CMN_String_Specialty_Id] != null) && (Request.QueryString[Constant.CMN_String_KPI_Id] != null)
                   && (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;

                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    int specialtyId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_Specialty_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();

                    FillControls(specialtyId, kpiId, financailYear);
                }
                else
                {
                    ClearControl();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Load Specialty Level Target Initial Data

    private void LoadSpecialtyLevelTargetInitialData()
    {
        //Specialty -  Table 0
        //KPI - Table 1
        DataSet DsSpecialtyLevelTargetInitialData = UtilController.GetSpecialtyLevelYTDTargetInitialData();

        ddlSpecialty.DataTextField = "Specialty";
        ddlSpecialty.DataValueField = "Id";
        ddlSpecialty.DataSource = DsSpecialtyLevelTargetInitialData.Tables[0];
        ddlSpecialty.DataBind();
        ListItem liSpecialtyItem = new ListItem("-- All --", "0");
        ddlSpecialty.Items.Insert(0, liSpecialtyItem);
        ddlSpecialty.Items.FindByValue(Request.QueryString[Constant.CMN_String_Specialty_Id] != null ? Request.QueryString[Constant.CMN_String_Specialty_Id].ToString() : "0").Selected = true;

        ddlKPI.Enabled = true;
        ddlKPI.DataTextField = "KPIDescription";
        ddlKPI.DataValueField = "Id";
        ddlKPI.DataSource = DsSpecialtyLevelTargetInitialData.Tables[1]; ;
        ddlKPI.DataBind();
        ListItem liKPIItem = new ListItem("-- All --", "0");
        ddlKPI.Items.Insert(0, liKPIItem);
        ddlKPI.Items.FindByValue(Request.QueryString[Constant.CMN_String_KPI_Id] != null ? Request.QueryString[Constant.CMN_String_KPI_Id].ToString() : "0").Selected = true;


        string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

        lblCurentFinancialYear.Text = currentFinYear;

        hdnUserType.Value = Master.NHSUser.RoleId.ToString();

        btnSave.Attributes.Add("onClick", "return ConfirmSaveAll();");
        btnUpdate.Attributes.Add("onClick", "return ConfirmUpdate();");
    }

    #endregion

    #region Fill Controls
    /// <summary>
    /// Fill Controls
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    private void FillControls(int specialtyId, int kpiId, string financailYear)
    {
        ClearControl();
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        divButtons.Visible = true;
        divStaticTarget.Visible = true;

        kPIController = new KPIController();
        string[] years = financailYear.Split('-');
        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
        lblCurentFinancialYear.Text = financailYear;



        if ((specialtyId > 0) && (kpiId > 0))
        {

            DataSet dsTargets = kPIController.GetSpecialtyKPIYTDTarget(specialtyId, kpiId, startDate, endDate);

            if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
            {

                btnSave.Visible = false;
                btnUpdate.Visible = true;

                DataView dvTargetList = new DataView(dsTargets.Tables[0]);

                dvTargetList.Sort = "TargetYTD";
                int rowindex = -1;
                DateTime aprilMonth = new DateTime(Convert.ToInt32(years[0]), 4, 1);

                rowindex = dvTargetList.Find(aprilMonth);

                if (rowindex != -1)
                {
                    txtYTDTargetDescription.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                    txtYTDTargetGreen.Text = dvTargetList[rowindex]["YTDGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDGreen"].ToString() : string.Empty;
                    txtYTDTargetAmber.Text = dvTargetList[rowindex]["YTDAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDAmber"].ToString() : string.Empty;


                }
            }
            else
            {
                ClearControl();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
        }

    }
    #endregion

    #region imgBtnPrevoius_Click
    /// <summary>
    /// imgBtnPrevoius_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnPrevoius_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) - 1;
            int nextsecond = Convert.ToInt32(years[1]) - 1;

            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

            lblCurentFinancialYear.Text = nextFinYear;
            ClearControl();
            FillControlsWithSelectedValues();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region imgBtnNext_Click
    /// <summary>
    /// imgBtnNext_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) + 1;
            int nextsecond = Convert.ToInt32(years[1]) + 1;

            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

            lblCurentFinancialYear.Text = nextFinYear;
            ClearControl();

            FillControlsWithSelectedValues();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btnSave_Click
    /// <summary>
    /// btnSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            kPIController = new KPIController();

            string selectedKPIId = ddlKPI.SelectedItem.Value;
            string selectedSpecialtyId = ddlSpecialty.SelectedItem.Value;

            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectValues(selectedSpecialtyId, selectedKPIId, firstYear);

                bool success = kPIController.InsertSpecialtyKPIYTDTarget(kpiObject);

                if (success)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    //ClearControl();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_YTD_Target_Success_Add;
                    lblMessage.CssClass = "alert-success";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
            //Log and display the error
            //lblMessage.Text = "Error"
        }

    }
    #endregion

    #region btnUpdate_Click
    /// <summary>
    /// btnUpdate_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            kPIController = new KPIController();
            string selectedKPIId = ddlKPI.SelectedItem.Value;
            string selectedSpecialtyId = ddlSpecialty.SelectedValue;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectValues(selectedSpecialtyId, selectedKPIId, firstYear);

                bool success = kPIController.UpdateSpecialtyKPIYTDTarget(kpiObject);

                if (success)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    //ClearControl();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_YTD_Target_Success_Update;
                    lblMessage.CssClass = "alert-success";
                }
            }
        }
        catch (Exception ex)
        {            //Log the exceptio nand 

            throw ex;
        }
    }
    #endregion

    #region IsKPIYTDTargetExist
    /// <summary>
    /// Check KPI YTD Target is exist
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsKPIYTDTargetExist1(int hospitalId, int kpiId, string financailYear)
    {
        DataSet dsTargets = GetSpecialtyKPIYTDTarget(hospitalId, kpiId, financailYear);

        if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Set Target
    /// <summary>
    /// Set the KPI Object
    /// </summary>
    /// <param name="selectedSpecialtyId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="firstYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectValues(string selectedSpecialtyId, string selectedKPIId, int firstYear)
    {
        kPIController = new KPIController();
        KPI kpiObject = new KPI();
        if (Convert.ToInt32(selectedKPIId) > 0)
        {
            kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
        }

        kpiObject.SpecialtyYTDTargetList = new System.Collections.Generic.List<KPISpecialtyYTDTarget>();
        //Add the April month details

        DateTime aprilTarget = new DateTime(firstYear, 4, 1);

        KPISpecialtyYTDTarget specialtyAprilTarget = new KPISpecialtyYTDTarget();
        specialtyAprilTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
        specialtyAprilTarget.KPIId = Convert.ToInt32(selectedKPIId);
        specialtyAprilTarget.TargetYTD = aprilTarget;
        specialtyAprilTarget.YTDTargetDescription = txtYTDTargetDescription.Text != string.Empty ? txtYTDTargetDescription.Text.ToString() : string.Empty;
        specialtyAprilTarget.YTDGreen = txtYTDTargetGreen.Text != string.Empty ? Convert.ToDouble(txtYTDTargetGreen.Text) : double.MinValue;
        specialtyAprilTarget.YTDAmber = txtYTDTargetAmber.Text != string.Empty ? Convert.ToDouble(txtYTDTargetAmber.Text) : double.MinValue;

        kpiObject.SpecialtyYTDTargetList.Add(specialtyAprilTarget);
        return kpiObject;
    }
    #endregion

    #region ClearControl
    /// <summary>
    /// Clear the all the controls
    /// </summary>
    private void ClearControl()
    {
        txtYTDTargetDescription.Text = string.Empty;
        txtYTDTargetGreen.Text = string.Empty;
        txtYTDTargetAmber.Text = string.Empty;
    }
    #endregion

    #region GetSpecialtyKPIYTDTarget
    /// <summary>
    /// Get the Specialty YTD data
    /// </summary>
    /// <param name="specialtyId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetSpecialtyKPIYTDTarget(int specialtyId, int kpiId, string financailYear)
    {
        kPIController = new KPIController();
        string[] years = financailYear.Split('-');

        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

        return kPIController.GetSpecialtyKPIYTDTarget(specialtyId, kpiId, startDate, endDate);

    }
    #endregion

    #region Fill controls with selected values
    /// <summary>
    /// Fill the UI controls with the drop down selected values
    /// </summary>
    private void FillControlsWithSelectedValues()
    {
        int kpiId = 0;
        int specialtyId = 0;

        if (ddlSpecialty.SelectedItem.Value != string.Empty)
        {
            specialtyId = Convert.ToInt32(ddlSpecialty.SelectedItem.Value);
        }

        if (ddlKPI.SelectedItem.Value != string.Empty)
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
        }

        string finYear = lblCurentFinancialYear.Text.ToString();


        FillControls(specialtyId, kpiId, finYear);


    }
    #endregion

    #region drop down selected index change
    /// <summary>
    /// ddlHospitalName_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillControlsWithSelectedValues();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// ddlKPI_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlKPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillControlsWithSelectedValues();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion 
}
