$(document).ready(function() {

   
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

function showhide(plusimageId, minusImageId, divId, clickon) {

    if (clickon == 'plus') {
        document.getElementById('ctl00_Contentbody_' + plusimageId).style.display = 'none';
        document.getElementById('ctl00_Contentbody_' + minusImageId).style.display = 'block';
        document.getElementById('ctl00_Contentbody_' + divId).style.display = 'none';

    }
    if (clickon == 'minus') {
        document.getElementById('ctl00_Contentbody_' + plusimageId).style.display = 'block';
        document.getElementById('ctl00_Contentbody_' + minusImageId).style.display = 'none';
        document.getElementById('ctl00_Contentbody_' + divId).style.display = 'block';

    }
    return false;
}