(function ($) {
    $.extend({
        bb_drawer: function (el, open) {
            var $el = $(el);
            if (open) {
                $el.addClass('is-open');
                $('body').addClass('overflow-hidden');
            }
            else {
                if ($el.hasClass('is-open')) {
                    $el.removeClass('is-open').addClass('is-close');
                    var handler = window.setTimeout(function () {
                        window.clearTimeout(handler);
                        $el.removeClass('is-close');
                        $('body').removeClass('overflow-hidden');
                    }, 350);
                }
            }
        }
    });
})(jQuery);
