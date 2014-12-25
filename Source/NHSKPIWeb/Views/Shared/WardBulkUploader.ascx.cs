using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIDataService.Util;
using NHSKPIDataService.Models;
using NHSKPIBusinessControllers;
using System.Configuration;
using System.Data;
using CSVReader;
using System.IO;

public partial class Views_Shared_WardBulkUploader : System.Web.UI.UserControl
{
    #region Fields

    private KPIController kPIController = null;
    private UserController userController = null;
    private WardController wardController = null;
    private string errorMessage = string.Empty;

    #endregion

    #region Properties

    public User nhsUser = null;
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

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Wizard 1st step
    protected void btnUploadWardFile_Click(object sender, EventArgs e)
    {
        UploadWardDataFile();
    }

    #endregion

    #region Methods

    private void UploadWardDataFile()
    {
        try
        {
            if (!string.IsNullOrEmpty(fuWardDataUpload.PostedFile.FileName))
            {
                if (IsCsvFile(fuWardDataUpload.PostedFile.FileName))
                {
                    using (CsvReader csv = new CsvReader(
                               new StreamReader(fuWardDataUpload.PostedFile.InputStream), true))
                    {

                        DataTable dt = new DataTable();
                        dt.TableName = "WardData";
                        DataColumn dc;
                        DataRow dr;

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.Int32");
                        dc.ColumnName = "Id";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.Int32");
                        dc.ColumnName = "HospitalId";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "WardCode";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "WardName";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "WardGroupId";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "WardGroupName";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "Description";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "IsActive";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        int row = 0;
                        try
                        {
                            List<string> columns = new List<string>()
                    {
                        "WardCode",
                        "WardName",
                        "WardGroupId",
                        "WardGroupName",
                        "Description",
                        "IsActive"
                    };


                            while (csv.ReadNextRecord())
                            {
                                row++;
                                if (csv[0].Trim() == "@!!@")
                                {
                                    break;
                                }

                                dr = dt.NewRow();

                                #region Read csv coulmn and add to row


                                //Adding at top since this is common to all records
                                try
                                {
                                    dr["HospitalId"] = NHSUser.HospitalId;
                                }
                                catch (Exception)
                                {
                                    dr["HospitalId"] = 0;
                                }

                                //Adding at top since this is common to all records
                                dr["Id"] = 0;

                                int i = 0;
                                foreach (var column in columns)
                                {
                                    try
                                    {
                                        dr[column] = csv[i].Trim() != string.Empty ? csv[i].Trim() : string.Empty;
                                    }
                                    catch (Exception)
                                    {
                                        dr[column] = string.Empty;
                                    }

                                    i++;
                                }

                                #endregion

                                dt.Rows.Add(dr);

                            }

                            DataRow[] wcRows = dt.Select("WardCode = ''");
                            DataRow[] wnRows = dt.Select("WardName = ''");
                            DataRow[] wgiRows = dt.Select("WardGroupId IS NULL");
                            DataRow[] wgnRows = dt.Select("WardGroupName = ''");

                            string rowErrorMessage = string.Empty;

                            if (wcRows.Length > 0 || wnRows.Length > 0 || wgiRows.Length > 0 || wgnRows.Length > 0)
                            {
                                if (wcRows.Length > 0)
                                {
                                    rowErrorMessage = "Ward Code is missing or not exist at row " + (dt.Rows.IndexOf(wcRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    lblAddWardDataMessage.CssClass = "alert-danger";

                                }
                                if (wnRows.Length > 0)
                                {
                                    rowErrorMessage = "Ward Name is missing or not exist at row " + (dt.Rows.IndexOf(wnRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    lblAddWardDataMessage.CssClass = "alert-danger";
                                }
                                if (wgiRows.Length > 0)
                                {
                                    rowErrorMessage = "Ward Group ID is missing or invalid format at row " + (dt.Rows.IndexOf(wgiRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    lblAddWardDataMessage.CssClass = "alert-danger";
                                }
                                if (wgnRows.Length > 0)
                                {
                                    rowErrorMessage = "Ward Group Name is missing or invalid format at row " + (dt.Rows.IndexOf(wgnRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    lblAddWardDataMessage.CssClass = "alert-danger";
                                }
                            }
                            else
                            {
                                wardController = new WardController();

                                if (wardController.BulkUploadWardAndWardGroup(dt))
                                {
                                    lblAddWardDataMessage.Text = "Ward File successfully uploaded";
                                    lblAddWardDataMessage.CssClass = "alert-success";
                                }
                                else
                                {
                                    lblAddWardDataMessage.Text = "Ward File Upload Error occured";
                                    lblAddWardDataMessage.CssClass = "alert-danger";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblAddWardDataMessage.Text = "Ward File Upload Error occured";
                            lblAddWardDataMessage.CssClass = "alert-danger";
                        }
                    }
                }
                else
                {
                    lblAddWardDataMessage.Text = "Only CSV files are allowed";
                    lblAddWardDataMessage.CssClass = "alert-danger";
                }
            }
            else
            {
                lblAddWardDataMessage.Text = "Please select a file";
                lblAddWardDataMessage.CssClass = "alert-danger";
            }
        }
        catch (Exception ex)
        {
            lblAddWardDataMessage.Text = "Ward File Upload Error occured";
            lblAddWardDataMessage.CssClass = "alert-danger";
        }
    }

    private bool IsCsvFile(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName) && fileName.Contains('.'))
        {
            string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1, 3);

            if (fileExtension.Equals("csv", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}