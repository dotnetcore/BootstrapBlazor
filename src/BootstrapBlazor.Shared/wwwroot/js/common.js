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
        _showToast: function () {
            var $toast = $('.row .toast').toast('show');
            $toast.find('.toast-progress').css({ "width": "100%" });
        },
        highlight: function (el) {
            var $el = $(el);
            $el.find('[data-bs-toggle="tooltip"]').tooltip();
            var code = $el.find('code')[0];
            if (code) {
                hljs.highlightBlock(code);
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
        },
        indexTyper: function (el, text1, text2, text3) {
            var $this = $(el);
            var $cursor = $this.next();

            var typeChar = function (original, reverse) {
                var plant = original.concat();
                return new Promise(function (resovle, reject) {
                    $cursor.addClass('active');
                    var eventHandler = window.setInterval(function () {
                        if (plant.length > 0) {
                            if (!reverse) {
                                var t1 = $this.text() + plant.shift();
                                $this.text(t1);
                            }
                            else {
                                var t1 = plant.pop();
                                $this.text(plant.join(''));
                            }
                        }
                        else {
                            window.clearInterval(eventHandler);
                            $cursor.removeClass('active');

                            var handler = window.setTimeout(function () {
                                window.clearTimeout(handler);
                                if (reverse) {
                                    return resovle();
                                }
                                else {
                                    typeChar(original, true).then(function () {
                                        return resovle();
                                    });
                                }
                            }, 1000);
                        }
                    }, 200);
                });
            };

            var loop = function () {
                var handler = window.setTimeout(function () {
                    window.clearTimeout(handler);
                    typeChar(text1, false).then(function () {
                        typeChar(text2, false).then(function () {
                            typeChar(text3).then(function () {
                                loop();
                            });
                        });
                    });
                }, 200);
            };

            loop();
            $.carouselHome();
        },
        carouselHome: function () {
            var $ele = $('#carouselExampleCaptions');
            var leaveHandler = null;
            $ele.hover(function () {
                if (leaveHandler != null) {
                    window.clearTimeout(leaveHandler);
                }

                var $this = $(this);
                var $bar = $this.find('[data-bs-slide]');
                $bar.removeClass('d-none');
                var hoverHandler = window.setTimeout(function () {
                    window.clearTimeout(hoverHandler);
                    $this.addClass('hover');
                }, 10);
            }, function () {
                var $this = $(this);
                var $bar = $this.find('[data-bs-slide]');
                $this.removeClass('hover');
                leaveHandler = window.setTimeout(function () {
                    window.clearTimeout(leaveHandler);
                    $bar.addClass('d-none');
                }, 300);
            });

            $('.welcome-footer [data-bs-toggle="tooltip"]').tooltip();
        },
        table_wrap: function () {
            var handler = window.setInterval(function () {
                var spans = $('body').find('.table-wrap-header-demo th .table-cell span');
                if (spans.length === 0) {
                    return;
                }

                window.clearInterval(handler);
                spans.each(function () {
                    $(this).tooltip({
                        title: $(this).text()
                    });
                });
            }, 500);
        },
        tooltip: function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        },
        table_test: function (el, obj, method) {
            var $el = $(el);
            $el.on('click', 'tbody tr', function () {
                $el.find('.active').removeClass('active');
                var index = $(this).addClass('active').data('index');

                obj.invokeMethodAsync(method, index);
            });
        },
        initTheme: function (el) {
            var $el = $(el);
            $el.find('[data-bs-toggle="tooltip"]').tooltip();
            $el.on('click', '.btn-theme, .btn-close, .theme-item', function (e) {
                var $theme = $el.find('.theme-list');
                $theme.toggleClass('is-open').slideToggle('fade');
            });
        },
        setTheme: function (css) {
            var $links = $('link');
            var $link = $links.filter(function (index, link) {
                var href = $(link).attr('href');
                return href.indexOf('_content/BootstrapBlazor.Shared/css/site.css') > -1;
            });

            while ($link.next().length === 1) {
                var $item = $link.next();
                $item.remove();
            }

            css.forEach(function (c) {
                $link.after('<link rel="stylesheet" href="' + c + '">');
            });
        },
        bb_open: function (method) {
            if (method === 'dispose') {
                $('#log').popover(method);
            }
            else {
                $('#log').popover({ delay: { 'show': 1000 } }).one('click', function () {
                    $(this).popover('toggle');
                }).trigger('click');
            }
        },
        bb_site_load: function (el, version) {
            $(el).tooltip();

            var width = $(window).width();
            if (width >= 768) {
                // Intro 弹窗
                var key = 'bb_intro_popup:' + version;
                var isShown = localStorage.getItem(key);
                if (!isShown) {
                    var $intro = $('.blazor-intro');
                    $intro.find('.version').text(version);
                    $('.blazor-intro-button').on('click', function () {
                        $intro.slideToggle('fade', function () {
                            localStorage.setItem(key, false);
                        });
                    });
                    $intro.slideToggle('fade');

                    // clean
                    for (var index = localStorage.length; index > 0; index--) {
                        var k = localStorage.key(index - 1);
                        if (k.indexOf('bb_intro_popup:') > -1) {
                            localStorage.removeItem(k);
                        }
                    }
                }
            }
        },
        bb_block: function (el) {
            var $el = $(el);
            var id = $.getUID();
            var $footer = $el.children('.card-footer-code');
            var $footerBar = $el.children('.card-footer-control');
            $footer.attr('id', id);
            $footerBar.attr('href', '#' + id);
        },
        bb_topology_handler(tagName) {
            if (window.bb_topology_demo_invoker !== undefined) {
                window.bb_topology_demo_invoker.toggleFan(tagName);
            }
        },
        bb_topology_demo(obj, method) {
            if (window.bb_topology_demo_invoker === undefined) {
                window.bb_topology_demo_invoker = {};
            }
            window.bb_topology_demo_invoker.toggleFan = function (tagName) {
                obj.invokeMethodAsync(method, tagName);
            }
        },
        bb_topology_demo_setOptions: function () {
            window.topology.setOptions({ hoverColor: '', hoverCursor: '', activeColor: '' });
        }
    });

    $(function () {
        $(document)
            .on('click', '.btn-clipboard', function (e) {
                e.preventDefault();

                var $el = $(this);
                var text = $el.prev().find('code').text();
                $.bb_copyText(text);

                var tId = $el.attr('aria-describedby');
                var $tooltip = $('#' + tId);
                $tooltip.find('.tooltip-inner').html('拷贝代码成功');
            });

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
            }
            else {
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
