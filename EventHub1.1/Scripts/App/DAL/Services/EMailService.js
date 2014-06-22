(function (EMailService) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    EMailService.UpdateEMail = function (submittedForm, successFunc, errorFunc) {
        var newEmail = $('#modalUpdateEMailInputBox').val();

        window.UltiSports.DAL.Ajaxer(submittedForm, window.productionURL + "/user/updateemail/?newEmail=" + newEmail, "json", "POST", successFunc, errorFunc);
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'EMailService' exists in the 
    // global EMailService - if not, assign window.EMailService an 
    // object literal
})(window.UltiSports.DAL.EMailService = window.UltiSports.DAL.EMailService || {});