(function ($) {
    $.extend({
        bb_markdown: function (el, type, method, value) {
            var $el = $(el);
            if (type == 1) {
                return $el.toastuiEditor(method);
            }
            else if (type == 0) {
                $el.toastuiEditor(method);
            } else if (type == 2) {
                $el.toastuiEditor(method, value);
            }
        }
    });
})(jQuery);
