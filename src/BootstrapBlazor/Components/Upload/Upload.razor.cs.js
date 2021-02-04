(function ($) {
    $.bb_upload = function (el) {
        var $el = $(el);
        $el.on('click', '.btn-browser', function (e) {
            var $this = $(this);
            var $file = $el.find(':file');
            $file.trigger('click');
        });
    };
})(jQuery);
