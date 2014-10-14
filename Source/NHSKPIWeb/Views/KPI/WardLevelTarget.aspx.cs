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

/// <summary>
/// This class will handle the code behind logic for the Ward level target aspx page.
/// </summary>
public partial class Views_KPI_WardLevelTarget : System.Web.UI.Page
{
    #region Private Variables

    private WardController wardController = null;
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
                

                //Load the ward
                FillWardList(CurrentHospitalId);

                //Load the KPI
                FillKPIList();


                string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

                lblCurentFinancialYear.Text = currentFinYear;

                hdnUserType.Value = Master.NHSUser.RoleId.ToString();

                if ((Request.QueryString[Constant.CMN_String_KPI_Id] != null) || (Request.QueryString[Constant.CMN_String_Ward_Id] != null)
                    || (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    int wardId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_Ward_Id]);
                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;                   
                    SelectWard(wardId);
                    SelectKPI(kpiId);

                    //Set the drop down values
                    FillControls(wardId, kpiId, CurrentHospitalId, financailYear);
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
    private void FillControls(int wardId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            ClearControl();
            btnSave.Visible             = true;
            btnUpdate.Visible           = false;
            divDynamicTarget.Visible    = true;
            divButtons.Visible          = true;

            kPIController   = new KPIController();
            wardController  = new WardController();           
            string[] years  = financailYear.Split('-');

            DateTime startDate          = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate            = new DateTime(Convert.ToInt32(years[1]), 3, 31);
            lblCurentFinancialYear.Text = financailYear;

            KPI selecteKPI = ShowHideDivToEnterData(kpiId);

            if ((hospitalId > 0) && (wardId > 0) && (kpiId > 0))
            {
                DataSet dsTargets = kPIController.GetWardKPITarget(wardId, kpiId, hospitalId, startDate, endDate);

                if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
                {
                   
                    btnSave.Visible         = false;
                    btnUpdate.Visible       = true;

                    DataView dvTargetList   = new DataView(dsTargets.Tables[0]);
                    dvTargetList.Sort       = "TargetMonth";
                    int rowindex            = -1;
                    DateTime aprilMonth     = new DateTime(Convert.ToInt32(years[0]), 4, 1);
                    rowindex                = dvTargetList.Find(aprilMonth);

                    if (rowindex != -1)
                    {
                        txtDescriptionApril.Text    = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
                        txtGreenApril.Text          = dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetGreen"].ToString() : string.Empty;
                        txtAmberApril.Text          = dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["MonthlyTargetAmber"].ToString() : string.Empty;

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
                        txtDescriptionMay.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionJune.Text     = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionJuly.Text     = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionAug.Text      = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionSep.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionOct.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionNov.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionDec.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionJan.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionFeb.Text  = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                        txtDescriptionMarch.Text    = dvTargetList[rowindex]["MonthlyTargetDescription"].ToString();
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
                    btnSave.Visible     = true;
                    btnUpdate.Visible   = false;
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

    #region Get Ward KPI Target
    /// <summary>
    /// Get Ward KPI Target values
    /// </summary>
    /// <param name="wardId">id of the ward</param>
    /// <param name="kpiId"> id of the kpi</param>
    /// <param name="hospitalId">id of the hospital</param>
    /// <param name="financailYear">string finacail year like 2013-2014</param>
    /// <returns>Data set of the Ward KPI</returns>
    private DataSet GetWardKPITarget(int wardId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            kPIController       = new KPIController();
            string[] years      = financailYear.Split('-');
            DateTime startDate  = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate    = new DateTime(Convert.ToInt32(years[1]), 3, 31);
            return kPIController.GetWardKPITarget(wardId, kpiId, hospitalId, startDate, endDate);
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
            string finYear          = lblCurentFinancialYear.Text;
            string[] years          = finYear.Split('-');
            int nextfist            = Convert.ToInt32(years[0]) - 1;
            int nextsecond          = Convert.ToInt32(years[1]) - 1;
            string nextFinYear      = nextfist.ToString() + "-" + nextsecond.ToString();
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
            string finYear              = lblCurentFinancialYear.Text;
            string[] years              = finYear.Split('-');
            int nextfist                = Convert.ToInt32(years[0]) + 1;
            int nextsecond              = Convert.ToInt32(years[1]) + 1;
            string nextFinYear          = nextfist.ToString() + "-" + nextsecond.ToString();
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
            kPIController               = new KPIController();
            string selectedWardId       = ddlWardName.SelectedItem.Value;
            string selectedKPIId        = ddlKPI.SelectedItem.Value;           
            int selectedHospitalId      = CurrentHospitalId;
            string finYear              = lblCurentFinancialYear.Text;
            string[] years              = finYear.Split('-');
            int firstYear               = Convert.ToInt32(years[0]);
            int secondYear              = Convert.ToInt32(years[1]);

            if ((selectedWardId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject           = SetKPIObjectWithMonthlyTarget(selectedWardId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                bool success            = kPIController.UpdateKPITarget(kpiObject);
                if (success)
                {
                    KPI selecteKPI = ShowHideDivToEnterData(Convert.ToInt32(selectedKPIId));
                    //ClearControl();
                    lblMessage.Visible  = true;
                    imgBtnNext.Enabled  = true;
                    imgBtnPrevoius.Enabled = true;                   
                    lblMessage.Text     = Constant.MSG_Ward_KPI_Target_Success_Update;
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
            kPIController               = new KPIController();
            string selectedWardId       = ddlWardName.SelectedItem.Value;
            string selectedKPIId        = ddlKPI.SelectedItem.Value;
            int selectedHospitalId   = CurrentHospitalId;

            string finYear              = lblCurentFinancialYear.Text;
            string[] years              = finYear.Split('-');
            int firstYear               = Convert.ToInt32(years[0]);
            int secondYear              = Convert.ToInt32(years[1]);

            if ((selectedWardId != string.Empty) && (selectedKPIId != string.Empty) && (selectedHospitalId > 0))
            {                
                KPI kpiObject   = SetKPIObjectWithMonthlyTarget(selectedWardId, selectedKPIId, selectedHospitalId, firstYear, secondYear);
                bool success    = kPIController.InsertKPITarget(kpiObject);
                if (success)
                {
                    KPI selecteKPI = ShowHideDivToEnterData(Convert.ToInt32(selectedKPIId)); 
                    //ClearControl();
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    lblMessage.Visible  = true;
                    lblMessage.Text     = Constant.MSG_Ward_KPI_Target_Success_Add;
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
    
    #region Is Ward Kpi Target Exist
    /// <summary>
    /// Check Ward Kpi Target Exist
    /// </summary>
    /// <param name="wardId"></param>
    /// <param name="kpiId"></param>
    /// <param name="hospitalId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsWardKpiTargetExist(int wardId, int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            DataSet dsTargets = GetWardKPITarget(wardId, kpiId, hospitalId, financailYear);

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
    /// <param name="selectedWardId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="selectedHospitalId"></param>
    /// <param name="firstYear"></param>
    /// <param name="secondYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectWithMonthlyTarget(string selectedWardId, string selectedKPIId, int selectedHospitalId, int firstYear, int secondYear)
    {
        try
        {
            kPIController       = new KPIController();
            KPI kpiObject       = new KPI();
            if (Convert.ToInt32(selectedKPIId) > 0)
            {
                kpiObject       = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
            }

            kpiObject.TargetMonthlyDetailsList = new System.Collections.Generic.List<KPIWardMonthlyTarget>();

            //Add the April month details

            DateTime aprilTarget                    = new DateTime(firstYear, 4, 1);
            KPIWardMonthlyTarget wardAprilTarget    = new KPIWardMonthlyTarget();
            wardAprilTarget.WardId                  = Convert.ToInt32(selectedWardId);
            wardAprilTarget.KpiId                   = Convert.ToInt32(selectedKPIId);
            wardAprilTarget.HospitalId              = Convert.ToInt32(selectedHospitalId);
            wardAprilTarget.TargetMonth = aprilTarget;
            if (kpiObject.StaticTarget)
            {
                wardAprilTarget.TargetDescription   = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardAprilTarget.TargetGreen         = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardAprilTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardAprilTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardAprilTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardAprilTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
          
            }
            else
            {
                wardAprilTarget.TargetDescription   = txtDescriptionApril.Text != string.Empty ? txtDescriptionApril.Text.ToString() : string.Empty;
                wardAprilTarget.TargetGreen = txtGreenApril.Text != string.Empty ? Convert.ToDouble(txtGreenApril.Text) : double.MinValue;
                wardAprilTarget.TargetAmber = txtAmberApril.Text != string.Empty ? Convert.ToDouble(txtAmberApril.Text) : double.MinValue;

                wardAprilTarget.TargetDescriptionYTD = txtDescriptionAprilYTD.Text != string.Empty ? txtDescriptionAprilYTD.Text.ToString() : string.Empty;
                wardAprilTarget.TargetGreenYTD = txtGreenAprilYTD.Text != string.Empty ? Convert.ToDouble(txtGreenAprilYTD.Text) : double.MinValue;
                wardAprilTarget.TargetAmberYTD = txtAmberAprilYTD.Text != string.Empty ? Convert.ToDouble(txtAmberAprilYTD.Text) : double.MinValue;
            }
            kpiObject.TargetMonthlyDetailsList.Add(wardAprilTarget);

            DateTime mayTarget                      = new DateTime(firstYear, 5, 1);
            KPIWardMonthlyTarget wardMayTarget      = new KPIWardMonthlyTarget();
            wardMayTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardMayTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardMayTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardMayTarget.TargetMonth = mayTarget;
            if (kpiObject.StaticTarget)
            {
                wardMayTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardMayTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardMayTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardMayTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardMayTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardMayTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            }
            else
            {

                wardMayTarget.TargetDescription     = txtDescriptionMay.Text != string.Empty ? txtDescriptionMay.Text.ToString() : string.Empty;
                wardMayTarget.TargetGreen = txtGreenMay.Text != string.Empty ? Convert.ToDouble(txtGreenMay.Text) : double.MinValue;
                wardMayTarget.TargetAmber = txtAmberMay.Text != string.Empty ? Convert.ToDouble(txtAmberMay.Text) : double.MinValue;

                wardMayTarget.TargetDescriptionYTD = txtDescriptionMayYTD.Text != string.Empty ? txtDescriptionMayYTD.Text.ToString() : string.Empty;
                wardMayTarget.TargetGreenYTD = txtGreenMayYTD.Text != string.Empty ? Convert.ToDouble(txtGreenMayYTD.Text) : double.MinValue;
                wardMayTarget.TargetAmberYTD = txtAmberMayYTD.Text != string.Empty ? Convert.ToDouble(txtAmberMayYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardMayTarget);
            DateTime juneTarget                     = new DateTime(firstYear, 6, 1);
            KPIWardMonthlyTarget wardJuneTarget     = new KPIWardMonthlyTarget();
            wardJuneTarget.WardId                   = Convert.ToInt32(selectedWardId);
            wardJuneTarget.KpiId                    = Convert.ToInt32(selectedKPIId);
            wardJuneTarget.HospitalId               = Convert.ToInt32(selectedHospitalId);
            wardJuneTarget.TargetMonth              = juneTarget;
            if (kpiObject.StaticTarget)
            {
                wardJuneTarget.TargetDescription    = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardJuneTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardJuneTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardJuneTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardJuneTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardJuneTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
        
            }
            else
            {
                wardJuneTarget.TargetDescription    = txtDescriptionJune.Text != string.Empty ? txtDescriptionJune.Text.ToString() : string.Empty;
                wardJuneTarget.TargetGreen = txtGreenJune.Text != string.Empty ? Convert.ToDouble(txtGreenJune.Text) : double.MinValue;
                wardJuneTarget.TargetAmber = txtAmberJune.Text != string.Empty ? Convert.ToDouble(txtAmberJune.Text) : double.MinValue;

                wardJuneTarget.TargetDescriptionYTD = txtDescriptionJuneYTD.Text != string.Empty ? txtDescriptionJuneYTD.Text.ToString() : string.Empty;
                wardJuneTarget.TargetGreenYTD = txtGreenJuneYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJuneYTD.Text) : double.MinValue;
                wardJuneTarget.TargetAmberYTD = txtAmberJuneYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJuneYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardJuneTarget);

            //July
            DateTime julyTarget                     = new DateTime(firstYear, 7, 1);
            KPIWardMonthlyTarget wardJulyTarget     = new KPIWardMonthlyTarget();
            wardJulyTarget.WardId                   = Convert.ToInt32(selectedWardId);
            wardJulyTarget.KpiId                    = Convert.ToInt32(selectedKPIId);
            wardJulyTarget.HospitalId               = Convert.ToInt32(selectedHospitalId);
            wardJulyTarget.TargetMonth              = julyTarget;
            if (kpiObject.StaticTarget)
            {
                wardJulyTarget.TargetDescription    = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardJulyTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardJulyTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardJulyTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardJulyTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardJulyTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            
            }
            else
            {
                wardJulyTarget.TargetDescription    = txtDescriptionJuly.Text != string.Empty ? txtDescriptionJuly.Text.ToString() : string.Empty;
                wardJulyTarget.TargetGreen = txtGreenJuly.Text != string.Empty ? Convert.ToDouble(txtGreenJuly.Text) : double.MinValue;
                wardJulyTarget.TargetAmber = txtAmberJuly.Text != string.Empty ? Convert.ToDouble(txtAmberJuly.Text) : double.MinValue;

                wardJulyTarget.TargetDescriptionYTD = txtDescriptionJulyYTD.Text != string.Empty ? txtDescriptionJulyYTD.Text.ToString() : string.Empty;
                wardJulyTarget.TargetGreenYTD = txtGreenJulyYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJulyYTD.Text) : double.MinValue;
                wardJulyTarget.TargetAmberYTD = txtAmberJulyYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJulyYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardJulyTarget);

            //Aug
            DateTime augTarget                      = new DateTime(firstYear, 8, 1);
            KPIWardMonthlyTarget wardAugTarget      = new KPIWardMonthlyTarget();
            wardAugTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardAugTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardAugTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardAugTarget.TargetMonth               = augTarget;
            if (kpiObject.StaticTarget)
            {
                wardAugTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardAugTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardAugTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardAugTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardAugTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardAugTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
           
            }
            else
            {
                wardAugTarget.TargetDescription     = txtDescriptionAug.Text != string.Empty ? txtDescriptionAug.Text.ToString() : string.Empty;
                wardAugTarget.TargetGreen = txtGreenAug.Text != string.Empty ? Convert.ToDouble(txtGreenAug.Text) : double.MinValue;
                wardAugTarget.TargetAmber = txtAmberAug.Text != string.Empty ? Convert.ToDouble(txtAmberAug.Text) : double.MinValue;

                wardAugTarget.TargetDescriptionYTD = txtDescriptionAugYTD.Text != string.Empty ? txtDescriptionAugYTD.Text.ToString() : string.Empty;
                wardAugTarget.TargetGreenYTD = txtGreenAugYTD.Text != string.Empty ? Convert.ToDouble(txtGreenAugYTD.Text) : double.MinValue;
                wardAugTarget.TargetAmberYTD = txtAmberAugYTD.Text != string.Empty ? Convert.ToDouble(txtAmberAugYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardAugTarget);

            //Sep
            DateTime sepTarget                      = new DateTime(firstYear, 9, 1);
            KPIWardMonthlyTarget wardSepTarget      = new KPIWardMonthlyTarget();
            wardSepTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardSepTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardSepTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardSepTarget.TargetMonth               = sepTarget;
            if (kpiObject.StaticTarget)
            {
                wardSepTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardSepTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardSepTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardSepTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardSepTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardSepTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            
            }
            else
            {
                wardSepTarget.TargetDescription     = txtDescriptionSep.Text != string.Empty ? txtDescriptionSep.Text.ToString() : string.Empty;
                wardSepTarget.TargetGreen = txtGreenSep.Text != string.Empty ? Convert.ToDouble(txtGreenSep.Text) : double.MinValue;
                wardSepTarget.TargetAmber = txtAmberSep.Text != string.Empty ? Convert.ToDouble(txtAmberSep.Text) : double.MinValue;

                wardSepTarget.TargetDescriptionYTD = txtDescriptionSepYTD.Text != string.Empty ? txtDescriptionSepYTD.Text.ToString() : string.Empty;
                wardSepTarget.TargetGreenYTD = txtGreenSepYTD.Text != string.Empty ? Convert.ToDouble(txtGreenSepYTD.Text) : double.MinValue;
                wardSepTarget.TargetAmberYTD = txtAmberSepYTD.Text != string.Empty ? Convert.ToDouble(txtAmberSepYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardSepTarget);

            //Oct
            DateTime octTarget                      = new DateTime(firstYear, 10, 1);
            KPIWardMonthlyTarget wardOctTarget      = new KPIWardMonthlyTarget();
            wardOctTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardOctTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardOctTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardOctTarget.TargetMonth               = octTarget;
            if (kpiObject.StaticTarget)
            {
                wardOctTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardOctTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardOctTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardOctTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardOctTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardOctTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            
            }
            else
            {
                wardOctTarget.TargetDescription     = txtDescriptionOct.Text != string.Empty ? txtDescriptionOct.Text.ToString() : string.Empty;
                wardOctTarget.TargetGreen = txtGreenOct.Text != string.Empty ? Convert.ToDouble(txtGreenOct.Text) : double.MinValue;
                wardOctTarget.TargetAmber = txtAmberOct.Text != string.Empty ? Convert.ToDouble(txtAmberOct.Text) : double.MinValue;

                wardOctTarget.TargetDescriptionYTD = txtDescriptionOctYTD.Text != string.Empty ? txtDescriptionOctYTD.Text.ToString() : string.Empty;
                wardOctTarget.TargetGreenYTD = txtGreenOctYTD.Text != string.Empty ? Convert.ToDouble(txtGreenOctYTD.Text) : double.MinValue;
                wardOctTarget.TargetAmberYTD = txtAmberOctYTD.Text != string.Empty ? Convert.ToDouble(txtAmberOctYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardOctTarget);

            //Nov
            DateTime novTarget                      = new DateTime(firstYear, 11, 1);
            KPIWardMonthlyTarget wardNovTarget      = new KPIWardMonthlyTarget();
            wardNovTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardNovTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardNovTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardNovTarget.TargetMonth               = novTarget;
            if (kpiObject.StaticTarget)
            {
                wardNovTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardNovTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardNovTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardNovTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardNovTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardNovTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            
            }
            else
            {
                wardNovTarget.TargetDescription     = txtDescriptionNov.Text != string.Empty ? txtDescriptionNov.Text.ToString() : string.Empty;
                wardNovTarget.TargetGreen = txtGreenNov.Text != string.Empty ? Convert.ToDouble(txtGreenNov.Text) : double.MinValue;
                wardNovTarget.TargetAmber = txtAmberNov.Text != string.Empty ? Convert.ToDouble(txtAmberNov.Text) : double.MinValue;

                wardNovTarget.TargetDescriptionYTD = txtDescriptionNovYTD.Text != string.Empty ? txtDescriptionNovYTD.Text.ToString() : string.Empty;
                wardNovTarget.TargetGreenYTD = txtGreenNovYTD.Text != string.Empty ? Convert.ToDouble(txtGreenNovYTD.Text) : double.MinValue;
                wardNovTarget.TargetAmberYTD = txtAmberNovYTD.Text != string.Empty ? Convert.ToDouble(txtAmberNovYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardNovTarget);

            //Dec
            DateTime decTarget                      = new DateTime(firstYear, 12, 1);
            KPIWardMonthlyTarget wardDecTarget      = new KPIWardMonthlyTarget();
            wardDecTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardDecTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardDecTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardDecTarget.TargetMonth               = decTarget;
            if (kpiObject.StaticTarget)
            {
                wardDecTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardDecTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardDecTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardDecTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardDecTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardDecTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
           
            }
            else
            {
                wardDecTarget.TargetDescription     = txtDescriptionDec.Text != string.Empty ? txtDescriptionDec.Text.ToString() : string.Empty;
                wardDecTarget.TargetGreen = txtGreenDec.Text != string.Empty ? Convert.ToDouble(txtGreenDec.Text) : double.MinValue;
                wardDecTarget.TargetAmber = txtAmberDec.Text != string.Empty ? Convert.ToDouble(txtAmberDec.Text) : double.MinValue;

                wardDecTarget.TargetDescriptionYTD = txtDescriptionDecYTD.Text != string.Empty ? txtDescriptionDecYTD.Text.ToString() : string.Empty;
                wardDecTarget.TargetGreenYTD = txtGreenDecYTD.Text != string.Empty ? Convert.ToDouble(txtGreenDecYTD.Text) : double.MinValue;
                wardDecTarget.TargetAmberYTD = txtAmberDecYTD.Text != string.Empty ? Convert.ToDouble(txtAmberDecYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardDecTarget);

            //Jan
            DateTime janTarget                      = new DateTime(secondYear, 1, 1);
            KPIWardMonthlyTarget wardJanTarget      = new KPIWardMonthlyTarget();
            wardJanTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardJanTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardJanTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardJanTarget.TargetMonth               = janTarget;
            if (kpiObject.StaticTarget)
            {
                wardJanTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardJanTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardJanTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardJanTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardJanTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardJanTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
           
            }
            else
            {
                wardJanTarget.TargetDescription     = txtDescriptionJan.Text != string.Empty ? txtDescriptionJan.Text.ToString() : string.Empty;
                wardJanTarget.TargetGreen = txtGreenJan.Text != string.Empty ? Convert.ToDouble(txtGreenJan.Text) : double.MinValue;
                wardJanTarget.TargetAmber = txtAmberJan.Text != string.Empty ? Convert.ToDouble(txtAmberJan.Text) : double.MinValue;

                wardJanTarget.TargetDescriptionYTD = txtDescriptionJanYTD.Text != string.Empty ? txtDescriptionJanYTD.Text.ToString() : string.Empty;
                wardJanTarget.TargetGreenYTD = txtGreenJanYTD.Text != string.Empty ? Convert.ToDouble(txtGreenJanYTD.Text) : double.MinValue;
                wardJanTarget.TargetAmberYTD = txtAmberJanYTD.Text != string.Empty ? Convert.ToDouble(txtAmberJanYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardJanTarget);

            //feb
            DateTime febTarget                      = new DateTime(secondYear, 2, 1);
            KPIWardMonthlyTarget wardFebTarget      = new KPIWardMonthlyTarget();
            wardFebTarget.WardId                    = Convert.ToInt32(selectedWardId);
            wardFebTarget.KpiId                     = Convert.ToInt32(selectedKPIId);
            wardFebTarget.HospitalId                = Convert.ToInt32(selectedHospitalId);
            wardFebTarget.TargetMonth               = febTarget;
            if (kpiObject.StaticTarget)
            {
                wardFebTarget.TargetDescription     = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardFebTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardFebTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardFebTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardFebTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardFebTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
           
            }
            else
            {
                wardFebTarget.TargetDescription     = txtDescriptionFeb.Text != string.Empty ? txtDescriptionFeb.Text.ToString() : string.Empty;
                wardFebTarget.TargetGreen = txtGreenFeb.Text != string.Empty ? Convert.ToDouble(txtGreenFeb.Text) : double.MinValue;
                wardFebTarget.TargetAmber = txtAmberFeb.Text != string.Empty ? Convert.ToDouble(txtAmberFeb.Text) : double.MinValue;

                wardFebTarget.TargetDescriptionYTD = txtDescriptionFebYTD.Text != string.Empty ? txtDescriptionFebYTD.Text.ToString() : string.Empty;
                wardFebTarget.TargetGreenYTD = txtGreenFebYTD.Text != string.Empty ? Convert.ToDouble(txtGreenFebYTD.Text) : double.MinValue;
                wardFebTarget.TargetAmberYTD = txtAmberFebYTD.Text != string.Empty ? Convert.ToDouble(txtAmberFebYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardFebTarget);

            //March
            DateTime marchTarget                    = new DateTime(secondYear, 3, 1);
            KPIWardMonthlyTarget wardMarchTarget    = new KPIWardMonthlyTarget();
            wardMarchTarget.WardId                  = Convert.ToInt32(selectedWardId);
            wardMarchTarget.KpiId                   = Convert.ToInt32(selectedKPIId);
            wardMarchTarget.HospitalId              = Convert.ToInt32(selectedHospitalId);
            wardMarchTarget.TargetMonth             = marchTarget;
            if (kpiObject.StaticTarget)
            {
                wardMarchTarget.TargetDescription   = txtStaticTargetDescription.Text != string.Empty ? txtStaticTargetDescription.Text.ToString() : string.Empty;
                wardMarchTarget.TargetGreen = txtStaticTargetGreen.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreen.Text) : double.MinValue;
                wardMarchTarget.TargetAmber = txtStaticTargetAmber.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmber.Text) : double.MinValue;

                wardMarchTarget.TargetDescriptionYTD = txtStaticTargetDescriptionYTD.Text != string.Empty ? txtStaticTargetDescriptionYTD.Text.ToString() : string.Empty;
                wardMarchTarget.TargetGreenYTD = txtStaticTargetGreenYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetGreenYTD.Text) : double.MinValue;
                wardMarchTarget.TargetAmberYTD = txtStaticTargetAmberYTD.Text != string.Empty ? Convert.ToDouble(txtStaticTargetAmberYTD.Text) : double.MinValue;
            
            }
            else
            {
                wardMarchTarget.TargetDescription   = txtDescriptionMarch.Text != string.Empty ? txtDescriptionMarch.Text.ToString() : string.Empty;
                wardMarchTarget.TargetGreen = txtGreenMarch.Text != string.Empty ? Convert.ToDouble(txtGreenMarch.Text) : double.MinValue;
                wardMarchTarget.TargetAmber = txtAmberMarch.Text != string.Empty ? Convert.ToDouble(txtAmberMarch.Text) : double.MinValue;

                wardMarchTarget.TargetDescriptionYTD = txtDescriptionMarchYTD.Text != string.Empty ? txtDescriptionMarchYTD.Text.ToString() : string.Empty;
                wardMarchTarget.TargetGreenYTD = txtGreenMarchYTD.Text != string.Empty ? Convert.ToDouble(txtGreenMarchYTD.Text) : double.MinValue;
                wardMarchTarget.TargetAmberYTD = txtAmberMarchYTD.Text != string.Empty ? Convert.ToDouble(txtAmberMarchYTD.Text) : double.MinValue;
            }

            kpiObject.TargetMonthlyDetailsList.Add(wardMarchTarget);
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


        txtDescriptionApril.Text    = string.Empty;
        txtDescriptionMay.Text      = string.Empty;
        txtDescriptionJune.Text     = string.Empty;
        txtDescriptionJuly.Text     = string.Empty;
        txtDescriptionAug.Text      = string.Empty;
        txtDescriptionSep.Text      = string.Empty;
        txtDescriptionOct.Text      = string.Empty;
        txtDescriptionNov.Text      = string.Empty;
        txtDescriptionDec.Text      = string.Empty;
        txtDescriptionJan.Text      = string.Empty;
        txtDescriptionFeb.Text      = string.Empty;
        txtDescriptionMarch.Text    = string.Empty;

        txtGreenApril.Text          = string.Empty;
        txtGreenMay.Text            = string.Empty;
        txtGreenJune.Text           = string.Empty;
        txtGreenJuly.Text           = string.Empty;
        txtGreenAug.Text            = string.Empty;
        txtGreenSep.Text            = string.Empty;
        txtGreenOct.Text            = string.Empty;
        txtGreenNov.Text            = string.Empty;
        txtGreenDec.Text            = string.Empty;
        txtGreenJan.Text            = string.Empty;
        txtGreenFeb.Text            = string.Empty;
        txtGreenMarch.Text          = string.Empty;

        txtAmberApril.Text          = string.Empty;
        txtAmberMay.Text            = string.Empty;
        txtAmberJune.Text           = string.Empty;
        txtAmberJuly.Text           = string.Empty;
        txtAmberAug.Text            = string.Empty;
        txtAmberSep.Text            = string.Empty;
        txtAmberOct.Text            = string.Empty;
        txtAmberNov.Text            = string.Empty;
        txtAmberDec.Text            = string.Empty;
        txtAmberJan.Text            = string.Empty;
        txtAmberFeb.Text            = string.Empty;
        txtAmberMarch.Text          = string.Empty;

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
        txtStaticTargetGreen.Text       = string.Empty;
        txtStaticTargetAmber.Text       = string.Empty;

        txtStaticTargetDescriptionYTD.Text = string.Empty;
        txtStaticTargetGreenYTD.Text = string.Empty;
        txtStaticTargetAmberYTD.Text = string.Empty;
    }

    #endregion

    #region Get all wards for the given hospital
   /// <summary>
   /// Get the all ward for the given hospital
   /// </summary>
   /// <param name="hospitalId"></param>
   /// <returns></returns>
    private DataSet GetAllWardsForHospital(int hospitalId)
    {
        try
        {
            wardController  = new WardController();
            DataSet dsWards = wardController.GetAllWardsForHospital(hospitalId);
            return dsWards;
        }
        catch (Exception ex)
        {
            throw ex;           
        }       
    }
    #endregion

    #region Fill ward drop down
    /// <summary>
    /// Fill the ward drop down list
    /// </summary>
    /// <param name="hospitlaId"></param>
    private void FillWardList(int hospitlaId)
    {
        try
        {
            ddlWardName.Items.Clear();
            DataSet dsWardList = GetAllWardsForHospital(hospitlaId);
            if ((dsWardList != null) && (dsWardList.Tables[0] != null) && (dsWardList.Tables[0].Rows != null) && (dsWardList.Tables[0].Rows.Count > 0))
            {
                ddlWardName.Enabled         = true;
                ListItem lioption           = new ListItem("-- All --", "0");
                ddlWardName.DataTextField   = "WardName";
                ddlWardName.DataValueField  = "Id";
                ddlWardName.DataSource      = dsWardList;
                ddlWardName.DataBind();
                ddlWardName.Items.Insert(0, lioption);
            }
            else
            {
                ListItem lioption           = new ListItem("-- No ward found  --", "");
                ddlWardName.Items.Insert(0, lioption);
                ddlWardName.Enabled = false;
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
            DataSet dsKPI = kPIController.GetKPIForWardTargetLevel();
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
            DataSet dsKPI           = GetAllKPI();
            if ((dsKPI != null) && (dsKPI.Tables[0] != null) && (dsKPI.Tables[0].Rows != null) && (dsKPI.Tables[0].Rows.Count > 0))
            {
                ListItem lioption       = new ListItem("-- Select KPI --", "-1");
                ddlKPI.DataTextField    = "KPIDescription";
                ddlKPI.DataValueField   = "Id";
                ddlKPI.DataSource       = dsKPI;
                ddlKPI.DataBind();
                ddlKPI.Items.Insert(0, lioption);
            }
            else
            {
                ListItem lioption   = new ListItem("-- No KPI found  --", "-1");
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
    protected void ddlWardName_SelectedIndexChanged(object sender, EventArgs e)
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
        int wardId = 0;

        hospitalId = CurrentHospitalId;


        if (ddlWardName.SelectedItem.Value != string.Empty)
        {
            wardId = Convert.ToInt32(ddlWardName.SelectedItem.Value);
        }

        if (ddlKPI.SelectedItem.Value != string.Empty)
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
        }

        string finYear = lblCurentFinancialYear.Text.ToString();

        FillControls(wardId, kpiId, hospitalId, finYear);

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
    /// Set the Selected value for ward drop down
    /// </summary>
    /// <param name="wardId"></param>
    private void SelectWard(int wardId)
    {
        ddlWardName.ClearSelection();
        ListItem liitem = ddlWardName.Items.FindByValue(wardId.ToString());

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
