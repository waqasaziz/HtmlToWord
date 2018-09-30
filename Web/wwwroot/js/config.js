requirejs.config({
    baseUrl: '/js',
    paths: {
        'jquery': '../lib/jquery/jquery',
        'jquery-validation': '../lib/jquery-validation/jquery.validate',
        'jquery.validate.unobtrusive': '../lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive',
        'bootstrap': '../lib/bootstrap/js/bootstrap',
        'jQCloud': '../lib/jqcloud2/dist/jqcloud',


        'wordSearch': 'search',
        'site': 'site'
    },

    shim: {
        'bootstrap': ['jquery'],
        'jquery-validation': ['jquery'],
        'jQCloud': ['jquery'],
        'wordSearch': ['jQCloud']
    }
});

require(['jquery', 'bootstrap', 'jquery-validation', 'jquery.validate.unobtrusive', 'jQCloud'], function ($) {

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


    require(['wordSearch'], function (wordSearch) {

        wordSearch.init();
    });

});
