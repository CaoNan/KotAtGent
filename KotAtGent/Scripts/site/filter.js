$(document).ready(function () {
    $('#quick-search select').on('change', function (e) {
        $('#info_list li a').each(function () {
            //$(this).show();
            $(this).parent().fadeIn();
        });
        //get filtered list
        updateInfoList();
    })
});


var updateInfoList = function () {
    //zone filter
    if ($('#quick-search select#selectZone').attr('value') != "All") {
        $('#info_list li a').each(function () {
            if ($(this).attr('data-zone') != $('select#selectZone').attr('value')) {
                $(this).parent().fadeOut();
            }
        });
    }
    //type filter
    if ($('#quick-search select#selectType').attr('value') != "All") {
        $('#info_list li a').each(function () {
            if ($(this).attr('data-type') != $('select#selectType').attr('value')) {
                $(this).parent().fadeOut();
            }
        });
    }
    //price filter
    if ($('#quick-search select#selectPrice').attr('value') != "All") {
        $('#info_list li a').each(function () {
            switch ($('select#selectPrice').attr('value')) {
                case "250":
                    if (parseInt($(this).attr('data-price')) > 250) $(this).parent().fadeOut();
                    //<dan 250
                    break;
                case "500":
                    if (parseInt($(this).attr('data-price')) > 500 && parseInt($(this).attr('data-price'))<250) $(this).parent().fadeOut();
                    break;
                case "501":
                    if (parseInt($(this).attr('data-price')) < 500 && parseInt($(this).attr('data-price')) < 250) $(this).parent().fadeOut();
                    break;
            }
        });
    }
}