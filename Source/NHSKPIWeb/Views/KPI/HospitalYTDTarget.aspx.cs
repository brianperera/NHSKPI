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
/// This class will handle the all code behind logic for the Hospital YTD traget aspx page
/// </summary>
public partial class Views_KPI_HospitalYTDTarget : System.Web.UI.Page
{
    #region Private Variables
    private KPIController kPIController = null;
    private int currentHospitalId;
    private HospitalController hospitalController = null;   

    #endregion

    #region Public Members
    /// <summary>
    /// Get or Set the hospital id
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

        //if the user is not super user keep the hospitla in session
        if (Master.NHSUser.RoleId > 1)
        {          
            CurrentHospitalId = Master.NHSUser.HospitalId;            
        }

        try
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onClick", "return ConfirmSaveAll();");
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

                    FillControls(CurrentHospitalId, kpiId, financailYear);
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
       
    #region Fill Controls
    /// <summary>
    /// Fill Controls
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    private void FillControls(int hospitalId, int kpiId, string financailYear)
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

        if ((hospitalId > 0) && (kpiId > 0))
        {
            DataSet dsTargets = kPIController.GetHospitalKPIYTDTarget(hospitalId, kpiId, startDate, endDate);

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
                    txtYTDTargetGreen.Text = dvTargetList[rowindex]["YTDTargetGreen"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetGreen"].ToString() : string.Empty;
                    txtYTDTargetAmber.Text = dvTargetList[rowindex]["YTDTargetAmber"].ToString() != "" ? dvTargetList[rowindex]["YTDTargetAmber"].ToString() : string.Empty;
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
            string selectedHospitalId = ddlHospitalName.SelectedItem.Value;     
           
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);

            if ((selectedHospitalId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectValues(selectedHospitalId, selectedKPIId, firstYear);

                bool success = kPIController.InsertKPIYTDTarget(kpiObject);

                if (success)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;       
                    //ClearControl();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_KPI_YTD_Target_Success_Add;
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
            string selectedHospitalId = CurrentHospitalId.ToString();
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int firstYear = Convert.ToInt32(years[0]);
            int secondYear = Convert.ToInt32(years[1]);                       

            if ((selectedHospitalId != string.Empty) && (selectedKPIId != string.Empty))
            {
                KPI kpiObject = SetKPIObjectValues(selectedHospitalId, selectedKPIId, firstYear);

                bool success = kPIController.UpdateKPIYTDTarget(kpiObject);

                if (success)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;       
                    //ClearControl();
                    lblMessage.Visible = true;
                    lblMessage.Text = Constant.MSG_YTD_KPI_Target_Success_Update;
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

    #region IsKPIYTDTargetExist
    /// <summary>
    /// Check KPI YTD Target is exist
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private bool IsKPIYTDTargetExist(int hospitalId, int kpiId, string financailYear)
    {
        DataSet dsTargets = GetHospitalKPIYTDTarget(hospitalId, kpiId, financailYear);

        if ((dsTargets != null) && (dsTargets.Tables[0] != null) && (dsTargets.Tables[0].Rows != null) && (dsTargets.Tables[0].Rows.Count > 0))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region SetTarget
    /// <summary>
    /// Set the KPI Object
    /// </summary>
    /// <param name="selectedHospitalId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="firstYear"></param>
    /// <returns></returns>
    private KPI SetKPIObjectValues(string selectedHospitalId, string selectedKPIId, int firstYear)
    {
        kPIController = new KPIController();
        KPI kpiObject = new KPI();
        if (Convert.ToInt32(selectedKPIId) > 0)
        {
            kpiObject = kPIController.ViewKPI(Convert.ToInt32(selectedKPIId));
        }

        kpiObject.TargetYTDDetailsList = new System.Collections.Generic.List<KPIHospitalYTDTarget>();
        //Add the April month details

        DateTime aprilTarget = new DateTime(firstYear, 4, 1);

        KPIHospitalYTDTarget wardAprilTarget = new KPIHospitalYTDTarget();
        wardAprilTarget.HospitalId = Convert.ToInt32(selectedHospitalId);
        wardAprilTarget.KpiId = Convert.ToInt32(selectedKPIId);
        wardAprilTarget.TargetYTD = aprilTarget;
        wardAprilTarget.TargetDescription = txtYTDTargetDescription.Text != string.Empty ? txtYTDTargetDescription.Text.ToString() : string.Empty;
        wardAprilTarget.TargetGreen = txtYTDTargetGreen.Text != string.Empty ? Convert.ToDouble(txtYTDTargetGreen.Text) : double.MinValue;
        wardAprilTarget.TargetAmber = txtYTDTargetAmber.Text != string.Empty ? Convert.ToDouble(txtYTDTargetAmber.Text) : double.MinValue;

        kpiObject.TargetYTDDetailsList.Add(wardAprilTarget);
        return kpiObject;
    }
    #endregion

    #region ClearControl
    /// <summary>
    /// Clear the all the controls
    /// </summary>
    private void ClearControl()
    {
        if (Master.NHSUser.RoleId < 2)
        {
            //txtHospitalName.Text = string.Empty;
        }
        //txtKPIDescription.Text = string.Empty;
        txtYTDTargetDescription.Text = string.Empty;
        txtYTDTargetGreen.Text = string.Empty;
        txtYTDTargetAmber.Text = string.Empty;
    }
    #endregion

    #region GetHospitalKPIYTDTarget
    /// <summary>
    /// Get the Hospital YTD data
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <param name="kpiId"></param>
    /// <param name="financailYear"></param>
    /// <returns></returns>
    private DataSet GetHospitalKPIYTDTarget(int hospitalId, int kpiId, string financailYear)
    {
        kPIController = new KPIController();
        string[] years = financailYear.Split('-');

        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

        return kPIController.GetHospitalKPIYTDTarget(hospitalId, kpiId, startDate, endDate);

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
                    ListItem lioption = new ListItem("-- All --", "0");
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
                ListItem lioption = new ListItem("-- All --", "0");
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


        FillControls(hospitalId, kpiId, finYear);
        

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
}
