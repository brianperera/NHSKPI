$(document).ready(function () {

    ManageManualHospitalEntryField();
    ChangeHopitalDropDownPlaceHolderColor();
});

function ManageManualHospitalEntryField() {

    $("#ddlHospitalName").change(function () {

        var hospitalDDLSelection = $('#ddlHospitalName :selected').text();

        if (hospitalDDLSelection == "Other") {
            $("#txtCompanyName").show();
        }
        else {
            $("#txtCompanyName").hide();
        }

        ChangeHopitalDropDownPlaceHolderColor();
    });
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