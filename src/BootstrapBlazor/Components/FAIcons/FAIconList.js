(function ($) {
    $.extend({
        bb_iconList: function (el) {
            var $el = $(el);
            $('body').scrollspy({ offset: 150, target: '.fa-nav' });
            $el.on('click', '.nav-link', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var targetId = $(this).attr('href');
                var $target = $(targetId);
                var container = window;
                var margin = $target.offset().top;
                var marginTop = $target.css('marginTop').replace('px', '');
                if (marginTop) {
                    margin = margin - parseInt(marginTop);
                }
                $(container).scrollTop(margin);
            })
        }
    });
})(jQuery);
