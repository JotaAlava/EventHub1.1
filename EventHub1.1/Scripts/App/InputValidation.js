(function (InputValidation) {
    // public methods and properties
    InputValidation.IsEmptyString = function (stringToEvaluate) {
        if (stringToEvaluate.length == 0) {
            return true;
        }

        return false;
    }

    InputValidation.IsLongerThan50Chars = function (stringToEvaluate) {
        if (stringToEvaluate.length == 50) {
            return true;
        }

        return false;
    }

})(window.UltiSports.InputValidation = window.UltiSports.InputValidation || {});