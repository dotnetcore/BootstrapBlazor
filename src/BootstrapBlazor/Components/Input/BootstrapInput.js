(function ($) {
    $.extend({
        bb_input: function (el, obj, enter, enterCallbackMethod, esc, escCallbackMethod) {
            var $el = $(el);
            $el.on('keyup', function (e) {
                if (enter && e.key === 'Enter') {
                    obj.invokeMethodAsync(enterCallbackMethod, $el.val());
                }
                else if (esc && e.key === 'Escape') {
                    obj.invokeMethodAsync(escCallbackMethod);
                }
            });
        },
        bb_input_selectAll_focus: function (el) {
            var $el = $(el);
            $el.on('focus', function () {
                $(this).select();
            });
        },
        bb_input_selectAll_enter: function (el) {
            var $el = $(el);
            $el.on('keyup', function (e) {
                if (e.key === 'Enter') {
                    $(this).select();
                }
            });
        },
        bb_input_selectAll: function (el) {
            $(el).select();
        }
    });
})(jQuery);
