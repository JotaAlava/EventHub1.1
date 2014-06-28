define(["ViewModels/Location"], function (Location) {

    var locations = function (locationsDTO) {
        var self = this;

        self.locations = ko.observableArray();
        self.createLocation = ko.observable(false);

        // Constructor
        for (var i = 0; i < locationsDTO.length; i++) {
            self.locations.push(new Location(locationsDTO[i]));
        }

        self.hideDetails = function (clickedLocationInsideObservableArrayOfLocations) {
            $('ul[id^="sportDetails_' + clickedLocationInsideObservableArrayOfLocations.id() + '"]').toggle()
        }

        self.toggleActive = function (clickedLocationInsideObservableArrayOfLocations) {

            $.ajax({
                url: window.appWideServiceURL + "location/ToggleActiveById/" + clickedLocationInsideObservableArrayOfLocations.id(),
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
                url: window.appWideServiceURL + "location/" + idOfLocationToDelete,
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
                url: window.appWideServiceURL + "location/",
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
                    Active: formData[3].checked == true ? true : false
                }

                return result;
            })(formData);

            $.ajax({
                dataType: "json",
                url: window.appWideServiceURL + "location/",
                context: document.body,
                type: 'PUT',
                data: result
            }).complete(function (result) {
                window.UltiSports.DAL.LocationService.GetAllLocations();
            });
        }

        self.showInactiveLocations = function (clickedLocationInsideObservableArrayOfLocations) {
            if (sessionStorage.getItem("viewingActiveLocations") == "true") {
                sessionStorage.setItem("viewingActiveLocations", false)
                self.locations.removeAll();
                $('#toggleBetweenLocationsButton').html("\nSee Active")
                $.ajax({
                    dataType: "json",
                    url: window.appWideServiceURL + "location/GetInactive",
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
            else if (sessionStorage.getItem("viewingActiveLocations") == "false") {
                sessionStorage.setItem("viewingActiveLocations", true)
                self.locations.removeAll();
                $('#toggleBetweenLocationsButton').html("\nSee Inactive")
                $.ajax({
                    dataType: "json",
                    url: window.appWideServiceURL + "location/",
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

            $('#updateLocationDetailsModal').on('shown.bs.modal', function () {
                $('#modalLocationNameInputBox').focus();
            })
        }

        self.NoneBeingViewed = ko.computed(function () {
            for (var i = 0; i < self.locations().length; i++) {
                if (self.locations()[i].isBeingViewed()) {
                    return false;
                }
            }

            if (self.createLocation()) {
                return false;
            }
            return true;
        });

        self.CreateLocation = function () {
            for (var i = 0; i < window.viewModels.locationViewModel.locations().length; i++) {
                window.viewModels.locationViewModel.locations()[i].isBeingViewed(false);
            }

            self.createLocation(!self.createLocation());
        }

        return self;
    }

    return locations;
});