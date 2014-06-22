(function (ErrorFeedback) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    ErrorFeedback.InvalidMessageInput = function () {
        if (IsErrorBannerHidden()) {
            $(window.UltiSports.Settings.ErrorBannerIdSelector).toggle();
        }

        var currentErrorMessage = $(window.UltiSports.Settings.ErrorBannerIdSelector).text();
        var ErrMsgPrefix = "<strong>ERROR:</strong> ";
        var FormatLessPrefix = "ERROR: ";
        var ErrMsg1 = "The message cannot be empty or larger than 50 characters!";
        var ErrMsg2 = "Come on! Fix the input for the message silly human. I want it not empty and not larger than 50 chars!";
        var ErrMsg3 = "Seriously " + window.usersUsername.substring(6) + "?";
        var ErrMsg4 = "NO!";

        switch (currentErrorMessage) {
            case FormatLessPrefix+ErrMsg1:
                {
                    $(window.UltiSports.Settings.ErrorBannerIdSelector).html(ErrMsgPrefix + ErrMsg2);
                    break;
                }
            case FormatLessPrefix+ErrMsg2:
                {
                    $(window.UltiSports.Settings.ErrorBannerIdSelector).html(ErrMsgPrefix + ErrMsg3);
                    break;
                }
            case FormatLessPrefix+ErrMsg3:
                {
                    $(window.UltiSports.Settings.ErrorBannerIdSelector).html(ErrMsgPrefix + ErrMsg4);
                    break;
                }

            default:
            {
                $(window.UltiSports.Settings.ErrorBannerIdSelector).html(ErrMsgPrefix + ErrMsg1);
            }
        }
    }

    ErrorFeedback.ResetErrorBox = function () {
        if (!IsErrorBannerHidden()) {
            $(window.UltiSports.Settings.ErrorBannerIdSelector).toggle();
        }
    }

    ErrorFeedback.WarnAbouhtHavingNoEMail = function() {
        $(window.UltiSports.Settings.WarningBannerIdSelector).toggle();
        var updateEMailButton = '<a href=# data-toggle="modal" data-target="#UpdateEmailModal">Click Here To Update EMail</a>';
        $(window.UltiSports.Settings.WarningBannerIdSelector).html("<strong>Warning: </strong> You have not set your email. If you don't, then you will not be notified of events." + updateEMailButton);
    }

    ErrorFeedback.UnWarnAbouhtHavingNoEMail = function() {
        $(window.UltiSports.Settings.WarningBannerIdSelector).toggle();
        $(window.UltiSports.Settings.WarningBannerIdSelector).html("");
    }

    // private method
    function IsErrorBannerHidden() {
      return $(window.UltiSports.Settings.ErrorBannerIdSelector).is(":hidden") 
    };

    // check to evaluate whether 'ErrorFeedback' exists in the 
    // global ErrorFeedback - if not, assign window.ErrorFeedback an 
    // object literal
})(window.UltiSports.ErrorFeedback = window.UltiSports.ErrorFeedback || {});