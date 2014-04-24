define(["ViewModels/Location"], function () {

    var locations = function (locationsDTO) {
        var self = this;

        self.locations = ko.observableArray();

        for (var i = 0; i < locationsDTO.length; i++) {
            self.locations.add(new Location(locationsDTO[i]));
        }

        return self.locations;
    }

    return locations;
})