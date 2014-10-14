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

public partial class Views_KPI_SpecialtyYTDTargetData : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kPIController = null;
    private SpecialtyController specialtyController = null;
    private UtilController utilController = null;

    #endregion

    #region Public Properties

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

    #region Page_Load
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divDynamicTarget.Visible = false;
            divButtons.Visible = false;
            lblMessage.Visible = false;

            
            if (!IsPostBack)
            {
                

                LoadSpecialtyLevelTargetInitialData();

                
                if ((Request.QueryString[Constant.CMN_String_Specialty_Id] != null) && (Request.QueryString[Constant.CMN_String_KPI_Id] != null)
                    && (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;

                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();

                    FillControls(kpiId, int.Parse(Request.QueryString[Constant.CMN_String_Specialty_Id] != null ? Request.QueryString[Constant.CMN_String_Specialty_Id].ToString() : "0"), financailYear);
                }
                else
                {
                    ClearControl();
                    DisableControls();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            //Log the exception and dispal the error message

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


        btnUpdate.Attributes.Add("onClick", "return ConfirmUpdate();");
    }

    #endregion

    #region FillControls
    /// <summary>
    /// Fill the controls with given the values
    /// </summary>
    /// <param name="sepectedKpiId"></param>
    /// <param name="specialtyId"></param>
    /// <param name="financailYear"></param>
    private void FillControls(int selectedKpiId, int specialtyId, string financailYear)
    {
        ClearControl();
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        divButtons.Visible = true;
        divDynamicTarget.Visible = true;

        kPIController = new KPIController();
        string[] years = financailYear.Split('-');
        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
        lblCurentFinancialYear.Text = financailYear;

        if ((specialtyId > 0) && (selectedKpiId > 0))
        {
            DataSet dsTargets = kPIController.GetSpecialtyYTDKPIData(selectedKpiId, specialtyId, startDate, endDate);

            if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
            {
                ClearControl();
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                DataView dvTargetList = new DataView(dsTargets.Tables[0]);

                dvTargetList.Sort = "TargetYearToDate";

                DateTime aprilMonth = new DateTime(Convert.ToInt32(years[0]), 4, 1);
                int rowindex = -1;
                rowindex = dvTargetList.Find(aprilMonth);

                if (rowindex != -1)
                {
                    txtGreenApril.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                    txtAmberApril.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
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

    #region Event imgBtnPrevoius_Click
    /// <summary>
    /// Prevoius Button Click
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
            DisableControls();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Event imgBtnNext_Click
    /// <summary>
    /// Next Button Click
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
            DisableControls();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Event btnSave_Click
    /// <summary>
    /// Save Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveTarget();
    }
    #endregion

    #region private method Save Target
    /// <summary>
    /// Save Target Data
    /// </summary>
    private void SaveTarget()
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

            if ((selectedKPIId != string.Empty))
            {
                if (!IsSpecialtyYTDKpiDataExist(Convert.ToInt32(selectedKPIId), Convert.ToInt32(selectedSpecialtyId), finYear))
                {
                    KPI kpiObject = SetKPIObjectWithYTDData(selectedKPIId, selectedSpecialtyId, firstYear, secondYear);

                    bool success = kPIController.InsertSpecialtyKPIYTDData(kpiObject);

                    if (success)
                    {
                        divDynamicTarget.Visible = true;
                        divButtons.Visible = true;
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        //ClearControl();
                        lblMessage.Visible = true;
                        lblMessage.Text = Constant.MSG_Specialty_YTD_KPI_Data_Success_Add;
                        lblMessage.CssClass = "alert-success";
                    }
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

    #region Event btnUpdate_Click
    /// <summary>
    /// Update Button Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
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

            if ((selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectWithYTDData(selectedKPIId, selectedSpecialtyId, firstYear, secondYear);

                bool success = kPIController.UpdateSpecialtyKPIYTDData(kpiObject);

                if (success)
                {
                    divDynamicTarget.Visible = true;
                    divButtons.Visible = true;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    //ClearControl();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_YTD_KPI_Data_Success_Update;
                    lblMessage.CssClass = "alert-success";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get Specialty YTD KPI Data
    /// <summary>
    /// Get Specialty KPI Data
    /// </summary>
    /// <param name="SpecialtyId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetSpecialtyYTDKPIData(int kpiId, int specialtyId, string financailYear)
    {
        try
        {
            kPIController = new KPIController();
            string[] years = financailYear.Split('-');

            DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

            return kPIController.GetSpecialtyYTDKPIData(kpiId, specialtyId, startDate, endDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Is Specialty Kpi data Exist
    /// <summary>
    /// Check the whether Specialty kpi data is exist
    /// </summary>
    /// <param name="kpiId"></param>
    /// <param name="hospital"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsSpecialtyYTDKpiDataExist(int kpiId, int specialty, string financailYear)
    {
        try
        {
            DataSet dsTargets = GetSpecialtyYTDKPIData(kpiId, specialty, financailYear);

            if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Create Objects
    /// <summary>
    /// Set the KPI object values 
    /// </summary>
    /// <param name="selectedSpecialtyId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="selectedKPIType"></param>
    /// <param name="firstYear"></param>
    /// <param name="secondYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectWithYTDData(string selectedKPIId, string selectedSpecialtyId, int firstYear, int secondYear)
    {
        try
        {
            kPIController = new KPIController();
            KPI kpiObject = new KPI();
            if (Convert.ToInt32(selectedKPIId) > 0)
            {
                kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
            }

            kpiObject.SpecialtyYTDDataList = new System.Collections.Generic.List<KPISpecialtyYTDData>();

            //Add the April month details

            DateTime aprilTarget = new DateTime(firstYear, 4, 1);

            KPISpecialtyYTDData specialtyAprilData = new KPISpecialtyYTDData();
            specialtyAprilData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyAprilData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyAprilData.TargetYearToDate = aprilTarget;
            specialtyAprilData.Numerator = txtGreenApril.Text != string.Empty ? Convert.ToDouble(txtGreenApril.Text) : double.MinValue;
            specialtyAprilData.Denominator = txtAmberApril.Text != string.Empty ? Convert.ToDouble(txtAmberApril.Text) : double.MinValue;
            kpiObject.SpecialtyYTDDataList.Add(specialtyAprilData);

            return kpiObject;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ClearControl
    /// <summary>
    /// Clear Control values
    /// </summary>
    private void ClearControl()
    {
        txtGreenApril.Text = string.Empty;

        txtAmberApril.Text = string.Empty;

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


        if ((specialtyId > 0) && (kpiId > 0))
        {
            FillControls(kpiId, specialtyId, finYear);
        }

        if (kpiId > 0)
        {
            ShowHideDenominator(kpiId);
        }

    }
    #endregion

    #region drop down selected index change
    /// <summary>
    /// ddlSpecialty_SelectedIndexChanged
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

    #region Show Hide Deniminator
    /// <summary>
    /// Show hide nominator and denominator
    /// </summary>
    /// <param name="kpiId"></param>
    private void ShowHideDenominator(int kpiId)
    {
        kPIController = new KPIController();
        KPI selectedKPI = kPIController.ViewKPI(kpiId);

        if (selectedKPI != null)
        {
            if (selectedKPI.NumeratorOnlyFlag)
            {
                divDenominator.Visible = false;
                divNumarator.Visible = true;
            }
            else
            {
                divDenominator.Visible = true;
                divNumarator.Visible = true;
            }
        }
    }
    #endregion

    #region Disable controls
    /// <summary>
    /// Disable UI controls based on the date
    /// </summary>
    private void DisableControls()
    {
        string selectedFinYear = lblCurentFinancialYear.Text;
        int currentYear = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        string[] years = selectedFinYear.Split('-');

        string systemFinYear = string.Empty;

        if (month < 4)
        {
            systemFinYear = (currentYear - 1).ToString() + "-" + (currentYear).ToString();
        }
        else
        {
            systemFinYear = currentYear.ToString() + "-" + (currentYear + 1).ToString();
        }
        imgBtnNext.Enabled = true;

        if (selectedFinYear == systemFinYear)
        {
            imgBtnNext.Enabled = false;

        }
    }

    #endregion
}
