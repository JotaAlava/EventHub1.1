define(["ViewModels/Activity"], function (Activity) {

    var activities = function (activityDTO) {
        var self = this;

        self.activeLocationsToSelectFrom = [];
        self.populateActivityDropdown = function () {
            $.ajax({
                url: "http://localhost:29196/location/",
                context: document.body
            }).done(function (data) {

                self.activeLocationsToSelectFrom = data
                for (var i in data) {
                    $('#locationsDropdown').append('<option value="' + data[i].LocationId + '">' + data[i].Name + '</option>')
                }

                self.activeLocationsToSelectFrom = data
                for (var i in data) {
                    $('#modalLocationsDropdown').append('<option value="' + data[i].LocationId + '">' + data[i].Name + '</option>')
                }
                // Overall viewmodel for this screen, along with initial state
                //activeLocations = new locationsViewModel(data);

                //for (location in data) {
                //    alert("Adding: " + location.name)
                //    self.activeLocationsToSelectFrom.push(location);
                //}
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
                    url: "http://localhost:29196/activity/GetInactive",
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
                    url: "http://localhost:29196/activity/",
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
                    DayOfWeek: formData[1].value,
                    Time: formData[2].value,
                    LocationId: formData[3].value,
                    Active: formData[4].value == "on" ? true : false,
                    PreferredNumberOfPlayers1: formData[5].value,
                    RequiredNumberOfPlayers1: formData[6].value
                }

                return result;
            })(formData);

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/activity/",
                context: document.body,
                type: 'POST',
                data: result
            }).success(function (result) {
                var newActivity = new Activity(result);

                newActivity.time = new Date(result.Time).toLocaleTimeString();
                self.activities.push(newActivity);
            });
        }

        self.toggleActiveActivity = function (clickedActivityInsideObservableArrayOfActivities) {
            $.ajax({
                url: "http://localhost:29196/activity/ToggleActiveById/" + clickedActivityInsideObservableArrayOfActivities.id(),
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
                    DayOfWeek: formData[2].value,
                    Time: formData[3].value,
                    LocationId: formData[4].value,
                    Active: formData[5].value == "on" ? true : false,
                    PreferredNumberOfPlayers1: formData[6].value,
                    RequiredNumberOfPlayers1: formData[7].value
                }

                return result;
            })(formData);

            var allActivitiesAreActive = result.Active == true ? true : false;

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/activity/",
                context: document.body,
                type: 'PUT',
                data: result
            }).complete(function (result) {
                var currentStateOfTheActivePropertyOfAllTheDisplayedActivities = allActivitiesAreActive;

                if (currentStateOfTheActivePropertyOfAllTheDisplayedActivities) {
                    self.activities.removeAll();
                    $('#toggleBetweenLocationsButton').html("See Inactive")
                    $.ajax({
                        dataType: "json",
                        url: "http://localhost:29196/activity/",
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
                else if (!currentStateOfTheActivePropertyOfAllTheDisplayedActivities) {
                    self.activities.removeAll();
                    $('#toggleBetweenLocationsButton').html("See Active")
                    $.ajax({
                        dataType: "json",
                        url: "http://localhost:29196/activity/GetInactive",
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

            });

            $('#activityNameInputBox').val("")
            $('#dayOfWeekInputBox').val("")
            $('#activityDatePicker').val("")

            $('#preferredPlayersInputBox').val("")
            $('#requiredPlayersInputBox').val("")
            $('#updateActivityDetailsModal').modal('hide');
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
                url: "http://localhost:29196/activity/" + idOfActivityToDelete,
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

        return self;
    }

    return activities;
});