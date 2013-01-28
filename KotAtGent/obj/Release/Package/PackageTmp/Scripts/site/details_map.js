var map;

$(document).ready(function () {
    var itemLocation = new google.maps.LatLng(parseFloat($('#dataLnk').attr('data-lat')), parseFloat($('#dataLnk').attr('data-long')));
    //init map, get user location and show the spot on the map.
    var myOptions = {
        zoom: 15,
        center: itemLocation,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("details_map"), myOptions);
    var marker = new google.maps.Marker({
        position: itemLocation,
        map: map,
    });
});
