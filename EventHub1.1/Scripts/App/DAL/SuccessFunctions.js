(function (SucessFunctions) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    SucessFunctions.NewMessage = function (result, serviceResultCode, serviceResponseHeader) {
        var idOfEventWeHaveAddedAMessageFor = serviceResponseHeader.responseJSON.EventId;
        for (var i = 0; i < window.viewModels.eventsViewModel.listOfEvents().length; i++) {
            if (window.viewModels.eventsViewModel.listOfEvents()[i].eventId() == idOfEventWeHaveAddedAMessageFor) {
                result.TimeCreated = new Date(result.TimeCreated).toLocaleTimeString();
                window.viewModels.eventsViewModel.listOfEvents()[0].messages.push(result);
            }
        }
        $(window.UltiSports.Settings.AddMessageInputFieldIdSelector + idOfEventWeHaveAddedAMessageFor).val('');
        $.growlUI('Message Posted!');
    }

    SucessFunctions.UpdateEMail = function (result, serviceResultCode, serviceResponseHeader) {
        $.growlUI('EMail Updated!');
    }

    SucessFunctions.SendInviteEMail = function(result, serviceResultCode, serviceResponseHeader) {
        $.growlUI('EMail has been sent!');
    }

    SucessFunctions.GetAllLocations = function (result, serviceResultCode, serviceResponseHeader) {
        if (window.viewModels.locationViewModel.locations().length == 1) {
            window.viewModels.locationViewModel.locations()[0].name(result[0].Name);
            window.viewModels.locationViewModel.locations()[0].address(result[0].Address);
            window.viewModels.locationViewModel.locations()[0].active(result[0].Active);
        }
        //window.viewModels.locationViewModel.locations(new window.UltiSports.Models.LocationModel.GetNewLocation(result));
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'SucessFunctions' exists in the 
    // global SucessFunctions - if not, assign window.SucessFunctions an 
    // object literal
})(window.UltiSports.DAL.SucessFunctions = window.UltiSports.DAL.SucessFunctions || {});