(function ($) {
    $.extend({
        bb_showLabelTooltip: function (el, method, title) {
            var $el = $(el);
            var instance = bootstrap.Tooltip.getInstance(el);
            if (instance) {
                instance.dispose();
            }
            if (method === 'init') {
                $el.tooltip({
                    title
                });
            }
        }
    });
}(jQuery));
