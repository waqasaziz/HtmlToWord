requirejs.config({
    baseUrl: '/js',
    paths: {
        'jquery': '../lib/jquery/jquery',
        'jquery-validation': '../lib/jquery-validation/jquery.validate',
        'jquery.validate.unobtrusive': '../lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive',
        'bootstrap': '../lib/bootstrap/js/bootstrap',
        site: '/site.js'
    },

    shim: {
        'bootstrap': ['jquery'],
        'jquery-validation': ['jquery'],
    }
});

require(['jquery', 'bootstrap', 'jquery-validation', 'jquery.validate.unobtrusive'], function () {


    $.validator.setDefaults({
        errorElement: "span",
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            console.log('errorPlacement');
            console.log(element);

            // Add the `help-block` class to the error element
            error.addClass("help-block");

            // Add `has-feedback` class to the parent div.form-group
            // in order to add icons to inputs
            element.closest(".form-group").addClass("has-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }

            // Add the span element, if doesn't exists, and apply the icon classes to it.
            if (!element.next("span")[0]) {
                $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            }
        },
        success: function (label, element) {
            // Add the span element, if doesn't exists, and apply the icon classes to it.
            if (!$(element).next("span")[0]) {
                $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .addClass("has-error has-feedback")
                .removeClass("has-success");

            $(element).next("span")
                .addClass("glyphicon glyphicon-remove form-control-feedback")
                .removeClass("glyphicon-ok");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .addClass("has-success has-feedback")
                .removeClass("has-error");

            $(element).next("span")
                .addClass("glyphicon glyphicon-ok form-control-feedback")
                .removeClass("glyphicon-remove");
        }
    });

    //$.validator.setDefaults({
    //    highlight: function (element) {
    //        $(element).closest('.form-group').addClass('has-error');
    //    },
    //    unhighlight: function (element) {
    //        $(element).closest('.form-group').removeClass('has-error');
    //    },
    //    errorElement: 'span',
    //    errorClass: 'help-block',
    //    errorPlacement: function (error, element) {
    //        if (element.parent('.input-group').length) {
    //            error.insertAfter(element.parent());
    //        } else {
    //            error.insertAfter(element);
    //        }
    //    }
    //});

    //$("#signupForm1").validate({
    //    rules: {
    //        firstname1: "required",
    //        lastname1: "required",
    //        username1: {
    //            required: true,
    //            minlength: 2
    //        },
    //        password1: {
    //            required: true,
    //            minlength: 5
    //        },
    //        confirm_password1: {
    //            required: true,
    //            minlength: 5,
    //            equalTo: "#password1"
    //        },
    //        email1: {
    //            required: true,
    //            email: true
    //        },
    //        agree1: "required"
    //    },
    //    messages: {
    //        firstname1: "Please enter your firstname",
    //        lastname1: "Please enter your lastname",
    //        username1: {
    //            required: "Please enter a username",
    //            minlength: "Your username must consist of at least 2 characters"
    //        },
    //        password1: {
    //            required: "Please provide a password",
    //            minlength: "Your password must be at least 5 characters long"
    //        },
    //        confirm_password1: {
    //            required: "Please provide a password",
    //            minlength: "Your password must be at least 5 characters long",
    //            equalTo: "Please enter the same password as above"
    //        },
    //        email1: "Please enter a valid email address",
    //        agree1: "Please accept our policy"
    //    },
    //    errorElement: "em",
    //    errorPlacement: function (error, element) {
    //        // Add the `help-block` class to the error element
    //        error.addClass("help-block");

    //        // Add `has-feedback` class to the parent div.form-group
    //        // in order to add icons to inputs
    //        element.parents(".col-sm-5").addClass("has-feedback");

    //        if (element.prop("type") === "checkbox") {
    //            error.insertAfter(element.parent("label"));
    //        } else {
    //            error.insertAfter(element);
    //        }

    //        // Add the span element, if doesn't exists, and apply the icon classes to it.
    //        if (!element.next("span")[0]) {
    //            $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
    //        }
    //    },
    //    success: function (label, element) {
    //        // Add the span element, if doesn't exists, and apply the icon classes to it.
    //        if (!$(element).next("span")[0]) {
    //            $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
    //        }
    //    },
    //    highlight: function (element, errorClass, validClass) {
    //        $(element).parents(".col-sm-5").addClass("has-error").removeClass("has-success");
    //        $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
    //    },
    //    unhighlight: function (element, errorClass, validClass) {
    //        $(element).parents(".col-sm-5").addClass("has-success").removeClass("has-error");
    //        $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
    //    }
    //});
});
