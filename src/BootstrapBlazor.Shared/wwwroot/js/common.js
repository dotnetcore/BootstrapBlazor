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

        // MVP learn
        $(document)
            .on('click', '.btn-learn', function (e) {
                var $button = $(this);
                var $list = $button.prev();
                $list.slideToggle('fade');
            })
            .on('click', '.btn-close', function (e) {
                var $div = $('.ms-learn');
                $div.fadeOut();
            });

        // Theme
        $(document)
            .on('click', function (e) {
                var $el = $(e.target);
                if ($el.closest('.theme').length == 0) {
                    $('.theme-list.is-open').toggleClass('is-open').slideToggle('fade');
                }
            });

        // scorll
        var prevScrollTop = 0;
        $(document).on('scroll', function () {
            var $header = $('app > header, .coms-search');
            var currentScrollTop = $(document).scrollTop();
            if (currentScrollTop > prevScrollTop) {
                $header.addClass('hide');
            } else {
                $header.removeClass('hide');
            }
            prevScrollTop = currentScrollTop;
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
