﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
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
using System.Text;

public partial class Views_KPI_WardLevelTargetBulk : System.Web.UI.Page
{
    #region Private Variables

    private WardController wardController = null;
    private KPIController kPIController = null;
    private HospitalController hospitalController = null;
    private string wardIdList = string.Empty;

    #endregion

    #region Public Properties
    /// <summary>
    /// Get the hospital Id
    /// </summary>
    public int CurrentHospitalId
    {
        get
        {
            return Master.NHSUser.HospitalId;
        }
    }

    public int CurrentKPIId
    {
        get
        {
            if (ddlKPI.SelectedItem.Value != string.Empty)
            {
              return Convert.ToInt32(ddlKPI.SelectedItem.Value);
            }
            return 0;
        }
    }

    public DateTime CurrentStartDate
    {
        get
        {
            if (lblCurentFinancialYear.Text != string.Empty)
            {
                string[] years = lblCurentFinancialYear.Text.Split('-');

                return new DateTime(Convert.ToInt32(years[0]), 4, 1);
                
               
            }
            return DateTime.MinValue;
        }
    }

    public DateTime CurrentEndDate
    {
        get
        {
            if (lblCurentFinancialYear.Text != string.Empty)
            {
                string[] years = lblCurentFinancialYear.Text.Split('-');               
                return new DateTime(Convert.ToInt32(years[1]), 3, 31);

            }
            return DateTime.MinValue;
        }
    }

    public DataSet CurrentTargets
    {
        get
        {
            if (Session[Constant.SSN_Current_Targets] != null)
            {
                return (DataSet)Session[Constant.SSN_Current_Targets];
            }
            else
            {
                DataSet dsTargets = kPIController.GetWardKPITarget(0, 0, CurrentHospitalId, CurrentStartDate, CurrentEndDate);
                Session[Constant.SSN_Current_Targets] = dsTargets;
                return dsTargets;
            }
        }
        set
        {
            Session[Constant.SSN_Current_Targets] = value;
 
        }
    }


    #endregion

    #region Page Load
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            divDynamicTarget.Visible = true;
            divButtons.Visible = true;
            

            AjaxPro.Utility.RegisterTypeForAjax(typeof(Views_KPI_WardLevelTargetBulk));
            
