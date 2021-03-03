(function ($) {
    $.extend({
        bb_markdown: function (el, isInit, method) {
            var key = 'bb_editor';
            var $el = $(el);
            if (!isInit) {
                return $el.toastuiEditor(method);
            }
            else {
                $el.toastuiEditor({
                    initialEditType: 'markdown',
                    previewStyle: 'vertical',
                    language: 'zh-CN',
                    initialValue: method
                });
            }
        }
    });
})(jQuery);
