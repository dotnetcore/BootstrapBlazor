(function ($) {
    $.extend({
        bb_auto_redirect: function (obj, interval, method) {
            var mousePosition = {};
            var count = 1;
            var traceMouseOrKey = window.setInterval(function () {
                $(document).off('mousemove').one('mousemove', function (e) {
                    if (mousePosition.screenX !== e.screenX || mousePosition.screenY !== e.screenY) {
                        mousePosition.screenX = e.screenX;
                        mousePosition.screenY = e.screenY;
                        count = 1;
                    }
                });
                $(document).off('keydown').one('keydown', function () {
                    count = 1;
                })
            }, 1000);
            var lockHandler = window.setInterval(function () {
                if (count++ > interval) {
                    window.clearInterval(lockHandler);
                    window.clearInterval(traceMouseOrKey);
                    obj.invokeMethodAsync(method);
                }
            }, 1000);
        }
    });
})(jQuery);
