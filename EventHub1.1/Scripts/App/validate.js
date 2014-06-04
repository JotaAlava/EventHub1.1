// validation
// Alex Buznik (http://buznik.net/)
// 2012-2013
//
// just call validize.validate_ini() when window ready. It is safe to recall it when new fields are added.
// fields to be validated need appropriate 'data-validate' attribute.

if (typeof page_var == "undefined") page_var = {}
page_var.lang = 0 // id of the language, corresponds with the second var name in err_string;


var validize = {


    err_string: {
        'full': {
            0: "The field must not be empty"
        },
        'length': {
            0: "At least 6 symbols"
        },
        'password': {
            0: "The passwords must match"
        },
        'text': {
            0: 'Letters only'
        },
        'phone': {
            0: "Numbers only and +"
        },
        'email': {
            0: "E-mail address is incorrect"
        },
        'general': {
            0: "This field is incorrect"
        }

    },
    addError: function (curObj, error, dv, err_string) {
        if (curObj.hasClass("error")) {
            curObj.parents(".i").prev("label").removeClass("valid").find(".err_string").remove();
        }

        err_string = validize.err_string[err_string][page_var.lang];
        curObj
			.addClass("error")
			.parents(".i").prev("label").removeClass("valid").addClass("invalid")
			.find("em").before('<span class="err_string">(' + err_string + ')</span>')
        error[dv] = err_string;
    },

    removeError: function (curObj, error, dv) {
        curObj.removeClass("error")
        if (!curObj.parents(".i").find(".error").length) {
            curObj.parents(".i").prev("label").addClass("valid").removeClass("invalid").find(".err_string").remove();
        }
        if (error) delete error.dv;
    },

    validate_ini: function (obj) {

        $(obj).unbind('blur').bind('blur', function (e) {
            var curObj = $(this);
            var error = {};
            var c = curObj.val();
            var dv = curObj.attr("data-validate");
            validize.removeError(curObj);
            if (!$(this).hasClass("opt")) {

                if (dv == "full") {
                    if (c == '' || c == ' ' || c == "") {
                        validize.addError(curObj, error, dv, "full");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "length") {
                    if (c == '' || c == ' ' || c == "") {
                        validize.addError(curObj, error, dv, "full");
                    } else if (c.length < 6) {
                        validize.addError(curObj, error, dv, "length");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "password") {
                    if (c == '' || c == ' ' || c == "") {
                        validize.addError(curObj, error, dv, "full");
                    } else if (c != curObj.parents("li").prev("li").find("input[data-pass-one]").val()) {
                        validize.addError(curObj, error, dv, "password");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "text") {
                    var reg = new RegExp('[0-9]+');
                    if (c == '' || c == ' ' || c == "") {
                        validize.addError(curObj, error, dv, "full");
                    } else if (reg.test(c)) {
                        validize.addError(curObj, error, dv, "text");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "phone") {
                    var reg = new RegExp('[0-9]+');
                    if (c == '' || c == ' ' || c == "" || c.length < 7) {
                        validize.addError(curObj, error, dv, "full");
                    } else if (!reg.test(c)) {
                        validize.addError(curObj, error, dv, "phone");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "date") {
                    var reg = new RegExp('[0-9\.]+');

                    if (c == '' || c == ' ' || c == "") {
                        validize.addError(curObj, error, dv, "full");
                    } else if (!reg.test(c)) {
                        validize.addError(curObj, error, dv, "general");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "select") {
                    if (!curObj.siblings(".dropdown").find("li[data-value='" + c + "']").length) {
                        validize.addError(curObj, error, dv, "general");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }

                if (dv == "email") {

                    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

                    if (reg.test(c) != true) {
                        validize.addError(curObj, error, dv, "email");
                    } else {
                        validize.removeError(curObj, error, dv);
                    }
                }
            }


            if (curObj.is(curObj.parents("form").find("[data-validate]").last()) && !curObj.parents("form").find(".error").length) {
                curObj.parents("form").addClass("is_valid")
                if (curObj.parents("form").hasClass("submit_after")) curObj.parents("form").submit();
            } else {
                curObj.parents("form").removeClass("is_valid");
                if (curObj.parents("form").hasClass("submit_after")) curObj.parents("form").removeClass("submit_after")
            }


        });
    }
}