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

public partial class Views_KPI_SpecialtyLevelTarget : System.Web.UI.Page
{
    #region Private Variables

    private SpecialtyController specialtyController = null;
    private KPIController kPIController = null;
    private int currentHospitalId = 0;
    private HospitalController hospitalController = null;

    #endregion

    #region Public Properties
    /// <summary>
    /// Get or Set the hospital Id
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
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divStaticTarget.Visible = false;
            divDynamicTarget.Visible = true;
            divButtons.Visible = true;
            lblMessage.Visible = false;

            //if the user is not super user keep the hospitla in session
            if (Master.NHSUser.RoleId > 1)
            {
                CurrentHospitalId = Master.NHSUser.HospitalId;
            }

            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onClick", "return ConfirmSaveAll();");
                btnUpdate.Attributes.Add("onClick", "return ConfirmUpdate();");
                btnCopyMonthly.Attributes.Add("onClick", "return CopyMonthly();");


                //Load the Specialty
                FillSpecialtyList();

                //Load the KPI
                FillKPIList();


                string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

                lblCurentFinancialYear.Text = currentFinYear;

                hdnUserType.Value = Master.NHSUser.RoleId.ToString();

                if ((Request.QueryString[Constant.CMN_String_KPI_Id] != null) || (Request.QueryString[Constant.CMN_String_Specialty_Id] != null)
                    || (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    int specialtyId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_Specialty_Id]);
                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    SelectSpecialty(specialtyId);
                    SelectKPI(kpiId);

