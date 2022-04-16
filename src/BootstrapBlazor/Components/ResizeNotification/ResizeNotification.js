(function ($) {
    $.extend({
        bb_resize_monitor: function (obj, method) {
            var currentBreakpoint = $.bb_get_responsive();
            var onResized = function () {
                var lastBreakpoint = currentBreakpoint;
                currentBreakpoint = $.bb_get_responsive();

                if (lastBreakpoint !== currentBreakpoint) {
                    lastBreakpoint = currentBreakpoint;
                    obj.invokeMethodAsync(method, currentBreakpoint);
                }
            };

            // 调整大小时重新计算断点
            if (window.attachEvent) {
                window.attachEvent('onresize', onResized);
            }
            else if (window.addEventListener) {
                window.addEventListener('resize', onResized, true);
            }
            return currentBreakpoint;
        },
        bb_get_responsive: function () {
            return window.getComputedStyle(document.body, ':before').content.replace(/\"/g, '');
        }
    });
})(jQuery);
