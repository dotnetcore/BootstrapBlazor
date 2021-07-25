(function ($) {
    $.extend({
        bb_anchor: function (el) {
            var $el = $(el);
            $el.on('click', function (e) {
                e.preventDefault();
                var $target = $($el.data('target'));
                var container = $el.data('container');
                if (!container) {
                    container = window;
                }
                var margin = $target.offset().top;
                var marginTop = $target.css('marginTop').replace('px', '');
                if (marginTop) {
                    margin = margin - parseInt(marginTop);
                }
                var offset = $el.data('offset');
                if (offset) {
                    margin = margin - parseInt(offset);
                }
                $(container).scrollTop(margin);
            });
        }
    });
})(jQuery);
