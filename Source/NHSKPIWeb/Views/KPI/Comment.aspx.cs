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
using System.Configuration;

public partial class Views_User_Comment : System.Web.UI.Page
{
    #region Private Variables

    private KPIController kpiController = null;
    private CommentController commentController = null;
    private UserController userController = null;
    private NHSKPIDataService.Models.KPI kpi = null;
    private static NHSKPIDataService.Models.Comment comment = null;
    private User nhsUser = null;
    private int kpiId;
    private int createdBy;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
   
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
    public int KpiId
    {
        get 
        {
            string kpiNo = (Request.QueryString["KPINo"]);
            DataSet ds = KPIController.SearchKPI(kpiNo, "", 0, true);
            kpiId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());

            return kpiId; 
        }
        set 
        { 
            kpiId = value; 
        }
    }
    public int CreatedBy
    {
        get 
        {
            if (Request.Url.AbsoluteUri.IndexOf("UserName") >= 0)
            {
                string userName = (Request.QueryString["UserName"]);
                DataSet dsAccess = UserController.SearchUser(userName, "", 0, 0, true);
                createdBy = Convert.ToInt32(dsAccess.Tables.Count > 0 && dsAccess.Tables[0].Rows.Count > 0 ? dsAccess.Tables[0].Rows[0]["Id"].ToString() : "-1" );

            }
            else if (Request.Url.AbsoluteUri.IndexOf("UserId") >= 0)
            {
                createdBy = NHSUser.Id;
            }
            return createdBy; 
        }
        set 
        { 
            createdBy = value; 
        }
    }
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    #endregion

    #region page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCommentsType();

            string userName = (Request.QueryString["UserName"]);
            DataSet dsAccess = UserController.SearchUser(userName, string.Empty, 0, 0, true);
            int userId = dsAccess.Tables[0].Rows.Count > 0 ? Convert.ToInt32(dsAccess.Tables[0].Rows[0]["Id"].ToString()) : -1;

            string kpiNo = (Request.QueryString["KPINo"]);
            DataSet ds = KPIController.SearchKPI(kpiNo, string.Empty, 0, true);
            int kpiNoCount = ds.Tables[0].Rows.Count;

            if (((Request.Url.AbsoluteUri.IndexOf("UserName") >= 0) && "External" == (Request.QueryString["Access"])) || ((Request.Url.AbsoluteUri.IndexOf("UserId") >= 0) && "Internal" == (Request.QueryString["Access"])))
            {

                if (CreatedBy > 0 && kpiNoCount > 0)
                {
                    lblKPINumberformat.Text = ds.Tables[0].Rows[0]["KPINo"].ToString();
                    lblKPINameFormat.Text = ds.Tables[0].Rows[0]["KPIDescription"].ToString();

                    FillUserList();
                    LoadCommentSearchResult();

                }
                else
                {
                    txtComments.Enabled = false;
                    btnSearchComment.Enabled = false;
                    btnSubmit.Enabled = false;
                    lblMessage.Text = Constant.MSG_Comment_Parameter_Invalid;
                    lblMessage.CssClass = "alert-danger";
                }
                
            }
            else
            {
                txtComments.Enabled = false;
                btnSearchComment.Enabled = false;
                btnSubmit.Enabled = false;
                lblMessage.Text = Constant.MSG_Comment_Parameter_Invalid;
                lblMessage.CssClass = "alert-danger";
            }


          
        }


    }
    #endregion
   
    #region UserList
    private void FillUserList()
    {
        ddlUser.DataSource = CommentController.GetCommentUsers(CreatedBy);
        ddlUser.DataTextField = "FullName";
        ddlUser.DataValueField = "Id";
        ddlUser.DataBind();

        ListItem li = new ListItem("", "0");
        ddlUser.Items.Insert(0, li);
    }
    #endregion

    #region Load Comments Type

    private void LoadCommentsType()
    {
        string[] commentTypes = Constant.CommentType.Split(',');
        foreach (string type in commentTypes)
        {
            ddlCommentsType.Items.Add(new ListItem(type, type));
        }
    }

    #endregion

    #region Load Comment Search
    private void LoadCommentSearchResult()
    {
        Comment.KpiNumber = (Request.QueryString["KPINo"]);        
        DataSet ds = KPIController.SearchKPI(Comment.KpiNumber, "", 0, true);

        int kpiId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
       // DateTime value = new DateTime(1900, 1, 1);
        DateTime createdDate = txtCreatedDate.Value == string.Empty ? DateTime.MinValue : new DateTime(int.Parse(txtCreatedDate.Value.Split('-')[2]), int.Parse(txtCreatedDate.Value.Split('-')[1]), int.Parse(txtCreatedDate.Value.Split('-')[0]));


        gvCommentsHistory.DataSource = CommentController.SearchComment(0, createdDate, kpiId);
        gvCommentsHistory.DataBind();
    }
    #endregion

    #region Set Comment
    private void SetComment()
    {
        Comment.KpiId = KpiId;
        Comment.Comments = txtComments.Text;
        Comment.CreatedDate = DateTime.Now.Date;
        Comment.CreatedBy = CreatedBy;
        Comment.CommentType = ddlCommentsType.SelectedItem.ToString();
           
    }
    #endregion 

    #region Add Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {    

        if ((Request.QueryString["Access"]) == "Internal")
        {
            SetComment();
            CommentController.AddComment(comment);
            LoadCommentSearchResult();
        }
        else if ((Request.QueryString["Access"]) == "External")
        {
            SetComment();
            CommentController.AddComment(comment);
            LoadCommentSearchResult();
        }
        FillUserList();

      
    }
    #endregion

    #region Search Button
    protected void btnSearchComment_Click(object sender, EventArgs e)
    {
        DataSet ds = KPIController.SearchKPI(Comment.KpiNumber, "", 0, true);
        int kpiId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());

        DateTime createdDate = txtCreatedDate.Value == string.Empty ? DateTime.MinValue : new DateTime(int.Parse(txtCreatedDate.Value.Split('-')[2]), int.Parse(txtCreatedDate.Value.Split('-')[1]), int.Parse(txtCreatedDate.Value.Split('-')[0]));

        gvCommentsHistory.DataSource = CommentController.SearchComment(int.Parse(ddlUser.SelectedValue), createdDate, kpiId);
        gvCommentsHistory.DataBind();

    }
    #endregion

    #region gv Comments History Row Data Bound

    protected void gvCommentsHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].ToolTip = e.Row.Cells[2].Text;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Length > 80 ? e.Row.Cells[2].Text.Substring(0, 79) : e.Row.Cells[2].Text;
            LinkButton l = (LinkButton)e.Row.FindControl("linkDelete");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?')");
            if (gvCommentsHistory.DataKeys[e.Row.RowIndex]["CreatedBy"].ToString() == CreatedBy.ToString())
            {
                e.Row.Cells[3].Enabled = true;
            }
            else
            {
                e.Row.Cells[3].Text = "";
            }
            
        }
    }

    #endregion

    #region Sort Grid View

    private void SortGridView(string sortExpression, string direction)
    {
        DataSet ds = KPIController.SearchKPI(Comment.KpiNumber, "", 0, true);
        int kpiId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());

        DateTime createdDate = txtCreatedDate.Value == string.Empty ? DateTime.MinValue : new DateTime(int.Parse(txtCreatedDate.Value.Split('-')[2]), int.Parse(txtCreatedDate.Value.Split('-')[1]), int.Parse(txtCreatedDate.Value.Split('-')[0]));
        DataTable dt = CommentController.SearchComment(int.Parse(ddlUser.SelectedValue), createdDate, kpiId).Tables[0];

        DataView dv = new DataView(dt);
        dv.Sort = sortExpression + direction;

        gvCommentsHistory.DataSource = dv;
        gvCommentsHistory.DataBind();
    }

    #endregion

    #region gv Comments History Sorting Event

    protected void gvCommentsHistory_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }
    }

    #endregion

    #region  gv Comments History Row Deleting Event
    protected void gvCommentsHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvCommentsHistory.DataKeys[e.RowIndex].Value;
        CommentController.DeleteComment(id);
        DataSet ds = KPIController.SearchKPI(Comment.KpiNumber, "", 0, true);
        int kpiId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());

        DateTime createdDate = txtCreatedDate.Value == string.Empty ? DateTime.MinValue : new DateTime(int.Parse(txtCreatedDate.Value.Split('-')[2]), int.Parse(txtCreatedDate.Value.Split('-')[1]), int.Parse(txtCreatedDate.Value.Split('-')[0]));

        gvCommentsHistory.DataSource = CommentController.SearchComment(int.Parse(ddlUser.SelectedValue), createdDate, kpiId);
        gvCommentsHistory.DataBind();
    }
    #endregion


}
