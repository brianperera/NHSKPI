$(document).ready(function () {

    $("#btnhideward").click(function() {

    $('#btnshowward').show();
    $('#btnhideward').hide();
    $('#ward').hide();

});

$("#btnshowward").click(function() {

$('#btnhideward').show();
    $('#btnshowward').hide();
    $('#ward').show();

});

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
    $("#divButtons").hide();
    


});

$("#ctl00_Contentbody_imgBtnPrevoius").click(function() {

    $("#divDynamicTarget").hide();
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


function DisableControls()
{
    var d = new Date();
    
    var year = d.getFullYear()
    var month = d.getMonth()+1;
    
    var newYearValue = 0;
    var currentFinYear = 0;
    if (month > 3)
    {
        newYearValue = parseInt(year) + 1;
         currentFinYear = year + "-" + newYearValue;
    }
    if (month < 4)
    {
        newYearValue = parseInt(year) - 1;
        currentFinYear = newYearValue + "-" + year;
    }   
   
   
    var finayear =  $("#ctl00_Contentbody_lblCurentFinancialYear").text();
    if ((finayear == currentFinYear))
    {
        $("#ctl00_Contentbody_imgBtnNext").prop("disabled",true);
        }
}

function ClearControls()
{

 $("#ctl00_Contentbody_txtGreenApril").val("");

 
 $("#ctl00_Contentbody_txtAmberApril").val("");

}

function ConfirmUpdate()
{
    if (Page_ClientValidate()) {
        if(confirm('Are you sure you want to update this record?'))
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

