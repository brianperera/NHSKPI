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

public partial class QuickStart : System.Web.UI.Page
{
    #region Fields

    private KPIController kPIController = null;
    private UserController userController = null;

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

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateKPIGroupResult();
        UpdateUserList();

        if (!IsPostBack)
        {

        }
    }

    #endregion

    #region Events

    //Wizard 1st step
    protected void btnUploadWardFile_Click(object sender, EventArgs e)
    {
        UploadCSV();
    }

    //Wizard 1st step
    protected void btnSpecialtyDataUpload_Click(object sender, EventArgs e)
    {
        //TODO
    }

    //Wizard 2nd step
    protected void btnAddKpiGroup_Click(object sender, EventArgs e)
    {
        KPIController kpiController = new KPIController();
        KPIGroup kpiGroup = new KPIGroup
        {
            HospitalID = NHSUser.HospitalId,
            IsActive = true,
            KpiGroupName = txtKpiGroupName.Text
        };

        if (NHSUser.HospitalId > 0 && !string.IsNullOrEmpty(kpiGroup.KpiGroupName))
        {
            if (kpiController.AddKPIGroup(kpiGroup) < 0)
            {
                lblAddKpiGroupMessage.Text = Constant.MSG_KPIGroup_Exist;
                lblAddKpiGroupMessage.CssClass = "alert-danger";
            }
            else
            {
                lblAddKpiGroupMessage.Text = Constant.MSG_KpiGroup_Success_Add;
                lblAddKpiGroupMessage.CssClass = "alert-success";
                UpdateKPIGroupResult();
            }
        }
        else
        {
            lblAddKpiGroupMessage.Text = Constant.MSG_KPIGroup_Empty;
            lblAddKpiGroupMessage.CssClass = "alert-danger";
        }

    }

    protected void btnAddUpdateManagerDetails_Click(object sender, EventArgs e)
    {
        userController = new UserController();

        DepartmentHead departmentHead = new DepartmentHead
        {
            Id = 0,
            Name = txbManagerName.Text,
            MobileNo = txbManagerContactDetails.Text,
            JobTitle = txbManagerJobTitle.Text,
            Email = txbManagerEmail.Text,
            ApprovedUserId = NHSUser.Id
        };

        if (userController.InsertUpdateDepartmentHead(departmentHead))
        {
            lbAddUpdateManagerDetailsMessage.Text = "Department Head's details successfully added";
            lbAddUpdateManagerDetailsMessage.CssClass = "alert-success";
        }
        else
        {
            lbAddUpdateManagerDetailsMessage.Text = "Error occured, record not added";
            lbAddUpdateManagerDetailsMessage.CssClass = "alert-danger";
        }
    }

    protected void btnAddUsers_Click(object sender, EventArgs e)
    {
        userController = new UserController();

        int defaultUserRoleId = 4;
        int.TryParse(ConfigurationManager.AppSettings["DefaultUserRole"].ToString(), out defaultUserRoleId);

        User user = new NHSKPIDataService.Models.User
        {
             UserName = txbUsername.Text,
             Password = ConfigurationManager.AppSettings["DefaultUserPassword"].ToString(), 
             FirstName = txbFirstName.Text,
             LastName = txbLastName.Text,
             Email = txbEmail.Text,
             MobileNo = txbMobile.Text,
             LastLogDate = DateTime.UtcNow,
             RoleId = defaultUserRoleId,
             HospitalId = NHSUser.HospitalId,
             IsActive = true,
             CreatedDate = DateTime.UtcNow,
             CreatedBy = NHSUser.Id
        };

        if (userController.AddUser(user, true) > 0)
        {
            lbAddUserMessage.Text = "User Successfully added";
            lbAddUserMessage.CssClass = "alert-success";

            UpdateUserList();
        }
        else
        {
            lbAddUserMessage.Text = "Error occured, record not added";
            lbAddUserMessage.CssClass = "alert-danger";
        }
    }

    #endregion

    #region Private methods

    private void UpdateUserList()
    {
        userController = new UserController();
        UserListGridView.DataSource = userController.SearchUser(string.Empty, string.Empty, 0, NHSUser.HospitalId, true).Tables[0];
        UserListGridView.DataBind();
    }

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
                       new StreamReader(fuWardDataUpload.PostedFile.InputStream), true))
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
                            if (dsInitialData.Tables[0].Select("WardCode = '" + dr["WardCode"].ToString() + "'").Length == 0)
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
                            dr["HospitalId"] = NHSUser.HospitalId.ToString();
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
                            lblAddWardDataMessage.Text = "WardCode is missing or not exist at row " + (dt.Rows.IndexOf(wcRows[0]) + 1).ToString();
                            lblAddWardDataMessage.CssClass = "alert-danger";

                        }
                        else if (knRows.Length > 0)
                        {
                            lblAddWardDataMessage.Text = "KPINo is missing or not exist at row " + (dt.Rows.IndexOf(knRows[0]) + 1).ToString();
                            lblAddWardDataMessage.CssClass = "alert-danger";
                        }
                        else if (tmRows.Length > 0)
                        {
                            lblAddWardDataMessage.Text = "TargetDate is missing or invalid format at row " + (dt.Rows.IndexOf(tmRows[0]) + 1).ToString();
                            lblAddWardDataMessage.CssClass = "alert-danger";
                        }
                        else if (nuRows.Length > 0)
                        {
                            lblAddWardDataMessage.Text = "Numerator is missing or invalid format at row " + (dt.Rows.IndexOf(nuRows[0]) + 1).ToString();
                            lblAddWardDataMessage.CssClass = "alert-danger";
                        }
                    }
                    else
                    {
                        if (kPIController.UpdateCSVWardData(dt))
                        {
                            lblAddWardDataMessage.Text = "CSV File successfully uploaded";
                            lblAddWardDataMessage.CssClass = "alert-success";
                        }
                        else
                        {
                            lblAddWardDataMessage.Text = "CSV File Upload Error occured";
                            lblAddWardDataMessage.CssClass = "alert-danger";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblAddWardDataMessage.Text = "CSV File Upload Error occured";
                    lblAddWardDataMessage.CssClass = "alert-danger";
                }
            }
        }
        catch (Exception ex)
        {
            lblAddWardDataMessage.Text = "CSV File Upload Error occured";
            lblAddWardDataMessage.CssClass = "alert-danger";
        }
    }

    private void UpdateKPIGroupResult()
    {
        KPIController kpiController = new KPIController();
        lbKpiGroups.DataSource = kpiController.SearchKPIGroup(string.Empty, true).Tables[0];
        lbKpiGroups.DataTextField = "KPIGroupName";
        lbKpiGroups.DataBind();
    }

    #endregion
}
