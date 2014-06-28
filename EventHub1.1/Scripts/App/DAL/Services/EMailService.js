(function (EMailService) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    EMailService.UpdateEMail = function (submittedForm, successFunc, errorFunc) {
        var newEmail = $('#modalUpdateEMailInputBox').val();
        window.UltiSports.DAL.Ajaxer(submittedForm, window.appWideServiceURL + "user/updateemail/?newEmail=" + newEmail, "json", "POST", successFunc, errorFunc);
    }

    EMailService.SendInviteEMail = function() {
        window.UltiSports.DAL.Ajaxer("undefined", window.appWideServiceURL + "event/sendInviteEmail", "json", "GET", window.UltiSports.DAL.SucessFunctions.SendInviteEMail, window.UltiSports.DAL.ErrorFunctions.SendInviteEMail);
    }

    EMailService.SendFinalEMail = function () {
        window.UltiSports.DAL.Ajaxer("undefined", window.appWideServiceURL + "event/sendFinalEmail", "json", "GET", window.UltiSports.DAL.SucessFunctions.SendInviteEMail, window.UltiSports.DAL.ErrorFunctions.SendInviteEMail);
    }

    EMailService.SendCancelationEMail = function () {
        window.UltiSports.DAL.Ajaxer("undefined", window.appWideServiceURL + "event/sendCancelationEmail", "json", "GET", window.UltiSports.DAL.SucessFunctions.SendInviteEMail, window.UltiSports.DAL.ErrorFunctions.SendInviteEMail);
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'EMailService' exists in the 
    // global EMailService - if not, assign window.EMailService an 
    // object literal
})(window.UltiSports.DAL.EMailService = window.UltiSports.DAL.EMailService || {});