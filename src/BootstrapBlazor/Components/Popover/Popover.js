(function ($) {
    $.extend({
        bb_confirm: function (id) {
            var $ele = $('[data-bs-target="' + id + '"]');
            var showClassName = 'is-show';
            var button = document.getElementById(id);
            var popover = bootstrap.Popover.getOrCreateInstance(button, {
                toggle: 'confirm',
                html: true,
                sanitize: false,
                content: $ele.find('.popover-body').html()
            });
            if (button.classList.contains(showClassName) === false) {
                popover.show();
                button.classList.add(showClassName);
            }
            else {
                popover.hide();
                button.classList.remove(showClassName);
            }
        },
        bb_confirm_submit: function (id) {
            var $ele = $('#' + id);
            var $submit = $('<button type="submit" hidden />');
            $submit.appendTo($ele.parent());
            $submit.trigger('click');
            $submit.remove();
        },
        bb_popover: function (id, method, title, content, placement, html, trigger, css) {
            var ele = document.getElementById(id);
            var instance = bootstrap.Popover.getInstance(ele);
            if (instance) {
                instance.dispose();
            }
            if (method !== 'dispose') {
                var op = { html, sanitize: false, title, content, placement, trigger };
                if (css !== '') {
                    op.customClass = css;
                }
                instance = new bootstrap.Popover(ele, op);
                if (method !== '') {
                    $(ele).popover(method);
                }
            }
        },
        bb_datetimePicker: function (el, method) {
            var input = el.querySelector('.datetime-picker-input');
            var p = bb.Popover.getOrCreateInstance(input, {
                bodyElement: el.querySelector('.date-picker')
            });

            if (method) {
                p.invoke(method);
            }
        },
        bb_datetimeRange: function (el, method) {
            var input = el.querySelector('.datetime-range-control');
            var p = bb.Popover.getOrCreateInstance(input, {
                bodyElement: el.querySelector('.datetime-range-body')
            });

            if (method) {
                p.invoke(method);
            }
        }
    });

    $(function () {
        // popover confirm
        // $.fn.popover.Constructor.prototype._isWithContent = function () {
        //     var components = ['', 'confirm', 'datetime-picker', 'datetime-range'];
        //     var toggle = this._config.toggle;
        //     return components.indexOf(toggle) || this.getTitle() || this._getContent();
        // }

        // add shadow
        // var getTipElement = $.fn.popover.Constructor.prototype._getTipElement;
        // $.fn.popover.Constructor.prototype._getTipElement = function () {
        //     var toggle = this._config.toggle;
        //     var tip = getTipElement.call(this);
        //     var $tip = $(tip).addClass('shadow');
        //     if ($tip.find('.popover-header').length > 0) {
        //         $tip.addClass('has-header');
        //     }
        //     if (toggle === 'datetime-picker') {
        //         $tip.addClass('popover-datetime popover-p0');
        //     }
        //     else if (toggle === 'datetime-range') {
        //         $tip.addClass('popover-datetime-range popover-p0');
        //     }
        //     else if (toggle === 'dropdown') {
        //         $tip.addClass('popover-dropdown popover-p0');
        //     }
        //     else if (toggle === 'multi-select') {
        //         $tip.addClass('popover-multi-select popover-p0');
        //     }
        //     return tip;
        // }

        var findConfirmButton = function ($el) {
            var button = null;
            var $parent = $el.parents('.popover');
            if ($parent.length > 0) {
                var id = $parent.attr('id');
                button = $('[aria-describedby="' + id + '"]');
            }
            return button;
        };

        var disposePopup = function ($ele) {
            $ele.popover('dispose');
            $ele.removeClass('is-show');
        };

        $(document).on('click', function (e) {
            // hide popover
            var hide = true;
            var $el = $(e.target);

            // 判断是否点击 popover 内部
            var $confirm = findConfirmButton($el);
            if ($confirm != null) hide = false;
            if (hide) {
                var $target = $el;
                if ($target.data('bs-toggle') !== 'confirm') {
                    $target = $target.parents('[data-bs-toggle="confirm"][aria-describedby^="popover"]');
                }
                $('[data-bs-toggle="confirm"][aria-describedby^="popover"]').each(function (index, ele) {
                    if ($target[0] !== ele) {
                        var $ele = $(ele);
                        disposePopup($ele);
                    }
                });
            }

            // multi-select
            if ($el.parents('.popover-multi-select.show').length === 0) {
                $('.popover-multi-select.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.parents('.dropdown-toggle').attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }
        });

        $(document).on('click', '.popover-confirm-buttons .btn', function (e) {
            e.stopPropagation();

            // 确认弹窗按钮事件
            var $confirm = findConfirmButton($(this));
            if ($confirm.length > 0) {
                // 关闭弹窗
                disposePopup($confirm);

                // remove popover
                var buttonId = $confirm.attr('id');
                $ele = $('[data-bs-target="' + buttonId + '"]');

                var $button = this.getAttribute('data-dismiss') === 'confirm'
                    ? $ele.find('.popover-confirm-buttons .btn:first')
                    : $ele.find('.popover-confirm-buttons .btn:last');
                $button.trigger('click');
            }
        });
    });
})(jQuery);
