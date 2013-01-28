$(document).ready(function () {
    var size = $('#info_list li a').length;
    $('#paging').pagination({
        items: size,
        itemsOnPage: 9,
        cssStyle: 'light-theme',
        displayedPages: 6,
        edges: 3,
        onInit: initList,
        onPageClick: changePage
    });
});
var initList = function () {
    $('#info_list li').each(function (index, element) {
        if (index >= 9) {
            $(this).hide();
        } else {
            $(this).show();
        }
    });
}
var changePage = function (pagenr) {
    $('#info_list li').each(function (index, element) {
        
        if (index < 9 * (pagenr - 1) || index >= 9 * pagenr) {
            $(this).hide();
        } else {
            $(this).show();
        }
    });
}