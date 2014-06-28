(function (LocationService) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    LocationService.GetAllLocations = function (clickedEvent, newMessage, successFunc, errorFunc) {
        window.UltiSports.DAL.Ajaxer(clickedEvent, window.appWideServiceURL +"location/all", "json", "GET", window.UltiSports.DAL.SucessFunctions.GetAllLocations, window.UltiSports.DAL.SucessFunctions.GetAllLocations);
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'LocationService' exists in the 
    // global LocationService - if not, assign window.LocationService an 
    // object literal
})(window.UltiSports.DAL.LocationService = window.UltiSports.DAL.LocationService || {});