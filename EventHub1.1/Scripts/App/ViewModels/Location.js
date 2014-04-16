define([/* Scripts that I deepend on - so other "modules" */], function () {
    var location = function (locationDTO) {
        var self = this;
        self.name = ko.observable(locationDTO.Name);
        self.address = ko.observable(locationDTO.Address);
    }

    return location;
})