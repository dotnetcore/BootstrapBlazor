(function ($) {
    $.extend({
        bb_modal_dialog: function (el, obj, method) {
            var $el = $(el);
            $el.data('bb_dotnet_invoker', { obj, method });

            // monitor mousedown ready to drag dialog
            var originX = 0;
            var originY = 0;
            var dialogWidth = 0;
            var dialogHeight = 0;
            var pt = { top: 0, left: 0 };
            if ($el.hasClass('is-draggable')) {
                $el.find('.btn-maximize').click(function () {
                    $button = $(this);
                    var status = $button.attr('aria-label');
                    if (status === "maximize") {
                        $el.css({
                            "marginLeft": "auto",
                            "width": $el.width(),
                        });
                    }
                    else {
                        var handler = window.setInterval(function () {
                            if ($el.attr('style')) {
                                $el.removeAttr('style');
                            }
                            else {
                                window.clearInterval(handler);
                            }
                        }, 100);
                    }
                });
                $el.css({
                    "marginLeft": "auto"
                });
                $el.find('.modal-header').drag(
                    function (e) {
                        originX = e.clientX || e.touches[0].clientX;
                        originY = e.clientY || e.touches[0].clientY;

                        // 弹窗大小
                        dialogWidth = $el.width();
                        dialogHeight = $el.height();

                        // 偏移量
                        pt.top = parseInt($el.css('marginTop').replace("px", ""));
                        pt.left = parseInt($el.css('marginLeft').replace("px", ""));

                        $el.css({ "marginLeft": pt.left, "marginTop": pt.top });

                        // 固定大小
                        $el.css("width", dialogWidth);
                        this.addClass('is-drag');
                    },
                    function (e) {
                        var eventX = e.clientX || e.changedTouches[0].clientX;
                        var eventY = e.clientY || e.changedTouches[0].clientY;

                        newValX = pt.left + Math.ceil(eventX - originX);
                        newValY = pt.top + Math.ceil(eventY - originY);

                        if (newValX <= 0) newValX = 0;
                        if (newValY <= 0) newValY = 0;

                        if (newValX + dialogWidth < $(window).width()) {
                            $el.css({ "marginLeft": newValX });
                        }
                        if (newValY + dialogHeight < $(window).height()) {
                            $el.css({ "marginTop": newValY });
                        }
                    },
                    function (e) {
                        this.removeClass('is-drag');
                    }
                );
            }
        },
        bb_modal: function (el, method) {
            var $el = $(el);

            if (method === 'dispose') {
                $el.remove();
            }
            else if (method === 'init') {
                if ($el.closest('.swal').length === 0) {
                    // move self end of the body
                    $('body').append($el);
                }
                $el.on('shown.bs.modal', function () {
                    var keyboard = $el.attr('data-bs-keyboard') === "true";
                    if (keyboard === true) {
                        $(document).one('keyup', function (e) {
                            if (e.key === 'Escape') {
                                var $dialog = $el.find('.modal-dialog');
                                var invoker = $dialog.data('bb_dotnet_invoker');
                                if (invoker != null) {
                                    invoker.obj.invokeMethodAsync(invoker.method);
                                }
                            }
                        });
                    }
                });
            }
            else {
                if (method !== 'hide' && method !== 'dispose') {
                    var keyboard = $el.attr('data-bs-keyboard') === "true";
                    var instance = bootstrap.Modal.getInstance(el);
                    if (instance != null) {
                        instance._config.keyboard = keyboard;
                    }
                }
                $el.modal(method);
            }
        }
    });
})(jQuery);
