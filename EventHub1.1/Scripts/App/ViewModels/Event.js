define([], function () {
    var event = function (eventDTO) {
        var self = this;

        self.eventId = ko.observable(eventDTO.EventId)
        self.name = ko.observable(eventDTO.Name)
        self.activityId = ko.observable(eventDTO.ActivityId)
        self.activity = ko.observable(eventDTO.Activity)
        self.active = ko.observable(eventDTO.Active)
        self.messages = ko.observableArray(eventDTO.Messages)
        self.users = ko.observableArray(eventDTO.Users)
        self.plusones = ko.observableArray(eventDTO.PlusOnes)
        self.isBeingViewed = ko.observable(false)

        self.getFormatedTime = ko.computed(function() {
            var activityTime = self.activity().Time
            var hours = activityTime.substring(11, 13);
            var minutes = activityTime.substring(14, 16);

            var result = new Date();
            result.setHours(hours)
            result.setMinutes(minutes)
            result.setSeconds(0)

            return result.toLocaleTimeString().substring(0, 4) + " PM";
        });

        self.getTotalNumberOfPlayers = ko.computed(function () {
            return self.users().length + self.plusones().length;
        })

        self.isCurrentUserAttending = ko.computed(function (event) {
            for (var i = 0; i < self.users().length; i++) {
                if (self.users()[i].Name == window.usersUsername.substring(6))
                {
                    return true;
                }
            }
            return false;
        })

        self.isCurrentUserNOTAttending = ko.computed(function(event) {
            for (var i = 0; i < self.users().length; i++) {
                if (self.users()[i].Name == window.usersUsername.substring(6)) {
                    return false;
                }
            }
            return true;
        });

        self.currentProgress = function () {
            return ((this.users().length + this.plusones().length) / this.activity().RequiredNumberOfPlayers1 * 100) + '%';
        }

        return self;
    }
    return event;
})