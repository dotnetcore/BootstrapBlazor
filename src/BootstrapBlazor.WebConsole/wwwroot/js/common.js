(function ($) {
    $.fn.extend({
        autoScrollSidebar: function (options) {
            var option = $.extend({ target: null, offsetTop: 0 }, options);
            var $navItem = option.target;
            if ($navItem === null || $navItem.length === 0) return this;

            // sidebar scroll animate
            var middle = this.outerHeight() / 2;
            var top = $navItem.offset().top + option.offsetTop - this.offset().top;
            var $scrollInstance = this[0]["__overlayScrollbars__"];
            if (top > middle) {
                if ($scrollInstance) $scrollInstance.scroll({ x: 0, y: top - middle }, 500, "swing");
                else this.animate({ scrollTop: top - middle });
            }
            return this;
        },
        addNiceScroll: function () {
            if ($(window).width() > 768) {
                this.overlayScrollbars({
                    className: 'os-theme-dark',
                    scrollbars: {
                        autoHide: 'leave',
                        autoHideDelay: 100
                    },
                    overflowBehavior: {
                        x: "hidden",
                        y: "scroll"
                    }
                });
            }
            else {
                this.css('overflow', 'auto');
            }
            return this;
        }
    });

    $.extend({
        addNiceScroll: function (element) {
            $(element).addNiceScroll();
        },
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
        getEChartOption: function (option) {
            var op = {
                title: {
                    text: '',
                    subtext: ''
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['日访问量']
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataZoom: { show: true },
                        dataView: { show: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                dataZoom: {
                    show: true,
                    realtime: true,
                    start: 0,
                    end: 100
                },
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        data: function () {
                            var list = [];
                            for (var i = 1; i <= 30; i++) {
                                list.push('2013-03-' + i);
                            }
                            return list;
                        }()
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: [
                    {
                        name: '日访问量',
                        type: 'line',
                        data: function () {
                            var list = [];
                            for (var i = 1; i <= 30; i++) {
                                list.push(Math.round(Math.random() * 30));
                            }
                            return list;
                        }()
                    }
                ]
            };
            return $.extend(true, {}, op, {
                legend: { data: option.legend },
                series: [
                    {
                        name: option.name,
                        data: option.data.map(function (val, index) {
                            return val.Value;
                        })
                    }
                ],
                xAxis: [
                    {
                        data: option.data.map(function (val, index) {
                            return val.Key;
                        })
                    }
                ],
                dataZoom: {
                    start: 0
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
