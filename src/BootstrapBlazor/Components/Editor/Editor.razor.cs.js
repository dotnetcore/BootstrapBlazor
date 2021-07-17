(function ($) {
    $.extend({
        bb_html5edit: function (el, options) {
            if (!$.isFunction($.fn.summernote)) {
                return;
            }

            var $this = $(el);
            var op = typeof options == 'object' && options;

            if (/destroy|hide/.test(options)) {
                return $this.toggleClass('open').summernote(options);
            }
            else if (typeof options == 'string') {
                return $this.hasClass('open') ? $this.summernote(options) : $this.html();
            }

            op = $.extend({ focus: true, lang: 'zh-CN', height: 80, dialogsInBody: true }, op);

            // div 点击事件
            $this.on('click', op, function (event, args) {
                var $this = $(this).tooltip('hide');
                var op = $.extend({ placeholder: $this.attr('placeholder') }, event.data, args || {});
                op.obj.invokeMethodAsync('GetToolBar').then(result => {
                    var $toolbar = $this.toggleClass('open').summernote($.extend({
                        callbacks: {
                            onChange: function (htmlString) {
                                op.obj.invokeMethodAsync(op.method, htmlString);
                            }
                        },
                        toolbar: result
                    }, op))
                        .next().find('.note-toolbar')
                        .on('click', 'button[data-method]', { note: $this, op: op }, function (event) {
                            var $btn = $(this);
                            switch ($btn.attr('data-method')) {
                                case 'submit':
                                    $btn.tooltip('dispose');
                                    var $note = event.data.note.toggleClass('open');
                                    var htmlString = $note.summernote('code');
                                    $note.summernote('destroy');
                                    event.data.op.obj.invokeMethodAsync(event.data.op.method, htmlString);
                                    break;
                            }
                        });
                    var $done = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn btn btn-sm note-btn-close" tabindex="-1" data-method="submit" title="完成" data-bs-placement="bottom"><i class="fa fa-check"></i></button></div>').appendTo($toolbar).find('button').tooltip({ container: 'body' });
                    $('body').find('.note-group-select-from-files [accept="image/*"]').attr('accept', 'image/bmp,image/png,image/jpg,image/jpeg,image/gif');
                });

            }).tooltip({ title: '点击展开编辑' });

            if (op.value) $this.html(op.value);
            if ($this.hasClass('open')) {
                // 初始化为 editor
                $this.trigger('click', { focus: false });
            }
            return this;
        },
        bb_editor: function (el, obj, attrMethod, callback, method, height, value) {
            var invoker = function () {
                var editor = el.getElementsByClassName("editor-body");

                if (obj === 'code') {
                    if ($(editor).hasClass('open')) {
                        $(editor).summernote('code', value);
                    }
                    else {
                        $(editor).html(value);
                    }
                }
                else {
                    var option = { obj: obj, method: method, height: height };
                    if (value) option.value = value;

                    $.bb_html5edit(editor, option);
                }
            }

            if (attrMethod !== "") {
                obj.invokeMethodAsync(attrMethod).then(result => {
                    for (var i in result) {
                        (function (plugin, pluginName) {
                            if (pluginName == null) {
                                return;
                            }
                            pluginObj = {};
                            pluginObj[pluginName] = function (context) {
                                var ui = $.summernote.ui;
                                context.memo('button.' + pluginName,
                                    function () {
                                        var button = ui.button({
                                            contents: '<i class="' + plugin.iconClass + '"></i>',
                                            container: "body",
                                            tooltip: plugin.tooltip,
                                            click: function () {
                                                obj.invokeMethodAsync(callback, pluginName).then(result => {
                                                    context.invoke('editor.pasteHTML', result);
                                                });
                                            }
                                        });
                                        this.$button = button.render();
                                        return this.$button;
                                    });
                            }
                            $.extend($.summernote.plugins, pluginObj);
                        })(result[i], result[i].buttonName);
                    }
                    invoker();
                });
            }
            else {
                invoker();
            }
        }
    });
})(jQuery);
