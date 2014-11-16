using NHSKPIBusinessControllers;
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

        string autoCompleteHospitalScript;

        //foreach (var item in AllHospitals.Table.Rows)
        //{
        //    hospitals = hospitals + "" +item.ToString();
        //}
        autoCompleteHospitalScript = "\n<script type=\"text/javascript\" language=\"Javascript\" id=\"EventScriptBlock\">\n";
        autoCompleteHospitalScript += "$(function () { var availableTags = [ ";

        int i = 0;
        foreach (DataRow hospital in AllHospitals.ToTable().Rows)
        {
            autoCompleteHospitalScript += string.Format("\"{0}\",", hospital[1]);
            i++;
        }

        autoCompleteHospitalScript += "]; $(\"#ddlHospitalName_txbAutoCompleteTextbox\").autocomplete({ source: availableTags }); });";
        autoCompleteHospitalScript += "\n\n </script>";

        //AutoCompleteValues.InnerHtml = hospitals;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "AutoCompleteScript", autoCompleteHospitalScript, false);
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