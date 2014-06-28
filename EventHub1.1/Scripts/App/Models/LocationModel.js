(function (LocationModel) {
    LocationModel.GetNewLocation = function (locationDTO) {
        var self = {};
        self.name = ko.observable(locationDTO.Name);
        self.address = ko.observable(locationDTO.Address);

        self.active = ko.observable(locationDTO.Active);
        self.id = ko.observable(locationDTO.LocationId);
        self.isBeingViewed = ko.observable(false);

        self.ToggleBeingViewed = function () {
            for (var i = 0; i < window.viewModels.locationViewModel.locations().length; i++) {
                window.viewModels.locationViewModel.locations()[i].isBeingViewed(false);
            }

            window.viewModels.locationViewModel.createLocation(false);
            self.isBeingViewed(true);
        }

        self.namePlace = ko.computed(function () {
            return self.name() + " (" + self.address() + ")";
        });

        return self;
    }


    // check to evaluate whether 'LocationModel' exists in the 
    // global LocationModel - if not, assign window.LocationModel an 
    // object literal
})(window.UltiSports.Models.LocationModel = window.UltiSports.Models.LocationModel || {});