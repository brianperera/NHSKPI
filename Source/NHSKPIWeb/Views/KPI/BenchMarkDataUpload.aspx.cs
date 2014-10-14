using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSVReader;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NHSKPIBusinessControllers;

public partial class Views_KPI_BenchMarkDataUpload : System.Web.UI.Page
{
    #region Private Variable

    private KPIController kPIController = null;

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Upload CSV

    private void UploadCSV()
    {
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

        try
        {
            using (CsvReader csv = new CsvReader(
                       new StreamReader(fuFile.PostedFile.InputStream), true))
            {
                int row = 0;
                try
                {
                    kPIController = new KPIController();
                    
                    while (csv.ReadNextRecord())
                    {
                        row++;
                        if (csv[0].Trim() == "@!!@")
                        {
                            break;
                        }
                        

                        #region Read csv coulmn and add to row
                        dr = dt.NewRow();

                        //TrustCode
                        try
                        {
                            dr["TrustCode"] = csv[0].Trim() != string.Empty ? csv[0].Trim() : string.Empty;

                        }
                        catch (Exception e)
                        {
                            dr["TrustCode"] = string.Empty;

                        }
                        //Trust Name
                        try
                        {
                            dr["TrustName"] = csv[1].Trim() != string.Empty ? csv[1].Trim() : string.Empty;

                        }
                        catch (Exception e)
                        {
                            dr["TrustName"] = string.Empty;

                        }
                        //KPI No
                        try
                        {
                            dr["KPINo"] = csv[2].Trim() != string.Empty ? csv[2].Trim() : string.Empty;
                        }
                        catch (Exception e)
                        {
                            dr["KPINo"] = string.Empty;

                        }
                        //Target Month
                        try
                        {
                            dr["TargetMonth"] = Convert.ToDateTime(csv[3]);
                        }
                        catch (Exception e)
                        {
                            dr["TargetMonth"] = DBNull.Value;

                        }
                        //Numerator
                        try
                        {
                            dr["Numerator"] = csv[4];
                        }
                        catch (Exception e)
                        {
                            dr["Numerator"] = DBNull.Value;

                        }

                        dt.Rows.Add(dr);
                        #endregion                        

                    }

                    DataRow[] tcRows = dt.Select("TrustCode = ''");
                    DataRow[] tnRows = dt.Select("TrustName = ''");
                    DataRow[] knRows = dt.Select("KPINo = ''");
                    
                    if (tcRows.Length > 0 || knRows.Length > 0 || tnRows.Length > 0)
                    {
                        if (tcRows.Length > 0)
                        {
                            lblAddMessage.Text = "TrustCode is missing or not exist at row " + (dt.Rows.IndexOf(tcRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";

                        }
                        else if (knRows.Length > 0)
                        {
                            lblAddMessage.Text = "KPINo is missing or not exist at row " + (dt.Rows.IndexOf(knRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                        }
                        else if (tnRows.Length > 0)
                        {
                            lblAddMessage.Text = "TrustName is missing or invalid format at row " + (dt.Rows.IndexOf(tnRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                        }
                        
                    }
                    else
                    {
                        if (kPIController.UpdateBenchmarkCSVData(dt))
                        {
                            lblAddMessage.Text = "Benchmark CSV Data File successfully uploaded";
                            lblAddMessage.CssClass = "alert-success";
                        }
                        else
                        {
                            lblAddMessage.Text = "Benchmark CSV Data File Upload Error occured";
                            lblAddMessage.CssClass = "alert-danger";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblAddMessage.Text = "Benchmark CSV Data File Upload Error occured";
                    lblAddMessage.CssClass = "alert-danger";
                }
            }
        }
        catch (Exception ex)
        {
            lblAddMessage.Text = "Benchmark CSV Data File Upload Error occured";
            lblAddMessage.CssClass = "alert-danger";
        }
    }

    #endregion

    #region Update Button Click

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UploadCSV();
    }

    #endregion
}