            if (!IsPostBack)
            {
                string wardIds = GetAllWardsIdListForHospital(Master.NHSUser.HospitalId);
                btnSaveUp.Attributes.Add("onClick", "return ValidateControls('" + wardIds + "');");
                btnSaveBottom.Attributes.Add("onClick", "return ValidateControls('" + wardIds + "');");

                //Empty the session target values
                CurrentTargets = null;     

                hdnWardIdList.Value = wardIds;
                hdnHospitalId.Value = Master.NHSUser.HospitalId.ToString();

                //Load the KPI
                FillKPIList();

                string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

                lblCurentFinancialYear.Text = currentFinYear;
                hdnYear.Value = currentFinYear;
                if ((Request.QueryString[Constant.CMN_String_KPI_Id] != null) && (Request.QueryString[Constant.CMN_String_Financail_Year] != null))
                {                    
                    int kpiId = Convert.ToInt32(Request.QueryString[Constant.CMN_String_KPI_Id]);
                    hdnKPIId.Value = kpiId.ToString();
                    string financailYear = Request.QueryString[Constant.CMN_String_Financail_Year].ToString();
                    lblCurentFinancialYear.Text = financailYear;
                    btnSaveUp.Visible = true;
                    btnSaveBottom.Visible = true;

                    SelectKPI(kpiId);

                    //Generatet the control set
                    GenerateControls();

                    //Set the drop down values
                    //FillControls(wardId, kpiId, CurrentHospitalId, financailYear);
                }
                else
                {
                    //ClearControl();
                    //btnSaveUp.Visible = true;
                    //btnSaveBottom.Visible = true;          
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

    #region Generate Controls
    /// <summary>
    /// Generate the controls
    /// </summary>
    private void GenerateControls()
    {
        try
        {
           
            DataSet dsWards = GetAllWardsForHospital(Master.NHSUser.HospitalId);
            if ((dsWards != null) && (dsWards.Tables[0] != null) && (dsWards.Tables[0].Rows != null) && (dsWards.Tables[0].Rows.Count > 0))
            {
                DataView dvWardList = new DataView(dsWards.Tables[0]);
                dvWardList.Sort = "Id";

                for (int i = 0; i < dsWards.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dsWards.Tables[0].Rows[i];
                    int wardId = Convert.ToInt32(dr["Id"]);

                    Ward ward = GetWard(wardId);
                    if (ward != null)
                    {
                        KPI selectedKPI = GetSelectedKPI();
                       
                        if (selectedKPI != null)
                        {                                                      

                            Panel divMain = new Panel();
                            divMain.Attributes.Add("class", "grid_24");

                            Panel divTarget = new Panel();
                            divTarget.Attributes.Add("class", "grid_24");
                            string divTargetId = wardId + "divTarget";
                            divTarget.ID = divTargetId;

                            Panel divWard = new Panel();
                            divWard.Attributes.Add("class", "grid_24");

                            Panel divWardImage = new Panel();
                            divWardImage.Attributes.Add("class", "grid_1");
                            HtmlAnchor a1 = new HtmlAnchor();

                            Image plusImage = new Image();
                            string plusId = wardId.ToString() + "plusImage";

                            Image minusImage = new Image();
                            string minusId = wardId.ToString() + "minusImage";

                            plusImage.ID = plusId;
                            plusImage.Attributes.Add("onclick", "return showhide('" + plusId + "', '" + minusId + "','" + divTargetId + "','plus');");
                            plusImage.Attributes.Add("src", "../../assets/images/minus.png");
                            a1.Controls.Add(plusImage);

                            minusImage.ID = minusId;
                            minusImage.Attributes.Add("onclick", "return showhide('" + plusId + "', '" + minusId + "','" + divTargetId + "','minus');");
                            minusImage.Attributes.Add("src", "../../assets/images/plus.png");
                            minusImage.Attributes.Add("style", "display:none");
                            a1.Controls.Add(minusImage);
                            divWardImage.Controls.Add(a1);

                            Panel divWardName = new Panel();
                            divWardName.Attributes.Add("class", "grid_8");
                            Label lblWardTitle = new Label();
                            lblWardTitle.Text = ward.WardName;
                            lblWardTitle.Attributes.Add("class", "ward-heading");
                            divWardName.Controls.Add(lblWardTitle);

                            divWard.Controls.Add(divWardImage);
                            divWard.Controls.Add(divWardName);

                            divMain.Controls.Add(divWard);

                            Panel divSpace1 = new Panel();
                            divSpace1.Attributes.Add("class", "clear Hgap10");
                            divMain.Controls.Add(divSpace1);

                            Panel divWardMonthly = new Panel();
                            divWardMonthly.Attributes.Add("class", "grid_24");
                            Label lblWardMonthlyTitle = new Label();
                            if (selectedKPI.StaticTarget)
                            {
                                lblWardMonthlyTitle.Text = "Monthly Static Target";
                            }
                            else
                            {
                                lblWardMonthlyTitle.Text = "Monthly Dynamic Target";
                            }
                            lblWardMonthlyTitle.Attributes.Add("class", "h3-text");
                            divWardMonthly.Controls.Add(lblWardMonthlyTitle);

                            divTarget.Controls.Add(divWardMonthly);
                            //divTarget.Controls.Add(divWardMonthly);

                            CreateTextBoxControls(wardId, selectedKPI, "Monthly", divTarget);

                            Panel divSpace2 = new Panel();
                            divSpace2.Attributes.Add("class", "clear Hgap10");
                            divTarget.Controls.Add(divSpace2);

                            Panel divWardYTD = new Panel();
                            divWardYTD.Attributes.Add("class", "grid_24");
                            Label lblWardYTDTitle = new Label();
                            if (selectedKPI.StaticTarget)
                            {
                                lblWardYTDTitle.Text = "YTD Static Target";
                            }
                            else
                            {
                                lblWardYTDTitle.Text = "YTD Dynamic Target";
                            }
                            lblWardYTDTitle.Attributes.Add("class", "h3-text");
                            divWardYTD.Controls.Add(lblWardYTDTitle);
                            divTarget.Controls.Add(divWardYTD);
                            //divTarget.Controls.Add(divWardYTD);

                            CreateTextBoxControls(wardId, selectedKPI, "YTD", divTarget);

                            divMain.Controls.Add(divTarget);

                            Panel divbreak = new Panel();
                            divbreak.Attributes.Add("class", "clear");
                            divMain.Controls.Add(divbreak);

                            Panel divSpace = new Panel();
                            divSpace.Attributes.Add("class", "Hgap15");
                            divMain.Controls.Add(divSpace);

                            divDynamicTarget.Controls.Add(divMain);
                        }
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

    #region Create text box controls
    /// <summary>
    /// Create text box controls
    /// </summary>
    /// <param name="wardId"></param>
    /// <param name="selectedKPI"></param>
    /// <param name="months"></param>
    /// <param name="type"></param>
    /// <param name="divMain"></param>
    private void CreateTextBoxControls(int wardId, KPI selectedKPI,  string type, Panel divMain)
    {
        try
        {
            string[] months = null;
            if (selectedKPI.StaticTarget)
            {
                months = new string[] { "Apr" };
            }
            else
            {
                months = new string[] { "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar" };
            }

            Panel divMonth = new Panel();
            divMonth.Attributes.Add("class", "grid_24");
            Panel divMonthTitle = new Panel();
            divMonthTitle.Attributes.Add("class", "grid_3");
            Label lblMonthTitle = new Label();
            lblMonthTitle.Text = "";
            divMonthTitle.Controls.Add(lblMonthTitle);
            divMonth.Controls.Add(divMonthTitle);

            for (int j = 0; j < months.Count(); j++)
            {
                Panel divDes = new Panel();
                divDes.Attributes.Add("class", "grid_1 center");
                Label monthlable = new Label();
                //monthlable.Attributes.Add("class", "month-lable");
                if (selectedKPI.StaticTarget)
                {
                    monthlable.Text = string.Empty;
                }
                else
                {
                    monthlable.Text = months[j].ToString();
                }
                divDes.Controls.Add(monthlable);
                divMonth.Controls.Add(divDes);
            }
            divMain.Controls.Add(divMonth);

            //Gernerate the description text boxes
            Panel divDescrption = new Panel();
            divDescrption.Attributes.Add("class", "grid_24");
            Panel divDescrptionTitle = new Panel();
            divDescrptionTitle.Attributes.Add("class", "grid_3");
            Label lblDescrptionTitle = new Label();
            lblDescrptionTitle.Text = "Target Descrption";
            divDescrptionTitle.Controls.Add(lblDescrptionTitle);
            divDescrption.Controls.Add(divDescrptionTitle);
            for (int x = 0; x < months.Count(); x++)
            {
                string description = GetTextBoxValue(wardId, selectedKPI.Id, type, x, "TargetDescription");

                Panel desCell = new Panel();
                desCell.Attributes.Add("class", "grid_1");
                TextBox descriptionTextBox = new TextBox();
                descriptionTextBox.ID = wardId + months[x] + "txtDescription" + type;
                descriptionTextBox.Text = description;
                descriptionTextBox.MaxLength = 50;
                desCell.Controls.Add(descriptionTextBox);
                divDescrption.Controls.Add(desCell);
            }
            divMain.Controls.Add(divDescrption);


            //Gernerate the Target Green text boxes
            Panel divTargetGreen = new Panel();
            divTargetGreen.Attributes.Add("class", "grid_24");
            Panel divTargetGreenTitle = new Panel();
            divTargetGreenTitle.Attributes.Add("class", "grid_3");
            Label lblTargetGreenTitle = new Label();
            lblTargetGreenTitle.Text = "Target Green";
            divTargetGreenTitle.Controls.Add(lblTargetGreenTitle);
            divTargetGreen.Controls.Add(divTargetGreenTitle);
            for (int x = 0; x < months.Count(); x++)
            {
                string greenTarget = GetTextBoxValue(wardId, selectedKPI.Id, type, x, "TargetGreen");

                Panel greenCell = new Panel();
                greenCell.Attributes.Add("class", "grid_1");
                TextBox greenTextBox = new TextBox();
                string textBoxId = wardId + months[x] + "txtTargetGreen" + type;
                greenTextBox.ID = textBoxId;
                greenTextBox.Text = greenTarget;
                greenTextBox.MaxLength = 10;
                greenTextBox.Attributes.Add("onkeypress", "return isDecimalKey(event);");
                greenCell.Controls.Add(greenTextBox);

                Panel divGreenCellRev = new Panel();
                divGreenCellRev.ID = wardId + months[x] + "divTargetGreen" + type;
                divGreenCellRev.Attributes.Add("class", "input-validation-error");
                greenCell.Controls.Add(divGreenCellRev);

                divTargetGreen.Controls.Add(greenCell);
            }
            divMain.Controls.Add(divTargetGreen);

            //Gernerate the Target Amber text boxes
            Panel divTargetAmber = new Panel();
            divTargetAmber.Attributes.Add("class", "grid_24");
            Panel divTargetAmberTitle = new Panel();
            divTargetAmberTitle.Attributes.Add("class", "grid_3");
            Label lblTargetAmberTitle = new Label();
            lblTargetAmberTitle.Text = "Target Amber";
            divTargetAmberTitle.Controls.Add(lblTargetAmberTitle);
            divTargetAmber.Controls.Add(divTargetAmberTitle);
            for (int x = 0; x < months.Count(); x++)
            {
                string amberTarget = GetTextBoxValue(wardId, selectedKPI.Id, type, x, "TargetAmber");
                Panel amberCell = new Panel();
                amberCell.Attributes.Add("class", "grid_1");
                TextBox amberTextBox = new TextBox();
                amberTextBox.ID = wardId + months[x] + "txtTargetAmber" + type;
                amberTextBox.Text = amberTarget;
                amberTextBox.MaxLength = 10;
                amberTextBox.Attributes.Add("onkeypress", "return isDecimalKey(event);");
                amberCell.Controls.Add(amberTextBox);

                Panel divAmberCellRev = new Panel();
                divAmberCellRev.ID = wardId + months[x] + "divTargetAmber" + type;
                divAmberCellRev.Attributes.Add("class", "input-validation-error");
                amberCell.Controls.Add(divAmberCellRev);

                divTargetAmber.Controls.Add(amberCell);
            }
            divMain.Controls.Add(divTargetAmber);
        }
        catch (Exception ex)
        {            
            throw;
        }
    }   

    #endregion

    #region Get the text values
    /// <summary>
    /// Get the values from data set the set the values to text box controls
    /// </summary>
    /// <param name="wardId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="type"></param>
    /// <param name="x"></param>
    /// <param name="dataType"></param>
    /// <returns></returns>
    private string GetTextBoxValue(int wardId, int selectedKPIId, string type, int x, string dataType)
    {
        string[] years = lblCurentFinancialYear.Text.Split('-');

        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

        //Get the data for selected ward and KPI
        DataSet dsTargets = CurrentTargets;

        //Filter the required data.
        DataTable dtTarget = GetFilteredTable(dsTargets.Tables[0], "WardId=" + wardId + " AND KPIId=" + selectedKPIId);

        DataView dvTargetList = new DataView(dtTarget);
        dvTargetList.Sort = "TargetMonth";
        int rowindex = -1;

        DateTime targetMonth = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        if (x < 9)
        {
            int month = x + 4;
            DateTime tMonth = new DateTime(Convert.ToInt32(years[0]), month, 1);
            targetMonth = tMonth;
        }
        else
        {
            int month = x - 8;
            DateTime tMonth = new DateTime(Convert.ToInt32(years[1]), month, 1);
            targetMonth = tMonth;
        }

        rowindex = dvTargetList.Find(targetMonth);
        string description = string.Empty;
        if (rowindex != -1)
        {
            if (type == "Monthly")
            {
                string rowdatatype = "Monthly" + dataType;
                description = dvTargetList[rowindex][rowdatatype].ToString();
            }
            else if (type == "YTD")
            {
                string rowdatatype = "YTD" + dataType;
                description = dvTargetList[rowindex][rowdatatype].ToString();
            }
            else
            {
                description = string.Empty;
            }
        }
        return description;
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
            CurrentTargets = null;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) - 1;
            int nextsecond = Convert.ToInt32(years[1]) - 1;
            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();
            lblCurentFinancialYear.Text = nextFinYear;
            hdnYear.Value = nextFinYear;
            //ClearControl();
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
            CurrentTargets = null;
            string finYear = lblCurentFinancialYear.Text;
            string[] years = finYear.Split('-');
            int nextfist = Convert.ToInt32(years[0]) + 1;
            int nextsecond = Convert.ToInt32(years[1]) + 1;
            string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();
            lblCurentFinancialYear.Text = nextFinYear;
            hdnYear.Value = nextFinYear;
            //ClearControl();
            FillControlsWithSelectedValues();
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
            wardController = new WardController();
            DataSet dsWards = wardController.GetAllWardsForHospital(hospitalId);
            return dsWards;
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
    /// Selected Index Changed KPI Name drop down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlKPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //GenerateControls();
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

        hospitalId = CurrentHospitalId;
        btnSaveUp.Visible = false;
        btnSaveBottom.Visible = false;

        if (ddlKPI.SelectedItem.Value != "-1")
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
            btnSaveUp.Visible = true;
            btnSaveBottom.Visible = true;
        }

        string finYear = lblCurentFinancialYear.Text.ToString();

        KPI selectedKPI = GetSelectedKPI();
        if (selectedKPI != null)
        {
            hdnKPIId.Value = selectedKPI.Id.ToString();
            if (selectedKPI.StaticTarget)
            {
                hdnKPIType.Value = "static";
            }
            else
            {
                hdnKPIType.Value = "dynamic";
            }
        }

        //FillControls(wardId, kpiId, hospitalId, finYear);
        GenerateControls();

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

    #region Save click event
    protected void btnSave_Click(object sender, EventArgs e)
    {
                
    }
    #endregion

    #region Get all wards for the given hospital
    /// <summary>
    /// Get the all ward for the given hospital
    /// </summary>
    /// <param name="hospitalId"></param>
    /// <returns></returns>
    private string GetAllWardsIdListForHospital(int hospitalId)
    {
        try
        {
            StringBuilder sb = new StringBuilder();


            wardController = new WardController();
            DataSet dsWards = wardController.GetAllWardsForHospital(hospitalId);
            if ((dsWards != null) && (dsWards.Tables[0] != null) && (dsWards.Tables[0].Rows != null) && (dsWards.Tables[0].Rows.Count > 0))
            {
                string[] idList = new string[dsWards.Tables[0].Rows.Count];

                for (int i = 0; i < dsWards.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dsWards.Tables[0].Rows[i];
                    if (i == dsWards.Tables[0].Rows.Count - 1)
                    {
                        sb.Append(dr["Id"].ToString());
                    }
                    else
                    {
                        sb.Append(dr["Id"].ToString()).Append(",");
                    }
                }

                return sb.ToString();
            }
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Get the selected the KPI
    /// <summary>
    ///Get the selected KPI
    /// </summary>
    private KPI GetSelectedKPI()
    {
        int kpiId = 0;
        KPI selectedKPI = null;

        if (ddlKPI.SelectedItem.Value != string.Empty)
        {
            kpiId = Convert.ToInt32(ddlKPI.SelectedItem.Value);
        }
        if (kpiId > 0)
        {
            selectedKPI = GetKPI(kpiId);
        }
        return selectedKPI;

    }
    #endregion

    #region Get Ward

    private Ward GetWard(int wardId)
    {
        wardController = new WardController();
        return wardController.ViewWards(wardId);
    }
    #endregion

    #region GetFilteredTable
    /// <summary>
    /// Get the data table from the data set for given filter criteria
    /// </summary>
    /// <param name="sourceTable"></param>
    /// <param name="selectFilter"></param>
    /// <returns></returns>
    public static DataTable GetFilteredTable(DataTable sourceTable, string selectFilter)
    {
        var filteredTable = sourceTable.Clone();
        var rows = sourceTable.Select(selectFilter);
        foreach (DataRow row in rows)
        {
            filteredTable.ImportRow(row);
        }
        return filteredTable;
    }
    #endregion

    #region Save Bulk Target
    [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
    public bool SaveBulkTarget(string[] monthlyDescription, string[] monthlyGreen, string[] monthlyAmber,string[] ytdDescription, string[] ytdGreen, string[] ytdAmber, string hospitalId, string kpiId, string year, string kpiType)
    {
        kPIController = new KPIController();
        KPI kpiObject = new KPI();
        kpiObject.TargetMonthlyDetailsList = new System.Collections.Generic.List<KPIWardMonthlyTarget>();

        DateTime targetMonth = new DateTime(int.Parse(year.Split('-')[0].ToString()), 3, 1);
        int yearFlag = 1;
        for (int i = 0; i < monthlyDescription.Length; i++)
        {
            KPIWardMonthlyTarget wardTarget = new KPIWardMonthlyTarget();
            wardTarget.TargetDescription = monthlyDescription[i].Split('_')[2].ToString();
            wardTarget.TargetGreen = monthlyGreen[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(monthlyGreen[i].Split('_')[2].ToString());
            wardTarget.TargetAmber = monthlyAmber[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(monthlyAmber[i].Split('_')[2].ToString());

            wardTarget.TargetDescriptionYTD = ytdDescription[i].Split('_')[2].ToString();
            wardTarget.TargetGreenYTD = ytdGreen[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(ytdGreen[i].Split('_')[2].ToString());
            wardTarget.TargetAmberYTD = ytdAmber[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(ytdAmber[i].Split('_')[2].ToString());

            wardTarget.WardId = int.Parse(monthlyDescription[i].Split('_')[0].ToString());
            wardTarget.KpiId = int.Parse(kpiId);
            wardTarget.HospitalId = int.Parse(hospitalId); 
            wardTarget.TargetMonth = targetMonth.AddMonths(1);

            targetMonth = targetMonth.AddMonths(1);
            kpiObject.TargetMonthlyDetailsList.Add(wardTarget);

            
            if (yearFlag == 12)
            {
                yearFlag = 1;
                targetMonth = targetMonth.AddYears(-1);
            }
            else
            {
                yearFlag++;
            }

            
        }

        string[] years = year.Split('-');

        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

        kPIController.InsertBulkWardTarget(kpiObject, startDate, endDate);
        CurrentTargets = null;
        return true;

    }

    #endregion

    

}




