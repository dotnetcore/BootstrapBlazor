(function ($) {
    $.extend({
        _showToast: function () {
            var $toast = $('.row .toast').toast('show');
            $toast.find('.toast-progress').css({ "width": "100%" });
        },
        highlight: function (code) {
            hljs.highlightBlock(code);
            $el = $(code).parent().parent().find('[data-toggle="tooltip"]').tooltip();
        },
        copyText: function (ele) {
            if (typeof ele !== "string") return false;
            var input = document.createElement('input');
            input.setAttribute('type', 'text');
            input.setAttribute('value', ele);
            document.body.appendChild(input);
            input.select();
            var ret = document.execCommand('copy');
            document.body.removeChild(input);
            return ret;
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
        }
    });

    $(function () {
        $(document)
            .on('click', '.card-footer-control', function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.toggleClass('show');
                $this.prev().toggle('show');

                // 更改自身状态
                var text = $this.hasClass('show') ? "隐藏代码" : "显示代码";
                $this.find('span').text(text);
            })
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
