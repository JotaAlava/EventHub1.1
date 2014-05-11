define(["ViewModels/Location"], function (Location) {

    var locations = function (locationsDTO) {
        var self = this;
        self.selectedItems = ko.observableArray([]);

        self.locations = ko.observableArray();

        self.hideDetails = function (currentLocationsInsideObservableArrayOfLocations) {
            $('ul[id^="sportDetails_' + currentLocationsInsideObservableArrayOfLocations.id() + '"]').toggle()
        }

        self.toggleActive = function (currentLocationsInsideObservableArrayOfLocations) {

            $.ajax({
                url: "http://localhost:29196/location/ToggleActiveById/" + currentLocationsInsideObservableArrayOfLocations.id(),
                context: document.body,
                type: 'POST',
                statusCode: {
                    200: function () {
                        currentLocationsInsideObservableArrayOfLocations.active(!currentLocationsInsideObservableArrayOfLocations.active())
                    }
                }
            })
        }

        self.deleteLocation = function (currentLocationsInsideObservableArrayOfLocations) {
            var idOfLocationToDelete = currentLocationsInsideObservableArrayOfLocations.id();

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/location/" + idOfLocationToDelete,
                context: document.body,
                dataType: 'json',
                type: 'DELETE',
                statusCode: {
                    200: function () {
                        self.locations.remove(currentLocationsInsideObservableArrayOfLocations);
                    }
                }
            })

            
        }

        // Probably should find a better way of doing this - Workaround for Circular Reference error
        self.doSomething = function (formData) {
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
        }

        for (var i = 0; i < locationsDTO.length; i++) {
            self.locations.push(new Location(locationsDTO[i]));
        }

        return self;
    }

    return locations;
});