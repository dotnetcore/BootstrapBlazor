(function ($) {
    $.blazorCulture = {
        get: () => {
            return window.localStorage['BlazorCulture'];
        },
        set: (value) => {
            window.localStorage['BlazorCulture'] = value;
        }
    };

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
