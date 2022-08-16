(function ($) {
    $.extend({
        bb_autoScrollItem: function (el, index) {
            var $el = $(el);
            var $menu = $el.find('.dropdown-menu');
            var maxHeight = parseInt($menu.css('max-height').replace('px', '')) / 2;
            var itemHeight = $menu.children('li:first').outerHeight();
            var height = itemHeight * index;
            var count = Math.floor(maxHeight / itemHeight);

            $menu.children().removeClass('active');
            var len = $menu.children().length;
            if (index < len) {
                $menu.children()[index].classList.add('active');
            }

            if (height > maxHeight) {
                $menu.scrollTop(itemHeight * (index > count ? index - count : index));
            }
            else if (index <= count) {
                $menu.scrollTop(0);
            }
        },
        bb_setDebounce: function (el, waitMs) {
            // ReaZhuang贡献
            var $el = $(el);
            let timer;
            var allowKeys = ['ArrowUp', 'ArrowDown', 'Escape', 'Enter'];

            $el.on('keyup', function (event) {
                if (allowKeys.indexOf(event.key) < 1 && timer) {
                    // 清空计时器的方法
                    clearTimeout(timer);
                    // 阻止事件冒泡，使之不能进入到c#
                    event.stopPropagation();

                    // 创建一个计时器，开始倒计时，倒计时结束后执行内部的方法
                    timer = setTimeout(function () {
                        // 清除计时器，使下次事件不能进入到if中
                        timer = null;
                        // 手动激发冒泡事件
                        event.target.dispatchEvent(event.originalEvent);
                    }, waitMs);
                } else {
                    // 创建一个空的计时器，在倒计时期间内，接收的事件将全部进入到if中
                    timer = setTimeout(function () { }, waitMs);
                }
            });
        }
    });
})(jQuery);
