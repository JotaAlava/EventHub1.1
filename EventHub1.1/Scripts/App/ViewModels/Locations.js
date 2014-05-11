define(["ViewModels/Location"], function (Location) {

    var locations = function (locationsDTO) {
        var self = this;
        self.selectedItems = ko.observableArray([]);

        self.locations = ko.observableArray();

        self.hideDetails = function (viewModelOnWhichThisMethodWasCalled) {
            $('ul[id^="sportDetails_' + viewModelOnWhichThisMethodWasCalled.id() + '"]').toggle()
        }

        self.deleteLocation = function (viewModelOnWhichThisMethodWasCalled) {
            var idOfLocationToDelete = viewModelOnWhichThisMethodWasCalled.id();

            $.ajax({
                url: "http://localhost:29196/location/" + idOfLocationToDelete,
                context: document.body,
            }).done(function (dataFromServiceCall) {
                self.locations.remove(dataFromServiceCall);
            });

            $.ajax({
                dataType: "json",
                url: "http://localhost:29196/location/" + idOfLocationToDelete,
                context: document.body,
                dataType: 'json',
                type: 'DELETE',
            });

            
        }

        // Probably should find a better way of doing this - Circular Reference error
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