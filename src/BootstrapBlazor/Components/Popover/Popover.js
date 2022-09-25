(function ($) {
    $.extend({
        bb_confirm: function (id) {
            let $ele = $('[data-bs-target="' + id + '"]');
            let showClassName = 'is-show';
            let button = document.getElementById(id);
            let popover = bootstrap.Popover.getOrCreateInstance(button, {
                toggle: 'confirm',
                html: true,
                sanitize: false,
                content: $ele.find('.popover-body').html()
            });
            if (button.classList.contains(showClassName) === false) {
                popover.show();
                button.classList.add(showClassName);
            } else {
                popover.hide();
                button.classList.remove(showClassName);
            }
        },
        bb_confirm_submit: function (id) {
            let $ele = $('#' + id);
            let $submit = $('<button type="submit" hidden />');
            $submit.appendTo($ele.parent());
            $submit.trigger('click');
            $submit.remove();
        },
        bb_popover: function (id, method, title, content, placement, html, trigger, css) {
            let ele = document.getElementById(id);
            let instance = bootstrap.Popover.getInstance(ele);
            if (instance) {
                instance.dispose();
            }
            if (method !== 'dispose') {
                let op = {html, sanitize: false, title, content, placement, trigger};
                if (css !== '') {
                    op.customClass = css;
                }
                instance = new bootstrap.Popover(ele, op);
                if (method !== '') {
                    $(ele).popover(method);
                }
            }
        }
    });

    $(function () {
        let findConfirmButton = function ($el) {
            let button = null;
            let $parent = $el.parents('.popover');
            if ($parent.length > 0) {
                let id = $parent.attr('id');
                button = $('[aria-describedby="' + id + '"]');
            }
            return button;
        };

        let disposePopup = function ($ele) {
            $ele.popover('dispose');
            $ele.removeClass('is-show');
        };

        $(document).on('click', function (e) {
            // hide popover
            let hide = true;
            let $el = $(e.target);

            // 判断是否点击 popover 内部
            let $confirm = findConfirmButton($el);
            if ($confirm != null) hide = false;
            if (hide) {
                let $target = $el;
                if ($target.data('bs-toggle') !== 'confirm') {
                    $target = $target.parents('[data-bs-toggle="confirm"][aria-describedby^="popover"]');
                }
                $('[data-bs-toggle="confirm"][aria-describedby^="popover"]').each(function (index, ele) {
                    if ($target[0] !== ele) {
                        let $ele = $(ele);
                        disposePopup($ele);
                    }
                });
            }

            // multi-select
            if ($el.parents('.popover-multi-select.show').length === 0) {
                $('.popover-multi-select.show').each(function (index, ele) {
                    let pId = this.getAttribute('id');
                    if (pId) {
                        let $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.parents('.dropdown-toggle').attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }
        });

        $(document).on('click', '.popover-confirm-buttons .btn', function (e) {
            e.stopPropagation();

            // 确认弹窗按钮事件
            let $confirm = findConfirmButton($(this));
            if ($confirm.length > 0) {
                // 关闭弹窗
                disposePopup($confirm);

                // remove popover
                let buttonId = $confirm.attr('id');
                $ele = $('[data-bs-target="' + buttonId + '"]');

                let $button = this.getAttribute('data-dismiss') === 'confirm'
                    ? $ele.find('.popover-confirm-buttons .btn:first')
                    : $ele.find('.popover-confirm-buttons .btn:last');
                $button.trigger('click');
            }
        });
    });
})(jQuery);
