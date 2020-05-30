(function ($) {
    $.extend({
        _showToast: function () {
            var $toast = $('.row .toast').toast('show');
            $toast.find('.toast-progress').css({ "width": "100%" });
        },
        highlight: function (el) {
            var $el = $(el);
            $el.find('[data-toggle="tooltip"]').tooltip();
            hljs.highlightBlock($el.find('code')[0]);
        },
        copyText: function (ele) {
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
        },
        _initChart: function (el, obj, method) {
            var showToast = false;
            var handler = null;
            $(document).on('chart.afterInit', '.chart', function () {
                showToast = $(this).height() < 200;
                if (handler != null) window.clearTimeout(handler);
                if (showToast) {
                    handler = window.setTimeout(function () {
                        if (showToast) {
                            obj.invokeMethodAsync(method);
                        }
                    }, 1000);
                }
            });
        },
        loading: function () {
            var $loader = $("#loading");
            if ($loader.length > 0) {
                $loader.addClass("is-done");
                var handler = window.setTimeout(function () {
                    window.clearTimeout(handler);
                    $loader.remove();
                    $('body').removeClass('overflow-hidden');
                }, 600);
            }
        }
    });

    $(function () {
        $(document)
            .on('click', '.copy-code', function (e) {
                e.preventDefault();

                var $el = $(this);
                var text = $el.prev().find('code').text();
                $.copyText(text);

                var tId = $el.attr('aria-describedby');
                var $tooltip = $('#' + tId);
                $tooltip.find('.tooltip-inner').html('拷贝代码成功');
            });
    });
})(jQuery);
