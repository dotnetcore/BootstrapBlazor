(function ($) {
    $.extend({
        bb_scrollelement: function (el, value) {
            var $parent = $(el);
            var parentheight = $parent.height();
            var activeitem = 32.57;// $parent.find('.active').height(); //应该是32.57 存疑怎么查找, 里面一层是21.57
            console.log('bb_scrollelement', value, "el.scrollTop", el.scrollTop, "scrollTop", activeitem * value);
            if (activeitem * value > parentheight / 2) {
                $parent.scrollTop(activeitem * value);
            } else if (value <= 4) {
                $parent.scrollTop(0);
            }
        }
    });
})(jQuery);
