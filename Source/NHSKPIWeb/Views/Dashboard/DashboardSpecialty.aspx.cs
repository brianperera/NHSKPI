using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NHSKPIBusinessControllers;
using NHSKPIDataService.Models;

public partial class Views_Dashboard_DashboardSpecialty : System.Web.UI.Page
{
    #region Private variable

    private Configuration nhsConfiguration = null;

    #endregion

    #region Properties

    public Configuration NHSConfiguration
    {
        get
        {
            if (Session["NHSConfiguration"] != null)
            {
                nhsConfiguration = (Configuration)Session["NHSConfiguration"];
            }

            return nhsConfiguration;
        }
    }

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadWardGroup();
            GeneratDashBoardDetail();
            SetTab();
        }
    }

    #endregion

    #region Generate the Dash Board Detail

    private void GeneratDashBoardDetail()
    {


        System.Text.StringBuilder strDashBoardHeader = new System.Text.StringBuilder();
        strDashBoardHeader.Append("<table cellspacing='0' border='1' style='border-collapse: collapse;' class='grid'>" +
            "<tr>" +
                "<td class='border_less' colspan='2'>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Apr" +
                    "</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "May</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Jun</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Jul</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Aug</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Sep</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Oct</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Nov</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Dec</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Jan</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Feb</label>" +
                "</td>" +
                "<td class='hdr_bg' colspan='2'>" +
                    "<label>" +
                        "Mar</label>" +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td class='border_less' colspan='2'>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
               "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
                "<td class='alert-success'>" +
                    "<i class='icon-ok'></i>" +
                "</td>" +
                "<td class='alert-danger'>" +
                    "<i class='icon-remove'></i>" +
                "</td>" +
            "</tr>");
        DataSet dsWard = new NHSKPIDataService.Services.UtilService().GetDashBoardSpecialtyData(Master.NHSUser.HospitalId, int.Parse(ddlWardGroup.SelectedValue));
        DataSet dsKPI = new NHSKPIDataService.Services.UtilService().GetDashBoardSpecialtyData(Master.NHSUser.HospitalId, int.Parse(ddlWardGroup.SelectedValue));
        DataSet dsData = new NHSKPIDataService.Services.UtilService().GetDashBoardSpecialtyData(Master.NHSUser.HospitalId, int.Parse(ddlWardGroup.SelectedValue));


        int newRow = 0;
        System.Text.StringBuilder strDashBoardItem = new System.Text.StringBuilder();
        foreach (DataRow rowWard in dsWard.Tables[0].Rows)
        {
            DateTime targetMonthFrom = new DateTime(((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1), 4, 1);
            DateTime targetMonthTo = targetMonthFrom.AddYears(1).AddDays(-1);


            for (DateTime targetMonth = targetMonthFrom; targetMonth < targetMonthTo; targetMonth = targetMonth.AddMonths(1))
            {
                int com = 0;
                int incom = 0;

                foreach (DataRow rowKPI in dsKPI.Tables[1].Rows)
                {
                    DataRow[] rowData = dsData.Tables[2].Select("Specialty = '" + rowWard["Specialty"].ToString() + "' AND TargetMonth = '" + targetMonth.Date.ToString("yyyy-MM-dd") + "' AND KPIId = " + Convert.ToInt32(rowKPI["Id"]));


                    if (rowData.Length > 0)
                    {
                        foreach (DataRow rowItem in rowData)
                        {
                            if (rowItem["num"].ToString() == "0" || rowItem["den"].ToString() == "0" || rowItem["ytd"].ToString() == "0")
                            {
                                incom++;

                            }

                            else
                            {
                                com++;
                            }

                        }
                    }
                    else
                    {


                        incom++;
                    }


                }

                if (newRow == 0)
                {
                    string currentFinYear = ((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1).ToString() + "-" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year).ToString();
                    strDashBoardItem.Append("<tr>");
                    strDashBoardItem.Append("<td class='ward_name' colspan='2'>" +
                       "<a href='../KPI/SpecialtyLevelDataBulkKPI.aspx?HospitalId=" + Master.NHSUser.HospitalId + "&SpecialtyId=" + rowWard["Id"].ToString() + "&FinancailYear=" + currentFinYear + "' >" + rowWard["Specialty"].ToString() + "</a>" +
                    "</td>");
                }
                newRow++;
                string inctxt = string.Empty;
                string ctxt = string.Empty;
                if (targetMonth < DateTime.Now.Date)
                {
                    inctxt = incom.ToString();
                    ctxt = com.ToString();
                }
                else
                {
                    inctxt = string.Empty;
                    ctxt = string.Empty;
                }
                strDashBoardItem.Append("<td>" +
                    ctxt +
                "</td>" +
                "<td>" +
                inctxt
                  +
                "</td>");

                if (newRow == 12)
                {
                    strDashBoardItem.Append("</tr>");
                    newRow = 0;
                }
            }


        }
        strDashBoardHeader.Append(strDashBoardItem.ToString());
        strDashBoardHeader.Append("</table>");
        divDashBoard.InnerHtml = strDashBoardHeader.ToString();
    }

    #endregion

    #region Load Ward Group

    private void LoadWardGroup()
    {

        DataSet ds = new WardGroupController().SearchWardGroup(string.Empty, 0, true);
        ddlWardGroup.DataSource = ds.Tables[0];
        ddlWardGroup.DataTextField = "WardGroupName";
        ddlWardGroup.DataValueField = "Id";
        ddlWardGroup.DataBind();
        ListItem li = new ListItem("--ALL--", "0");
        ddlWardGroup.Items.Insert(0, li);

    }

    #endregion

    #region Search Button Click

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GeneratDashBoardDetail();
    }

    #endregion

    #region Set Tab

    private void SetTab()
    {
        liWard.Visible = false;
        liSpecialty.Visible = false;

        if (NHSConfiguration.TargetApply == "1")
        {
            liWard.Visible = true;
            
        }
        else if (NHSConfiguration.TargetApply == "2")
        {
            liSpecialty.Visible = true;
        }
        else if (NHSConfiguration.TargetApply == "3")
        {
            liWard.Visible = true;
            liSpecialty.Visible = true;
        }
    }

    #endregion
}