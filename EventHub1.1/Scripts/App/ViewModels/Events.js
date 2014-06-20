define(["ViewModels/Event", "ViewModels/Player"], function (Event, Player) {
    var events = function (eventDTO) {
        var self = this;

        self.listOfEvents = ko.observableArray();

        self.messages = ko.observableArray();

        // Constructor
        for (var i = 0; i < eventDTO.length; i++) {
            self.listOfEvents.push(new Event(eventDTO[i]));
        }       

        self.hideDetails = function (clickedEventInsideObservableArrayOfEvents) {
            $('ul[id^="eventDetails_' + clickedEventInsideObservableArrayOfEvents.eventId() + '"]').toggle()
        }

        self.addMessage = function (clickedEvent, formData) {
            var message = {
                Body: formData[0].value,
                UserId: 1,
                EventId: clickedEvent.eventId()
            }

            $.ajax({
                dataType: "json",
                url: window.productionURL + "/message/",
                context: this,
                type: 'POST',
                data: message,
                statusCode: {
                    201: function (result) {
                        clickedEvent.messages.push(result);
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })

            $('#messageBox_' + clickedEvent.eventId()).val('');
        }

        self.joinEvent = function (clickedEventInsideObservableArrayOfEvents) {
            $.ajax({
                dataType: "json",
                url: window.productionURL + "/event/attend/" + clickedEventInsideObservableArrayOfEvents.eventId(),
                context: clickedEventInsideObservableArrayOfEvents,
                type: 'PUT',
                statusCode: {
                    200: function (result) {
                        $.ajax({
                            dataType: "json",
                            url: window.productionURL + "/event/getParticipantsByEventId/" + result.substring(31),
                            context: this,
                            type: 'GET'
                        }).success(function (result) {
                            // Here I am reconstructing the entire list of participants for this event.
                            // Until I figure out a better way.
                            this.users.removeAll()
                            for (var i = 0; i < result.length; i++) {
                                this.users.push(result[i])
                            }
                        })
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })
        }

        self.leaveEvent = function (clickedEventInsideObservableArrayOfEvents) {
            $.ajax({
                dataType: "json",
                url: window.productionURL + "/event/leave/" + clickedEventInsideObservableArrayOfEvents.eventId(),
                context: this,
                type: 'PUT',
                statusCode: {
                    200: function (result) {
                        var idForEvent = result.substring(31);

                        $.ajax({
                            dataType: "json",
                            url: window.productionURL + "/event/getParticipantsByEventId/" + result.substring(31),
                            context: this,
                            type: 'GET'
                        }).success(function (result) {
                            // Here I am reconstructing the entire list of participants for this event.
                            // Until I figure out a better way.
                            this.users.removeAll()
                            for (var i = 0; i < result.length; i++) {
                                this.users.push(result[i])
                            }
                        })
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })
        }

        self.addPlusOne = function (formData, jQueryEvent) {
            jQueryEvent.preventDefault();
            var nameDidNotPassValidation = $('#AddPlusOneNameFormLabel').hasClass("invalid")

            if (nameDidNotPassValidation) {
                $('#AddPlusOneNameFormLabel').removeClass("invalid")
                return;
            }

            $.ajax({
                dataType: "json",
                url: window.productionURL + "/plusone/" + formData[0].value + "?nameForPlusOne=" + formData[1].value,
                context: this,
                type: 'POST',
                statusCode: {
                    200: function (result) {
                        var eventId = result.substring(31);

                        $.ajax({
                            dataType: "json",
                            url: window.productionURL + "/plusone/byeventid/" + eventId,
                            context: this,
                            type: 'GET'
                        }).success(function (result) {
                            // Here I am reconstructing the entire list of plusones for this event.
                            // Until I figure out a better way.
                            //this.listOfEvents()[0].plusones.removeAll()

                            for (var i = 0; i < this.listOfEvents().length; i++) {
                                if (this.listOfEvents()[i].eventId() == eventId) {
                                    this.listOfEvents()[i].plusones.removeAll();

                                    for (var k = 0; k < result.length; k++) {
                                        this.listOfEvents()[i].plusones.push(result[k])
                                    }
                                }
                            }


                        })
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })

            $('#modalPlusOneNameInputBox').val("")
            $('#addPlusOneModal').modal('hide');
        }

        self.showAddPlusOneModal = function (clickedEventInsideObservableArrayOfEvents) {
            $('#modalEventIdHiddenInputBox').val(clickedEventInsideObservableArrayOfEvents.eventId());
            $('#addPlusOneModal').on('shown.bs.modal', function () {
                $('#modalPlusOneNameInputBox').focus();
            })

        }

        self.removePlusOne = function (parentViewModel, clickedPlusOne) {
            var eventId = clickedPlusOne.EventId;
            var plusOneId = clickedPlusOne.PlusOneId;

            $.ajax({
                dataType: "json",
                url: window.productionURL + "/plusone/" + plusOneId,
                context: this,
                type: 'PUT',
                statusCode: {
                    200: function (result) {
                        for (var i = 0; i < parentViewModel.listOfEvents().length; i++) {
                            if (_.contains(parentViewModel.listOfEvents()[i].plusones(), clickedPlusOne) && parentViewModel.listOfEvents()[i].eventId() == eventId) {
                                parentViewModel.listOfEvents()[i].plusones.remove(clickedPlusOne);
                            }
                        }
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })
        }

        self.generateEmail = function (clickedPlusOne) {
            $.ajax({
                dataType: "json",
                url: window.productionURL + "/event/sendEmail",
                context: this,
                type: 'GET',
                statusCode: {
                    200: function (result) {
                        alert(result.responseJSON)
                    },
                    400: function (result) {
                        alert(result.responseJSON)
                    }
                }
            })
        }

        self.scrollLeft = function (event) {
            var sizeOfListOfEvents = window.viewModels.eventsViewModel.listOfEvents().length;
            for (var i = 0; i < sizeOfListOfEvents; i++) {
                if(window.viewModels.eventsViewModel.listOfEvents()[i].eventId() == event.eventId())
                {
                    event.isBeingViewed(false);

                    if (i == 0) {
                        window.viewModels.eventsViewModel.listOfEvents()[sizeOfListOfEvents - 1].isBeingViewed(true);
                    }
                    else {
                        window.viewModels.eventsViewModel.listOfEvents()[i - 1].isBeingViewed(true);
                    }
                }
            }
        }

        self.scrollRight = function (event) {
            var sizeOfListOfEvents = window.viewModels.eventsViewModel.listOfEvents().length;
            for (var i = 0; i < sizeOfListOfEvents; i++) {
                if (window.viewModels.eventsViewModel.listOfEvents()[i].eventId() == event.eventId()) {
                    event.isBeingViewed(false);

                    if (i == sizeOfListOfEvents - 1) {
                        window.viewModels.eventsViewModel.listOfEvents()[sizeOfListOfEvents - sizeOfListOfEvents].isBeingViewed(true);
                    }
                    else {
                        window.viewModels.eventsViewModel.listOfEvents()[i + 1].isBeingViewed(true);
                    }
                }
            }
        }

        self.areThereAnyEvents = function () {
            return self.listOfEvents().length == 0 ? true : false;
        }

        return self;
    }

    return events;
})