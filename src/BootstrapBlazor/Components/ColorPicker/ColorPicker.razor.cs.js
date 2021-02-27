(function ($) {
    $.extend({
        bb_color_picker: function (el, obj, method) {
            $(el).colorpicker()
                .on('change', function (e) {
                    obj.invokeMethodAsync(method, e.value);
                });
        }
    });
})(jQuery);
