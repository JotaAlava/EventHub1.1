﻿define(["ViewModels/Location"], function (Location) {

    var locations = function (locationsDTO) {
        var self = this;
        self.selectedItems = ko.observableArray([]);

        self.locations = ko.observableArray();

        self.hideDetails = function (clickedLocationInsideObservableArrayOfLocations) {
            $('ul[id^="sportDetails_' + clickedLocationInsideObservableArrayOfLocations.id() + '"]').toggle()
        }

        self.toggleActive = function (clickedLocationInsideObservableArrayOfLocations) {

            $.ajax({
                url: "http://localhost:29196/location/ToggleActiveById/" + clickedLocationInsideObservableArrayOfLocations.id(),
                context: document.body,
                type: 'POST',
                statusCode: {
                    200: function () {
                        //clickedLocationInsideObservableArrayOfLocations.active(!clickedLocationInsideObservableArrayOfLocations.active())
                        self.locations.remove(clickedLocationInsideObservableArrayOfLocations);
                    }
                }
            })
        }

        self.deleteLocation = function (clickedLocationInsideObservableArrayOfLocations) {
            var idOfLocationToDelete = clickedLocationInsideObservableArrayOfLocations.id();

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/location/" + idOfLocationToDelete,
                context: document.body,
                dataType: 'json',
                type: 'DELETE',
                statusCode: {
                    200: function () {
                        self.locations.remove(clickedLocationInsideObservableArrayOfLocations);
                    }
                }
            })


        }

        // Probably should find a better way of doing this - Workaround for Circular Reference error
        self.addLocation = function (formData) {
            var result = (function (formData) {
                var result = {
                    name: formData[0].value,
                    address: formData[1].value,
                    active: formData[2].value == "on" ? true : false,
                    id: 0
                }

                return result;
            })(formData);

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/location/",
                context: document.body,
                type: 'POST',
                data: result
            }).success(function (result) {
                var newLocaiton = new Location(result);

                self.locations.push(newLocaiton);
            });

            $('#nameInputBox').val("")
            $('#addressInputBox').val("")
        }

        // This can definitely be made better....
        self.updateLocation = function (formData, clickedLocationInsideObservableArrayOfLocations) {
            var result = (function (formData) {
                var result = {
                    LocationId: formData[0].value,
                    name: formData[1].value,
                    address: formData[2].value,
                    Active: formData[3].value
                }

                return result;
            })(formData);

            var allLocationsAreActive = result.Active == "true" ? true : false;

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/location/",
                context: document.body,
                type: 'PUT',
                data: result
            }).complete(function (result) {
                var currentStateOfTheActivePropertyOfAllTheDisplayedLocations = allLocationsAreActive;

                if (currentStateOfTheActivePropertyOfAllTheDisplayedLocations) {
                    self.locations.removeAll();
                    $('#toggleBetweenLocationsButton').html("See Inactive")
                    $.ajax({
                        dataType: "json",
                        url: "http://localhost:29196/location/",
                        context: document.body,
                        type: 'GET',
                        statusCode: {
                            200: function (arrayOfActiveLocations) {
                                for (var i = 0; i < arrayOfActiveLocations.length; i++) {
                                    self.locations.push(new Location(arrayOfActiveLocations[i]));
                                }
                            }
                        }
                    })
                }
                else if (!currentStateOfTheActivePropertyOfAllTheDisplayedLocations) {
                    self.locations.removeAll();
                    $('#toggleBetweenLocationsButton').html("See Active")
                    $.ajax({
                        dataType: "json",
                        url: "http://localhost:29196/location/GetInactive",
                        context: document.body,
                        type: 'GET',
                        statusCode: {
                            200: function (arrayOfInactiveLocations) {
                                for (var i = 0; i < arrayOfInactiveLocations.length; i++) {
                                    self.locations.push(new Location(arrayOfInactiveLocations[i]));
                                }
                            }
                        }
                    })
                }

            });

            $('#modalLocationNameInputBox').val("")
            $('#modalLocationAddressInputBox').val("")
            $('#updateLocationDetailsModal').modal('hide');
        }

        for (var i = 0; i < locationsDTO.length; i++) {
            self.locations.push(new Location(locationsDTO[i]));
        }

        self.showInactiveLocations = function (clickedLocationInsideObservableArrayOfLocations) {
            var currentStateOfTheActivePropertyOfAllTheDisplayedLocations = false;
            if (clickedLocationInsideObservableArrayOfLocations.locations().length == 0) {
                return
            }
            else {
                currentStateOfTheActivePropertyOfAllTheDisplayedLocations = clickedLocationInsideObservableArrayOfLocations.locations()[0].active();
            }

            if (currentStateOfTheActivePropertyOfAllTheDisplayedLocations) {
                self.locations.removeAll();
                $('#toggleBetweenLocationsButton').html("See Active")
                $.ajax({
                    dataType: "json",
                    url: "http://localhost:29196/location/GetInactive",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfInactiveLocations) {
                            for (var i = 0; i < arrayOfInactiveLocations.length; i++) {
                                self.locations.push(new Location(arrayOfInactiveLocations[i]));
                            }
                        }
                    }
                })
            }
            else if (!currentStateOfTheActivePropertyOfAllTheDisplayedLocations) {
                self.locations.removeAll();
                $('#toggleBetweenLocationsButton').html("See Inactive")
                $.ajax({
                    dataType: "json",
                    url: "http://localhost:29196/location/",
                    context: document.body,
                    type: 'GET',
                    statusCode: {
                        200: function (arrayOfActiveLocations) {
                            for (var i = 0; i < arrayOfActiveLocations.length; i++) {
                                self.locations.push(new Location(arrayOfActiveLocations[i]));
                            }
                        }
                    }
                })
            }
        }

        self.showUpdateLocationModal = function (clickedLocationInsideObservableArrayOfLocations) {
            $('#modalLocationIdHiddenInputBox').val(clickedLocationInsideObservableArrayOfLocations.id())
            $('#modalLocationNameInputBox').val(clickedLocationInsideObservableArrayOfLocations.name())
            $('#modalLocationAddressInputBox').val(clickedLocationInsideObservableArrayOfLocations.address())
            $('#modalActiveStatusHiddenInputBox').val(clickedLocationInsideObservableArrayOfLocations.active())
        }

        return self;
    }

    return locations;
});