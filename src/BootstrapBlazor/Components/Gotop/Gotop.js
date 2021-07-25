(function ($) {
    $.extend({
        bb_gotop: function (el, target) {
            var $el = $(el);
            var tooltip = $el.tooltip();
            $el.on('click', function (e) {
                e.preventDefault();
                $(target || window).scrollTop(0);
                tooltip.tooltip('hide');
            });
        }
    });
})(jQuery);
