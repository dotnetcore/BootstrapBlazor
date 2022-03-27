(function ($) {
    $(function () {
        $(document)
            .on('click', '.anchor-link', function (e) {
                var $el = $(this);
                var hash = $el.attr('id');
                if (hash) {
                    var title = $el.attr('data-title');
                    var href = window.location.origin + window.location.pathname + '#' + hash;
                    $.bb_copyText(href);
                    $el.tooltip({
                        title: title
                    });
                    $el.tooltip('show');
                    var handler = window.setTimeout(function () {
                        window.clearTimeout(handler);
                        $el.tooltip('dispose');
                    }, 1000);
                }
            });
    });
})(jQuery);
