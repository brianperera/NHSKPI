$(document).ready(function () {

    ManageManualHospitalEntryField();
    
});

function ManageManualHospitalEntryField() {
    var hospitalDDLSelection = $("#ddlHospitalName").value;

    if (hospitalDDLSelection == "More") {
        alert("More");
        $("#txtCompanyName").show();
    }
}