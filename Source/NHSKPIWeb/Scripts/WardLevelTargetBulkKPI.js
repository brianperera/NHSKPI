$(document).ready(function() {  


});


function isDecimalKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 8 && charCode != 189) {
        return false;
    }
    return true;
}

function ValidateControls(idlist)
{  
    //Id list will be kpi id list
   
    var wardlist = idlist.split(","); 
      var monthlyDescription = new Array();
     var YTDDescription = new Array();
     var monthlyTargetGreen = new Array();
     var YTDTargetGreen = new Array();
     var monthlyTargetAmber = new Array();
     var YTDTargetAmber = new Array();
      var monthlycontrolcount = 0;
       var months = new Array("Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar");
        
         var type = new Array("Monthly", "YTD");
   
      
  
    var valid = true;
    if (wardlist.length > 0)
    {
    for (var j=0; j<wardlist.length; j++)
    {
        var wardid = wardlist[j];       
   
        var txtbox = 'ctl00_Contentbody_'+ wardid + month +'txtTargetGreen';
       
         for (var k = 0; k < type.length; k++)
        {
        
        var targettype = type[k];
    
        for (var i=0;i<months.length;i++)
        {
            var month = months[i];
            if ((document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetGreen'+targettype) != null)) 
            {    
    
                if(document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetGreen'+targettype).value != '')
                {
                    var target = document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetGreen'+targettype).value;
                    var regexp = new RegExp('[-]?[0-9]*[.]?[0-9]*');           
               
                    if (!(regexp.test(target)))
                    { 
                        valid = false;              
                        if(document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetGreen'+targettype).innerText == '')
                        {                    
                            document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetGreen'+targettype).innerText = 'Invalid';
                            
                        }
                    } 
                    else
                    {               
                        if(document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetGreen'+targettype).innerText != '')
                        { 
                            document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetGreen'+targettype).innerText = ''; 
                            
                        }
                    } 
                
                } 
            }
            
            if (document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetAmber'+targettype) != null)
            {
         
                if(document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetAmber'+targettype).value != '')
                {
                    var target = document.getElementById('ctl00_Contentbody_'+ wardid + month +'txtTargetAmber'+targettype).value;
                    var regexp = new RegExp('[-]?[0-9]*[.]?[0-9]*');           
               
                    if (!(regexp.test(target)))
                    { 
                        valid = false;              
                        if(document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetAmber'+targettype).innerText == '')
                        {                    
                            document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetAmber'+targettype).innerText = 'Invalid';
                            valid = false;
                        }
                    } 
                    else
                    {               
                        if(document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetAmber'+targettype).innerText != '')
                        { 
                            document.getElementById('ctl00_Contentbody_'+ wardid + month + 'divTargetAmber'+targettype).innerText = '';                             
                        }
                    }
                }
            }
            
        }//End month loop
        }//End of type loop
    } //End ward loop
    if (!valid)
        return false;
    if (valid)
    {
    //Ward list paramter is having id array of kpi id list
   
        for (var z=0; z < wardlist.length; z++)
        {
            var wardid = wardlist[z];
            
             for (var w=0;w < months.length; w++)
            {
           
            var month = months[w];
           
            //Get the monthly description values ctl00_Contentbody_16AprtxtDescriptionMonthly

           
               var monthlyDesValue = '';
               if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDescriptionMonthly') != null) {
                   monthlyDesValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDescriptionMonthly').value;
               }
               else {
                   monthlyDesValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtDescriptionMonthly').value;
               }
               
               monthlyDescription[monthlycontrolcount] =  wardid + '_' + month + '_' + monthlyDesValue;
               
               var YTDDesValue = '';
               if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDescriptionYTD') != null) {
                   YTDDesValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtDescriptionYTD').value;
               }
               else {
                   YTDDesValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtDescriptionYTD').value;
               }
               
               YTDDescription[monthlycontrolcount] =  wardid + '_' + month + '_' + YTDDesValue;
               
               var monthlyTargetGreenValue = '';
               if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetGreenMonthly') != null) {
                   monthlyTargetGreenValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetGreenMonthly').value;
               }
               else {
                   monthlyTargetGreenValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtTargetGreenMonthly').value;
               }
               
               monthlyTargetGreen[monthlycontrolcount] =  wardid + '_' + month + '_' + monthlyTargetGreenValue;
               
                var YTDTargetGreenValue = '';
                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetGreenYTD') != null) {
                    YTDTargetGreenValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetGreenYTD').value;
                }
                else {
                    YTDTargetGreenValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtTargetGreenYTD').value;
                }
               
               YTDTargetGreen[monthlycontrolcount] =  wardid + '_' + month + '_' + YTDTargetGreenValue;
               
                var monthlyTargetAmberValue = '';
                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetAmberMonthly') != null) {
                    monthlyTargetAmberValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetAmberMonthly').value;
                }
                else {
                    monthlyTargetAmberValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtTargetAmberMonthly').value;
                }
               
               monthlyTargetAmber[monthlycontrolcount] =  wardid + '_' + month + '_' + monthlyTargetAmberValue;
               
                var YTDTargetAmberValue = '';
                if (document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetAmberYTD') != null) {
                    YTDTargetAmberValue = document.getElementById('ctl00_Contentbody_' + wardid + month + 'txtTargetAmberYTD').value;
                }
                else {
                    YTDTargetAmberValue = document.getElementById('ctl00_Contentbody_' + wardid + months[0] + 'txtTargetAmberYTD').value;
                }
               
               YTDTargetAmber[monthlycontrolcount] =  wardid + '_' + month + '_' + YTDTargetAmberValue;
                                 
          
          monthlycontrolcount++;

        } 
      }
      var response = Views_KPI_WardLevelTargetBulkKPI.SaveBulkTarget(monthlyDescription, monthlyTargetGreen, monthlyTargetAmber, YTDDescription, YTDTargetGreen, YTDTargetAmber, document.getElementById('ctl00_Contentbody_hdnHospitalId').value, document.getElementById('ctl00_Contentbody_hdnWardId').value, document.getElementById('ctl00_Contentbody_hdnYear').value);
      
      if (response.value = 'true') {
          document.getElementById('divSucess').style.display = 'block';
      }
      return false;
   }
    
    }
}

function showhide(plusimageId, minusImageId, divId, clickon)
{  
    
     if (clickon == 'plus')
     {
        document.getElementById('ctl00_Contentbody_'+ plusimageId).style.display='none'; 
        document.getElementById('ctl00_Contentbody_'+ minusImageId).style.display='block'; 
        document.getElementById('ctl00_Contentbody_'+ divId).style.display='none'; 
     
     }
     if (clickon == 'minus')
     {
        document.getElementById('ctl00_Contentbody_'+ plusimageId).style.display='block'; 
        document.getElementById('ctl00_Contentbody_'+ minusImageId).style.display='none'; 
        document.getElementById('ctl00_Contentbody_'+ divId).style.display='block'; 
     
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
