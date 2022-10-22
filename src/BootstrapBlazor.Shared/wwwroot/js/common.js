(function ($) {
    $.blazorCulture = {
        get: () => {
            return window.localStorage['BlazorCulture'];
        },
        set: (value) => {
            window.localStorage['BlazorCulture'] = value;
        }
    };

    $.extend({
        loading: function (wasm, error, reload) {
            if (wasm) {
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

            $('.reload').text(reload);
            $('#blazor-error-ui > span:first').text(error);
        }
    });

    $(function () {
        // chart animation
        $(document)
            .on('click', '[data-method]', function (e) {
                var $this = $(this);
                var method = $this.attr('data-method');

                var $btnGroup = $this.closest('.text-center').next().find('.btn');
                switch (method) {
                    case 'play':
                        $btnGroup.prop('disabled', 'disabled');
                        break;
                    case 'stop':
                        $btnGroup.removeAttr('disabled');
                        break;
                }
            });
    });

    $(function () {
        //new MutationObserver((mutations, observer) => {
        //    if (document.querySelector('#components-reconnect-modal h5 a')) {
        //        function attemptReload() {
        //            fetch('').then(() => {
        //                location.reload();
        //            });
        //        }
        //        observer.disconnect();
        //        attemptReload();
        //        setInterval(attemptReload, 10000);
        //    }
        //}).observe(document.body, { childList: true, subtree: true });
    });
})(jQuery);
