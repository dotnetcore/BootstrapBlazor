(function ($) {
    $.extend({
        bb_markdown: function (el, isInit, method) {
            var key = 'bb_editor';
            var $el = $(el);
            if (!isInit) {
                var result = $el.toastuiEditor(method);
                    console.log(result);
                    return result;
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
