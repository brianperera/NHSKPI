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
/// This class the will handle all the code behind functionlity for the Hospital yeart to date Data page.
/// </summary>
public partial class Views_KPI_HospitalYTDTargetData : System.Web.UI.Page
{
    #region Private Variables
   
    private KPIController kPIController = null;   
    private int currentHospitalId = 0;
    private HospitalController hospitalController = null;

    #endregion

    #region Public Properties 
    /// <summary>
    /// Get or Set Hospital Id
    /// </summary>
    public int CurrentHospitalId
    {
        get
        {
            if (Request.QueryString[Constant.CMN_String_Hospital_Id] != null)
            {
                currentHospitalId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_Hospital_Id]);
            }
            else if (ddlHospitalName.SelectedItem.Value != string.Empty)
            {
                return Convert.ToInt32(ddlHospitalName.SelectedItem.Value);
            }
            else
            {
                currentHospitalId = 0;
            }
            return currentHospitalId;
        }
        set
        {
            Session[Constant.SSN_Current_Hospital_Id] = value;

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

            //if the user is not super user keep the hospital in session
            if (Master.NHSUser.RoleId > 1)
            {               
                CurrentHospitalId = Master.NHSUser.HospitalId;
                //Need to set the text otherwise vaildation will be false.              
            }

            if (!IsPostBack)
            {
                btnUpdate.Attributes.Add("onClick", "return ConfirmUpdate();");
                //Load the hospital
                FillHospitalList();

                //Load the KPI
                FillKPIList();

                string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();
                lblCurentFinancialYear.Text = currentFinYear;              
                hdnUserType.Value = Master.NHSUser.RoleId.ToString();

                if ((Request.QueryString[Constant.CMN_String_Hospital_Id] != null) && (Request.QueryString[Constant.CMN_String_KPI_Id] != null)
                    && (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;

                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();

                    SelectHospital();

                    SelectKPI(kpiId);

                    FillControls(kpiId, CurrentHospitalId, financailYear);
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
    /// Fill the controls with given the values
    /// </summary>
    /// <param name="sepectedKpiId"></param>
    /// <param name="hospitalId"></param>
    /// <param name="financailYear"></param>
    private void FillControls(int sepectedKpiId, int hospitalId, string financailYear)
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

        if ((hospitalId > 0) && (sepectedKpiId > 0))
        {
            DataSet dsTargets = kPIController.GetHospitalYTDKPIData(sepectedKpiId, hospitalId, startDate, endDate);

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
            string selectedHospitalId = ddlHospitalName.SelectedItem.Value;


            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedKPIId != string.Empty))
            {
                if (!IsHospitalYTDKpiDataExist(Convert.ToInt32(selectedKPIId), Convert.ToInt32(selectedHospitalId), finYear))
                {
                    KPI kpiObject = SetKPIObjectWithYTDData(selectedKPIId, selectedHospitalId, firstYear, secondYear);

                    bool success = kPIController.InsertHospitalKPIData(kpiObject);

                    if (success)
                    {
                        divDynamicTarget.Visible = true;
                        divButtons.Visible = true;
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        //ClearControl();
                        lblMessage.Visible = true;
                        lblMessage.Text = Constant.MSG_Hospital_YTD_KPI_Data_Success_Add;
                        lblMessage.CssClass = "alert-success";
                    }
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;           
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
            string selectedHospitalId = CurrentHospitalId.ToString();

            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectWithYTDData(selectedKPIId, selectedHospitalId, firstYear, secondYear);

                bool success = kPIController.UpdateKPIYTDData(kpiObject);

                if (success)
                {
                    //ClearControl();
                    divDynamicTarget.Visible = true;
                    divButtons.Visible = true;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_Hospital_YTD_KPI_Data_Success_Update;
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

    #region Get Hospital YTD KPI Data
    /// <summary>
    /// Get Hospital KPI Data
    /// </summary>
    /// <param name="wardId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetHospitalYTDKPIData(int kpiId, int hospitalId, string financailYear)
    {
        try
        {
            kPIController = new KPIController();
            string[] years = financailYear.Split('-');

            DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

            return kPIController.GetHospitalYTDKPIData(kpiId, hospitalId, startDate, endDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion   

    #region Is Hospital Kpi data Exist
    /// <summary>
    /// Check the whether hospital kpi data is exist
    /// </summary>
    /// <param name="kpiId"></param>
    /// <param name="hospital"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsHospitalYTDKpiDataExist(int kpiId, int hospital, string financailYear)
    {
        try
        {
            DataSet dsTargets = GetHospitalYTDKPIData(kpiId, hospital, financailYear);

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
    /// <param name="selectedWardId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="selectedKPIType"></param>
    /// <param name="firstYear"></param>
    /// <param name="secondYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectWithYTDData(string selectedKPIId, string selectedHospitalId, int firstYear, int secondYear)
    {
        try
        {
            kPIController = new KPIController();
            KPI kpiObject = new KPI();
            if (Convert.ToInt32(selectedKPIId) > 0)
            {
                kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
            }

            kpiObject.DataYTDDetailsList = new System.Collections.Generic.List<KPIHospitalYTDData>();

            //Add the April month details

            DateTime aprilTarget = new DateTime(firstYear, 4, 1);

            KPIHospitalYTDData wardAprilData = new KPIHospitalYTDData();          
            wardAprilData.KpiId = Convert.ToInt32(selectedKPIId);
            wardAprilData.HospitalId = Convert.ToInt32(selectedHospitalId);
            wardAprilData.TargetYearToDate = aprilTarget;
            wardAprilData.Nominator = txtGreenApril.Text != string.Empty ? Convert.ToDouble(txtGreenApril.Text) : double.MinValue;
            wardAprilData.Denominator = txtAmberApril.Text != string.Empty ? Convert.ToDouble(txtAmberApril.Text) : double.MinValue;
            kpiObject.DataYTDDetailsList.Add(wardAprilData);            
       
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

    #region Fill hospital drop down
    /// <summary>
    /// Fill the hospital drop down
    /// </summary>
    private void FillHospitalList()
    {

        try
        {
            ddlHospitalName.Items.Clear();
            if (Master.NHSUser.RoleId > 1)
            {
                //Hospitla admin has login.
                hospitalController = new HospitalController();
                Hospital userHospital = hospitalController.ViewHospital(Master.NHSUser.HospitalId);

                if (userHospital != null)
                {
                    ListItem lioption = new ListItem(userHospital.HospitalName, userHospital.Id.ToString());
                    ddlHospitalName.Items.Insert(0, lioption);
                    ddlHospitalName.Enabled = false;
                }
            }
            else
            {
                //Super user login
                DataSet dsHosList = GetAllHospital();

                if ((dsHosList != null) && (dsHosList.Tables[0] != null) && (dsHosList.Tables[0].Rows != null) && (dsHosList.Tables[0].Rows.Count > 0))
                {
                    ListItem lioption = new ListItem("-- Select Hospital --", "-1");
                    ddlHospitalName.DataTextField = "Name";
                    ddlHospitalName.DataValueField = "Id";
                    ddlHospitalName.DataSource = dsHosList;
                    ddlHospitalName.DataBind();
                    ddlHospitalName.Items.Insert(0, lioption);
                }
                else
                {
                    ListItem lioption = new ListItem("-- No Hospital found  --", "");
                    ddlHospitalName.Items.Insert(0, lioption);
                    ddlHospitalName.Enabled = false;

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get all hospital
    /// <summary>
    /// Get the all hospital
    /// </summary>
    /// <returns></returns>
    private DataSet GetAllHospital()
    {
        try
        {
            hospitalController = new HospitalController();
            DataSet dsHospital = hospitalController.SearchHospital(string.Empty, string.Empty, true, 0);
            return dsHospital;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get all KPI
    /// <summary>
    /// Get all KPI
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
    /// Fill KPI drop down
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
    /// Set the selected value in hospital drop down
    /// </summary>
    private void SelectHospital()
    {
        ddlHospitalName.ClearSelection();
        ListItem liitem = ddlHospitalName.Items.FindByValue(CurrentHospitalId.ToString());

        if (liitem != null && !liitem.Selected)
        {
            liitem.Selected = true;
        }
    }

    /// <summary>
    /// Set the selected value in KPI drop down
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
    /// Fill the UI controls with the drop down selected values
    /// </summary>
    private void FillControlsWithSelectedValues()
    {
        int kpiId = 0;
        int hospitalId = 0;

        if (ddlHospitalName.SelectedItem.Value != string.Empty)
        {
            hospitalId = Convert.ToInt32(ddlHospitalName.SelectedItem.Value);
        }

        if (ddlKPI.SelectedItem.Value != string.Empty)
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
        }

        string finYear = lblCurentFinancialYear.Text.ToString();


        if ((hospitalId > 0) && (kpiId > 0))
        {
            FillControls(kpiId, hospitalId, finYear);
        }

        if (kpiId > 0)
        {
            ShowHideDenominator(kpiId);
        }

    }
    #endregion

    #region drop down selected index change
    /// <summary>
    /// ddlHospitalName_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlHospitalName_SelectedIndexChanged(object sender, EventArgs e)
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
