(function ($) {
    $.extend({
        bb_select: function (el) {
            var $el = $(el);
            var $search = $el.find('input.search-text');
            if ($search.length > 0) {
                $el.on('shown.bs.dropdown', function () {
                    $search.focus();
                });
            }
        }
    });
})(jQuery);
