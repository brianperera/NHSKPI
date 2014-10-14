using System;
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

public partial class Views_KPI_SpecialtyLevelDataBulk : System.Web.UI.Page
{
    #region Private Variables

    private SpecialtyController specialtyController = null;
    private KPIController kPIController = null;
    private HospitalController hospitalController = null;
    private string specialtyIdList = string.Empty;

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

    public DataSet CurrentDatas
    {
        get
        {
            if (Session[Constant.SSN_Current_Datas] != null)
            {
                return (DataSet)Session[Constant.SSN_Current_Datas];
            }
            else
            {
                DataSet dsDatas = kPIController.GetSpecialtyKPIData(0, 0, CurrentHospitalId, CurrentStartDate, CurrentEndDate);
                Session[Constant.SSN_Current_Datas] = dsDatas;
                return dsDatas;
            }
        }
        set
        {
            Session[Constant.SSN_Current_Datas] = value;

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


            AjaxPro.Utility.RegisterTypeForAjax(typeof(Views_KPI_SpecialtyLevelDataBulk));

            if (!IsPostBack)
            {
                string specialtyIds = GetAllSpecialtyIdList();
                btnSaveUp.Attributes.Add("onClick", "return ValidateControls('" + specialtyIds + "');");
                btnSaveBottom.Attributes.Add("onClick", "return ValidateControls('" + specialtyIds + "');");

                //Empty the session target values
                CurrentDatas = null;

                hdnSpecialtyIdList.Value = specialtyIds;
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

            DataSet dsSpecialty = GetAllSpecialty();
            if ((dsSpecialty != null) && (dsSpecialty.Tables[0] != null) && (dsSpecialty.Tables[0].Rows != null) && (dsSpecialty.Tables[0].Rows.Count > 0))
            {
                DataView dvSpecialtyList = new DataView(dsSpecialty.Tables[0]);
                dvSpecialtyList.Sort = "Id";

                for (int i = 0; i < dsSpecialty.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dsSpecialty.Tables[0].Rows[i];
                    int specialtyId = Convert.ToInt32(dr["Id"]);

                    Specialty specialty = GetSpecialty(specialtyId);
                    if (specialty != null)
                    {
                        KPI selectedKPI = GetSelectedKPI();

                        if (selectedKPI != null)
                        {

                            Panel divMain = new Panel();
                            divMain.Attributes.Add("class", "grid_24");

                            Panel divTarget = new Panel();
                            divTarget.Attributes.Add("class", "grid_24");
                            string divTargetId = specialtyId + "divTarget";
                            divTarget.ID = divTargetId;

                            Panel divWard = new Panel();
                            divWard.Attributes.Add("class", "grid_24");

                            Panel divWardImage = new Panel();
                            divWardImage.Attributes.Add("class", "grid_1");
                            HtmlAnchor a1 = new HtmlAnchor();

                            Image plusImage = new Image();
                            string plusId = specialtyId.ToString() + "plusImage";

                            Image minusImage = new Image();
                            string minusId = specialtyId.ToString() + "minusImage";

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
                            lblWardTitle.Text = specialty.SpecialtyName;
                            lblWardTitle.Attributes.Add("class", "ward-heading");
                            divWardName.Controls.Add(lblWardTitle);

                            divWard.Controls.Add(divWardImage);
                            divWard.Controls.Add(divWardName);

                            divMain.Controls.Add(divWard);

                            Panel divSpace1 = new Panel();
                            divSpace1.Attributes.Add("class", "clear Hgap10");
                            divMain.Controls.Add(divSpace1);



                            if (selectedKPI.NumeratorOnlyFlag)
                            {
                                CreateTextBoxControls(specialtyId, selectedKPI, "num", divTarget);
                            }
                            else
                            {
                                CreateTextBoxControls(specialtyId, selectedKPI, "num", divTarget);
                                CreateTextBoxControls(specialtyId, selectedKPI, "den", divTarget);

                            }

                            if (selectedKPI.SeparateYTDFigure)
                            {
                                CreateTextBoxControls(specialtyId, selectedKPI, "ytd", divTarget);
                            }

                            Panel divSpace4 = new Panel();
                            divSpace4.Attributes.Add("class", "clear Hgap10");
                            divTarget.Controls.Add(divSpace4);


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
    /// <param name="specialtyId"></param>
    /// <param name="selectedKPI"></param>
    /// <param name="months"></param>
    /// <param name="type"></param>
    /// <param name="divMain"></param>
    private void CreateTextBoxControls(int specialtyId, KPI selectedKPI, string type, Panel divMain)
    {
        try
        {



            string[] months = null;

            months = new string[] { "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar" };

            if (type == "num")
            {
                Panel divMonth = new Panel();
                divMonth.Attributes.Add("class", "grid_24");
                Panel divMonthTitle = new Panel();
                divMonthTitle.Attributes.Add("class", "grid_3");

                Panel divWrap = new Panel();
                divWrap.Attributes.Add("class", "grid_20");

                Label lblMonthTitle = new Label();
                lblMonthTitle.Text = "";
                divMonthTitle.Controls.Add(lblMonthTitle);
                divMonth.Controls.Add(divMonthTitle);

                for (int j = 0; j < months.Count(); j++)
                {
                    Panel divDes = new Panel();
                    divDes.Attributes.Add("class", "grid_2 center");
                    Label monthlable = new Label();
                    monthlable.Text = months[j].ToString();
                    divDes.Controls.Add(monthlable);

                    divWrap.Controls.Add(divDes);
                    divMonth.Controls.Add(divWrap);
                }
                divMain.Controls.Add(divMonth);
            }
            if (type == "num")
            {
                //Gernerate the Numarator text boxes
                Panel divNumarator = new Panel();
                divNumarator.Attributes.Add("class", "grid_24");
                Panel divNumaratorTitle = new Panel();
                divNumaratorTitle.Attributes.Add("class", "grid_3");

                Panel divWrap = new Panel();
                divWrap.Attributes.Add("class", "grid_20");

                Label lblNumaratorTitle = new Label();
                lblNumaratorTitle.Text = selectedKPI.NumeratorDescription != "" ? selectedKPI.NumeratorDescription : "Numerator";
                divNumaratorTitle.Controls.Add(lblNumaratorTitle);
                divNumarator.Controls.Add(divNumaratorTitle);
                for (int x = 0; x < months.Count(); x++)
                {
                    string Numarator = GetTextBoxValue(specialtyId, selectedKPI.Id, type, x, "Numarator");

                    Panel NumaratorCell = new Panel();
                    NumaratorCell.Attributes.Add("class", "grid_2");
                    TextBox NumaratorTextBox = new TextBox();
                    DateTime fromDate = new DateTime(int.Parse(lblCurentFinancialYear.Text.Split('-')[0]), x + 1, 1).AddMonths(3);
                    if (fromDate.AddDays(-1) > DateTime.Now)
                    {
                        NumaratorTextBox.Enabled = false;
                    }
                    string textBoxId = specialtyId + months[x] + "txtNumarator" + type;
                    NumaratorTextBox.ID = textBoxId;
                    NumaratorTextBox.Text = Numarator;
                    NumaratorTextBox.MaxLength = 10;
                    NumaratorTextBox.Attributes.Add("onkeypress", "return isDecimalKey(event);");
                    NumaratorCell.Controls.Add(NumaratorTextBox);

                    Panel divNumaratorCellRev = new Panel();
                    divNumaratorCellRev.ID = specialtyId + months[x] + "divNumarator" + type;
                    divNumaratorCellRev.Attributes.Add("class", "input-validation-error");
                    NumaratorCell.Controls.Add(divNumaratorCellRev);


                    divWrap.Controls.Add(NumaratorCell);
                    divNumarator.Controls.Add(divWrap);

                }
                divMain.Controls.Add(divNumarator);
            }
            if (type == "den")
            {
                //Gernerate the Denominator text boxes
                Panel divDenominator = new Panel();
                divDenominator.Attributes.Add("class", "grid_24");
                Panel divDenominatorTitle = new Panel();
                divDenominatorTitle.Attributes.Add("class", "grid_3");

                Panel divWrap = new Panel();
                divWrap.Attributes.Add("class", "grid_20");

                Label lblDenominatorTitle = new Label();
                //lblDenominatorTitle.Text = "Denominator";
                lblDenominatorTitle.Text = selectedKPI.DenominatorDescription != "" ? selectedKPI.DenominatorDescription : "Denominator";
                divDenominatorTitle.Controls.Add(lblDenominatorTitle);
                divDenominator.Controls.Add(divDenominatorTitle);
                for (int x = 0; x < months.Count(); x++)
                {
                    string Denominator = GetTextBoxValue(specialtyId, selectedKPI.Id, type, x, "Denominator");

                    Panel DenominatorCell = new Panel();
                    DenominatorCell.Attributes.Add("class", "grid_2");
                    TextBox DenominatorTextBox = new TextBox();
                    DateTime fromDate = new DateTime(int.Parse(lblCurentFinancialYear.Text.Split('-')[0]), x + 1, 1).AddMonths(3);
                    if (fromDate.AddDays(-1) > DateTime.Now)
                    {
                        DenominatorTextBox.Enabled = false;
                    }
                    string textBoxId = specialtyId + months[x] + "txtDenominator" + type;
                    DenominatorTextBox.ID = textBoxId;
                    DenominatorTextBox.Text = Denominator;
                    DenominatorTextBox.MaxLength = 10;
                    DenominatorTextBox.Attributes.Add("onkeypress", "return isDecimalKey(event);");
                    DenominatorCell.Controls.Add(DenominatorTextBox);

                    Panel divDenominatorCellRev = new Panel();
                    divDenominatorCellRev.ID = specialtyId + months[x] + "divDenominator" + type;
                    divDenominatorCellRev.Attributes.Add("class", "input-validation-error");
                    DenominatorCell.Controls.Add(divDenominatorCellRev);

                    divWrap.Controls.Add(DenominatorCell);
                    divDenominator.Controls.Add(divWrap);
                }
                divMain.Controls.Add(divDenominator);
            }

            if (type == "ytd")
            {
                //Gernerate the YTD Value text boxes
                Panel divYTD = new Panel();
                divYTD.Attributes.Add("class", "grid_24");
                Panel divYTDTitle = new Panel();
                divYTDTitle.Attributes.Add("class", "grid_3");

                Panel divWrap = new Panel();
                divWrap.Attributes.Add("class", "grid_20");

                Label lblYTDTitle = new Label();
                lblYTDTitle.Text = "YTD Value";
                lblYTDTitle.Text = selectedKPI.YTDValueDescription != "" ? selectedKPI.YTDValueDescription : "YTD Value";
                divYTDTitle.Controls.Add(lblYTDTitle);
                divYTD.Controls.Add(divYTDTitle);
                for (int x = 0; x < months.Count(); x++)
                {
                    string YTD = GetTextBoxValue(specialtyId, selectedKPI.Id, type, x, "YTD");
                    Panel YTDCell = new Panel();
                    YTDCell.Attributes.Add("class", "grid_2");
                    TextBox YTDTextBox = new TextBox();
                    DateTime fromDate = new DateTime(int.Parse(lblCurentFinancialYear.Text.Split('-')[0]), x + 1, 1).AddMonths(3);
                    if (fromDate.AddDays(-1) > DateTime.Now)
                    {
                        YTDTextBox.Enabled = false;
                    }
                    YTDTextBox.ID = specialtyId + months[x] + "txtYTD" + type;
                    YTDTextBox.Text = YTD;
                    YTDTextBox.MaxLength = 10;
                    YTDTextBox.Attributes.Add("onkeypress", "return isDecimalKey(event);");
                    YTDCell.Controls.Add(YTDTextBox);

                    Panel divYTDCellRev = new Panel();
                    divYTDCellRev.ID = specialtyId + months[x] + "divYTD" + type;
                    divYTDCellRev.Attributes.Add("class", "input-validation-error");
                    YTDCell.Controls.Add(divYTDCellRev);

                    divWrap.Controls.Add(YTDCell);
                    divYTD.Controls.Add(divWrap);
                }
                divMain.Controls.Add(divYTD);
            }
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
    /// <param name="specialtyId"></param>
    /// <param name="selectedKPIId"></param>
    /// <param name="type"></param>
    /// <param name="x"></param>
    /// <param name="dataType"></param>
    /// <returns></returns>
    private string GetTextBoxValue(int specialtyId, int selectedKPIId, string type, int x, string dataType)
    {
        string[] years = lblCurentFinancialYear.Text.Split('-');

        DateTime startDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        DateTime endDate = new DateTime(Convert.ToInt32(years[1]), 3, 31);

        //Get the data for selected ward and KPI
        DataSet dsDatas = CurrentDatas;

        //Filter the required data.
        DataTable dtData = GetFilteredTable(dsDatas.Tables[0], "SpecialtyId=" + specialtyId + " AND KPIId=" + selectedKPIId);

        DataView dvDataList = new DataView(dtData);
        dvDataList.Sort = "TargetMonth";
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

        rowindex = dvDataList.Find(targetMonth);
        string val = string.Empty;
        if (rowindex != -1)
        {
            if (type == "num")
            {
                string rowdatatype = "Numerator";
                val = dvDataList[rowindex][rowdatatype].ToString();
            }
            else if (type == "den")
            {
                string rowdatatype = "Denominator";
                val = dvDataList[rowindex][rowdatatype].ToString();
            }
            else if (type == "ytd")
            {
                string rowdatatype = "YTDValue";
                val = dvDataList[rowindex][rowdatatype].ToString();
            }
            else
            {
                val = string.Empty;
            }
        }
        return val;
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
            CurrentDatas = null;
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
            CurrentDatas = null;
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
            DataSet dsWards = specialtyController.SearchSpecialty(string.Empty, true);
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
        btnComments.Visible = false;
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

            string id = "KPINo";
            string userId = "UserId";
            string access = "Access";
            btnComments.Visible = true;
            btnComments.OnClientClick = String.Format("javascript:popUp(750,550, 'Transactions','Comment.aspx?{0}={1}&{2}={3}&{4}={5}'); return false;", id, new KPIController().ViewKPI(kpiId).KPINo, userId, Master.NHSUser.Id, access, "Internal");
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

    #region Get all Specialty
    /// <summary>
    /// Get the all Specialty
    /// </summary>
    /// <returns></returns>
    private string GetAllSpecialtyIdList()
    {
        try
        {
            StringBuilder sb = new StringBuilder();


            specialtyController = new SpecialtyController();
            DataSet dsSpecialty = specialtyController.SearchSpecialty(string.Empty, true);
            if ((dsSpecialty != null) && (dsSpecialty.Tables[0] != null) && (dsSpecialty.Tables[0].Rows != null) && (dsSpecialty.Tables[0].Rows.Count > 0))
            {
                string[] idList = new string[dsSpecialty.Tables[0].Rows.Count];

                for (int i = 0; i < dsSpecialty.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dsSpecialty.Tables[0].Rows[i];
                    if (i == dsSpecialty.Tables[0].Rows.Count - 1)
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

    #region Get Specialty

    private Specialty GetSpecialty(int specialtyId)
    {
        specialtyController = new SpecialtyController();
        return specialtyController.ViewSpecialty(specialtyId);
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

    #region Save Bulk Data
    [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
    public string SaveBulkData(string[] Numarator, string[] Denominator, string[] YTD, string hospitalId, string kpiId, string year)
    {
        try
        {

            kPIController = new KPIController();
            KPI kpiObject = new KPI();
            kpiObject.SpecialtyMonthlyDataList = new System.Collections.Generic.List<KPISpecialtyMonthlyData>();

            DateTime targetMonth = new DateTime(int.Parse(year.Split('-')[0].ToString()), 3, 1);
            int yearFlag = 1;
            for (int i = 0; i < Numarator.Length; i++)
            {
                KPISpecialtyMonthlyData specialtyData = new KPISpecialtyMonthlyData();
                specialtyData.Numerator = Numarator[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(Numarator[i].Split('_')[2].ToString());
                specialtyData.Denominator = Denominator[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(Denominator[i].Split('_')[2].ToString());
                specialtyData.YTDValue = YTD[i].Split('_')[2].ToString() == string.Empty ? double.MinValue : double.Parse(YTD[i].Split('_')[2].ToString());

                specialtyData.SpecialtyId = int.Parse(Numarator[i].Split('_')[0].ToString());
                specialtyData.KPIId = int.Parse(kpiId);
                specialtyData.HospitalId = int.Parse(hospitalId);
                specialtyData.TargetMonth = targetMonth.AddMonths(1);

                targetMonth = targetMonth.AddMonths(1);
                kpiObject.SpecialtyMonthlyDataList.Add(specialtyData);


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

            kPIController.InsertBulkSpecialtyData(kpiObject, startDate, endDate);
            CurrentDatas = null;
            return true.ToString().ToLower();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    #endregion
}