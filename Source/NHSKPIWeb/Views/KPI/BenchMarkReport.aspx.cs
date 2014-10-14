using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using System.Data;

public partial class Views_KPI_BenchMarkReport : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;

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
       
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialData();
        }
    }

    #endregion

    #region Load Initial Data

    private void LoadInitialData()
    {
        string nextFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();

        lblCurentFinancialYear.Text = nextFinYear;

        DataSet dsInitialData = KPIController.GetBenchmarkInitialData();

        ddlKPI.DataTextField = "KPIDescription";
        ddlKPI.DataValueField = "KPINo";
        ddlKPI.DataSource = dsInitialData.Tables[1];
        ddlKPI.DataBind();

        chkHospital.DataTextField = "TrustName";
        chkHospital.DataValueField = "TrustCode";
        chkHospital.DataSource = dsInitialData.Tables[0];
        chkHospital.DataBind();
    }

    #endregion

    #region Previous Button Click Event

    protected void imgBtnPrevoius_Click(object sender, ImageClickEventArgs e)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');
        int nextfist = Convert.ToInt32(years[0]) - 1;
        int nextsecond = Convert.ToInt32(years[1]) - 1;

        string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

        lblCurentFinancialYear.Text = nextFinYear;
    }

    #endregion

    #region Next Button Click Event

    protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');
        int nextfist = Convert.ToInt32(years[0]) + 1;
        int nextsecond = Convert.ToInt32(years[1]) + 1;

        string nextFinYear = nextfist.ToString() + "-" + nextsecond.ToString();

        lblCurentFinancialYear.Text = nextFinYear;
    }

    #endregion   

    #region Generate Button Click Event

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');

        DateTime FromDate = DateTime.MinValue;
        DateTime ToDate = DateTime.MinValue;
        if (rdoDateRange.SelectedValue == "1")
        {
            FromDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
            ToDate = FromDate.AddYears(1);
        }
        else
        {
            FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12);
            ToDate = FromDate.AddMonths(12);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<ALLCODES>");
        foreach(ListItem li in chkHospital.Items)
        {
            if (li.Selected)
            {

                sb.AppendFormat("<CODE ID=\"{0}\" />", li.Value);
                
            }
        }
        sb.Append("</ALLCODES>");
        GenerateReport(KPIController.GetBenchmarkReportData(FromDate, ToDate, ddlKPI.SelectedValue, sb.ToString(),Master.NHSUser.HospitalId));
    }

    #endregion

    #region Generate Report

    private void GenerateReport(DataSet reportData)
    {
        string finYear = lblCurentFinancialYear.Text;
        string[] years = finYear.Split('-');
        DateTime FromDate = DateTime.MinValue;
        if (rdoDateRange.SelectedValue == "1")
        {
            FromDate = new DateTime(Convert.ToInt32(years[0]), 4, 1);
        }
        else
        {
            FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12);
        }

        System.Text.StringBuilder tableContent = new System.Text.StringBuilder();
        tableContent =tableContent.Append("<table id='Chart' class='chart_data' style='display: block'>");
        tableContent.Append("<caption>");
        tableContent.Append(ddlKPI.SelectedItem.Text+" by Month</caption>");
        tableContent.Append("<thead>");
        tableContent.Append("<tr>");
        tableContent.Append("<td>");
        tableContent.Append("</td>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(1).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(2).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(3).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(4).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(5).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(6).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(7).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(8).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(9).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(10).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("<th scope='col'>");
        tableContent.Append(FromDate.AddMonths(11).ToString("MMM-yyyy"));
        tableContent.Append("</th>");
        tableContent.Append("</tr>");
        tableContent.Append("</thead>");
        tableContent.Append("<tbody>");

        DateTime ToDate = FromDate.AddMonths(11);

        foreach(DataRow row in reportData.Tables[1].Rows)
        {
            for (DateTime d = FromDate; d <= ToDate; d=d.AddMonths(1))
            {
               DataRow[] eixtingRow =  reportData.Tables[0].Select("TrustCode = '" + row["TrustCode"].ToString() + "' AND TargetMonth = '" + d.Date.ToString("yyyy-MM-dd") + "'");
               if (eixtingRow.Length == 0)
                {
                    DataRow newRow = reportData.Tables[0].NewRow();
                    newRow["TrustCode"] = row["TrustCode"];
                    newRow["TrustName"] = row["TrustName"];
                    newRow["TargetMonth"] = d.Date;
                    newRow["Numerator"] = DBNull.Value;
                    newRow["KPINo"] = ddlKPI.SelectedValue;
                    reportData.Tables[0].Rows.Add(newRow);
                }
            }
        }

        DataTable dt = new DataTable();
        dt.TableName = "BenchmarkData";
        DataColumn dc;
        DataRow dr;

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "TrustCode";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "TrustName";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.DateTime");
        dc.ColumnName = "TargetMonth";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.Decimal");
        dc.ColumnName = "Numerator";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "KPINo";
        dc.Unique = false;
        dt.Columns.Add(dc);

        foreach (DataRow row in reportData.Tables[1].Rows)
        {
            for (DateTime d = FromDate; d <= ToDate; d = d.AddMonths(1))
            {
                DataRow[] eixtingRow = reportData.Tables[0].Select("TrustCode = '" + row["TrustCode"].ToString() + "' AND TargetMonth = '" + d.Date.ToString("yyyy-MM-dd") + "'");
                if (eixtingRow.Length > 0)
                {
                    dr = dt.NewRow();
                    dr["TrustCode"] = eixtingRow[0]["TrustCode"];
                    dr["TrustName"] = eixtingRow[0]["TrustName"];
                    dr["TargetMonth"] = eixtingRow[0]["TargetMonth"];
                    dr["Numerator"] = eixtingRow[0]["Numerator"];
                    dr["KPINo"] = eixtingRow[0]["KPINo"];
                    dt.Rows.Add(dr);
                }
            }
        }
        
            int count = 0;
            string trust = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                count = count + 1;                
                if (trust != row["TrustName"].ToString())
                {
                    trust = row["TrustName"].ToString();

                    tableContent.Append("<tr>");
                    tableContent.Append("<th scope='row'>");
                    tableContent.Append(trust);
                    tableContent.Append("</th>");
                    tableContent.Append("<td>");
                    tableContent.Append(row["Numerator"] != DBNull.Value ? Convert.ToInt32(row["Numerator"]).ToString() : string.Empty);
                    tableContent.Append("</td>");
                }
                else
                {
                    tableContent.Append("<td>");
                    tableContent.Append(row["Numerator"] != DBNull.Value ? Convert.ToInt32(row["Numerator"]).ToString() : string.Empty);
                    tableContent.Append("</td>");
                    
                }
                if (count == 12)
                {
                    tableContent.Append("</tr>");
                    count = 0;
                }
            }
                          
                    
                    
                   
                
            tableContent.Append("</tbody>");
            tableContent.Append("</table>");

            divReportTableContent.InnerHtml = tableContent.ToString();
    }

    #endregion

    #region Date Range Selected Index change Option

    protected void rdoDateRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        divFinancialYearControl.Visible = false;
        divFinancialYearLabel.Visible = false;
        divFinancialYearSpace.Visible = false;

        if (rdoDateRange.SelectedValue == "1")
        {
            divFinancialYearControl.Visible = true;
            divFinancialYearLabel.Visible = true;
            divFinancialYearSpace.Visible = true;
        }
    }

    #endregion
}