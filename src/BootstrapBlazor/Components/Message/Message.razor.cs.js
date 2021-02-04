(function ($) {
    $.extend({
        bb_message: function (el, obj, method) {
            if (!window.Messages) window.Messages = [];
            Messages.push(el);

            var $el = $(el);
            var autoHide = $el.attr('data-autohide') !== 'false';
            var delay = parseInt($el.attr('data-delay'));
            var autoHideHandler = null;

            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    // auto close
                    autoHideHandler = window.setTimeout(function () {
                        window.clearTimeout(autoHideHandler);
                        $el.close();
                    }, delay);
                }
                $el.addClass('show');
            }, 50);

            $el.close = function () {
                if (autoHideHandler != null) {
                    window.clearTimeout(autoHideHandler);
                }
                $el.removeClass('show');
                var hideHandler = window.setTimeout(function () {
                    window.clearTimeout(hideHandler);

                    // remove Id
                    Messages.remove(el);
                    if (Messages.length === 0) {
                        // call server method prepare remove dom
                        obj.invokeMethodAsync(method);
                    }
                }, 500);
            };

            $el.on('click', '.close', function (e) {
                e.preventDefault();
                e.stopPropagation();

                $el.close();
            });
        }
    });
})(jQuery);
