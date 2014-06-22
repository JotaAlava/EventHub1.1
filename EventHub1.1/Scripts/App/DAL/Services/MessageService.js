(function (MessageService) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    MessageService.AddNewMessage = function (clickedEvent, newMessage, successFunc, errorFunc) {
        var isEmptyString = window.UltiSports.InputValidation.IsEmptyString(newMessage.Body);
        var isLongerThan255Chars = window.UltiSports.InputValidation.IsLongerThan50Chars(newMessage.Body);

        if (!isEmptyString && !isLongerThan255Chars) {
            window.UltiSports.ErrorFeedback.ResetErrorBox();
            window.UltiSports.DAL.Ajaxer(clickedEvent, window.productionURL + "/message/", "json", "POST", newMessage, successFunc, errorFunc);
        }
        else {
            window.UltiSports.ErrorFeedback.InvalidMessageInput();
        }
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'MessageService' exists in the 
    // global MessageService - if not, assign window.MessageService an 
    // object literal
})(window.UltiSports.DAL.MessageService = window.UltiSports.DAL.MessageService || {});