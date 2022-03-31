(function ($) {
    $.extend({
        bb_form: function (id) {
            var $el = $('#' + id);
            $el.find('[aria-describedby]').each(function (index, ele) {
                var tooltip = bootstrap.Tooltip.getInstance(ele);
                if (tooltip) {
                    var $ele = $(ele);
                    $ele.tooltip('dispose');
                }
            });
        }
    });
})(jQuery);
