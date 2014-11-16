using NHSKPIBusinessControllers;
using NHSKPIDataService.Models;
using NHSKPIDataService.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_Shared_AddKPIGroup : System.Web.UI.UserControl
{
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


    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        KPIController kpiController = new KPIController();
        KPIGroup kpiGroup = new KPIGroup
        {
            HospitalID = NHSUser.HospitalId,
            IsActive = chbIsActive.Checked,
            KpiGroupName = txtKPIGroupName.Text
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
            }
        }
        else
        {
            lblAddKpiGroupMessage.Text = Constant.MSG_KPIGroup_Empty;
            lblAddKpiGroupMessage.CssClass = "alert-danger";
        }
    }

    #endregion
}