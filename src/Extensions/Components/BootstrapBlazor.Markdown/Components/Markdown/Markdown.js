/*!
 * Markdown : Argo@163.com
 * @version 5.1.0
 */

(function ($) {
    $.extend({
        bb_markdown: function (el, obj, value, method) {
            var $el = $(el);
            if (method === "setMarkdown") {
                //$el.toastuiEditor('setMarkdown', value);
                var editor = $.data(el,'editor');
                editor.setMarkdown(value);
            }
            else {
                $.extend(value, {
                    events: {
                        blur: function () {
                            var editor = $.data(el, 'editor');
                            var val = editor.getMarkdown();
                            var html = editor.getHTML();
                            obj.invokeMethodAsync(method, [val, html]);
                        }
                    }
                })
                
                // 修复弹窗内初始化值不正确问题
                var handler = window.setInterval(function () {
                    if ($el.is(':visible')) {
                        window.clearInterval(handler);
                        value.el = el;
                        value.plugins = [];
                        if (value.enableHighlight) {
                            value.plugins.push(toastui.Editor.plugin.codeSyntaxHighlight);
                        }
                        delete value.enableHighlight;
                        const editor = toastui.Editor.factory(value);
                        $.data(el, 'editor', editor);
                    }
                }, 100);
            }
        }
    });
})(jQuery);
