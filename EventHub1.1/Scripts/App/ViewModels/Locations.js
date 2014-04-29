define(["ViewModels/Location"], function (location) {

    var locations = function (locationsDTO) {
        var self = this;

        self.locations = ko.observableArray();

        for (var i = 0; i < locationsDTO.length; i++) {
            self.locations.push(new location(locationsDTO[i]));
        }

        return self.locations;
    }

    return locations;
});