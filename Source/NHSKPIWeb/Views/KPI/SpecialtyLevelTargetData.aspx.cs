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

public partial class Views_KPI_SpecialtyLevelTargetData : System.Web.UI.Page
{
    #region Private Variables

    private SpecialtyController specialtyController = null;
    private KPIController kPIController = null;
    
    #endregion

    #region Public Properties
    /// <summary>
    /// Get or set the hospital id
    /// </summary>
    public int CurrentHospitalId
    {
        get
        {
            return Master.NHSUser.HospitalId;
        }
        set
        {
            Session[Constant.SSN_Current_Hospital_Id] = value;

        }
    }

    #endregion

    #region Page Load Event
    /// <summary>
    /// Page_Load Event
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

            //if the user is not super user keep the hospitla in session
            if (Master.NHSUser.RoleId > 1)
            {
                CurrentHospitalId = Master.NHSUser.HospitalId;
            }
            if (!IsPostBack)
            {
                btnUpdate.Attributes.Add("onClick", "return ConfirmUpdate();");

                FillSpecialtyList();
                FillKPIList();
                string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();
                lblCurentFinancialYear.Text = currentFinYear;
                hdnUserType.Value = Master.NHSUser.RoleId.ToString();

                if ((Request.QueryString[Constant.CMN_String_Specialty_Id] != null) || (Request.QueryString[Constant.CMN_String_KPI_Id] != null) || (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    int specialtyId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_Specialty_Id]);
                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();

                    SelectSpecialty(specialtyId);
                    SelectKPI(kpiId);
                    //FillControls(wardId, kpiId, CurrentHospitalId, financailYear);
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

    #region FillControls
    /// <summary>
    /// Fill UI Controls
    /// </summary>
    /// <param name="specialtyId">Id of the Specialty</param>
    /// <param name="kpiId">Id of the KPI</param>
    /// <param name="hospitalId">Id of the hospital</param>
    /// <param name="financailYear">Finacail year like 2013-2014</param>
    private void FillControls(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        ClearControl();
        kPIController = new KPIController();
        specialtyController = new SpecialtyController();
        KPI kpi = kPIController.ViewKPI(kpiId);
        lblNumeratorDes.Text = kpi.NumeratorDescription != "" ? kpi.NumeratorDescription : "Numerator";
        lblDenominatorDes.Text = kpi.DenominatorDescription != "" ? kpi.DenominatorDescription : "Denominator";
        lblYTDValueDes.Text = kpi.YTDValueDescription != "" ? kpi.YTDValueDescription : "YTD Value";

        string[] years = financailYear.Split('-');
        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
        lblCurentFinancialYear.Text = financailYear;
        ShowHideDenominator(kpiId);
        divDynamicTarget.Visible = true;
        divButtons.Visible = true;

        DataSet dsTargets = kPIController.GetSpecialtyKPIData(specialtyId, kpiId, hospitalId, startDate, endDate);

        if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
        {
            ClearControl();
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            DataView dvTargetList = new DataView(dsTargets.Tables[0]);
            dvTargetList.Sort = "TargetMonth";
            DateTime aprilMonth = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            int rowindex = -1;
            rowindex = dvTargetList.Find(aprilMonth);
            if (rowindex != -1)
            {
                txtGreenApril.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberApril.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDApril.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime mayMonth = new DateTime(Convert.ToInt32(years[0]), 5, 1);
            rowindex = dvTargetList.Find(mayMonth);
            if (rowindex != -1)
            {
                txtGreenMay.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberMay.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDMay.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime juneMonth = new DateTime(Convert.ToInt32(years[0]), 6, 1);
            rowindex = dvTargetList.Find(juneMonth);
            if (rowindex != -1)
            {
                txtGreenJune.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberJune.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDJune.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime julyMonth = new DateTime(Convert.ToInt32(years[0]), 7, 1);
            rowindex = dvTargetList.Find(julyMonth);
            if (rowindex != -1)
            {
                txtGreenJuly.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberJuly.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDJuly.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime augMonth = new DateTime(Convert.ToInt32(years[0]), 8, 1);
            rowindex = dvTargetList.Find(augMonth);
            if (rowindex != -1)
            {
                txtGreenAug.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberAug.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDAug.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime sepMonth = new DateTime(Convert.ToInt32(years[0]), 9, 1);
            rowindex = dvTargetList.Find(sepMonth);
            if (rowindex != -1)
            {
                txtGreenSep.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberSep.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDSep.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime octMonth = new DateTime(Convert.ToInt32(years[0]), 10, 1);
            rowindex = dvTargetList.Find(octMonth);
            if (rowindex != -1)
            {
                txtGreenOct.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberOct.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDOct.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime novMonth = new DateTime(Convert.ToInt32(years[0]), 11, 1);
            rowindex = dvTargetList.Find(novMonth);
            if (rowindex != -1)
            {
                txtGreenNov.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberNov.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDNov.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime decMonth = new DateTime(Convert.ToInt32(years[0]), 12, 1);
            rowindex = dvTargetList.Find(decMonth);
            if (rowindex != -1)
            {
                txtGreenDec.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberDec.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDDec.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime janMonth = new DateTime(Convert.ToInt32(years[1]), 1, 1);
            rowindex = dvTargetList.Find(janMonth);
            if (rowindex != -1)
            {
                txtGreenJan.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberJan.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDJan.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime febMonth = new DateTime(Convert.ToInt32(years[1]), 2, 1);
            rowindex = dvTargetList.Find(febMonth);
            if (rowindex != -1)
            {
                txtGreenFeb.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberFeb.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDFeb.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DateTime marchMonth = new DateTime(Convert.ToInt32(years[1]), 3, 1);
            rowindex = dvTargetList.Find(marchMonth);
            if (rowindex != -1)
            {
                txtGreenMarch.Text = dvTargetList[rowindex]["Numerator"].ToString() != "" ? dvTargetList[rowindex]["Numerator"].ToString() : string.Empty;
                txtAmberMarch.Text = dvTargetList[rowindex]["Denominator"].ToString() != "" ? dvTargetList[rowindex]["Denominator"].ToString() : string.Empty;
                txtYTDMarch.Text = dvTargetList[rowindex]["YTDValue"].ToString() != "" ? dvTargetList[rowindex]["YTDValue"].ToString() : string.Empty;
            }

            DisableControls();

        }
        else
        {
            ClearControl();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            DisableControls();
        }

    }
    #endregion

    #region Show Hide denominator
    /// <summary>
    /// Show hide nominator and the denominator
    /// </summary>
    /// <param name="kpiId"></param>
    private void ShowHideDenominator(int kpiId)
    {
        try
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

                if (selectedKPI.SeparateYTDFigure)
                {
                    divYTD.Visible = true;
                }
                else
                {
                    divYTD.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get Specialty KPI Target
    /// <summary>
    /// Get the Specialty KPI Target
    /// </summary>
    /// <param name="specialtyId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetSpecialtyKPITarget(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            string[] years = financailYear.Split('-');
            DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
            return kPIController.GetSpecialtyKPITarget(specialtyId, kpiId, hospitalId, startDate, endDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Get Specialty KPI Data
    /// <summary>
    /// Get the Specialty KPI Data
    /// </summary>
    /// <param name="specialtyId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetSpecialtyKPIData(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            string[] years = financailYear.Split('-');
            DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
            return kPIController.GetSpecialtyKPIData(specialtyId, kpiId, hospitalId, startDate, endDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Click event for the  Prevoius button
    /// <summary>
    /// Click event for the  Prevoius button
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

    #region Click event for the  Next button
    /// <summary>
    /// Click event for the  Prevoius button
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

    #region Click event for the  Save button
    /// <summary>
    /// Click event for the  Save button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveTarget();
    }
    #endregion

    #region Is Specialty Kpi Target Exist
    /// <summary>
    /// Check the whether Specialty kpi data is exist
    /// </summary>
    /// <param name="SpecialtyId">Specialty id</param>
    /// <param name="kpiId">Kpi id</param>
    /// <param name="hospital">hospital id</param>
    /// <param name="financailYear">finacail year like 2013-2014</param>
    /// <returns>true if exist otherwise false</returns>
    private bool IsSpecialtyKpiDataExist(int specialtyId, int kpiId, int hospital, string financailYear)
    {
        try
        {
            DataSet dsTargets = GetSpecialtyKPIData(specialtyId, kpiId, hospital, financailYear);
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

    #region Save Target
    /// <summary>
    /// Save Method
    /// </summary>
    private void SaveTarget()
    {
        try
        {
            kPIController = new KPIController();

            string selectedSpecialtyId = ddlSpecialty.SelectedItem.Value;
            string selectedKPIId = ddlKPI.SelectedItem.Value;
            int selectedHospitalId = Master.NHSUser.HospitalId;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedHospitalId > 0) && (selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty))
            {
                if (!IsSpecialtyKpiDataExist(Convert.ToInt32(selectedSpecialtyId), Convert.ToInt32(selectedKPIId), Convert.ToInt32(selectedHospitalId), finYear))
                {

                    KPI kpiObject = SetKPIObjectWithMonthlyData(selectedSpecialtyId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                    bool success = kPIController.InsertSpecialtyKPIData(kpiObject);

                    if (success)
                    {
                        divDynamicTarget.Visible = true;
                        divButtons.Visible = true;
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        //ClearControl();
                        DisableControls();
                        lblMessage.Visible = true;
                        lblMessage.Text = Constant.MSG_Specialty_KPI_Data_Success_Add;
                        lblMessage.CssClass = "alert-success";
                    }

                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_Data_Already_Exist;
                    lblMessage.CssClass = "alert-info";
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

    #region Click event for the Update button
    /// <summary>
    /// Click event for the Update button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            kPIController = new KPIController();
            string selectedSpecialtyId = ddlSpecialty.SelectedItem.Value;
            string selectedKPIId = ddlKPI.SelectedItem.Value;
            int selectedHospitalId = Master.NHSUser.HospitalId;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);
            if ((selectedHospitalId > 0) && (selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectWithMonthlyData(selectedSpecialtyId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                bool success = kPIController.UpdateSpecialtyKPIData(kpiObject);
                if (success)
                {
                    divDynamicTarget.Visible = true;
                    divButtons.Visible = true;
                    //ClearControl();
                    DisableControls();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_Data_Success_Update;
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

    #region Set KPI Targets Object
    /// <summary>
    /// Set the KPI object values 
    /// </summary>
    /// <param name="selectedSpecialtyId">Specialty id</param>
    /// <param name="selectedKPIId">kpi id</param>
    /// <param name="selectedKPIType">type of the kpi</param>
    /// <param name="firstYear">first year of the fincail year</param>
    /// <param name="secondYear">second year of the fincail year</param>
    /// <returns></returns>
    private KPI SetKPIObjectWithMonthlyData(string selectedSpecialtyId, string selectedKPIId, int selectedHospitalId, int firstYear, int secondYear)
    {
        try
        {
            kPIController = new KPIController();
            KPI kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
            kpiObject.SpecialtyMonthlyDataList = new System.Collections.Generic.List<KPISpecialtyMonthlyData>();

            //Add the April month details

            DateTime aprilTarget = new DateTime(firstYear, 4, 1);
            KPISpecialtyMonthlyData specialtyAprilData = new KPISpecialtyMonthlyData();
            specialtyAprilData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyAprilData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyAprilData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyAprilData.TargetMonth = aprilTarget;
            specialtyAprilData.Numerator = txtGreenApril.Text != string.Empty ? Convert.ToDouble(txtGreenApril.Text) : double.MinValue;
            specialtyAprilData.Denominator = txtAmberApril.Text != string.Empty ? Convert.ToDouble(txtAmberApril.Text) : double.MinValue;           
            specialtyAprilData.YTDValue = txtYTDApril.Text != string.Empty ? Convert.ToDouble(txtYTDApril.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyAprilData);

            DateTime mayTarget = new DateTime(firstYear, 5, 1);
            KPISpecialtyMonthlyData specialtyMayData = new KPISpecialtyMonthlyData();
            specialtyMayData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyMayData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyMayData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyMayData.TargetMonth = mayTarget;
            specialtyMayData.Numerator = txtGreenMay.Text != string.Empty ? Convert.ToDouble(txtGreenMay.Text) : double.MinValue;
            specialtyMayData.Denominator = txtAmberMay.Text != string.Empty ? Convert.ToDouble(txtAmberMay.Text) : double.MinValue;
            specialtyMayData.YTDValue = txtYTDMay.Text != string.Empty ? Convert.ToDouble(txtYTDMay.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyMayData);

            DateTime juneTarget = new DateTime(firstYear, 6, 1);
            KPISpecialtyMonthlyData specialtyJuneData = new KPISpecialtyMonthlyData();
            specialtyJuneData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJuneData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJuneData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJuneData.TargetMonth = juneTarget;
            specialtyJuneData.Numerator = txtGreenJune.Text != string.Empty ? Convert.ToDouble(txtGreenJune.Text) : double.MinValue;
            specialtyJuneData.Denominator = txtAmberJune.Text != string.Empty ? Convert.ToDouble(txtAmberJune.Text) : double.MinValue;
            specialtyJuneData.YTDValue = txtYTDJune.Text != string.Empty ? Convert.ToDouble(txtYTDJune.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyJuneData);

            //July
            DateTime julyTarget = new DateTime(firstYear, 7, 1);
            KPISpecialtyMonthlyData specialtyJulyData = new KPISpecialtyMonthlyData();
            specialtyJulyData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJulyData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJulyData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJulyData.TargetMonth = julyTarget;
            specialtyJulyData.Numerator = txtGreenJuly.Text != string.Empty ? Convert.ToDouble(txtGreenJuly.Text) : double.MinValue;
            specialtyJulyData.Denominator = txtAmberJuly.Text != string.Empty ? Convert.ToDouble(txtAmberJuly.Text) : double.MinValue;
            specialtyJulyData.YTDValue = txtYTDJuly.Text != string.Empty ? Convert.ToDouble(txtYTDJuly.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyJulyData);

            //Aug
            DateTime augTarget = new DateTime(firstYear, 8, 1);
            KPISpecialtyMonthlyData specialtyAugData = new KPISpecialtyMonthlyData();
            specialtyAugData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyAugData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyAugData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyAugData.TargetMonth = augTarget;
            specialtyAugData.Numerator = txtGreenAug.Text != string.Empty ? Convert.ToDouble(txtGreenAug.Text) : double.MinValue;
            specialtyAugData.Denominator = txtAmberAug.Text != string.Empty ? Convert.ToDouble(txtAmberAug.Text) : double.MinValue;
            specialtyAugData.YTDValue = txtYTDAug.Text != string.Empty ? Convert.ToDouble(txtYTDAug.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyAugData);

            //Sep
            DateTime sepTarget = new DateTime(firstYear, 9, 1);
            KPISpecialtyMonthlyData specialtySepData = new KPISpecialtyMonthlyData();
            specialtySepData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtySepData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtySepData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtySepData.TargetMonth = sepTarget;
            specialtySepData.Numerator = txtGreenSep.Text != string.Empty ? Convert.ToDouble(txtGreenSep.Text) : double.MinValue;
            specialtySepData.Denominator = txtAmberSep.Text != string.Empty ? Convert.ToDouble(txtAmberSep.Text) : double.MinValue;
            specialtySepData.YTDValue = txtYTDSep.Text != string.Empty ? Convert.ToDouble(txtYTDSep.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtySepData);

            //Oct
            DateTime octTarget = new DateTime(firstYear, 10, 1);
            KPISpecialtyMonthlyData specialtyOctData = new KPISpecialtyMonthlyData();
            specialtyOctData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyOctData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyOctData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyOctData.TargetMonth = octTarget;
            specialtyOctData.Numerator = txtGreenOct.Text != string.Empty ? Convert.ToDouble(txtGreenOct.Text) : double.MinValue;
            specialtyOctData.Denominator = txtAmberOct.Text != string.Empty ? Convert.ToDouble(txtAmberOct.Text) : double.MinValue;
            specialtyOctData.YTDValue = txtYTDOct.Text != string.Empty ? Convert.ToDouble(txtYTDOct.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyOctData);

            //Nov
            DateTime novTarget = new DateTime(firstYear, 11, 1);
            KPISpecialtyMonthlyData specialtyNovData = new KPISpecialtyMonthlyData();
            specialtyNovData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyNovData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyNovData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyNovData.TargetMonth = novTarget;
            specialtyNovData.Numerator = txtGreenNov.Text != string.Empty ? Convert.ToDouble(txtGreenNov.Text) : double.MinValue;
            specialtyNovData.Denominator = txtAmberNov.Text != string.Empty ? Convert.ToDouble(txtAmberNov.Text) : double.MinValue;
            specialtyNovData.YTDValue = txtYTDNov.Text != string.Empty ? Convert.ToDouble(txtYTDNov.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyNovData);

            //Dec
            DateTime decTarget = new DateTime(firstYear, 12, 1);
            KPISpecialtyMonthlyData specialtyDecData = new KPISpecialtyMonthlyData();
            specialtyDecData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyDecData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyDecData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyDecData.TargetMonth = decTarget;
            specialtyDecData.Numerator = txtGreenDec.Text != string.Empty ? Convert.ToDouble(txtGreenDec.Text) : double.MinValue;
            specialtyDecData.Denominator = txtAmberDec.Text != string.Empty ? Convert.ToDouble(txtAmberDec.Text) : double.MinValue;
            specialtyDecData.YTDValue = txtYTDDec.Text != string.Empty ? Convert.ToDouble(txtYTDDec.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyDecData);

            //Jan
            DateTime janTarget = new DateTime(secondYear, 1, 1);
            KPISpecialtyMonthlyData specialtyJanData = new KPISpecialtyMonthlyData();
            specialtyJanData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJanData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJanData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJanData.TargetMonth = janTarget;
            specialtyJanData.Numerator = txtGreenJan.Text != string.Empty ? Convert.ToDouble(txtGreenJan.Text) : double.MinValue;
            specialtyJanData.Denominator = txtAmberJan.Text != string.Empty ? Convert.ToDouble(txtAmberJan.Text) : double.MinValue;
            specialtyJanData.YTDValue = txtYTDJan.Text != string.Empty ? Convert.ToDouble(txtYTDJan.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyJanData);

            //feb
            DateTime febTarget = new DateTime(secondYear, 2, 1);
            KPISpecialtyMonthlyData specialtyFebData = new KPISpecialtyMonthlyData();
            specialtyFebData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyFebData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyFebData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyFebData.TargetMonth = febTarget;
            specialtyFebData.Numerator = txtGreenFeb.Text != string.Empty ? Convert.ToDouble(txtGreenFeb.Text) : double.MinValue;
            specialtyFebData.Denominator = txtAmberFeb.Text != string.Empty ? Convert.ToDouble(txtAmberFeb.Text) : double.MinValue;
            specialtyFebData.YTDValue = txtYTDFeb.Text != string.Empty ? Convert.ToDouble(txtYTDFeb.Text) : double.MinValue;
            kpiObject.SpecialtyMonthlyDataList.Add(specialtyFebData);

            //March
            DateTime marchTarget = new DateTime(secondYear, 3, 1);
            KPISpecialtyMonthlyData specialtyMarchData = new KPISpecialtyMonthlyData();
            specialtyMarchData.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyMarchData.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyMarchData.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyMarchData.TargetMonth = marchTarget;
            specialtyMarchData.Numerator = txtGreenMarch.Text != string.Empty ? Convert.ToDouble(txtGreenMarch.Text) : double.MinValue;
            specialtyMarchData.Denominator = txtAmberMarch.Text != string.Empty ? Convert.ToDouble(txtAmberMarch.Text) : double.MinValue;
            specialtyMarchData.YTDValue = txtYTDMarch.Text != string.Empty ? Convert.ToDouble(txtYTDMarch.Text) : double.MinValue;

            kpiObject.SpecialtyMonthlyDataList.Add(specialtyMarchData);
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
    /// Clear All UI Controls
    /// </summary>
    private void ClearControl()
    {
        try
        {
            txtGreenApril.Text = string.Empty;
            txtGreenMay.Text = string.Empty;
            txtGreenJune.Text = string.Empty;
            txtGreenJuly.Text = string.Empty;
            txtGreenAug.Text = string.Empty;
            txtGreenSep.Text = string.Empty;
            txtGreenOct.Text = string.Empty;
            txtGreenNov.Text = string.Empty;
            txtGreenDec.Text = string.Empty;
            txtGreenJan.Text = string.Empty;
            txtGreenFeb.Text = string.Empty;
            txtGreenMarch.Text = string.Empty;

            txtAmberApril.Text = string.Empty;
            txtAmberMay.Text = string.Empty;
            txtAmberJune.Text = string.Empty;
            txtAmberJuly.Text = string.Empty;
            txtAmberAug.Text = string.Empty;
            txtAmberSep.Text = string.Empty;
            txtAmberOct.Text = string.Empty;
            txtAmberNov.Text = string.Empty;
            txtAmberDec.Text = string.Empty;
            txtAmberJan.Text = string.Empty;
            txtAmberFeb.Text = string.Empty;
            txtAmberMarch.Text = string.Empty;

            txtYTDApril.Text = string.Empty;
            txtYTDMay.Text = string.Empty;
            txtYTDJune.Text = string.Empty;
            txtYTDJuly.Text = string.Empty;
            txtYTDAug.Text = string.Empty;
            txtYTDSep.Text = string.Empty;
            txtYTDOct.Text = string.Empty;
            txtYTDNov.Text = string.Empty;
            txtYTDDec.Text = string.Empty;
            txtYTDJan.Text = string.Empty;
            txtYTDFeb.Text = string.Empty;
            txtYTDMarch.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Fill Specialty drop down
    /// <summary>
    /// Fill Specialty drop down
    /// </summary>
    private void FillSpecialtyList()
    {
        try
        {
            ddlSpecialty.Items.Clear();
            DataSet dsSpecialtyList = GetAllSpecialty();
            if ((dsSpecialtyList != null) && (dsSpecialtyList.Tables[0] != null) && (dsSpecialtyList.Tables[0].Rows != null) && (dsSpecialtyList.Tables[0].Rows.Count > 0))
            {

                ddlSpecialty.Enabled = true;
                ListItem lioption = new ListItem("-- Select specialty --", "-1");
                ddlSpecialty.DataTextField = "Specialty";
                ddlSpecialty.DataValueField = "Id";
                ddlSpecialty.DataSource = dsSpecialtyList;
                ddlSpecialty.DataBind();
                ddlSpecialty.Items.Insert(0, lioption);
            }
            else
            {
                ListItem lioption = new ListItem("-- No specialty found --", "");
                ddlSpecialty.Items.Insert(0, lioption);
                ddlSpecialty.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get All Specialty
    /// <summary>
    /// Get All Specialty
    /// </summary>
    /// <returns>Specialty data set</returns>
    private DataSet GetAllSpecialty()
    {
        try
        {
            specialtyController = new SpecialtyController();
            DataSet dsSpecialty = specialtyController.SearchSpecialty(string.Empty, true);
            return dsSpecialty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get all KPI
    /// <summary>
    /// Get All kpi
    /// </summary>
    /// <returns>KPI data set</returns>
    private DataSet GetAllKPI()
    {
        try
        {
            kPIController = new KPIController();
            DataSet dsKPI = kPIController.GetKPIForSpecialtyDataLevel();
            return dsKPI;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Fill KPI drop down
    /// <summary>
    /// Fill the KPI drop down
    /// </summary>
    private void FillKPIList()
    {
        try
        {
            ddlKPI.Items.Clear();
            DataSet dsKPI = GetAllKPI();
            if ((dsKPI != null) && (dsKPI.Tables[0] != null) && (dsKPI.Tables[0].Rows != null) && (dsKPI.Tables[0].Rows.Count > 0))
            {

                ListItem lioption = new ListItem("-- Select KPI --", "-1");
                ddlKPI.DataTextField = "KPIDescription";
                ddlKPI.DataValueField = "Id";
                ddlKPI.DataSource = dsKPI;
                ddlKPI.DataBind();
                ddlKPI.Items.Insert(0, lioption);
            }
            else
            {
                ListItem lioption = new ListItem("-- No KPI found  --", "");
                ddlKPI.Items.Insert(0, lioption);
                ddlKPI.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Set Selected values

    /// <summary>
    /// Set the selected value in Specialty drop down
    /// </summary>
    /// <param name="specialtyId"></param>
    private void SelectSpecialty(int specialtyId)
    {
        ddlSpecialty.ClearSelection();
        ListItem liitem = ddlSpecialty.Items.FindByValue(specialtyId.ToString());

        if (liitem != null && !liitem.Selected)
        {
            liitem.Selected = true;
        }
    }

    /// <summary>
    /// Set the selected value in kpi drop down
    /// </summary>
    /// <param name="kpiId"></param>
    private void SelectKPI(int kpiId)
    {
        ddlKPI.ClearSelection();
        ListItem liitem = ddlKPI.Items.FindByValue(kpiId.ToString());

        if (liitem != null && !liitem.Selected)
        {
            liitem.Selected = true;

        }
    }

    #endregion

    #region Fill controls with selected values
    /// <summary>
    /// Fill Controls with selected values
    /// </summary>
    private void FillControlsWithSelectedValues()
    {
        try
        {
            btnComments.Visible = false;
            int kpiId = 0;
            int hospitalId = Master.NHSUser.HospitalId;
            int specialtyId = 0;

            if (ddlSpecialty.SelectedItem.Value != string.Empty)
            {
                specialtyId = Convert.ToInt32(ddlSpecialty.SelectedItem.Value);
            }

            if (ddlKPI.SelectedItem.Value != string.Empty)
            {
                kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
                KPI kpi = new KPIController().ViewKPI(kpiId);
                lblNumeratorDes.Text = kpi.NumeratorDescription != "" ? kpi.NumeratorDescription : "Numerator";
                lblDenominatorDes.Text = kpi.DenominatorDescription != "" ? kpi.DenominatorDescription : "Denominator";
                lblYTDValueDes.Text = kpi.YTDValueDescription != "" ? kpi.YTDValueDescription : "YTD Value";
                string id = "KPINo";
                string userId = "UserId";
                string access = "Access";
                btnComments.Visible = true;
                btnComments.OnClientClick = String.Format("javascript:popUp(750,550, 'Transactions','Comment.aspx?{0}={1}&{2}={3}&{4}={5}'); return false;", id, new KPIController().ViewKPI(kpiId).KPINo, userId, Master.NHSUser.Id, access, "Internal");
            }

            string finYear = lblCurentFinancialYear.Text.ToString();

            if ((specialtyId > 0) && (kpiId > 0))
            {
                FillControls(specialtyId, kpiId, hospitalId, finYear);
            }

            if (kpiId > 0)
            {
                ShowHideDenominator(kpiId);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Disable controls
    /// <summary>
    /// Disable UI controls based on the date
    /// </summary>
    private void DisableControls()
    {
        try
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
            txtGreenApril.Enabled = true;
            txtGreenMay.Enabled = true;
            txtGreenJune.Enabled = true;
            txtGreenJuly.Enabled = true;
            txtGreenAug.Enabled = true;
            txtGreenSep.Enabled = true;
            txtGreenOct.Enabled = true;
            txtGreenNov.Enabled = true;
            txtGreenDec.Enabled = true;
            txtGreenJan.Enabled = true;
            txtGreenFeb.Enabled = true;
            txtGreenMarch.Enabled = true;

            txtAmberApril.Enabled = true;
            txtAmberMay.Enabled = true;
            txtAmberJune.Enabled = true;
            txtAmberJuly.Enabled = true;
            txtAmberAug.Enabled = true;
            txtAmberSep.Enabled = true;
            txtAmberOct.Enabled = true;
            txtAmberNov.Enabled = true;
            txtAmberDec.Enabled = true;
            txtAmberJan.Enabled = true;
            txtAmberFeb.Enabled = true;
            txtAmberMarch.Enabled = true;

            txtYTDApril.Enabled = true;
            txtYTDMay.Enabled = true;
            txtYTDJune.Enabled = true;
            txtYTDJuly.Enabled = true;
            txtYTDAug.Enabled = true;
            txtYTDSep.Enabled = true;
            txtYTDOct.Enabled = true;
            txtYTDNov.Enabled = true;
            txtYTDDec.Enabled = true;
            txtYTDJan.Enabled = true;
            txtYTDFeb.Enabled = true;
            txtYTDMarch.Enabled = true;

            if (selectedFinYear == systemFinYear)
            {
                imgBtnNext.Enabled = false;

                if (month == 4)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = false;
                    txtGreenJune.Enabled = false;
                    txtGreenJuly.Enabled = false;
                    txtGreenAug.Enabled = false;
                    txtGreenSep.Enabled = false;
                    txtGreenOct.Enabled = false;

                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = false;
                    txtAmberJune.Enabled = false;
                    txtAmberJuly.Enabled = false;
                    txtAmberAug.Enabled = false;
                    txtAmberSep.Enabled = false;
                    txtAmberOct.Enabled = false;
                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = false;
                    txtYTDJune.Enabled = false;
                    txtYTDJuly.Enabled = false;
                    txtYTDAug.Enabled = false;
                    txtYTDSep.Enabled = false;
                    txtYTDOct.Enabled = false;
                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }
                if (month == 5)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = false;
                    txtGreenJuly.Enabled = false;
                    txtGreenAug.Enabled = false;
                    txtGreenSep.Enabled = false;
                    txtGreenOct.Enabled = false;

                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = false;
                    txtAmberJuly.Enabled = false;
                    txtAmberAug.Enabled = false;
                    txtAmberSep.Enabled = false;
                    txtAmberOct.Enabled = false;
                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = false;
                    txtYTDJuly.Enabled = false;
                    txtYTDAug.Enabled = false;
                    txtYTDSep.Enabled = false;
                    txtYTDOct.Enabled = false;
                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }
                if (month == 6)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = false;
                    txtGreenAug.Enabled = false;
                    txtGreenSep.Enabled = false;
                    txtGreenOct.Enabled = false;

                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = false;
                    txtAmberAug.Enabled = false;
                    txtAmberSep.Enabled = false;
                    txtAmberOct.Enabled = false;
                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = false;
                    txtYTDAug.Enabled = false;
                    txtYTDSep.Enabled = false;
                    txtYTDOct.Enabled = false;
                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }

                if (month == 7)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = false;
                    txtGreenSep.Enabled = false;
                    txtGreenOct.Enabled = false;
                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = false;
                    txtAmberSep.Enabled = false;
                    txtAmberOct.Enabled = false;

                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = false;
                    txtYTDSep.Enabled = false;
                    txtYTDOct.Enabled = false;

                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }
                if (month == 8)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = false;
                    txtGreenOct.Enabled = false;
                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = false;
                    txtAmberOct.Enabled = false;

                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = false;
                    txtYTDOct.Enabled = false;

                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }
                if (month == 9)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = false;

                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = false;

                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = false;

                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }
                if (month == 10)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;

                    txtGreenNov.Enabled = false;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;

                    txtAmberNov.Enabled = false;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;

                    txtYTDNov.Enabled = false;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }

                if (month == 11)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;

                    txtGreenNov.Enabled = true;
                    txtGreenDec.Enabled = false;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;

                    txtAmberNov.Enabled = true;
                    txtAmberDec.Enabled = false;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;

                    txtYTDNov.Enabled = true;
                    txtYTDDec.Enabled = false;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }

                if (month == 12)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;

                    txtGreenNov.Enabled = true;
                    txtGreenDec.Enabled = true;
                    txtGreenJan.Enabled = false;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;

                    txtAmberNov.Enabled = true;
                    txtAmberDec.Enabled = true;
                    txtAmberJan.Enabled = false;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;

                    txtYTDNov.Enabled = true;
                    txtYTDDec.Enabled = true;
                    txtYTDJan.Enabled = false;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;
                }
                if (month == 1)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;
                    txtGreenNov.Enabled = true;
                    txtGreenDec.Enabled = true;
                    txtGreenJan.Enabled = true;
                    txtGreenFeb.Enabled = false;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;
                    txtAmberNov.Enabled = true;
                    txtAmberDec.Enabled = true;
                    txtAmberJan.Enabled = true;
                    txtAmberFeb.Enabled = false;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;
                    txtYTDNov.Enabled = true;
                    txtYTDDec.Enabled = true;
                    txtYTDJan.Enabled = true;
                    txtYTDFeb.Enabled = false;
                    txtYTDMarch.Enabled = false;

                }

                if (month == 2)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;
                    txtGreenNov.Enabled = true;
                    txtGreenDec.Enabled = true;
                    txtGreenJan.Enabled = true;
                    txtGreenFeb.Enabled = true;
                    txtGreenMarch.Enabled = false;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;
                    txtAmberNov.Enabled = true;
                    txtAmberDec.Enabled = true;
                    txtAmberJan.Enabled = true;
                    txtAmberFeb.Enabled = true;
                    txtAmberMarch.Enabled = false;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;
                    txtYTDNov.Enabled = true;
                    txtYTDDec.Enabled = true;
                    txtYTDJan.Enabled = true;
                    txtYTDFeb.Enabled = true;
                    txtYTDMarch.Enabled = false;


                }
                if (month == 3)
                {
                    txtGreenApril.Enabled = true;
                    txtGreenMay.Enabled = true;
                    txtGreenJune.Enabled = true;
                    txtGreenJuly.Enabled = true;
                    txtGreenAug.Enabled = true;
                    txtGreenSep.Enabled = true;
                    txtGreenOct.Enabled = true;
                    txtGreenNov.Enabled = true;
                    txtGreenDec.Enabled = true;
                    txtGreenJan.Enabled = true;
                    txtGreenFeb.Enabled = true;
                    txtGreenMarch.Enabled = true;

                    txtAmberApril.Enabled = true;
                    txtAmberMay.Enabled = true;
                    txtAmberJune.Enabled = true;
                    txtAmberJuly.Enabled = true;
                    txtAmberAug.Enabled = true;
                    txtAmberSep.Enabled = true;
                    txtAmberOct.Enabled = true;
                    txtAmberNov.Enabled = true;
                    txtAmberDec.Enabled = true;
                    txtAmberJan.Enabled = true;
                    txtAmberFeb.Enabled = true;
                    txtAmberMarch.Enabled = true;

                    txtYTDApril.Enabled = true;
                    txtYTDMay.Enabled = true;
                    txtYTDJune.Enabled = true;
                    txtYTDJuly.Enabled = true;
                    txtYTDAug.Enabled = true;
                    txtYTDSep.Enabled = true;
                    txtYTDOct.Enabled = true;
                    txtYTDNov.Enabled = true;
                    txtYTDDec.Enabled = true;
                    txtYTDJan.Enabled = true;
                    txtYTDFeb.Enabled = true;
                    txtYTDMarch.Enabled = true;

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region drop down selected index change
    /// <summary>
    /// Selected Index Changed event for Ward Name drop down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillControlsWithSelectedValues();
    }

    /// <summary>
    /// Selected Index Changed event for KPI Name drop down
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
