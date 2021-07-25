(function ($) {
    $.extend({
        bb_layout: function (refObj, method) {
            if (method === 'dispose') {
                $(window).off('resize');
                return;
            }
            var calcWindow = function () {
                var width = $(window).width();
                refObj.invokeMethodAsync(method, width);
            }

            $('.layout-header').find('[data-bs-toggle="tooltip"]').tooltip();

            calcWindow();

            $(window).on('resize', function () {
                calcWindow();
            });
        }
    });
})(jQuery);
