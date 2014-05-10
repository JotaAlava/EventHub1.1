define(["ViewModels/Location"], function (Location) {

    var locations = function (locationsDTO) {
        var self = this;

        self.locations = ko.observableArray();
        
        self.hideDetails = function () {
            $('ul[id^="sportDetails_"]').toggle()
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