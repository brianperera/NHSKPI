using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHSKPIBusinessControllers;
using NHSKPIDataService;
using NHSKPIDataService.Util;
using System.Web.Services;
using NHSKPIDataService.Models;
using System.Data;


public partial class Views_KPI_KPISearch : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private CommentController commentController = null;
    private UserController userController = null;   
    private NHSKPIDataService.Models.KPI kpi = null;
    private static NHSKPIDataService.Models.Comment comment = null;
    private User nhsUser = null;  
    
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
    public CommentController CommentController
    {
        get
        {
            if (commentController == null)
            {
                commentController = new CommentController();
            } 
            return commentController; 
        }
        set 
        { 
            commentController = value; 
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
    public static NHSKPIDataService.Models.Comment Comment
    {
        get
        {
            if (comment == null)
            {
                comment = new NHSKPIDataService.Models.Comment();
            } 
            return comment; 
        }
        set 
        { 
            comment = value; 
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
    public UserController UserController
    {
        get
        {
            if (userController == null)
            {
                userController = new UserController();
            } 
            return userController; 
        }
        set 
        { 
            userController = value; 
        }
    }
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillKPIGroupList();                       
            LoadSearchResult();           
        }
    }

    #endregion

    #region Search Button Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadSearchResult();
    }

    #endregion

    #region KPIGroupList
    /// <summary>
    /// List All KPI Group Name
    /// </summary>
    private void FillKPIGroupList()
    {
        ddlKPIGroupName.DataSource = KPIController.SearchKPIGroup(string.Empty, true);
        ddlKPIGroupName.DataTextField = "KPIGroupName";
        ddlKPIGroupName.DataValueField = "Id";
        ddlKPIGroupName.DataBind();

        ListItem li = new ListItem("", "0");
        ddlKPIGroupName.Items.Insert(0, li);
    }
    #endregion   

    #region Load Search Result

    private void LoadSearchResult()
    {
        gvSearchResult.DataSource = KPIController.SearchKPI(txtKPINo.Text, txtKPIDescription.Text, int.Parse(ddlKPIGroupName.SelectedValue), chkIsActive.Checked);
        gvSearchResult.DataBind();
    }

    #endregion

    #region Windows Pop up Row Bound
    protected void gvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow )
        {  
            LinkButton linkjobid = (LinkButton)e.Row.Cells[3].FindControl("lbButton");
            string id = "KPINo";
            string userId = "UserId";
            string access = "Access";         
            
            linkjobid.OnClientClick = String.Format("javascript:popUp(750,550, 'Transactions','Comment.aspx?{0}={1}&{2}={3}&{4}={5}'); return false;", id, gvSearchResult.DataKeys[e.Row.RowIndex]["KPINo"].ToString(), userId, NHSUser.Id,access,"Internal");
        }
    }
    #endregion
}
