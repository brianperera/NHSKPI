$(document).ready(function () {

    $("#txtPassword").hide();
    $("#txtRetypePassword").hide();

    ManageManualHospitalEntryField();
    ChangeHopitalDropDownPlaceHolderColor();

    var isPostBackObject = document.getElementById('isPostBack');

    if (isPostBackObject != null) {

    }
    else {
        $(".ward-bulk-upload").hide();
    }

    $('#ChbWardBulkUpload').change(function () {
        if ($(this).is(":checked")) {
            $(".ward-bulk-upload").show();

            //Disaable the normal data entry
            DisableWardDataEntryFields();
        }
        else {
            $(".ward-bulk-upload").hide();
            EnableWardDataEntryFields();
        }
    });

    //$.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());

    $('[placeholder]').focus(function () {
        var input = $(this);
        if (input.val() == input.attr('placeholder') && input.attr('type') != 'password') {
            input.val('');
            input.removeClass('placeholder');
        }
    }).blur(function () {
        var input = $(this);
        if ((input.val() == '' || input.val() == input.attr('placeholder')) && input.attr('type') != 'password') {
            input.addClass('placeholder');
            input.val(input.attr('placeholder'));
        }
    }).blur();

    $("#txtFakePassword").focus(function () {
        $(this).hide();
        var realInput = $("#txtPassword");
        realInput.show();
        realInput.focus();
    });

    $("#txtPassword").blur(function () {
        var realInput = $(this);
        if (realInput.val() == '') {
            realInput.hide();
            var fakeInput = $("#txtFakePassword");
            fakeInput.show();
            fakeInput.addClass('placeholder');
            //fakeInput.val(fakeInput.attr('placeholder'));
        }
    });

    $("#txtFakeRePassword").focus(function () {
        $(this).hide();
        var realInput = $("#txtRetypePassword");
        realInput.show();
        realInput.focus();
    });

    $("#txtRetypePassword").blur(function () {
        var realInput = $(this);
        if (realInput.val() == '') {
            realInput.hide();
            var fakeInput = $("#txtFakeRePassword");
            fakeInput.show();
            fakeInput.addClass('placeholder');
            //fakeInput.val(fakeInput.attr('placeholder'));
        }       
    });

});

function DisableWardDataEntryFields() {
    //Ward Page
    $("#ctl00_Contentbody_txtWardCode").attr("disabled", "disabled");
    $("#ctl00_Contentbody_txtWardName").attr("disabled", "disabled");
    $("#ctl00_Contentbody_ddlWardGroupName").attr("disabled", "disabled");
    $("#ctl00_Contentbody_chkIsActive").attr("disabled", "disabled");
    $("#ctl00_Contentbody_btnSave").hide();
    $("#ctl00_Contentbody_btnUpdate").hide();

    //Specialty Page
    $("#ctl00_Contentbody_ddlSpecialtyGroup").attr("disabled", "disabled");
    $("#ctl00_Contentbody_txtSpecialtyCode").attr("disabled", "disabled");
    $("#ctl00_Contentbody_txtSpecialty").attr("disabled", "disabled");
    $("#ctl00_Contentbody_txtNationalSpecialtyCode").attr("disabled", "disabled");
    $("#ctl00_Contentbody_txtNationalSpecialty").attr("disabled", "disabled");
    $("#ctl00_Contentbody_chkIsActive").attr("disabled", "disabled");
}

function EnableWardDataEntryFields() {
    $("#ctl00_Contentbody_txtWardCode").removeAttr("disabled");
    $("#ctl00_Contentbody_txtWardName").removeAttr("disabled");
    $("#ctl00_Contentbody_ddlWardGroupName").removeAttr("disabled");
    $("#ctl00_Contentbody_chkIsActive").removeAttr("disabled");
    $("#ctl00_Contentbody_btnSave").show();
    $("#ctl00_Contentbody_btnUpdate").show();

    //Specialty Page
    $("#ctl00_Contentbody_ddlSpecialtyGroup").removeAttr("disabled");
    $("#ctl00_Contentbody_txtSpecialtyCode").removeAttr("disabled");
    $("#ctl00_Contentbody_txtSpecialty").removeAttr("disabled");
    $("#ctl00_Contentbody_txtNationalSpecialtyCode").removeAttr("disabled");
    $("#ctl00_Contentbody_txtNationalSpecialty").removeAttr("disabled");
    $("#ctl00_Contentbody_chkIsActive").removeAttr("disabled");
}

function ManageManualHospitalEntryField() {

    //$("#ddlHospitalName").change(function () {

    //    var hospitalDDLSelection = $('#ddlHospitalName :selected').text();

    //    if (hospitalDDLSelection == "Other") {
    //        $("#txtCompanyName").show();
    //    }
    //    else {
    //        $("#txtCompanyName").hide();
    //    }

    //    ChangeHopitalDropDownPlaceHolderColor();
    //});
}

function ChangeHopitalDropDownPlaceHolderColor() {
    var hospitalDDLSelection = $('#ddlHospitalName :selected').text();

    if (hospitalDDLSelection == "Select Hospital" || hospitalDDLSelection == "Other") {
        $("#ddlHospitalName").css('color', '#AAA9A9');
    }
    else {
        $("#ddlHospitalName").css('color', 'black');
    }
}

function ShowPopup(message) {
    $(function () {
        $("#dialog").html(message);
        $("#dialog").dialog({
            title: "Information",
            width: 400,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            },
            modal: true
        });

        // Fix for replacing modal popup title bar close button
        var xSpan = $(".ui-icon-closethick");
        xSpan.removeAttr('class');
        xSpan.text("X");
    });
}