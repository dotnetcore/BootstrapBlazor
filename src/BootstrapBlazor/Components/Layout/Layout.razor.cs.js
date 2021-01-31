(function ($) {
    $.extend({
        bb_layout: function (refObj, method) {
            var calcWindow = function () {
                var width = $(window).width();
                refObj.invokeMethodAsync(method, width);
            }

            $('.layout-header').find('[data-toggle="tooltip"]').tooltip();

            calcWindow();

            $(window).on('resize', function () {
                calcWindow();
            });
        }
    });
})(jQuery);
