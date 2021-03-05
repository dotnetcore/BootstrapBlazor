(function ($) {
    $.extend({
        bb_markdown: function (el, type, method, value) {
            var $el = $(el);
            if (type === 'init') {
                $el.toastuiEditor(method);
            }
            if (type === 'get') {
                return $el.toastuiEditor(method);
            }
            else if (type === 'set') {
                $el.toastuiEditor(method, value);
            }
        }
    });
})(jQuery);
