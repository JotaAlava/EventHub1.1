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
                    Time: formData[2].value == "on" ? true : false,
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
                var newLocaiton = new Location(result);

                self.locations.push(newLocaiton);
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

        self.showUpdateActivityModal = function () {
            alert("showUpdateActivityModal")
        }

        self.hideActivityDetails = function (clickedActivityInsideObservableArrayOfActivities) {
            $('ul[id^="activityDetails_' + clickedActivityInsideObservableArrayOfActivities.id() + '"]').toggle()
        }

        self.deleteActivity = function () {
            alert("deleteActivity")
        }

        return self;
    }

    return activities;
});