$(document).ready(function() {


});


function isDecimalKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 8 && charCode != 189) {
        return false;
    }
    return true;
}

function ValidateControls(idlist) {

    var wardlist = idlist.split(",");
    var Numarator = new Array();
    var Denominator = new Array();
    var YTD = new Array();

    var monthlycontrolcount = 0;
    var ytdcontrolcount = 0;
    var months = new Array("Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar");
    var type = new Array("num", "den", "ytd");

    var valid = true;
    if (wardlist.length > 0) {
        for (var j = 0; j < wardlist.length; j++) {
            var wardid = wardlist[j];

            for (var k = 0; k < type.length; k++) {

                var targettype = type[k];

                for (var i = 0; i < months.length; i++) {

                    var month = months[i];


                    if ((document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtNumarator' + targettype) != null)) {

                        if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtNumarator' + targettype).value != '') {
                            var target = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtNumarator' + targettype).value;
                            var regexp = new RegExp('[-]?[0-9]*[.]?[0-9]*');

                            if (!(regexp.test(target))) {
                                valid = false;
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divNumarator' + targettype).innerText == '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divNumarator' + targettype).innerText = 'Invalid';

                                }
                            }
                            else {
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divNumarator' + targettype).innerText != '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divNumarator' + targettype).innerText = '';

                                }
                            }

                        }
                    }

                    if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDenominator' + targettype) != null) {

                        if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDenominator' + targettype).value != '') {
                            var target = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDenominator' + targettype).value;
                            var regexp = new RegExp('[-]?[0-9]*[.]?[0-9]*');

                            if (!(regexp.test(target))) {
                                valid = false;
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divDenominator' + targettype).innerText == '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divDenominator' + targettype).innerText = 'Invalid';
                                    valid = false;
                                }
                            }
                            else {
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divDenominator' + targettype).innerText != '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divDenominator' + targettype).innerText = '';
                                }
                            }
                        }
                    }

                    if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtYTD' + targettype) != null) {

                        if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtYTD' + targettype).value != '') {
                            var target = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtYTD' + targettype).value;
                            var regexp = new RegExp('[-]?[0-9]*[.]?[0-9]*');

                            if (!(regexp.test(target))) {
                                valid = false;
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divYTD' + targettype).innerText == '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divYTD' + targettype).innerText = 'Invalid';
                                    valid = false;
                                }
                            }
                            else {
                                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'divYTD' + targettype).innerText != '') {
                                    document.getElementById('ctl00_Contentbody_' + wardid + month + 'divYTD' + targettype).innerText = '';
                                }
                            }
                        }
                    }


                } //End month loop
            } //End of type loop
        } //End ward loop

        if (!valid)
            return false;

        if (valid) {

            for (var z = 0; z < wardlist.length; z++) {
                var wardid = wardlist[z];

                for (var w = 0; w < months.length; w++) {

                    var month = months[w];

                    var NumaratorValue = '';
                    if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtNumaratornum') != null) {
                        NumaratorValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtNumaratornum').value;
                    }
                    else {
                        NumaratorValue = '';
                    }

                    Numarator[monthlycontrolcount] = wardid + '_' + month + '_' + NumaratorValue;

                    var DenominatorValue = '';

                    if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDenominatorden') != null) {

                        DenominatorValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDenominatorden').value;
                    }
                    else {
                        DenominatorValue = '';
                    }

                    Denominator[monthlycontrolcount] = wardid + '_' + month + '_' + DenominatorValue;


                    var YTDValue = '';
                    if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtYTDytd') != null) {
                        YTDValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtYTDytd').value;
                    }
                    else {
                        YTDValue = '';
                    }

                    YTD[monthlycontrolcount] = wardid + '_' + month + '_' + YTDValue;





                    monthlycontrolcount++;

                }
            }
            //alert(Numarator);
            //alert(Denominator);
            //alert(YTD);
            var response = Views_KPI_WardLevelDataBulk.SaveBulkData(Numarator, Denominator, YTD, document.getElementById('ctl00_Contentbody_hdnHospitalId').value, document.getElementById('ctl00_Contentbody_hdnKPIId').value, document.getElementById('ctl00_Contentbody_hdnYear').value, getKPIBulkWardData_callback);
            //var response = Views_KPI_WardLevelDataBulk.SaveBulkData(document.getElementById('Contentbody_hdnHospitalId').value, document.getElementById('Contentbody_hdnKPIId').value, document.getElementById('Contentbody_hdnYear').value, getKPIBulkWardData_callback);

            


            return false;
        }

    }
}

function getKPIBulkWardData_callback(res) {
    
    if (res.value == 'true') {
        document.getElementById('divSucess').style.display = 'block';
    }
    else {
        alert(res.value);
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

function checkRegexp(o, emailReg, errorMsg) {
    var regexp = new RegExp(emailReg);
    var re = "^\d{1,8}(\.\d{1,2})?$";
    //return re.test(email); 
    if (!(re.test(email))) {
        //o.removeClass("text ui-widget-content ui-corner-all").addClass("input-validation-error");
        errorMsg.innerText = 'Invalid';
        return false;
    } else {
        return true;
    }
}
