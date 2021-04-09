(function ($) {
    $.extend({
        bb_form_load: function (el, method) {
            var $el = $(el);
            if (method === 'show')
                $el.addClass('show');
            else
                $el.removeClass('show');
        }
    });
})(jQuery);
