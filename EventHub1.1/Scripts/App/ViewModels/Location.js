﻿define([/* Scripts that I deepend on - so other "modules" */], function () {
    var location = function (locationDTO) {
        var self = this;
        self.name = ko.observable(locationDTO.Name);
        self.address = ko.observable(locationDTO.Address);

        self.active = ko.observable(locationDTO.Active);
        self.id = ko.observable(locationDTO.LocationId);
        self.isBeingViewed = ko.observable(false);

        self.ToggleBeingViewed = function() {
            for (var i = 0; i < window.viewModels.locationViewModel.locations().length; i++) {
                window.viewModels.locationViewModel.locations()[i].isBeingViewed(false);
            }

            window.viewModels.locationViewModel.createLocation(false);
            self.isBeingViewed(true);
        }

        self.namePlace = ko.computed(function () {
                return self.name() + " (" + self.address() + ")";
            });
        }

        return location;
    })