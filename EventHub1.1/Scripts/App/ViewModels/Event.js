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

        return self;
    }
    return event;
})