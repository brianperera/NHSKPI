$(document).ready(function () {

    $("#btnstatichide").click(function() {

    $('#btnstaticshow').show();
    $('#btnstatichide').hide();
    $('#static').hide();

});

$("#btnstaticshow").click(function() {

$('#btnstatichide').show();
    $('#btnstaticshow').hide();
    $('#static').show();

});

$("#ctl00_Contentbody_imgBtnNext").click(function() {
    $("#divDynamicTarget").hide();
    $("#divStaticTarget").hide(); 
    $("#divButtons").hide();   


});

$("#ctl00_Contentbody_imgBtnPrevoius").click(function() {
    $("#divDynamicTarget").hide();
    $("#divStaticTarget").hide(); 
    $("#divButtons").hide();
});
      

});


function isDecimalKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 8 && charCode != 189) {
        return false;
    }
    return true;
}

function ClearControls()
{

 $("#ctl00_Contentbody_txtYTDTargetGreen").val("");
 $("#ctl00_Contentbody_revYTDTargetGreen").hide();
 $("#ctl00_Contentbody_txtYTDTargetAmber").val("");
 $("#ctl00_Contentbody_revYTDTargetAmber").hide();
 $("#ctl00_Contentbody_txtYTDTargetDescription").val("");
 $("#ctl00_Contentbody_rfYTDTargetDescription").hide();
 }
 
 function ConfirmSaveAll()
{
    var ddlHospital = document.getElementById("ctl00_Contentbody_ddlHospitalName");
    var hospitalId = ddlHospital.options[ddlHospital.selectedIndex].value;   

    
     var ddlKPI = document.getElementById("ctl00_Contentbody_ddlKPI");
    var kpiId = ddlKPI.options[ddlKPI.selectedIndex].value;
   
    if ((hospitalId == 0) || (kpiId == 0))
    {
        if(confirm('\'All\' option will overwrite the existing records related to selected criteria.\n Are you sure you want overwrite the records for your selection?'))
        { 
            return true;
        }
        else
        {
            return false;
        }
    }
}

function ConfirmUpdate()
{
    if (Page_ClientValidate()) {
        if(confirm('Are you sure you want yo update this record?'))
        { 
            return true;
        }
        else
        {
            return false;
        }
     }
     else
     {
        return false;
     }   
}
