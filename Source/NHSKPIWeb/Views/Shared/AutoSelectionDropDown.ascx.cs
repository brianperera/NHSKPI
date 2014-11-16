﻿using NHSKPIBusinessControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_Shared_AutoSelectionDropDown : System.Web.UI.UserControl
{
    private HospitalController hospitalController = null;
    public string SelectedHospitalName{ get; set; }
    public string SelectedHospitalCode { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        PopulateHospitalList();
        SetHospital();
    }

    private void SetHospital()
    {
        SelectedHospitalName = ddlHospitalName.SelectedItem.Text;
        SelectedHospitalCode = ddlHospitalName.SelectedItem.Value;
    }

    private void PopulateHospitalList()
    {
        ddlHospitalName.DataSource = AllHospitals;
        ddlHospitalName.DataValueField = "Code";
        ddlHospitalName.DataTextField = "Name";
        ddlHospitalName.DataBind();

        //Append the Other value to the datasource, this will allow the user to manualy key-in the hospital name
        ddlHospitalName.Items.Insert(ddlHospitalName.Items.Count, new ListItem("Other"));
    }

    public HospitalController HospitalController
    {
        get
        {
            if (hospitalController == null)
            {
                hospitalController = new HospitalController();
            }

            return hospitalController;
        }
        set
        {
            hospitalController = value;
        }
    }

    public DataView AllHospitals
    {
        get
        {
            return HospitalController.GetAllHospitals();
        }
    }
}