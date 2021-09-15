(function ($) {
    $.extend({
        bb_copyText: function (ele) {
            if (navigator.clipboard) {
                navigator.clipboard.writeText(ele);
            }
            else {
                if (typeof ele !== "string") return false;
                var input = document.createElement('input');
                input.setAttribute('type', 'text');
                input.setAttribute('value', ele);
                document.body.appendChild(input);
                input.select();
                document.execCommand('copy');
                document.body.removeChild(input);
            }
        }
    });

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
