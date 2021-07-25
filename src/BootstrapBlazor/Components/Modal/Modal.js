(function ($) {
    $.extend({
        bb_modal_dialog: function (el, obj, method) {
            var $el = $(el);
            $el.data('bb_dotnet_invoker', { obj, method });
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

                    // monitor mousedown ready to drag dialog
                    var originX = 0;
                    var originY = 0;
                    var dialogWidth = 0;
                    var dialogHeight = 0;
                    var pt = { top: 0, left: 0 };
                    var $dialog = null;
                    $el.find('.is-draggable .modal-header').drag(
                        function (e) {
                            originX = e.clientX || e.touches[0].clientX;
                            originY = e.clientY || e.touches[0].clientY;

                            // 弹窗大小
                            $dialog = this.closest('.modal-dialog');
                            dialogWidth = $dialog.width();
                            dialogHeight = $dialog.height();

                            // 偏移量
                            pt.top = parseInt($dialog.css('marginTop').replace("px", ""));
                            pt.left = parseInt($dialog.css('marginLeft').replace("px", ""));

                            // 移除 Center 样式
                            $dialog.css({ "marginLeft": pt.left, "marginTop": pt.top });
                            $dialog.removeClass('modal-dialog-centered');

                            // 固定大小
                            $dialog.css("width", dialogWidth);
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
                                if ($dialog != null) {
                                    $dialog.css({ "marginLeft": newValX });
                                }
                            }
                            if (newValY + dialogHeight < $(window).height()) {
                                if ($dialog != null) {
                                    $dialog.css({ "marginTop": newValY });
                                }
                            }
                        },
                        function (e) {
                            this.removeClass('is-drag');
                        }
                    );
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
