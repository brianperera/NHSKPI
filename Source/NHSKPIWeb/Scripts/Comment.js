
$(document).ready(function() {

    $(function() {
        $("#txtCreatedDate").datepicker({
            dateFormat: "dd-mm-yy", 
            showOn: "button",
            buttonImage: "../../assets/images/calendar1.png",
            buttonImageOnly: true

        });

//        $('[id*=gvCommentsHistory] tr').each(function() {
//            var toolTip = $(this).attr("title");
//            $(this).find("td").each(function() {
//                $(this).simpletip({
//                    content: toolTip
//                });
//            });
//            $(this).removeAttr("title");
//        });

    });

});