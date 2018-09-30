define(['jquery', 'jQCloud', 'exports'], function ($, jQCloud, exports) {

    const uri = '/';

    exports.init = () => {

        $(document).ready(function ($) {
            $("#loading-div-background").css({ opacity: 0.8 });
        });

        $('#btnSearch').click(function () {

            $("#searchForm").validate();

            if ($("#searchForm").valid()) {

                $("#loading-div-background").show();


                $.ajax({
                    type: 'POST',
                    accepts: 'application/json',
                    url: uri,
                    contentType: 'application/json',
                    data: JSON.stringify($('#URL').val()),
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("#loading-div-background").hide();
                    },
                    success: function (result) {

                        $("#demo").jQCloud([], {
                            width: 500,
                            height: 350
                        });

                        $('#demo').jQCloud('update', result);

                        $("#loading-div-background").hide();
                    }
                });
            }
        });
    };

    return exports;
});