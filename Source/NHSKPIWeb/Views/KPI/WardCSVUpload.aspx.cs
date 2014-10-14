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

public partial class Views_KPI_WardCSVUpload : System.Web.UI.Page
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
        dt.TableName = "CSVWardData";
        DataColumn dc;
        DataRow dr;

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "WardCode";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "KPINo";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.Int32");
        dc.ColumnName = "HospitalId";
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
        dc.DataType = System.Type.GetType("System.Decimal");
        dc.ColumnName = "Denominator";
        dc.Unique = false;
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.Decimal");
        dc.ColumnName = "YTDValue";
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
                    DataSet dsInitialData = kPIController.GetCSVUploadInitialData();

                    while (csv.ReadNextRecord())
                    {
                        row++;
                        if (csv[0].Trim() == "@!!@")
                        {
                            break;
                        }
                        dr = dt.NewRow();

                        #region Read csv coulmn and add to row
                        //Ward code
                        try
                        {
                            dr["WardCode"] = csv[0].Trim() != string.Empty ? csv[0].Trim() : string.Empty;
                            if (dsInitialData.Tables[0].Select("WardCode = '" + dr["WardCode"].ToString()+"'").Length == 0)
                            {
                                dr["WardCode"] = string.Empty;
                            }                            
                            
                        }
                        catch (Exception e)
                        {
                            dr["WardCode"] = string.Empty;
                            
                        }
                        //KPI No
                        try
                        {
                            dr["KPINo"] = csv[1].Trim() != string.Empty ? csv[1].Trim() : string.Empty;
                            if (dsInitialData.Tables[1].Select("KPINo ='" + dr["KPINo"].ToString() + "'").Length == 0)
                            {
                                dr["KPINo"] = string.Empty;
                            }
                            
                        }
                        catch (Exception e)
                        {
                            dr["KPINo"] = string.Empty;
                            
                        }
                        //Hospital Id
                        try
                        {
                            dr["HospitalId"] = Master.NHSUser.HospitalId.ToString();
                        }
                        catch (Exception e)
                        {
                            dr["HospitalId"] = DBNull.Value;
                            
                        }
                        //Target Month
                        try
                        {
                            string[] datesParts = csv[2].Trim().ToString().Split('/');
                            dr["TargetMonth"] = new DateTime(int.Parse(datesParts[2].ToString()), int.Parse(datesParts[1].ToString()), int.Parse(datesParts[0].ToString()));
                        }
                        catch (Exception e)
                        {
                            dr["TargetMonth"] = DBNull.Value;
                            
                        }
                        //Numerator
                        try
                        {
                            dr["Numerator"] = csv[3].Trim();
                        }
                        catch (Exception e)
                        {
                            dr["Numerator"] = DBNull.Value;
                            
                        }
                        //Denominator
                        try
                        {
                            dr["Denominator"] = csv[4].Trim();
                        }
                        catch (Exception e)
                        {
                            dr["Denominator"] = DBNull.Value;
                            
                        }
                        //YTD Value
                        try
                        {
                            dr["YTDValue"] = csv[5].Trim();
                        }
                        catch (Exception e)
                        {
                            dr["YTDValue"] = DBNull.Value;
                            
                        }
                        #endregion

                        dt.Rows.Add(dr);
                        
                    }

                    DataRow[] wcRows = dt.Select("WardCode = ''");
                    DataRow[] knRows = dt.Select("KPINo = ''");
                    DataRow[] tmRows = dt.Select("TargetMonth IS NULL");
                    DataRow[] nuRows = dt.Select("Numerator IS NULL");

                    if (wcRows.Length > 0 || knRows.Length > 0 || tmRows.Length > 0)
                    {
                        if (wcRows.Length > 0)
                        {
                            lblAddMessage.Text = "WardCode is missing or not exist at row " + (dt.Rows.IndexOf(wcRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                            
                        }
                        else if (knRows.Length > 0)
                        {
                            lblAddMessage.Text = "KPINo is missing or not exist at row " + (dt.Rows.IndexOf(knRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                        }
                        else if (tmRows.Length > 0)
                        {
                            lblAddMessage.Text = "TargetDate is missing or invalid format at row " + (dt.Rows.IndexOf(tmRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                        }
                        else if (nuRows.Length > 0)
                        {
                            lblAddMessage.Text = "Numerator is missing or invalid format at row " + (dt.Rows.IndexOf(nuRows[0]) + 1).ToString();
                            lblAddMessage.CssClass = "alert-danger";
                        }
                    }
                    else 
                    {
                        if (kPIController.UpdateCSVWardData(dt))
                        {
                            lblAddMessage.Text = "CSV File successfully uploaded";
                            lblAddMessage.CssClass = "alert-success";
                        }
                        else
                        {
                            lblAddMessage.Text = "CSV File Upload Error occured";
                            lblAddMessage.CssClass = "alert-danger";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblAddMessage.Text = "CSV File Upload Error occured";
                    lblAddMessage.CssClass = "alert-danger";
                }
            }
        }
        catch (Exception ex)
        {
            lblAddMessage.Text = "CSV File Upload Error occured";
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