$(document).ready(function () {

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

});

function DisableWardDataEntryFields()
{
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

    function ChangeHopitalDropDownPlaceHolderColor()
    {
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
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
        });
    }