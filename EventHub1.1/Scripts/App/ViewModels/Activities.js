define(["ViewModels/Activity"], function (Activity) {

    var activities = function (activityDTO) {
        var self = this;

        //self.activities = ko.observableArray(activityDTO);
        self.activities = ko.observableArray();

        // Constructor
        for (var i = 0; i < activityDTO.length; i++) {
            self.activities.push(new Activity(activityDTO[i]));
        }

        return self;
    }

    return activities;
});