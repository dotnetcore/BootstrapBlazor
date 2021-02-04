(function ($) {
    $.extend({
        bb_tooltip: function (id, method, title, placement, html, trigger) {
            var op = { html: html, sanitize: !html, title: title, placement: placement, trigger: trigger };
            var $ele = $('#' + id);
            if (method === "") {
                if ($ele.data('bs.tooltip')) $ele.tooltip('dispose');
                $ele.tooltip(op);
            }
            else if (method === 'enable') {
                if ($ele.data('bs.tooltip')) $ele.tooltip('dispose');
                $ele.tooltip(op);
                var $ctl = $ele.parents('form').find('.is-invalid:first');
                if ($ctl.prop("nodeName") === 'INPUT') {
                    if ($ctl.prop('readonly')) {
                        $ctl.trigger('focus');
                    }
                    else {
                        $ctl.focus();
                    }
                }
                else if ($ctl.prop("nodeName") === 'DIV') {
                    $ctl.trigger('focus');
                }
            }
            else if (method === "dispose") {
                if ($ele.data('bs.tooltip')) $ele.tooltip(method);
            }
            else {
                if ($ele.data('bs.tooltip')) {
                    $ele.tooltip('dispose');
                }
                $ele.tooltip(op);
                $ele.tooltip(method);
            }
        },
        bb_popover: function (id, method, title, content, placement, html, trigger) {
            var $ele = $('#' + id);
            var op = { html: html, sanitize: false, title: title, content: content, placement: placement, trigger: trigger };
            if (method === "") {
                if ($ele.data('bs.popover')) $ele.popover('dispose');
                $ele.popover(op);
            }
            else if (method === "dispose") {
                if ($ele.data('bs.popover')) $ele.popover(method);
            }
            else {
                if (!$ele.data('bs.popover')) $ele.popover(op);
                $ele.popover(method);
            }
        },
        bb_confirm: function (id) {
            var $ele = $('[data-target="' + id + '"]');
            var $button = $('#' + id);

            $button.popover({
                toggle: 'confirm',
                html: true,
                sanitize: false,
                content: $ele.find('.popover-body').html()
            });
            $button.popover('show');
        },
        markdown: function (el, method) {
            var key = 'bb_editor';
            var $el = $(el);
            if (method) {
                var editor = $el.data(key);
                if (editor) {
                    var result = editor[method]();
                    console.log(result);
                    return result;
                }
            }
            else {
                var id = $.getUID();
                $el.attr('id', id);
                var editor = editormd(id, {
                    saveHTMLToTextarea: true,
                    path: "/lib/"
                });
                $el.data(key, editor);
            }
        },
        bb_console_log: function (el) {
            var $el = $(el);
            var $body = $el.find('[data-scroll="auto"]');
            if ($body.length > 0) {
                var $win = $body.find('.console-window');
                $body.scrollTop($win.height());
            }
        },
        bb_multi_select: function (el, obj, method) {
            $(el).data('bb_multi_select', { obj: obj, method: method });
        },
        bb_tree: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });
        }
    });

    $(function () {
        $(document)
            .on('inserted.bs.tooltip', '.is-invalid', function () {
                $('#' + $(this).attr('aria-describedby')).addClass('is-invalid');
            });

        // popover confirm
        $.fn.popover.Constructor.prototype.isWithContent = function () {
            var components = ['', 'confirm', 'datetime-picker', 'datetime-range'];
            var toggle = this.config.toggle;
            return components.indexOf(toggle) || this.getTitle() || this._getContent();
        }

        var findConfirmButton = function ($el) {
            var button = null;
            var $parent = $el.parents('.popover');
            if ($parent.length > 0) {
                var id = $parent.attr('id');
                button = $('[aria-describedby="' + id + '"]');
            }
            return button;
        };

        $(document).on('click', function (e) {
            // hide popover
            var hide = true;
            var $el = $(e.target);

            // 判断是否点击 popover 内部
            var $confirm = findConfirmButton($el);
            if ($confirm != null) hide = false;
            if (hide) $('[data-toggle="confirm"][aria-describedby^="popover"]').popover('hide');

            if ($el.parents('.popover-datetime-range.show').length === 0) {
                $('.popover-datetime-range.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.parents('.datetime-range-bar').attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }

            // 处理 MultiSelect 弹窗
            var $select = $el.closest('.multi-select');
            $('.multi-select.show').each(function () {
                if ($select.length === 0 || this != $select[0]) {
                    var select = $(this).data('bb_multi_select');
                    select.obj.invokeMethodAsync(select.method);
                }
            });
        });

        $(document).on('click', '.popover-confirm-buttons .btn', function (e) {
            e.stopPropagation();

            // 确认弹窗按钮事件
            var $confirm = findConfirmButton($(this));
            if ($confirm != null) {
                // 关闭弹窗
                $confirm.popover('hide');

                // remove popover
                var buttonId = $confirm.attr('id');
                $ele = $('[data-target="' + buttonId + '"]');

                var $button = this.getAttribute('data-dismiss') === 'confirm'
                    ? $ele.find('.popover-confirm-buttons .btn:first')
                    : $ele.find('.popover-confirm-buttons .btn:last');
                $button.trigger('click');
            }
        });
    });
})(jQuery);
