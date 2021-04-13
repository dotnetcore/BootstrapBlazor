(function ($) {
    $.extend({
        bb_timePicker: function (el) {
            var $el = $(el);
            return $el.find('.time-spinner-item').height();
        },
        bb_timecell: function (el, obj, up, down) {
            var $el = $(el);
            $el.find('.time-spinner-list').on('mousewheel wheel', function (e) {
                var margin = e.originalEvent.wheelDeltaY || -e.originalEvent.deltaY;
                if (margin > 0) {
                    obj.invokeMethodAsync(up);
                }
                else {
                    obj.invokeMethodAsync(down);
                }
                return false;
            });
        }
    });
})(jQuery);
