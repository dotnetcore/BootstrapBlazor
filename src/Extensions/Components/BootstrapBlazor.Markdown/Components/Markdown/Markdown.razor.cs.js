(function ($) {
    $.extend({
        bb_markdown: function (el, obj, value, method) {
            var $el = $(el);
            $.extend(value, {
                events: {
                    blur: function () {
                        var val = $el.toastuiEditor('getMarkdown');
                        var html = $el.toastuiEditor('getHtml');
                        obj.invokeMethodAsync(method, [val, html]);
                    }
                }
            })
            $el.toastuiEditor(value);
        }
    });
})(jQuery);
