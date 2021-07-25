(function ($) {
    $.extend({
        bb_toast_close: function (el) {
            var $el = $(el);
            $el.find('.btn-close').trigger('click');
        },
        bb_toast: function (el, toast, method) {
            if (window.Toasts === undefined) window.Toasts = [];

            // 记录 Id
            Toasts.push(el);

            // 动画弹出
            var $toast = $(el);

            // check autohide
            var autoHide = $toast.attr('data-bs-autohide') !== 'false';
            var delay = parseInt($toast.attr('data-bs-delay'));

            $toast.addClass('d-block');
            var autoHideHandler = null;
            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    $toast.find('.toast-progress').css({ 'width': '100%', 'transition': 'width ' + delay / 1000 + 's linear' });

                    // auto close
                    autoHideHandler = window.setTimeout(function () {
                        window.clearTimeout(autoHideHandler);
                        $toast.find('.btn-close').trigger('click');
                    }, delay);
                }
                $toast.addClass('show');
            }, 50);

            // handler close
            $toast.on('click', '.btn-close', function (e) {
                e.preventDefault();
                e.stopPropagation();

                if (autoHideHandler != null) {
                    window.clearTimeout(autoHideHandler);
                }
                $toast.removeClass('show');
                var hideHandler = window.setTimeout(function () {
                    window.clearTimeout(hideHandler);
                    $toast.removeClass('d-block');

                    // remove Id
                    Toasts.remove($toast[0]);
                    if (Toasts.length === 0) {
                        // call server method prepare remove dom
                        toast.invokeMethodAsync(method);
                    }
                }, 500);
            });
        }
    });
})(jQuery);
