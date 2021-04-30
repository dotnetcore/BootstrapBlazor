(function ($) {
    $.extend({
        bb_slider: function (el, slider, method) {
            var $slider = $(el);

            var isDisabled = $slider.find('.disabled').length > 0;
            if (!isDisabled) {
                var originX = 0;
                var curVal = 0;
                var newVal = 0;
                var slider_width = $slider.innerWidth();
                $slider.find('.slider-button-wrapper').drag(
                    function (e) {
                        originX = e.clientX || e.touches[0].clientX;
                        curVal = parseInt($slider.attr('aria-valuetext'));
                        $slider.find('.slider-button-wrapper, .slider-button').addClass('dragging');
                    },
                    function (e) {
                        var eventX = e.clientX || e.changedTouches[0].clientX;

                        newVal = Math.ceil((eventX - originX) * 100 / slider_width) + curVal;

                        if (newVal <= 0) newVal = 0;
                        if (newVal >= 100) newVal = 100;

                        $slider.find('.slider-bar').css({ "width": newVal.toString() + "%" });
                        $slider.find('.slider-button-wrapper').css({ "left": newVal.toString() + "%" });
                        $slider.attr('aria-valuetext', newVal.toString());

                        slider.invokeMethodAsync(method, newVal);
                    },
                    function (e) {
                        $slider.find('.slider-button-wrapper, .slider-button').removeClass('dragging');

                        slider.invokeMethodAsync(method, newVal);
                    });
            }
        }
    });
})(jQuery);
