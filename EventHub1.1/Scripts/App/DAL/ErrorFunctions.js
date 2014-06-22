(function (ErrorFunctions) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    ErrorFunctions.NewMessage = function (result, clickedEvent) {
        alert(result);
    }

    ErrorFunctions.UpdateEMail = function (result, serviceResultCode, serviceResponseHeader) {
        //TODO: Figure out why even thought it's success, this function is being called after the ajax to UpdateEMail!
        $(window.UltiSports.Settings.UpdateEMailModalSelector).modal('hide');
        window.UltiSports.ErrorFeedback.UnWarnAbouhtHavingNoEMail()
        $.growlUI('EMail Updated!');
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'ErrorFunctions' exists in the 
    // global ErrorFunctions - if not, assign window.ErrorFunctions an 
    // object literal
})(window.UltiSports.DAL.ErrorFunctions = window.UltiSports.DAL.ErrorFunctions || {});