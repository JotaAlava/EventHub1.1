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

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'SucessFunctions' exists in the 
    // global SucessFunctions - if not, assign window.SucessFunctions an 
    // object literal
})(window.UltiSports.DAL.SucessFunctions = window.UltiSports.DAL.SucessFunctions || {});