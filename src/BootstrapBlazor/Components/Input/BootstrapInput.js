(function ($) {
    $.extend({
        bb_input: function (el, obj, enter, enterCallbackMethod, esc, escCallbackMethod) {
            var $el = $(el);
            $el.on('keyup', function (e) {
                if (enter && e.key === 'Enter') {
                    obj.invokeMethodAsync(enterCallbackMethod);
                }
                else if (esc && e.key === 'Escape') {
                    obj.invokeMethodAsync(escCallbackMethod, $el.val());
                }
            });
        }
    });
})(jQuery);
