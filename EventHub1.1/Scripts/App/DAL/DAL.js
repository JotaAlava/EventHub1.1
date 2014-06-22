(function (DAL) {
    // private properties
    var foo = "foo",
        bar = "bar";

    // public methods and properties
    DAL.Ajaxer = function (clickedViewModelElement, URL, DataType, Verb, dataToPost, SuccessFunc, ErrorFunc) {
        $.ajax({
            url: URL,
            dataType: DataType,
            type: Verb,
            data: dataToPost,
            success: SuccessFunc,
            error: ErrorFunc
        });
    }

    // private method
    function speak(msg) {
        console.log("You said: " + msg);
    };

    // check to evaluate whether 'DAL' exists in the 
    // global DAL - if not, assign window.DAL an 
    // object literal
})(window.UltiSports.DAL = window.UltiSports.DAL || {});