(function (Settings) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    Settings.AddMessageInputFieldIdSelector = "#messageBox_";
    Settings.ErrorBannerIdSelector = '#ErrorBanner';
    Settings.WarningBannerIdSelector = '#WarningBanner';
    Settings.UpdateEMailModalSelector = '#UpdateEmailModal';

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'Settings' exists in the 
    // global Settings - if not, assign window.Settings an 
    // object literal
})(window.UltiSports.Settings = window.UltiSports.Settings || {});