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
        },
        indexTyper: function (el) {
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

            var text1 = ['最', '好', '用', '的'];
            var text2 = ['最', '好', '看', '的'];
            var text3 = ['最', '简', '单', '实', '用', '的'];

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
                var $bar = $this.find('[data-slide]');
                $bar.removeClass('d-none');
                var hoverHandler = window.setTimeout(function () {
                    window.clearTimeout(hoverHandler);
                    $this.addClass('hover');
                }, 10);
            }, function () {
                var $this = $(this);
                var $bar = $this.find('[data-slide]');
                $this.removeClass('hover');
                leaveHandler = window.setTimeout(function () {
                    window.clearTimeout(leaveHandler);
                    $bar.addClass('d-none');
                }, 300);
            });

            $('.welcome-footer [data-toggle="tooltip"]').tooltip();
        },
        block: function (el) {
            var $el = $(el);
            var id = $.getUID();
            var $footer = $el.children('.card-footer-code');
            var $footerBar = $el.children('.card-footer-control');
            $footer.attr('id', id);
            $footerBar.attr('href', '#' + id);
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
})(jQuery);
