(function ($) {
    $.extend({
        bb_transition: function (el, obj, method) {
            $(el).on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oAnimationEnd', function () {
                obj.invokeMethodAsync(method);
            });
        },
    });
})(jQuery);
