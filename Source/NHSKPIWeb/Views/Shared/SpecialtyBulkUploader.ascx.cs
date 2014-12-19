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

public partial class Views_Shared_SpecialtyBulkUploader : System.Web.UI.UserControl
{
    #region Fields

    private KPIController kPIController = null;
    private UserController userController = null;
    private SpecialtyController specialtyController = null;
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

    #endregion

    //Wizard 1st step
    protected void btnSpecialtyDataUpload_Click(object sender, EventArgs e)
    {
        UploadSpecialtyDataFile();
    }

    #region Methods

    private void UploadSpecialtyDataFile()
    {
        try
        {
            if (!string.IsNullOrEmpty(fuSpecialtyDataUpload.PostedFile.FileName))
            {
                if (IsCsvFile(fuSpecialtyDataUpload.PostedFile.FileName))
                {
                    using (CsvReader csv = new CsvReader(
                                new StreamReader(fuSpecialtyDataUpload.PostedFile.InputStream), true))
                    {

                        DataTable dt = new DataTable();
                        dt.TableName = "SpecialtyData";
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
                        dc.ColumnName = "SpecialtyCode";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "Specialty";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "GroupId";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "NationalSpecialty";
                        dc.Unique = false;
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.DataType = System.Type.GetType("System.String");
                        dc.ColumnName = "NationalCode";
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
                        "SpecialtyCode",
                        "Specialty",
                        "GroupId", // AKA Ward Group
                        "NationalSpecialty",
                        "NationalCode",
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

                            DataRow[] scRows = dt.Select("SpecialtyCode = ''");
                            DataRow[] spRows = dt.Select("Specialty = ''");
                            //DataRow[] gidRows = dt.Select("GroupId IS NULL");
                            //DataRow[] nspRows = dt.Select("NationalSpecialty = ''");
                            //DataRow[] ncRows = dt.Select("NationalCode = ''");

                            string rowErrorMessage = string.Empty;

                            //if (scRows.Length > 0 || spRows.Length > 0 || gidRows.Length > 0 || nspRows.Length > 0 || ncRows.Length > 0)
                            if (scRows.Length > 0 || spRows.Length > 0)
                            {
                                if (scRows.Length > 0)
                                {
                                    rowErrorMessage = "Specialty Code is missing or not exist at row " + (dt.Rows.IndexOf(scRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                }
                                if (spRows.Length > 0)
                                {
                                    rowErrorMessage = "Specialty is missing or not exist at row " + (dt.Rows.IndexOf(spRows[0]) + 1).ToString();
                                    errorMessage = errorMessage + ", " + rowErrorMessage;
                                    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                }
                                //if (gidRows.Length > 0)
                                //{
                                //    rowErrorMessage = "Ward Group ID is missing or invalid format at row " + (dt.Rows.IndexOf(gidRows[0]) + 1).ToString();
                                //    errorMessage = errorMessage + ", " + rowErrorMessage;
                                //    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                //}
                                //if (nspRows.Length > 0)
                                //{
                                //    rowErrorMessage = "National Specialty is missing or invalid format at row " + (dt.Rows.IndexOf(nspRows[0]) + 1).ToString();
                                //    errorMessage = errorMessage + ", " + rowErrorMessage;
                                //    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                //}

                                //if (ncRows.Length > 0)
                                //{
                                //    rowErrorMessage = "National Code is missing or invalid format at row " + (dt.Rows.IndexOf(ncRows[0]) + 1).ToString();
                                //    errorMessage = errorMessage + ", " + rowErrorMessage;
                                //    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                //}
                            }
                            else
                            {
                                specialtyController = new SpecialtyController();

                                if (specialtyController.BulkAddUpdateSpecialty(dt))
                                {
                                    AddSpecialtyDataMessage.Text = "Specialty file successfully uploaded";
                                    AddSpecialtyDataMessage.CssClass = "alert-success";
                                }
                                else
                                {
                                    AddSpecialtyDataMessage.Text = "Specialty file upload error occured";
                                    AddSpecialtyDataMessage.CssClass = "alert-danger";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AddSpecialtyDataMessage.Text = "Specialty file upload error occured";
                            AddSpecialtyDataMessage.CssClass = "alert-danger";
                        }
                    }
                }
                else
                {
                    AddSpecialtyDataMessage.Text = "Only CSV files are allowed";
                    AddSpecialtyDataMessage.CssClass = "alert-danger";
                }
            }
            else
            {
                AddSpecialtyDataMessage.Text = "Please select a file";
                AddSpecialtyDataMessage.CssClass = "alert-danger";
            }
        }
        catch (Exception ex)
        {
            AddSpecialtyDataMessage.Text = "Specialty File Upload Error occured";
            AddSpecialtyDataMessage.CssClass = "alert-danger";
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