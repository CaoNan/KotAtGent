$(document).ready(function () {
    var username = $('#user_panel_authenticated').attr("data-userName");
    createNote();
    $('.clickToAdd').on('click', function () {
        if(username!=null&&username!=""){
        if ($("#user_panel_authenticated ul").children().length < 3) {
            //ui.draggable.appendTo(this);
            var item_id = ($(this).attr('data-id'));
            var i = 1;
            while (readCookie(username+i)!="") {
                i++;
            }
            createCookie(username + i, item_id, 1);
            createNote();
        } else {
            alert("Maximum 3 record.");
        }
        } else { alert("Please login first");}
    });
    $('.draggable').draggable({
        appendTo: "body",
        helper: "clone"
    });
    $("#user_panel_authenticated ul").droppable({
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        accept: ":not(.ui-sortable-helper)",
        drop: function (event, ui) {
            if ($("#user_panel_authenticated ul").children().length < 3) {
                //ui.draggable.appendTo(this);
                var item_id = ($(ui.draggable).children('a').attr('id'));
                //createCookie(username + $("#user_panel_authenticated ul").children().length, item_id, 1);
                var i = 1;
                while (readCookie(username + i) != "") {
                    i++;
                }
                createCookie(username + i, item_id, 1);
                createNote();
            } else {
                alert("Maximum 3 record.");
            }
        }
    }).sortable({
        items: "li",
        sort: function () {
            // gets added unintentionally by droppable interacting with sortable
            // using connectWithSortable fixes this, but doesn't allow you to customize active/hoverClass options
            $(this).removeClass("ui-state-default");
        }
    });
});

var createCookie = function (name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
var readCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return  c.substring(nameEQ.length, c.length) ;
    }
    return "";
}

var eraseCookie = function (name) {
    //createCookie(name + "1", "", -1);
    //createCookie(name + "2", "", -1);
    //createCookie(name + "3", "", -1);
    createCookie(name , "", -1);

}
var createNote = function () {
    var username = $('#user_panel_authenticated').attr("data-userName");
    $(".deletable").remove();
    for (var i = 1; i <= 3; i++) {
        if (readCookie(username + i) != '') {
            var item_id = readCookie(username + i);
            var html = '<li class="deletable clear-fix" id="' + username + i + '">' + $('#info_list li #' + item_id).parent().html() + '<a href="#" class="deleteNote float-left">Delete</li>';
            //alert(html);
            $("#user_panel_authenticated ul").append(html);
        }
    }

    $('.deleteNote').on('click', function (e) {
        eraseCookie($(this).parent().attr('id'));
        $(this).parent().remove();
    });
}