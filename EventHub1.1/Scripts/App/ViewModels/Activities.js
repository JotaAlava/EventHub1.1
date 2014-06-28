define(["ViewModels/Activity"], function (Activity) {

    var activities = function (activityDTO) {
        var self = this;

        self.createActivity = ko.observable(false);
        self.activeLocationsToSelectFrom = ko.observableArray();
        self.populateActivityDropdown = function () {
            $.ajax({
                url: window.appWideServiceURL + "location/",
                context: document.body
            }).done(function (data) {

                for (var i = 0; i < data.length; i++) {
                    self.activeLocationsToSelectFrom(data);
                }
            });
        }

        self.activities = ko.observableArray();

        // Constructor
        for (var i = 0; i < activityDTO.length; i++) {
            self.activities.push(new Activity(activityDTO[i]));
        }

        self.showInactiveActivities = function (clickedActivityInsideObservableArrayOfActivities) {
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });

            if (sessionStorage.getItem("viewingActiveActivities") == "true") {
                sessionStorage.setItem("viewingActiveActivities", false)
                self.activities.removeAll();
                $('#toggleBetweenActivitiesButton').html("See Active")
                $.ajax({
                    dataType: "json",
                    url: window.appWideServiceURL + "activityGetInactive",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfInactiveActivities) {
                            for (var i = 0; i < arrayOfInactiveActivities.length; i++) {
                                self.activities.push(new Activity(arrayOfInactiveActivities[i]));
                            }
                        }
                    }
                })
            }
            else if (sessionStorage.getItem("viewingActiveActivities") == "false") {
                sessionStorage.setItem("viewingActiveActivities", true)
                self.activities.removeAll();
                $('#toggleBetweenActivitiesButton').html("See Inactive")
                $.ajax({
                    dataType: "json",
                    url: window.appWideServiceURL + "activity",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfActiveActivities) {
                            for (var i = 0; i < arrayOfActiveActivities.length; i++) {
                                self.activities.push(new Activity(arrayOfActiveActivities[i]));
                            }
                        }
                    }
                })
            }

            setTimeout($.unblockUI, 0);
        }

        self.addActivity = function (formData) {
            var result = (function (formData) {
                var result = {
                    Name: formData[0].value,
                    DayOfWeek: formData[2].value,
                    Time: formData[3].value,
                    LocationId: formData[1].value,
                    Active: formData[6].checked == true ? true : false,
                    PreferredNumberOfPlayers1: formData[5].value,
                    RequiredNumberOfPlayers1: formData[4].value
                }

                return result;
            })(formData);

            $.ajax({
                dataType: "json",
                url: window.appWideServiceURL + "activity",
                context: document.body,
                type: 'POST',
                data: result
            }).success(function (result) {
                var newActivity = new Activity(result);

                newActivity.time = new Date(result.Time).toLocaleTimeString();
                self.activities.push(newActivity);
                $.growlUI('Activity was created!');
            });
        }

        self.toggleActiveActivity = function (clickedActivityInsideObservableArrayOfActivities) {
            $.ajax({
                url: window.appWideServiceURL + "activityToggleActiveById/" + clickedActivityInsideObservableArrayOfActivities.id(),
                context: document.body,
                type: 'POST',
                statusCode: {
                    200: function () {
                        self.activities.remove(clickedActivityInsideObservableArrayOfActivities);
                    }
                }
            })
        }

        self.updateActivityModal = function (formData) {

            var result = (function (formData) {
                var result = {
                    ActivityId: formData[0].value,
                    Name: formData[1].value,
                    DayOfWeek: formData[3].value,
                    Time: formData[4].value,
                    LocationId: formData[2].value,
                    Active: formData[7].checked == true ? true : false,
                    PreferredNumberOfPlayers1: formData[5].value,
                    RequiredNumberOfPlayers1: formData[6].value
                }

                return result;
            })(formData);

            $.ajax({
                dataType: "json",
                url: window.appWideServiceURL + "activity",
                context: document.body,
                type: 'PUT',
                data: result
            }).complete(function (result) {
                    self.activities.removeAll();
                $.ajax({
                    dataType: "json",
                    url: window.appWideServiceURL + "activity/all",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function(arrayOfActiveActivities) {
                            for (var i = 0; i < arrayOfActiveActivities.length; i++) {
                                self.activities.push(new Activity(arrayOfActiveActivities[i]));
                            }
                            $.growlUI('Activity was updated!');
                        }
                    }
                });
            });
        }

        self.showUpdateActivityModal = function (clickedLocationInsideObservableArrayOfActivities) {
            $('#modalActivityIdHiddenInputBox').val(clickedLocationInsideObservableArrayOfActivities.id())

            $('#modalActivityNameInputBox').val(clickedLocationInsideObservableArrayOfActivities.name())
            $('#modalDayOfWeekInputBox').val(clickedLocationInsideObservableArrayOfActivities.dayOfWeek())

            // TODO: Make the update date same as current date of event to update
            //$('#modalActivityDatePicker').val(clickedLocationInsideObservableArrayOfActivities.time())

            $('#updateActivityDetailsModal').on('shown.bs.modal', function () {
                $('#modalActivityNameInputBox').focus();
            })

            if (clickedLocationInsideObservableArrayOfActivities.active()) {
                $('#modalActivityActiveFlag').prop("checked", true)
                $('#modalActivityActiveFlag').val("on")
            }
            else if (!clickedLocationInsideObservableArrayOfActivities.active()) {
                $('#modalActivityActiveFlag').val("off")
            }

            for (var i = 0; i < $('#modalLocationsDropdown option').length; i++) {
                if (clickedLocationInsideObservableArrayOfActivities.locationId() == $('#modalLocationsDropdown option').eq(i).val()) {
                    $('#modalLocationsDropdown option').eq(i).attr("selected", true)
                }
            }

            $('#modalPreferredPlayersInputBox').val(clickedLocationInsideObservableArrayOfActivities.preferredNumberOfPlayers())
            $('#modalRequiredPlayersInputBox').val(clickedLocationInsideObservableArrayOfActivities.requiredNumberOfPlayers())

            $('#updateActivityDetailsModal').modal('hide');
        }

        self.hideActivityDetails = function (clickedActivityInsideObservableArrayOfActivities) {
            $('ul[id^="activityDetails_' + clickedActivityInsideObservableArrayOfActivities.id() + '"]').toggle()
        }

        self.deleteActivity = function (clickedActivityInsideObservableArrayOfActivities) {
            var idOfActivityToDelete = clickedActivityInsideObservableArrayOfActivities.id();

            $.ajax({
                dataType: "json",
                url: window.appWideServiceURL + "activity" + idOfActivityToDelete,
                context: document.body,
                dataType: 'json',
                type: 'DELETE',
                statusCode: {
                    200: function () {
                        self.activities.remove(clickedActivityInsideObservableArrayOfActivities);
                    },
                    400: function () {
                        alert("An event has been created with this activity!")
                    }
                }
            })
        }

        self.NoneBeingViewed = ko.computed(function() {
            for (var i = 0; i < self.activities().length; i++) {
                if (self.activities()[i].isBeingViewed()) {
                    return false;
                }
            }

            if (self.createActivity()) {
                return false;
            }
            return true;
        });

        self.CreateActivity = function () {
            for (var i = 0; i < window.viewModels.activitiesViewModel.activities().length; i++) {
                window.viewModels.activitiesViewModel.activities()[i].isBeingViewed(false);
            }

            self.createActivity(!self.createActivity());
        }

        return self;
    }

    return activities;
});