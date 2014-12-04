    private UtilController utilController = null;
    private User nhsUser = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadHospitalLevelEmailList();

        if (!IsPostBack)
        {
            
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

    public void LoadHospitalLevelEmailList()
    {
        //READ
        utilController = new UtilController();
        lbEmailList.DataSource = utilController.GetEmailList(NHSUser.HospitalId);
        lbEmailList.DataBind();
    }

    protected void btnAddEmail_Click(object sender, EventArgs e)
    {
        Email currentEmail = new Email();
        currentEmail.EmailAddress = txtEmail.Text;
        currentEmail.Id = 0;
        currentEmail.Description = txtEmail.Text;
        currentEmail.HospitalId = NHSUser.HospitalId;

        utilController = new UtilController();
        utilController.InsertEmailToBucket(currentEmail);
    }