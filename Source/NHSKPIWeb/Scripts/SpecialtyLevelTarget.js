$(document).ready(function () {

    $("#btnhideward").click(function () {

        $('#btnshowward').show();
        $('#btnhideward').hide();
        $('#ward').hide();

    });

    $("#btnshowward").click(function () {

        $('#btnhideward').show();
        $('#btnshowward').hide();
        $('#ward').show();

    });

    $("#btnstatichide").click(function () {

        $('#btnstaticshow').show();
        $('#btnstatichide').hide();
        $('#static').hide();

    });

    $("#btnstaticshow").click(function () {

        $('#btnstatichide').show();
        $('#btnstaticshow').hide();
        $('#static').show();

    });
    //=======
    $("#ctl00_Contentbody_imgBtnNext").click(function () {
        $("#divDynamicTarget").hide();
        $("#divStaticTarget").hide();
        $("#divButtons").hide();


    });

    $("#ctl00_Contentbody_imgBtnPrevoius").click(function () {
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

function ClearControls() {
    $("#ctl00_Contentbody_txtDescriptionApril").val("");
    $("#ctl00_Contentbody_txtDescriptionMay").val("");
    $("#ctl00_Contentbody_txtDescriptionJune").val("");
    $("#ctl00_Contentbody_txtDescriptionJuly").val("");
    $("#ctl00_Contentbody_txtDescriptionAug").val("");
    $("#ctl00_Contentbody_txtDescriptionSep").val("");
    $("#ctl00_Contentbody_txtDescriptionOct").val("");
    $("#ctl00_Contentbody_txtDescriptionNov").val("");
    $("#ctl00_Contentbody_txtDescriptionDec").val("");
    $("#ctl00_Contentbody_txtDescriptionJan").val("");
    $("#ctl00_Contentbody_txtDescriptionFeb").val("");
    $("#ctl00_Contentbody_txtDescriptionMarch").val("");

    $("#ctl00_Contentbody_txtGreenApril").val("");
    $("#ctl00_Contentbody_revGreenApril").hide();
    $("#ctl00_Contentbody_txtGreenMay").val("");
    $("#ctl00_Contentbody_revGreenMay").hide();
    $("#ctl00_Contentbody_txtGreenJune").val("");
    $("#ctl00_Contentbody_revGreenJune").hide();
    $("#ctl00_Contentbody_txtGreenJuly").val("");
    $("#ctl00_Contentbody_revGreenJuly").hide();
    $("#ctl00_Contentbody_txtGreenAug").val("");
    $("#ctl00_Contentbody_revGreenAug").hide();
    $("#ctl00_Contentbody_txtGreenSep").val("");
    $("#ctl00_Contentbody_revGreenSep").hide();

    $("#ctl00_Contentbody_txtGreenOct").val("");
    $("#ctl00_Contentbody_revGreenOct").hide();
    $("#ctl00_Contentbody_txtGreenNov").val("");
    $("#ctl00_Contentbody_revGreenNov").hide();
    $("#ctl00_Contentbody_txtGreenDec").val("");
    $("#ctl00_Contentbody_revGreenDec").hide();
    $("#ctl00_Contentbody_txtGreenJan").val("");
    $("#ctl00_Contentbody_revGreenJan").hide();
    $("#ctl00_Contentbody_txtGreenFeb").val("");
    $("#ctl00_Contentbody_revGreenFeb").hide();
    $("#ctl00_Contentbody_txtGreenMarch").val("");
    $("#ctl00_Contentbody_revGreenMarch").hide();

    $("#ctl00_Contentbody_txtAmberApril").val("");
    $("#ctl00_Contentbody_revAmberApril").hide();
    $("#ctl00_Contentbody_txtAmberMay").val("");
    $("#ctl00_Contentbody_revAmberMay").hide();

    $("#ctl00_Contentbody_txtAmberJune").val("");
    $("#ctl00_Contentbody_revAmberJune").hide();
    $("#ctl00_Contentbody_txtAmberJuly").val("");
    $("#ctl00_Contentbody_revAmberJuly").hide();

    $("#ctl00_Contentbody_txtAmberAug").val("");
    $("#ctl00_Contentbody_revAmberAug").hide();

    $("#ctl00_Contentbody_txtAmberSep").val("");
    $("#ctl00_Contentbody_revAmberSep").hide();
    $("#ctl00_Contentbody_txtAmberOct").val("");
    $("#ctl00_Contentbody_revAmberOct").hide();

    $("#ctl00_Contentbody_txtAmberNov").val("");
    $("#ctl00_Contentbody_revAmberNov").hide();
    $("#ctl00_Contentbody_txtAmberDec").val("");
    $("#ctl00_Contentbody_revAmberDec").hide();

    $("#ctl00_Contentbody_txtAmberJan").val("");
    $("#ctl00_Contentbody_revAmberJan").hide();

    $("#ctl00_Contentbody_txtAmberFeb").val("");
    $("#ctl00_Contentbody_revAmberFeb").hide();
    $("#ctl00_Contentbody_txtAmberMarch").val("");
    $("#ctl00_Contentbody_revAmberMarch").hide();

    $("#ctl00_Contentbody_txtStaticTargetGreen").val("");
    $("#ctl00_Contentbody_revStaticTargetGreen").hide();

    $("#ctl00_Contentbody_txtStaticTargetAmber").val("");
    $("#ctl00_Contentbody_revStaticTargetAmber").hide();

    $("#ctl00_Contentbody_hdnKPIType").val("");

}

function ConfirmSaveAll() {
    if (Page_ClientValidate()) {

        var ddlSpecialty = document.getElementById("ctl00_Contentbody_ddlSpecialty");
        var SpecialtyId = ddlSpecialty.options[ddlSpecialty.selectedIndex].value;

        var ddlKPI = document.getElementById("ctl00_Contentbody_ddlKPI");
        var kpiId = ddlKPI.options[ddlKPI.selectedIndex].value;

        if ((SpecialtyId == 0) || (kpiId == 0)) {
            if (confirm('You have selected All Wards. This will update all the wards with the specified targets.Do you want to proceed?')) {
                return true; ;
            }
            else {
                return false;
            }
        }
    }
    else {

        return false;
    }
}

function ConfirmUpdate() {
    if (Page_ClientValidate()) {
        if (confirm('Are you sure you want to update this record?')) {
            return true;
        }
        else {
            return false;
        }
    }
    else {

        return false;
    }

}

function CopyMonthly() {
    document.getElementById("ctl00_Contentbody_txtStaticTargetDescriptionYTD").value = document.getElementById("ctl00_Contentbody_txtStaticTargetDescription").value;
    document.getElementById("ctl00_Contentbody_txtStaticTargetGreenYTD").value = document.getElementById("ctl00_Contentbody_txtStaticTargetGreen").value;
    document.getElementById("ctl00_Contentbody_txtStaticTargetAmberYTD").value = document.getElementById("ctl00_Contentbody_txtStaticTargetAmber").value;

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