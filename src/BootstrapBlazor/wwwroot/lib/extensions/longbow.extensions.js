(function ($) {
    if (!$.isFunction(Date.prototype.format)) {
        Date.prototype.format = function (format) {
            var o = {
                "M+": this.getMonth() + 1,
                "d+": this.getDate(),
                "h+": this.getHours() % 12 === 0 ? 12 : this.getHours() % 12,
                "H+": this.getHours(),
                "m+": this.getMinutes(),
                "s+": this.getSeconds(),
                "q+": Math.floor((this.getMonth() + 3) / 3),
                "S": this.getMilliseconds()
            };
            var week = {
                0: "日",
                1: "一",
                2: "二",
                3: "三",
                4: "四",
                5: "五",
                6: "六"
            };

            if (/(y+)/.test(format))
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));

            if (/(E+)/.test(format))
                format = format.replace(RegExp.$1, (RegExp.$1.length > 1 ? RegExp.$1.length > 2 ? "星期" : "周" : "") + week[this.getDay()]);

            for (var k in o)
                if (new RegExp("(" + k + ")").test(format))
                    format = format.replace(RegExp.$1, RegExp.$1.length === 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            return format;
        };
    }

    $.browser = {
        versions: function () {
            var u = navigator.userAgent;
            return {         //移动终端浏览器版本信息
                trident: u.indexOf('Trident') > -1, //IE内核
                presto: u.indexOf('Presto') > -1, //opera内核
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') === -1, //火狐内核
                mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器
                iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器
                iPod: u.indexOf('iPod') > -1, //是否为iPod或者QQHD浏览器
                iPad: u.indexOf('iPad') > -1, //是否iPad
                mac: u.indexOf('Macintosh') > -1,
                webApp: u.indexOf('Safari') === -1 //是否web应该程序，没有头部与底部
            };
        }(),
        language: (navigator.browserLanguage || navigator.language).toLowerCase()
    };

    Array.prototype.indexOf = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };

    Array.prototype.remove = function (val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.splice(index, 1);
        }
    };

    $.extend({
        format: function (source, params) {
            if (params === undefined || params === null) {
                return null;
            }
            if (arguments.length > 2 && params.constructor !== Array) {
                params = $.makeArray(arguments).slice(1);
            }
            if (params.constructor !== Array) {
                params = [params];
            }
            $.each(params, function (i, n) {
                source = source.replace(new RegExp("\\{" + i + "\\}", "g"), function () {
                    return n;
                });
            });
            return source;
        },
        getUID: function (prefix) {
            if (!prefix) prefix = 'b';
            do prefix += ~~(Math.random() * 1000000);
            while (document.getElementById(prefix));
            return prefix;
        },
        webClient: function (obj, url, method) {
            var data = {};
            var browser = new Browser();
            data.Browser = browser.browser + ' ' + browser.version;
            data.Os = browser.os + ' ' + browser.osVersion;
            data.Device = browser.device;
            data.Language = browser.language;
            data.Engine = browser.engine;
            data.UserAgent = navigator.userAgent;

            $.ajax({
                type: "GET",
                url: url,
                success: function (result) {
                    obj.invokeMethodAsync(method, result.Id, result.Ip, data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent);
                },
                error: function (xhr, state, errorThrown) {
                    console.error('Please add UseBootstrapBlazor middleware');
                    obj.invokeMethodAsync(method, '', '', data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent);
                }
            });
        },
        bb_vibrate: function () {
            if ('vibrate' in window.navigator) {
                window.navigator.vibrate([200, 100, 200]);
                var handler = window.setTimeout(function () {
                    window.clearTimeout(handler);
                    window.navigator.vibrate([]);
                }, 1000);
            }
        },
        bb_setIndeterminate: function (id, state) {
            document.getElementById(id).indeterminate = state;
        }
    });

    $.fn.extend({
        drag: function (start, move, end) {
            var $this = $(this);

            var handleDragStart = function (e) {
                e.preventDefault();
                e.stopPropagation();

                document.addEventListener('mousemove', handleDragMove);
                document.addEventListener('touchmove', handleDragMove);
                document.addEventListener('mouseup', handleDragEnd);
                document.addEventListener('touchend', handleDragEnd);

                if ($.isFunction(start)) {
                    start.call($this, e);
                }
            };

            var handleDragMove = function (e) {
                if (e.touches && e.touches.length > 1) {
                    return;
                }

                if ($.isFunction(move)) {
                    move.call($this, e);
                }
            };

            var handleDragEnd = function (e) {
                // 结束拖动
                if ($.isFunction(end)) {
                    end.call($this, e);
                }

                window.setTimeout(function () {
                    document.removeEventListener('mousemove', handleDragMove);
                    document.removeEventListener('touchmove', handleDragMove);
                    document.removeEventListener('mouseup', handleDragEnd);
                    document.removeEventListener('touchend', handleDragEnd);
                }, 100);
            };

            $this.on('mousedown', handleDragStart);
            $this.on('touchstart', handleDragStart);
        },
        touchScale: function (cb, options) {
            options = $.extend({ max: null, min: 0.195 }, options);
            var eleImg = this[0];
            var store = {
                scale: 1
            };
            var $this = $(this);

            // 缩放处理
            eleImg.addEventListener('touchstart', function (event) {
                var touches = event.touches;
                var events = touches[0];
                var events2 = touches[1];

                event.preventDefault();

                store.pageX = events.pageX;
                store.pageY = events.pageY;
                store.moveable = true;

                if (events2) {
                    store.pageX2 = events2.pageX;
                    store.pageY2 = events2.pageY;
                }

                store.originScale = store.scale || 1;
            });

            document.addEventListener('touchmove', function (event) {
                if (!store.moveable) {
                    return;
                }

                var touches = event.touches;
                var events = touches[0];
                var events2 = touches[1];

                if (events2) {
                    event.preventDefault();
                    if (!$this.hasClass('transition-none')) {
                        $this.addClass('transition-none');
                    }

                    if (!store.pageX2) {
                        store.pageX2 = events2.pageX;
                    }
                    if (!store.pageY2) {
                        store.pageY2 = events2.pageY;
                    }

                    var getDistance = function (start, stop) {
                        return Math.hypot(stop.x - start.x, stop.y - start.y);
                    };

                    var zoom = getDistance({
                        x: events.pageX,
                        y: events.pageY
                    }, {
                        x: events2.pageX,
                        y: events2.pageY
                    }) /
                        getDistance({
                            x: store.pageX,
                            y: store.pageY
                        }, {
                            x: store.pageX2,
                            y: store.pageY2
                        });

                    var newScale = store.originScale * zoom;

                    if (options.max != null && newScale > options.max) {
                        newScale = options.max;
                    }

                    if (options.min != null && newScale < options.min) {
                        newScale = options.min;
                    }

                    store.scale = newScale;

                    if ($.isFunction(cb)) {
                        cb.call($this, newScale);
                    }
                }
            });

            document.addEventListener('touchend', function () {
                store.moveable = false;
                $this.removeClass('transition-none');

                delete store.pageX2;
                delete store.pageY2;
            });

            document.addEventListener('touchcancel', function () {
                store.moveable = false;
                $this.removeClass('transition-none');

                delete store.pageX2;
                delete store.pageY2;
            });
        }
    });

    var load = function (targetName, callback, interval) {
        if (!interval) {
            interval = 100;
        }
        if (!window[targetName]) {
            var handler = window.setInterval(function () {
                if (!!window[targetName]) {
                    window.clearInterval(handler);

                    callback();
                }
            }, interval);
        }
        else {
            callback();
        }
    };

    var addScript = function (content) {
        // content 文件名
        const links = [...document.getElementsByTagName('script')];
        var link = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('script');
            link.setAttribute('src', content);
            document.body.appendChild(link);
        }
    };

    var removeScript = function (content) {
        const links = [...document.getElementsByTagName('script')];
        var nodes = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.body.removeChild(nodes[index]);
        }
    }

    var addLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var link = links.filter(function (link) {
            return link.href.indexOf(href) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('link');
            link.setAttribute('href', href);
            link.setAttribute("rel", "stylesheet");
            document.getElementsByTagName("head")[0].appendChild(link);
        }
    }

    var removeLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var nodes = links.filter(function (link) {
            return link.href.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.getElementsByTagName("head")[0].removeChild(nodes[index]);
        }
    }

    window.BootstrapBlazorModules = {
        load: load,
        addScript: addScript,
        removeScript: removeScript,
        addLink: addLink,
        removeLink: removeLink
    };
})(jQuery);
