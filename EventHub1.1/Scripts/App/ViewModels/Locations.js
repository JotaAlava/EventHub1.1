define(["ViewModels/Location"], function () {

    var locations = function (locationsDTO) {
        var self = this;
        self.locations = ko.observableArray(locationsDTO);

    }

    return locations;
})