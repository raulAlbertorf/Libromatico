$(document).ready(function() {
    var prestamos = $("#owl-prestamos");
    prestamos.owlCarousel({});
});

$(document).ready(function () {
    var recibidos = $("#owl-recibidos");
    recibidos.owlCarousel({});
});

function myMap() {
    var mapCanvas = document.getElementById("map");
    var mapOptions = {
        center: new google.maps.LatLng(-34.98279, -71.23943), zoom: 10
    };
    var map = new google.maps.Map(mapCanvas, mapOptions);
}