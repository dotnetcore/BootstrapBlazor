(function ($) {
    $.extend({
        bb_autoScrollItem: function (el, index) {
            var $el = $(el);
            var $menu = $el.find('.dropdown-list');
            var maxHeight = parseInt($menu.css('max-height').replace('px', '')) / 2;
            var itemHeight = $menu.children('li:first').outerHeight();
            var height = itemHeight * index;
            var count = Math.floor(maxHeight / itemHeight);
            if (height > maxHeight) {
                $menu.scrollTop(itemHeight * (index > count ? index - count : index));
            }
            else if (index <= count) {
                $menu.scrollTop(0);
            }
        }
    });
})(jQuery);
