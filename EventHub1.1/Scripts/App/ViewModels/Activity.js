define([/* Scripts that I deepend on - so other "modules" */], function () {
    var activity = function (activityDTO) {
        var self = this;
        self.id = ko.observable(activityDTO.ActivityId);
        self.name = ko.observable(activityDTO.Name);
        self.dayOfWeek = ko.observable(activityDTO.DayOfWeek)

        self.time = ko.observable(activityDTO.Time);
        self.locationId = ko.observable(activityDTO.LocationId)
        self.active = ko.observable(activityDTO.Active)

        self.preferredNumberOfPlayers = ko.observable(activityDTO.PreferredNumberOfPlayers1)
        self.requiredNumberOfPlayers = ko.observable(activityDTO.RequiredNumberOfPlayers1)
    }

    return activity;
})