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
        bb_popover: function (id, method, title, content, placement, html, trigger) {
            var $ele = $('#' + id);
            var op = { html, sanitize: false, title, content, placement, trigger };
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
        bb_datetimePicker: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-bs-placement') || 'auto';
            var $input = $el.find('.datetime-picker-input');
            if (!method) {
                $input.popover({
                    toggle: 'datetime-picker',
                    placement: placement
                })
                    .on('show.bs.popover', function () {
                        var disabled = $(this).parent().hasClass('disabled');
                        return !disabled;
                    })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $body = $pop.find('.popover-body');
                            if ($body.length === 0) {
                                $body = $('<div class="popover-body"></div>').appendTo($pop);
                            }
                            $body.append($el.find('.date-picker').removeClass('d-none'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.date-picker');
                            $el.append($picker.addClass('d-none'));
                        }
                    });

                $el.find('.datetime-picker-input-icon').on('click', function (e) {
                    // handler disabled event
                    if ($(this).hasClass('disabled')) return;

                    e.stopImmediatePropagation();
                    var $input = $(this).parents('.datetime-picker-bar').find('.datetime-picker-input');
                    $input.trigger('click');
                });

                $el.find('.date-table .cell').on('click', function (e) {
                    if ($(e.target).parent().parent().hasClass('disabled')) {
                        e.preventDefault();
                        e.stopImmediatePropagation();
                    }
                });
            }
            else $input.popover(method);
        },
        bb_datetimeRange: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-bs-placement') || 'auto';
            var $input = $el.find('.datetime-range-bar');
            if (!method) {
                $input.popover({
                    toggle: 'datetime-range',
                    placement: placement
                })
                    .on('show.bs.popover', function () {
                        var disabled = $(this).parent().hasClass('disabled');
                        return !disabled;
                    })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $body = $pop.find('.popover-body');
                            if ($body.length === 0) {
                                $body = $('<div class="popover-body"></div>').appendTo($pop);
                            }
                            $body.append($el.find('.datetime-range-body').removeClass('d-none'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.datetime-range-body');
                            $el.append($picker.addClass('d-none'));
                        }
                    });

                $el.find('.is-clear').on('click', function () {
                    $input.popover('hide');
                });
            }
            else $input.popover(method);
        }
    });

    $(function () {
        // popover confirm
        $.fn.popover.Constructor.prototype.isWithContent = function () {
            var components = ['', 'confirm', 'datetime-picker', 'datetime-range'];
            var toggle = this._config.toggle;
            return components.indexOf(toggle) || this.getTitle() || this._getContent();
        }

        // add shadow
        var getTipElement = $.fn.popover.Constructor.prototype.getTipElement;
        $.fn.popover.Constructor.prototype.getTipElement = function () {
            var toggle = this._config.toggle;
            var tip = getTipElement.call(this);
            var $tip = $(tip).addClass('shadow');
            if ($tip.find('.popover-header').length > 0) {
                $tip.addClass('has-header');
            }
            if (toggle === 'datetime-picker') {
                $tip.addClass('popover-datetime');
            }
            if (toggle === 'datetime-range') {
                $tip.addClass('popover-datetime-range');
            }
            return tip;
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

            // datetime picker
            if ($el.parents('.popover-datetime.show').length === 0) {
                $('.popover-datetime.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }

            // datetime range
            if ($el.parents('.popover-datetime-range.show').length === 0) {
                $('.popover-datetime-range.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.parents('.datetime-range-bar').attr('aria-describedby') !== pId) $input.popover('hide');
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
