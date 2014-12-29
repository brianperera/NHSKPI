using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;
using System.Data;

public partial class Views_KPI_DataExport : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private NHSKPIDataService.Models.KPI kpi = null;
    private int kpiId;
    private User nhsUser = null;
    private static DataSet ds = null;

    #endregion

    #region Public Properties


    public KPIController KPIController
    {
        get
        {
            if (kpiController == null)
            {
                kpiController = new KPIController();
            }

            return kpiController;
        }
        set
        {
            kpiController = value;
        }
    }
    public NHSKPIDataService.Models.KPI Kpi
    {
        get 
        {
            if (kpi == null)
            {
                kpi = new NHSKPIDataService.Models.KPI();
            }
            return kpi; 
        }
        set 
        { 
            kpi = value; 
        }
    }
    public int KpiId
    {
        get 
        {
            if (Request.QueryString["Id"] != null && int.Parse(Request.QueryString["Id"].ToString()) > 0)
            {
                kpiId = int.Parse(Request.QueryString["Id"].ToString());
            }
            else
            {
                kpiId = 0;
            }
            return kpiId; 
        }
        set 
        { 
            kpiId = value; 
        }
    }
    public User NHSUser
    {
        get
        {
            if (Session["NHSUser"] != null)
            {
                nhsUser = (User)Session["NHSUser"];
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

            return nhsUser;
        }
        set
        {
            nhsUser = value;
        }
    }

 

    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckBox_SelectAll.Checked = true;
            SetCheckBoxList();
            PopulateYearDropDownList();
        } 
    }

    #endregion   
    
    protected void Export_Data_Button_Click(object sender, EventArgs e)
    {
        DataTable table = ds.Tables[0];
        string attachment = string.Empty;

        if (DataType_DropDownList.SelectedIndex == 0)
        {
            attachment = "attachment; filename=Ward_Data.xls";
        }
        else if (DataType_DropDownList.SelectedIndex == 1)
        {
            attachment = "attachment; filename=Specialty_Data.xls";
        }

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;

        foreach (DataColumn column in table.Columns)
        {

            if (ColumnList_CheckBoxList.Items.FindByValue(column.ColumnName)!=null && 
                ColumnList_CheckBoxList.Items.FindByValue(column.ColumnName).Selected)
            {
                Response.Write(tab + column.ColumnName);
                tab = "\t";
            }
        }

        Response.Write("\n");

        foreach (DataRow row in table.Rows)
        {
            if (row[3] == null)
                continue;

            DateTime rowDate = (DateTime)row[3];
            if (!rowDate.Year.ToString().Equals(Year_DropDownList.SelectedValue.ToString()))
                continue;

            tab = string.Empty;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (ColumnList_CheckBoxList.Items.Count>i && 
                    ColumnList_CheckBoxList.Items[i].Selected)
                {
                    Response.Write(tab + row[i].ToString());
                    tab = "\t";
                }
            }
            Response.Write("\n");
        }

        Response.End();
    }

    protected void DataType_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCheckBoxList();
        PopulateYearDropDownList();
    }

    private void SetCheckBoxList()
    {  
        if (DataType_DropDownList.SelectedIndex == 0)
        {
            ds = new WardController().GetWardData(NHSUser.HospitalId);
            ColumnList_CheckBoxList.Items.Clear();
            ColumnList_CheckBoxList.DataSource = ds.Tables[0].Columns;
            ColumnList_CheckBoxList.DataBind();
            
        }
        else
        {
            ds = new SpecialtyController().GetSpecialtyData(NHSUser.HospitalId);
            ColumnList_CheckBoxList.Items.Clear();
            ColumnList_CheckBoxList.DataSource = ds.Tables[0].Columns;
            ColumnList_CheckBoxList.DataBind();
            Export_Data_Button.Text = "Export Specialty Data";
        }
        SetSelectionOfCheckBoxList();
    }

    private void SetSelectionOfCheckBoxList()
    {
        foreach (ListItem checkBox in ColumnList_CheckBoxList.Items)
        {
            checkBox.Selected = CheckBox_SelectAll.Checked;
        }
        HandleNoSelection();
    }

    private void PopulateYearDropDownList()
    {
        Year_DropDownList.Items.Clear();
        List<int> yearList = new List<int>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (row[3] == null)
                continue;

            if (!yearList.Contains(((System.DateTime)row[3]).Year))
            {
                yearList.Add(((System.DateTime)row[3]).Year);
            }
        }
        if (yearList.Count > 0)
        {
            Year_DropDownList.DataSource = yearList;
            Year_DropDownList.DataBind();
        }
        else
        {
            Year_DropDownList.Items.Add("No Data");
        }
        Year_DropDownList.SelectedIndex = 0;
    }

    protected void CheckBox_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        SetSelectionOfCheckBoxList();
    }

    protected void ColumnList_CheckBoxList_CheckedChanged(object sender, EventArgs e)
    {
        HandleNoSelection();
    }

    private void HandleNoSelection()
    {
        ListItem item = ColumnList_CheckBoxList.SelectedItem;
        if (item == null)
        {
            Export_Data_Button.Enabled = false;
            Message_Label.Text = "Please select one or more checkboxes to export data";
        }
        else
        {
            Export_Data_Button.Enabled = true;
            Message_Label.Text = string.Empty;
        }
    }
}
