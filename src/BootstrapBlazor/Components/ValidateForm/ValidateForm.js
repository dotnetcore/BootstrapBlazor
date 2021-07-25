(function ($) {
    $.extend({
        bb_form: function (id, method) {
            var $el = $('#' + id);
            $el.find('[aria-describedby]').each(function (index, ele) {
                var $ele = $(ele);
                $ele.tooltip('dispose');
            });
        }
    });
})(jQuery);
