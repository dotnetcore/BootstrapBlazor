(function ($) {
    $.extend({
        bb_confirm: function (id) {
            var $ele = $('[data-target="' + id + '"]');
            var $button = $('#' + id);

            $button.popover({
                toggle: 'confirm',
                html: true,
                sanitize: false,
                content: $ele.find('.popover-body').html()
            });
            $button.popover('show');
        }
    });
})(jQuery);
