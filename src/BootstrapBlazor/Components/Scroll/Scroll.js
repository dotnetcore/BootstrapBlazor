(function ($) {
    $.extend({
        bb_scroll: function (el, force) {
            var $el = $(el);

            // 移动端不需要修改滚动条
            // 苹果系统不需要修改滚动条
            var mobile = $(window).width() < 768 || navigator.userAgent.match(/Macintosh/);
            if (force || !mobile) {
                var autoHide = $el.attr('data-hide');
                var height = $el.attr('data-height');
                var width = $el.attr('data-width');

                var option = {
                    alwaysVisible: autoHide !== 'true',
                };

                if (!height) {
                    height = 'auto';
                }
                if (height !== '') {
                    option.height = height;
                }
                if (!width) {
                    option.width = width;
                }
                $el.slimScroll(option);
            }
            else {
                $el.addClass('is-phone');
            }
        }
    });
})(jQuery);