                    //Set the drop down values
                    FillControls(specialtyId, kpiId, CurrentHospitalId, financailYear);
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
            //Log the exception and dispaly the message to user
            throw ex;
        }
    }

    #endregion

    #region FillControls
    /// <summary>
    /// Fill the UI controls with given paramters
    /// </summary>
    /// <param name="wardId">id of the ward</param>
    /// <param name="kpiId"> id of the kpi</param>
    /// <param name="hospitalId">id of the hospital</param>
    /// <param name="financailYear">string finacail year like 2013-2014</param>
    private void FillControls(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            ClearControl();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            divDynamicTarget.Visible = true;
            divButtons.Visible = true;

            kPIController = new KPIController();
            specialtyController = new SpecialtyController();
            string[] years = financailYear.Split('-');

            DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);
            lblCurentFinancialYear.Text = financailYear;

            KPI selecteKPI = ShowHideDivToEnterData(kpiId);

            if ((hospitalId > 0) && (specialtyId > 0) && (kpiId > 0))
            {
                DataSet dsTargets = kPIController.GetSpecialtyKPITarget(specialtyId, kpiId, hospitalId, startDate, endDate);

                if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
                {

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    DataView dvTargetList = new DataView(dsTargets.Tables[0]);
                    dvTargetList.Sort = "TargetMonth";
                    int rowindex = -1;
                    DateTime aprilMonth = new DateTime(Convert.ToInt32(years[0]), 4, 1);
                    rowindex = dvTargetList.Find(aprilMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionApril.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenApril.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberApril.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionAprilYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenAprilYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberAprilYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;


                        if (selecteKPI.StaticTarget )
                        {
                            txtStaticTargetDescription.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                            txtStaticTargetGreen.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                            txtStaticTargetAmber.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                            txtStaticTargetDescriptionYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                            txtStaticTargetGreenYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                            txtStaticTargetAmberYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                        }
                    }

                    DateTime mayMonth = new DateTime(Convert.ToInt32(years[0]), 5, 1);

                    rowindex = dvTargetList.Find(mayMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionMay.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenMay.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberMay.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionMayYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenMayYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberMayYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime juneMonth = new DateTime(Convert.ToInt32(years[0]), 6, 1);

                    rowindex = dvTargetList.Find(juneMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionJune.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenJune.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberJune.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionJuneYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenJuneYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberJuneYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime julyMonth = new DateTime(Convert.ToInt32(years[0]), 7, 1);

                    rowindex = dvTargetList.Find(julyMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionJuly.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenJuly.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberJuly.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionJulyYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenJulyYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberJulyYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime augMonth = new DateTime(Convert.ToInt32(years[0]), 8, 1);

                    rowindex = dvTargetList.Find(augMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionAug.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenAug.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberAug.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionAugYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenAugYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberAugYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime sepMonth = new DateTime(Convert.ToInt32(years[0]), 9, 1);

                    rowindex = dvTargetList.Find(sepMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionSep.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenSep.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberSep.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionSepYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenSepYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberSepYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime octMonth = new DateTime(Convert.ToInt32(years[0]), 10, 1);

                    rowindex = dvTargetList.Find(octMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionOct.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenOct.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberOct.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionOctYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenOctYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberOctYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime novMonth = new DateTime(Convert.ToInt32(years[0]), 11, 1);

                    rowindex = dvTargetList.Find(novMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionNov.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenNov.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberNov.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionNovYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenNovYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberNovYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime decMonth = new DateTime(Convert.ToInt32(years[0]), 12, 1);

                    rowindex = dvTargetList.Find(decMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionDec.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenDec.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberDec.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionDecYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenDecYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberDecYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime janMonth = new DateTime(Convert.ToInt32(years[1]), 1, 1);

                    rowindex = dvTargetList.Find(janMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionJan.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenJan.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberJan.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionJanYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenJanYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberJanYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime febMonth = new DateTime(Convert.ToInt32(years[1]), 2, 1);

                    rowindex = dvTargetList.Find(febMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionFeb.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenFeb.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberFeb.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionFebYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenFebYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberFebYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

                    }

                    DateTime marchMonth = new DateTime(Convert.ToInt32(years[1]), 3, 1);

                    rowindex = dvTargetList.Find(marchMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionMarch.Text = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenMarch.Text = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberMarch.Text = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

                        txtDescriptionMarchYTD.Text = dvTargetList[rowindex]["YTDTargetDescription"].ToString();
                        txtGreenMarchYTD.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                        txtAmberMarchYTD.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;

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
        catch (Exception ex)
        {
            throw ex;
        }

    }


    #endregion

    #region ShowHideDivToEnterData

    private KPI ShowHideDivToEnterData(int kpiId)
    {
        KPI selecteKPI = null;

        if (kpiId > 0)
        {
            selecteKPI = this.GetKPI(kpiId);

            if (selecteKPI != null)
            {
                if (selecteKPI.StaticTarget )
                {
                    divStaticTarget.Visible = true;
                    divDynamicTarget.Visible = false;
                    divButtons.Visible = true;
                }
                else
                {
                    //if the data is exist display the data.
                    divStaticTarget.Visible = false;
                    divDynamicTarget.Visible = true;
                    divButtons.Visible = true;
                }
            }
        }
        return selecteKPI;
    }

    #endregion

    #region Get Specialty KPI Target
    /// <summary>
    /// Get Specialty KPI Target values
    /// </summary>
    /// <param name="specialtyId">id of the specialty</param>
    /// <param name="kpiId"> id of the kpi</param>
    /// <param name="hospitalId">id of the hospital</param>
    /// <param name="financailYear">string finacail year like 2013-2014</param>
    /// <returns>Data set of the Specialty KPI</returns>
    private DataSet GetSpecialtyKPITarget(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            kPIController = new KPIController();
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

    #region Click Event of the Prevoius Button
    /// <summary>
    /// Click Event of the Prevoius Button
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

    #region Click Event of the Next Button
    /// <summary>
    /// Click Event of the Next Button
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

    #region Click Event of the Save Button
    /// <summary>
    /// Click Event of the Save Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveTarget();
    }
    #endregion

    #region Click Event of the Update Button
    /// <summary>
    /// Click Event of the Update Button
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
            int selectedHospitalId = CurrentHospitalId;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectWithMonthlyTarget(selectedSpecialtyId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                bool success = kPIController.UpdateSpecialtyKPITarget(kpiObject);
                if (success)
                {
                    KPI selecteKPI = ShowHideDivToEnterData(Convert.ToInt32(selectedKPIId));
                    //ClearControl();
                    lblMessage.Visible = true;
                    imgBtnNext.Enabled = true;
                    imgBtnPrevoius.Enabled = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_Target_Success_Update;
                    lblMessage.CssClass = "alert-success";
                }
            }
        }
        catch (Exception ex)
        {
            //Log the exceptio nand 

            throw ex;
        }

    }
    #endregion

    #region Save Target
    /// <summary>
    /// Save Target
    /// </summary>
    private void SaveTarget()
    {
        try
        {
            kPIController = new KPIController();
            string selectedSpecialtyId = ddlSpecialty.SelectedItem.Value;
            string selectedKPIId = ddlKPI.SelectedItem.Value;
            int selectedHospitalId = CurrentHospitalId;

            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedSpecialtyId != string.Empty) && (selectedKPIId != string.Empty) && (selectedHospitalId > 0))
            {
                KPI kpiObject = SetKPIObjectWithMonthlyTarget(selectedSpecialtyId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                bool success = kPIController.InsertSpecialtyKPITarget(kpiObject);
                if (success)
                {
                    KPI selecteKPI = ShowHideDivToEnterData(Convert.ToInt32(selectedKPIId));
                    //ClearControl();
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Specialty_KPI_Target_Success_Add;
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

    #region Is Specialty Kpi Target Exist
    /// <summary>
    /// Check Specialty Kpi Target Exist
    /// </summary>
    /// <param name="specialtyId"></param>
    /// <param name="kpiId"></param>
    /// <param name="hospitalId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsSpecialtyKpiTargetExist(int specialtyId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            DataSet dsTargets = GetSpecialtyKPITarget(specialtyId, kpiId, hospitalId, financailYear);

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

    #region Set Targets
    /// <summary>
    /// Set the KPI object with values
    /// </summary>
    /// <param name="selectedSpecialtyId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="selectedHospitalId"></param>
    /// <param name="firstYear"></param>
    /// <param name="secondYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectWithMonthlyTarget(string selectedSpecialtyId, string selectedKPIId, int selectedHospitalId, int firstYear, int secondYear)
    {
        try
        {
            kPIController = new KPIController();
            KPI kpiObject = new KPI();
            if (Convert.ToInt32(selectedKPIId) > 0)
            {
                kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
            }

            kpiObject.SpecialtyTargetMonthlyList = new System.Collections.Generic.List<KPISpecialtyMonthlyTarget>();

            //Add the April month details

            DateTime aprilTarget = new DateTime(firstYear, 4, 1);
            KPISpecialtyMonthlyTarget specialtyAprilTarget = new KPISpecialtyMonthlyTarget();
            specialtyAprilTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyAprilTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyAprilTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyAprilTarget.TargetMonth = aprilTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyAprilTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyAprilTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyAprilTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyAprilTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyAprilTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyAprilTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyAprilTarget.SpecialtyTargetDescription = txtDescriptionApril.Text != string.Empty ? txtDescriptionApril.Text.ToString() : string.Empty;
                specialtyAprilTarget.SpecialtyGreen = txtGreenApril.Text != string.Empty ? Convert.ToDouble(txtGreenApril.Text) : double.MinValue;
                specialtyAprilTarget.SpecialtyAmber = txtAmberApril.Text != string.Empty ? Convert.ToDouble(txtAmberApril.Text) : double.MinValue;

                specialtyAprilTarget.TargetDescriptionYTD = txtDescriptionAprilYTD.Text != string.Empty ? txtDescriptionAprilYTD.Text.ToString() : string.Empty;
                specialtyAprilTarget.TargetGreenYTD = txtGreenAprilYTD.Text != string.Empty ? Convert.ToDouble(txtGreenAprilYTD.Text) : double.MinValue;
                specialtyAprilTarget.TargetAmberYTD = txtAmberAprilYTD.Text != string.Empty ? Convert.ToDouble(txtAmberAprilYTD.Text) : double.MinValue;
            }
            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyAprilTarget);

            DateTime mayTarget = new DateTime(firstYear, 5, 1);
            KPISpecialtyMonthlyTarget specialtyMayTarget = new KPISpecialtyMonthlyTarget();
            specialtyMayTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyMayTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyMayTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyMayTarget.TargetMonth = mayTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyMayTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyMayTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyMayTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyMayTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyMayTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyMayTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            }
            else
            {

                specialtyMayTarget.SpecialtyTargetDescription = txtDescriptionMay.Text != string.Empty ? txtDescriptionMay.Text.ToString() : string.Empty;
                specialtyMayTarget.SpecialtyGreen = txtGreenMay.Text != string.Empty ? Convert.ToDouble(txtGreenMay.Text) : double.MinValue;
                specialtyMayTarget.SpecialtyAmber = txtAmberMay.Text != string.Empty ? Convert.ToDouble(txtAmberMay.Text) : double.MinValue;

                specialtyMayTarget.TargetDescriptionYTD = txtDescriptionMayYTD.Text != string.Empty ? txtDescriptionMayYTD.Text.ToString() : string.Empty;
                specialtyMayTarget.TargetGreenYTD = txtGreenMayYTD.Text != string.Empty ? Convert.ToDouble(txtGreenMayYTD.Text) : double.MinValue;
                specialtyMayTarget.TargetAmberYTD = txtAmberMayYTD.Text != string.Empty ? Convert.ToDouble(txtAmberMayYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyMayTarget);
            DateTime juneTarget = new DateTime(firstYear, 6, 1);
            KPISpecialtyMonthlyTarget specialtyJuneTarget = new KPISpecialtyMonthlyTarget();
            specialtyJuneTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJuneTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJuneTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJuneTarget.TargetMonth = juneTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyJuneTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyJuneTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyJuneTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyJuneTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyJuneTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyJuneTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyJuneTarget.SpecialtyTargetDescription = txtDescriptionJune.Text != string.Empty ? txtDescriptionJune.Text.ToString() : string.Empty;
                specialtyJuneTarget.SpecialtyGreen = txtGreenJune.Text != string.Empty ? Convert.ToDouble(txtGreenJune.Text) : double.MinValue;
                specialtyJuneTarget.SpecialtyAmber = txtAmberJune.Text != string.Empty ? Convert.ToDouble(txtAmberJune.Text) : double.MinValue;

                specialtyJuneTarget.TargetDescriptionYTD = txtDescriptionJuneYTD.Text != string.Empty ? txtDescriptionJuneYTD.Text.ToString() : string.Empty;
                specialtyJuneTarget.TargetGreenYTD = txtGreenJuneYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJuneYTD.Text) : double.MinValue;
                specialtyJuneTarget.TargetAmberYTD = txtAmberJuneYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJuneYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyJuneTarget);

            //July
            DateTime julyTarget = new DateTime(firstYear, 7, 1);
            KPISpecialtyMonthlyTarget specialtyJulyTarget = new KPISpecialtyMonthlyTarget();
            specialtyJulyTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJulyTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJulyTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJulyTarget.TargetMonth = julyTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyJulyTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyJulyTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyJulyTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyJulyTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyJulyTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyJulyTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyJulyTarget.SpecialtyTargetDescription = txtDescriptionJuly.Text != string.Empty ? txtDescriptionJuly.Text.ToString() : string.Empty;
                specialtyJulyTarget.SpecialtyGreen = txtGreenJuly.Text != string.Empty ? Convert.ToDouble(txtGreenJuly.Text) : double.MinValue;
                specialtyJulyTarget.SpecialtyAmber = txtAmberJuly.Text != string.Empty ? Convert.ToDouble(txtAmberJuly.Text) : double.MinValue;

                specialtyJulyTarget.TargetDescriptionYTD = txtDescriptionJulyYTD.Text != string.Empty ? txtDescriptionJulyYTD.Text.ToString() : string.Empty;
                specialtyJulyTarget.TargetGreenYTD = txtGreenJulyYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJulyYTD.Text) : double.MinValue;
                specialtyJulyTarget.TargetAmberYTD = txtAmberJulyYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJulyYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyJulyTarget);

            //Aug
            DateTime augTarget = new DateTime(firstYear, 8, 1);
            KPISpecialtyMonthlyTarget specialtyAugTarget = new KPISpecialtyMonthlyTarget();
            specialtyAugTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyAugTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyAugTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyAugTarget.TargetMonth = augTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyAugTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyAugTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyAugTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyAugTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyAugTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyAugTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyAugTarget.SpecialtyTargetDescription = txtDescriptionAug.Text != string.Empty ? txtDescriptionAug.Text.ToString() : string.Empty;
                specialtyAugTarget.SpecialtyGreen = txtGreenAug.Text != string.Empty ? Convert.ToDouble(txtGreenAug.Text) : double.MinValue;
                specialtyAugTarget.SpecialtyAmber = txtAmberAug.Text != string.Empty ? Convert.ToDouble(txtAmberAug.Text) : double.MinValue;

                specialtyAugTarget.TargetDescriptionYTD = txtDescriptionAugYTD.Text != string.Empty ? txtDescriptionAugYTD.Text.ToString() : string.Empty;
                specialtyAugTarget.TargetGreenYTD = txtGreenAugYTD.Text != string.Empty ? Convert.ToDouble(txtGreenAugYTD.Text) : double.MinValue;
                specialtyAugTarget.TargetAmberYTD = txtAmberAugYTD.Text != string.Empty ? Convert.ToDouble(txtAmberAugYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyAugTarget);

            //Sep
            DateTime sepTarget = new DateTime(firstYear, 9, 1);
            KPISpecialtyMonthlyTarget specialtySepTarget = new KPISpecialtyMonthlyTarget();
            specialtySepTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtySepTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtySepTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtySepTarget.TargetMonth = sepTarget;
            if (kpiObject.StaticTarget)
            {
                specialtySepTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtySepTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtySepTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtySepTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtySepTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtySepTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtySepTarget.SpecialtyTargetDescription = txtDescriptionSep.Text != string.Empty ? txtDescriptionSep.Text.ToString() : string.Empty;
                specialtySepTarget.SpecialtyGreen = txtGreenSep.Text != string.Empty ? Convert.ToDouble(txtGreenSep.Text) : double.MinValue;
                specialtySepTarget.SpecialtyAmber = txtAmberSep.Text != string.Empty ? Convert.ToDouble(txtAmberSep.Text) : double.MinValue;

                specialtySepTarget.TargetDescriptionYTD = txtDescriptionSepYTD.Text != string.Empty ? txtDescriptionSepYTD.Text.ToString() : string.Empty;
                specialtySepTarget.TargetGreenYTD = txtGreenSepYTD.Text != string.Empty ? Convert.ToDouble(txtGreenSepYTD.Text) : double.MinValue;
                specialtySepTarget.TargetAmberYTD = txtAmberSepYTD.Text != string.Empty ? Convert.ToDouble(txtAmberSepYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtySepTarget);

            //Oct
            DateTime octTarget = new DateTime(firstYear, 10, 1);
            KPISpecialtyMonthlyTarget specialtyOctTarget = new KPISpecialtyMonthlyTarget();
            specialtyOctTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyOctTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyOctTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyOctTarget.TargetMonth = octTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyOctTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyOctTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyOctTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyOctTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyOctTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyOctTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyOctTarget.SpecialtyTargetDescription = txtDescriptionOct.Text != string.Empty ? txtDescriptionOct.Text.ToString() : string.Empty;
                specialtyOctTarget.SpecialtyGreen = txtGreenOct.Text != string.Empty ? Convert.ToDouble(txtGreenOct.Text) : double.MinValue;
                specialtyOctTarget.SpecialtyAmber = txtAmberOct.Text != string.Empty ? Convert.ToDouble(txtAmberOct.Text) : double.MinValue;

                specialtyOctTarget.TargetDescriptionYTD = txtDescriptionOctYTD.Text != string.Empty ? txtDescriptionOctYTD.Text.ToString() : string.Empty;
                specialtyOctTarget.TargetGreenYTD = txtGreenOctYTD.Text != string.Empty ? Convert.ToDouble(txtGreenOctYTD.Text) : double.MinValue;
                specialtyOctTarget.TargetAmberYTD = txtAmberOctYTD.Text != string.Empty ? Convert.ToDouble(txtAmberOctYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyOctTarget);

            //Nov
            DateTime novTarget = new DateTime(firstYear, 11, 1);
            KPISpecialtyMonthlyTarget specialtyNovTarget = new KPISpecialtyMonthlyTarget();
            specialtyNovTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyNovTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyNovTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyNovTarget.TargetMonth = novTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyNovTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyNovTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyNovTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyNovTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyNovTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyNovTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyNovTarget.SpecialtyTargetDescription = txtDescriptionNov.Text != string.Empty ? txtDescriptionNov.Text.ToString() : string.Empty;
                specialtyNovTarget.SpecialtyGreen = txtGreenNov.Text != string.Empty ? Convert.ToDouble(txtGreenNov.Text) : double.MinValue;
                specialtyNovTarget.SpecialtyAmber = txtAmberNov.Text != string.Empty ? Convert.ToDouble(txtAmberNov.Text) : double.MinValue;

                specialtyNovTarget.TargetDescriptionYTD = txtDescriptionNovYTD.Text != string.Empty ? txtDescriptionNovYTD.Text.ToString() : string.Empty;
                specialtyNovTarget.TargetGreenYTD = txtGreenNovYTD.Text != string.Empty ? Convert.ToDouble(txtGreenNovYTD.Text) : double.MinValue;
                specialtyNovTarget.TargetAmberYTD = txtAmberNovYTD.Text != string.Empty ? Convert.ToDouble(txtAmberNovYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyNovTarget);

            //Dec
            DateTime decTarget = new DateTime(firstYear, 12, 1);
            KPISpecialtyMonthlyTarget specialtyDecTarget = new KPISpecialtyMonthlyTarget();
            specialtyDecTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyDecTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyDecTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyDecTarget.TargetMonth = decTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyDecTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyDecTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyDecTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyDecTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyDecTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyDecTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyDecTarget.SpecialtyTargetDescription = txtDescriptionDec.Text != string.Empty ? txtDescriptionDec.Text.ToString() : string.Empty;
                specialtyDecTarget.SpecialtyGreen = txtGreenDec.Text != string.Empty ? Convert.ToDouble(txtGreenDec.Text) : double.MinValue;
                specialtyDecTarget.SpecialtyAmber = txtAmberDec.Text != string.Empty ? Convert.ToDouble(txtAmberDec.Text) : double.MinValue;

                specialtyDecTarget.TargetDescriptionYTD = txtDescriptionDecYTD.Text != string.Empty ? txtDescriptionDecYTD.Text.ToString() : string.Empty;
                specialtyDecTarget.TargetGreenYTD = txtGreenDecYTD.Text != string.Empty ? Convert.ToDouble(txtGreenDecYTD.Text) : double.MinValue;
                specialtyDecTarget.TargetAmberYTD = txtAmberDecYTD.Text != string.Empty ? Convert.ToDouble(txtAmberDecYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyDecTarget);

            //Jan
            DateTime janTarget = new DateTime(secondYear, 1, 1);
            KPISpecialtyMonthlyTarget specialtyJanTarget = new KPISpecialtyMonthlyTarget();
            specialtyJanTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyJanTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyJanTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyJanTarget.TargetMonth = janTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyJanTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyJanTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyJanTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyJanTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyJanTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyJanTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyJanTarget.SpecialtyTargetDescription = txtDescriptionJan.Text != string.Empty ? txtDescriptionJan.Text.ToString() : string.Empty;
                specialtyJanTarget.SpecialtyGreen = txtGreenJan.Text != string.Empty ? Convert.ToDouble(txtGreenJan.Text) : double.MinValue;
                specialtyJanTarget.SpecialtyAmber = txtAmberJan.Text != string.Empty ? Convert.ToDouble(txtAmberJan.Text) : double.MinValue;

                specialtyJanTarget.TargetDescriptionYTD = txtDescriptionJanYTD.Text != string.Empty ? txtDescriptionJanYTD.Text.ToString() : string.Empty;
                specialtyJanTarget.TargetGreenYTD = txtGreenJanYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJanYTD.Text) : double.MinValue;
                specialtyJanTarget.TargetAmberYTD = txtAmberJanYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJanYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyJanTarget);

            //feb
            DateTime febTarget = new DateTime(secondYear, 2, 1);
            KPISpecialtyMonthlyTarget specialtyFebTarget = new KPISpecialtyMonthlyTarget();
            specialtyFebTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyFebTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyFebTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyFebTarget.TargetMonth = febTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyFebTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyFebTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyFebTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyFebTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyFebTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyFebTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyFebTarget.SpecialtyTargetDescription = txtDescriptionFeb.Text != string.Empty ? txtDescriptionFeb.Text.ToString() : string.Empty;
                specialtyFebTarget.SpecialtyGreen = txtGreenFeb.Text != string.Empty ? Convert.ToDouble(txtGreenFeb.Text) : double.MinValue;
                specialtyFebTarget.SpecialtyAmber = txtAmberFeb.Text != string.Empty ? Convert.ToDouble(txtAmberFeb.Text) : double.MinValue;

                specialtyFebTarget.TargetDescriptionYTD = txtDescriptionFebYTD.Text != string.Empty ? txtDescriptionFebYTD.Text.ToString() : string.Empty;
                specialtyFebTarget.TargetGreenYTD = txtGreenFebYTD.Text != string.Empty ? Convert.ToDouble(txtGreenFebYTD.Text) : double.MinValue;
                specialtyFebTarget.TargetAmberYTD = txtAmberFebYTD.Text != string.Empty ? Convert.ToDouble(txtAmberFebYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyFebTarget);

            //March
            DateTime marchTarget = new DateTime(secondYear, 3, 1);
            KPISpecialtyMonthlyTarget specialtyMarchTarget = new KPISpecialtyMonthlyTarget();
            specialtyMarchTarget.SpecialtyId = Convert.ToInt32(selectedSpecialtyId);
            specialtyMarchTarget.KPIId = Convert.ToInt32(selectedKPIId);
            specialtyMarchTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
            specialtyMarchTarget.TargetMonth = marchTarget;
            if (kpiObject.StaticTarget)
            {
                specialtyMarchTarget.SpecialtyTargetDescription = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                specialtyMarchTarget.SpecialtyGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                specialtyMarchTarget.SpecialtyAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                specialtyMarchTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                specialtyMarchTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                specialtyMarchTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;

            }
            else
            {
                specialtyMarchTarget.SpecialtyTargetDescription = txtDescriptionMarch.Text != string.Empty ? txtDescriptionMarch.Text.ToString() : string.Empty;
                specialtyMarchTarget.SpecialtyGreen = txtGreenMarch.Text != string.Empty ? Convert.ToDouble(txtGreenMarch.Text) : double.MinValue;
                specialtyMarchTarget.SpecialtyAmber = txtAmberMarch.Text != string.Empty ? Convert.ToDouble(txtAmberMarch.Text) : double.MinValue;

                specialtyMarchTarget.TargetDescriptionYTD = txtDescriptionMarchYTD.Text != string.Empty ? txtDescriptionMarchYTD.Text.ToString() : string.Empty;
                specialtyMarchTarget.TargetGreenYTD = txtGreenMarchYTD.Text != string.Empty ? Convert.ToDouble(txtGreenMarchYTD.Text) : double.MinValue;
                specialtyMarchTarget.TargetAmberYTD = txtAmberMarchYTD.Text != string.Empty ? Convert.ToDouble(txtAmberMarchYTD.Text) : double.MinValue;
            }

            kpiObject.SpecialtyTargetMonthlyList.Add(specialtyMarchTarget);
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
    /// Clear UI controls
    /// </summary>
    private void ClearControl()
    {


        txtDescriptionApril.Text = string.Empty;
        txtDescriptionMay.Text = string.Empty;
        txtDescriptionJune.Text = string.Empty;
        txtDescriptionJuly.Text = string.Empty;
        txtDescriptionAug.Text = string.Empty;
        txtDescriptionSep.Text = string.Empty;
        txtDescriptionOct.Text = string.Empty;
        txtDescriptionNov.Text = string.Empty;
        txtDescriptionDec.Text = string.Empty;
        txtDescriptionJan.Text = string.Empty;
        txtDescriptionFeb.Text = string.Empty;
        txtDescriptionMarch.Text = string.Empty;

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

        txtDescriptionAprilYTD.Text = string.Empty;
        txtDescriptionMayYTD.Text = string.Empty;
        txtDescriptionJuneYTD.Text = string.Empty;
        txtDescriptionJulyYTD.Text = string.Empty;
        txtDescriptionAugYTD.Text = string.Empty;
        txtDescriptionSepYTD.Text = string.Empty;
        txtDescriptionOctYTD.Text = string.Empty;
        txtDescriptionNovYTD.Text = string.Empty;
        txtDescriptionDecYTD.Text = string.Empty;
        txtDescriptionJanYTD.Text = string.Empty;
        txtDescriptionFebYTD.Text = string.Empty;
        txtDescriptionMarchYTD.Text = string.Empty;

        txtGreenAprilYTD.Text = string.Empty;
        txtGreenMayYTD.Text = string.Empty;
        txtGreenJuneYTD.Text = string.Empty;
        txtGreenJulyYTD.Text = string.Empty;
        txtGreenAugYTD.Text = string.Empty;
        txtGreenSepYTD.Text = string.Empty;
        txtGreenOctYTD.Text = string.Empty;
        txtGreenNovYTD.Text = string.Empty;
        txtGreenDecYTD.Text = string.Empty;
        txtGreenJanYTD.Text = string.Empty;
        txtGreenFebYTD.Text = string.Empty;
        txtGreenMarchYTD.Text = string.Empty;

        txtAmberAprilYTD.Text = string.Empty;
        txtAmberMayYTD.Text = string.Empty;
        txtAmberJuneYTD.Text = string.Empty;
        txtAmberJulyYTD.Text = string.Empty;
        txtAmberAugYTD.Text = string.Empty;
        txtAmberSepYTD.Text = string.Empty;
        txtAmberOctYTD.Text = string.Empty;
        txtAmberNovYTD.Text = string.Empty;
        txtAmberDecYTD.Text = string.Empty;
        txtAmberJanYTD.Text = string.Empty;
        txtAmberFebYTD.Text = string.Empty;
        txtAmberMarchYTD.Text = string.Empty;

        txtStaticTargetDescription.Text = string.Empty;
        txtStaticTargetGreen.Text = string.Empty;
        txtStaticTargetAmber.Text = string.Empty;

        txtStaticTargetDescriptionYTD.Text = string.Empty;
        txtStaticTargetGreenYTD.Text = string.Empty;
        txtStaticTargetAmberYTD.Text = string.Empty;
    }

    #endregion

    #region Get all Specialty
    /// <summary>
    /// Get the all Specialty
    /// </summary>
   /// <returns></returns>
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

    #region Fill specialty drop down
    /// <summary>
    /// Fill the Specialty drop down list
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
                ListItem lioption = new ListItem("-- All --", "0");
                ddlSpecialty.DataTextField = "Specialty";
                ddlSpecialty.DataValueField = "Id";
                ddlSpecialty.DataSource = dsSpecialtyList;
                ddlSpecialty.DataBind();
                ddlSpecialty.Items.Insert(0, lioption);
            }
            else
            {
                ListItem lioption = new ListItem("-- No specialty found  --", "");
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

    #region Get all KPI
    /// <summary>
    /// Get All KPI
    /// </summary>
    /// <returns></returns>
    private DataSet GetAllKPI()
    {
        try
        {
            kPIController = new KPIController();
            DataSet dsKPI = kPIController.GetKPIForSpecialtyTargetLevel();
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
                ListItem lioption = new ListItem("-- No KPI found  --", "-1");
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

    #region drop down selected index change
    /// <summary>
    /// Selected Index Changed Ward Name drop down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillControlsWithSelectedValues();
    }


    /// <summary>
    /// Selected Index Changed KPI Name drop down
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

    #region Fill controls with selected values
    /// <summary>
    /// Fill Controls with selected values
    /// </summary>
    private void FillControlsWithSelectedValues()
    {
        int kpiId = 0;
        int hospitalId = 0;
        int specialtyId = 0;

        hospitalId = CurrentHospitalId;


        if (ddlSpecialty.SelectedItem.Value != string.Empty)
        {
            specialtyId = Convert.ToInt32(ddlSpecialty.SelectedItem.Value);
        }

        if (ddlKPI.SelectedItem.Value != string.Empty)
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
        }

        string finYear = lblCurentFinancialYear.Text.ToString();

        FillControls(specialtyId, kpiId, hospitalId, finYear);

    }
    #endregion

    #region GetHospital
    /// <summary>
    /// Get hospital
    /// </summary>
    /// <param name="hospitalId">Id of the hospital</param>
    /// <returns>Hospital object</returns>
    private Hospital GetHospital(int hospitalId)
    {
        try
        {
            hospitalController = new HospitalController();

            return hospitalController.ViewHospital(hospitalId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetKPI
    /// <summary>
    /// Get the KPI
    /// </summary>
    /// <param name="kpiId">id of the KPI</param>
    /// <returns>KPI Object</returns>
    private KPI GetKPI(int kpiId)
    {
        try
        {
            kPIController = new KPIController();

            return kPIController.ViewKPI(kpiId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Set Selected values

    /// <summary>
    /// Set the Selected value for specialtyId drop down
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
    /// Set the Selected value for kpi drop down
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
    
}
