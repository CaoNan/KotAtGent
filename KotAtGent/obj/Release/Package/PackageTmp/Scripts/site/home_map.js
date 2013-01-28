var map;
var errors = $('#errors');
var userLocation;
//var ajaxData;
//var filteredData;
$(document).ready(function () {
    //init map, get user location and show the spot on the map.
    var myOptions = {
        zoom: 15,
        center: new google.maps.LatLng(-34.397, 150.644),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("home_map"), myOptions);
    getUserLocation();
    showListOnMap();

    /*
    //get data from remote.
    $.ajax({
        url: 'http://data.appsforghent.be/poi/publieksanitair.json',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            ajaxData = data.publieksanitair;
            getFilteredData();//method defined in filter.js
            fillListAndMap();//add list items on the left side, add markers on the map
            fillDetailsInfo(filteredData[0]["id"]);//fill in the details box
            sendRouteRequest(filteredData[0]["lat"], filteredData[0]["long"]);//method defined in route.js
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("error" + errorThrown);
        }
    });

    $("#filter").on("change", function (e) {
        getFilteredData();
        fillListAndMap();
        fillDetailsInfo(filteredData[0]["id"]);
        sendRouteRequest(filteredData[0]["lat"], filteredData[0]["long"]);//method defined in route.js
    });
*/
});

//get the current user location
var getUserLocation = function () {
    //W3C Geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            //userLat = position.coords.latitude;
            //userLong = position.coords.longitude;
            userLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            var marker = new google.maps.Marker({
                position: userLocation,
                map: map,
                title: "You are here."
            });
            map.setCenter(userLocation);
        });
    } else { errors.html("Geolocation is not supported by this browser."); }
}


var showListOnMap = function () {
    //blue icon
    //alert(document.location.hostname);
    var blueIcon = new google.maps.MarkerImage("/App2/Images/user_location.png");
    $('#info_list li a').each(function () {

        var itemLocation = new google.maps.LatLng(parseFloat($(this).attr('data-lat')), parseFloat($(this).attr('data-long')));
        console.log(parseFloat($(this).attr('data-lat')) + ";" + parseFloat($(this).attr('data-long')));
        var title = $(this).attr('data-title');
        var link = "/Overzicht/ShowDetails/"+ $(this).attr('id');
        var marker = new google.maps.Marker({
            icon: blueIcon,
            position: itemLocation,
            map: map,
            title: title,
            url: link
        });
        google.maps.event.addListener(marker,'click', function () {
            window.location.href = marker.url;
        });
    });
}