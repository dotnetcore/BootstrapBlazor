(function ($) {
    window.Toasts = [];
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
                    className: 'os-theme-light',
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
        },
        fadeShow: function (delay) {
            var $ele = this;
            if ($ele.length > 0) {
                if (delay === undefined) delay = 50;
                $ele.addClass("d-block");
                var handler = window.setTimeout(function () {
                    if (handler != null) window.clearTimeout(handler);
                    $ele.addClass("show");
                }, delay);
            }
        },
        fadeHide: function (delay, callback) {
            var $ele = this;
            if ($ele.length > 0) {
                if (delay === undefined) delay = 150;
                if ($.isFunction(delay)) callback = delay;
                $ele.removeClass("show");
                var handler = window.setTimeout(function () {
                    if (handler != null) window.clearTimeout(handler);
                    $ele.removeClass("d-block");
                    if ($.isFunction(callback)) callback.call(this);
                }, delay);
            }
        },
    });

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
        run: function (code) {
            eval(code);
        },
        showToast: function (id, toast, method) {
            // 记录 Id
            Toasts.push(id);

            // 动画弹出
            var $toast = $('#' + id);

            // check autohide
            var autoHide = $toast.attr('data-autohide') !== 'false';
            var delay = parseInt($toast.attr('data-delay'));

            $toast.addClass('d-block');
            var autoHideHandler = null;
            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    $toast.find('.toast-progress').css({ 'width': '100%' });

                    // auto close
                    autoHideHandler = window.setTimeout(function () {
                        window.clearTimeout(autoHideHandler);
                        $toast.find('.close').trigger('click');
                    }, delay);
                }
                $toast.addClass('show');
            }, 50);

            // handler close
            $toast.on('click', '.close', function (e) {
                e.preventDefault();
                e.stopPropagation();

                if (autoHideHandler != null) {
                    window.clearTimeout(autoHideHandler);
                }
                $toast.removeClass('show');
                var hideHandler = window.setTimeout(function () {
                    window.clearTimeout(hideHandler);
                    $toast.removeClass('d-block');

                    // remove Id
                    Toasts.remove($toast.attr('id'));
                    if (Toasts.length === 0) {
                        // call server method prepare remove dom
                        toast.invokeMethodAsync(method);
                    }
                }, 500);
            });
        },
        carousel(id) {
            var $ele = $('#' + id).carousel();

            // focus event
            var leaveHandler = null;
            $ele.hover(function () {
                if (leaveHandler != null) window.clearTimeout(leaveHandler);

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
        },
        slider: function (id, slider, method) {
            var $slider = $('#' + id);
            var isMouseDown = false;
            var originX = 0;
            var curVal = 0;
            var newVal = 0;
            var slider_width = $slider.innerWidth();
            var isDisabled = $slider.find('.disabled').length > 0;

            if (!isDisabled) {
                //var $button = $slider.find('.slider-button-wrapper').tooltip({ trigger: 'focus hover' });
                //var $tooltip = null;

                var handleDragStart = function (e) {
                    // 开始拖动
                    isMouseDown = true;

                    originX = e.clientX || e.touches[0].clientX;
                    curVal = parseInt($slider.attr('aria-valuetext'));
                    $slider.find('.slider-button-wrapper, .slider-button').addClass('dragging');
                    //$tooltip = $('#' + $button.attr('aria-describedby'));
                };

                var handleDragMove = function (e) {
                    if (!isMouseDown) return false;

                    var eventX = e.clientX || e.changedTouches[0].clientX;
                    if (eventX === originX) return false;

                    newVal = Math.ceil((eventX - originX) * 100 / slider_width) + curVal;

                    // tooltip
                    //var tooltipLeft = eventX - originX + 8;
                    //if (val >= 0 && val <= 100)
                    //    $tooltip.css({ 'left': tooltipLeft.toString() + 'px' });

                    if (newVal <= 0) newVal = 0;
                    if (newVal >= 100) newVal = 100;

                    $slider.find('.slider-bar').css({ "width": newVal.toString() + "%" });
                    $slider.find('.slider-button-wrapper').css({ "left": newVal.toString() + "%" });
                    $slider.attr('aria-valuetext', newVal.toString());

                    slider.invokeMethodAsync(method, newVal);
                };

                var handleDragEnd = function (e) {
                    if (!isMouseDown) return false;
                    isMouseDown = false;

                    // 结束拖动
                    $slider.find('.slider-button-wrapper, .slider-button').removeClass('dragging');

                    slider.invokeMethodAsync(method, newVal);
                };

                $slider.on('mousedown', '.slider-button-wrapper', handleDragStart);
                $slider.on('touchstart', '.slider-button-wrapper', handleDragStart);

                document.addEventListener('mousemove', handleDragMove);
                document.addEventListener('touchmove', handleDragMove);
                document.addEventListener('mouseup', handleDragEnd);
                document.addEventListener('touchend', handleDragEnd);

                document.addEventListener('mousedown', function () { return false; });
                document.addEventListener('touchstart', function () { return false; });
                document.addEventListener('swipe', function () { return false; });
            }
        },
        removeTab: function (tabId) {
            // 通过当前 Tab 返回如果移除后新的 TabId
            var activeTabId = $('#navBar').find('.active').first().attr('id');
            var $curTab = $('#' + tabId);
            if ($curTab.hasClass('active')) {
                var $nextTab = $curTab.next();
                var $prevTab = $curTab.prev();
                if ($nextTab.length === 1) activeTabId = $nextTab.attr('id');
                else if ($prevTab.length === 1) activeTabId = $prevTab.attr('id');
                else activeTabId = "";
            }
            return activeTabId;
        },
        log: function (msg) {
            console.log(msg);
        },
        resetTab: function (tabId) {
            // 通过计算 Tab 宽度控制滚动条显示完整 Tab
            var $tab = $('#' + tabId);
            if ($tab.length === 0) return;

            var $navBar = $('#navBar');
            var $first = $navBar.children().first();
            var marginLeft = $tab.position().left - $first.position().left;
            var scrollLeft = $navBar.scrollLeft();
            if (marginLeft < scrollLeft) {
                // overflow left
                $navBar.scrollLeft(marginLeft);
                return;
            }

            var marginRight = $tab.position().left + $tab.outerWidth() - $navBar.outerWidth();
            if (marginRight < 0) return;
            $navBar.scrollLeft(marginRight - $first.position().left);
        },
        movePrevTab: function () {
            var $navBar = $('#navBar');
            var $curTab = $navBar.find('.active').first();
            return $curTab.prev().attr('url');
        },
        moveNextTab: function () {
            var $navBar = $('#navBar');
            var $curTab = $navBar.find('.active').first();
            return $curTab.next().attr('url');
        },
        tooltip: function (id, method, title, content, html) {
            var $ele = $('#' + id);
            if (method === "") {
                var op = { html: html, sanitize: !html, title: title };
                $ele.tooltip(op);
            }
            else if (method === 'enable') {
                var op = { html: html, sanitize: !html, title: title };
                $ele.tooltip(op);
                var $ctl = $ele.parents('form').find('.invalid:first');
                if ($ctl.prop("nodeName") === 'INPUT') {
                    $ctl.focus();
                }
            }
            else {
                $ele.tooltip(method);
            }
        },
        popover: function (id, method, title, content, html) {
            var $ele = $('#' + id);
            if (method === "") {
                var op = { html: html, sanitize: false, title: title, content: content };
                $ele.popover(op);
            }
            else {
                $ele.popover(method);
            }
        },
        calcPosition: function ($ele, $button) {
            // 获得组件大小
            var elWidth = $ele.outerWidth();
            var elHeight = $ele.outerHeight();

            // 获得 button 大小
            var width = $button.outerWidth();
            var height = $button.outerHeight();

            var iHeight = $button.height();

            // check top or bottom
            var placement = $button.attr('data-placement');

            // 设置自己位置
            var left = 0;
            var top = 0;

            // 根据自身位置自动判断出现位置
            var x = $button.offset().left;
            var y = $button.offset().top;
            var margin = y - $(window).scrollTop() - elHeight;

            if (margin < 0) {
                // top 不可用
                placement = 'bottom';
            }
            else {
                // 判断左右侧是否位置够用
                var marginRight = $(window).width() - x - elWidth > 0;
                var marginLeft = x - elWidth > 0;
                if (!marginLeft && marginRight) {
                    // 右侧空间满足
                    placement = "right";
                }
                else if (marginLeft && !marginRight) {
                    // 左侧空间不足
                    placement = 'left';
                }
                else if (!marginLeft && !marginRight) {
                    // 左右两侧空间都不够
                    placement = 'bottom';
                }
            }

            $ele.removeClass('top bottom left right').addClass(placement);

            if (placement === 'top') {
                left = x - Math.ceil((elWidth - width) / 2);
                top = y - elHeight;
            }
            else if (placement === 'bottom') {
                left = x - Math.ceil((elWidth - width) / 2);
                top = y + height;
            }
            else if (placement === 'left') {
                left = x - elWidth - 8;
                top = y - Math.ceil((elHeight - height) / 2);
            }
            else if (placement === 'right') {
                left = x + width + 8;
                top = y - Math.ceil((elHeight - height) / 2);
            }

            return { left, top };
        },
        confirm: function (id, el, method) {
            var $ele = $('[data-target="' + id + '"]');
            var $button = $('#' + id);

            var inited = $('.popover-confirm-container').hasClass('is-init');
            if (!inited) {
                $(document).on('click', function (e) {
                    var $this = $(e.target);
                    var self = $this.attr('data-toggle') === "popover" || $this.parents('[data-toggle="popover"]').length > 0;
                    if (self) {
                        return;
                    }

                    $ele.fadeHide(function () {
                        el.invokeMethodAsync(method);
                    });
                });

                $('.popover-confirm-container').on('click', 'popover-confirm-buttons .btn', function (e) {
                    e.preventDefault();

                    $ele.fadeHide(function () {
                        el.invokeMethodAsync(method);
                    });
                });
                $('.popover-confirm-container').addClass('is-init');
            }

            var offset = $.calcPosition($ele, $button);
            $ele.css({ "left": offset.left.toString() + "px", "top": offset.top.toString() + "px" });
            $ele.attr('aria-hidden', true);

            // 开启动画效果
            $ele.fadeShow();
        },
        fixTableHeader: function (el) {
            var $ele = $(el);
            var $thead = $ele.find('thead');
            $ele.on('scroll', function () {
                var top = $ele.scrollTop();
                $thead.css({ 'transform': 'translateY(' + top + 'px)' });
            });
        }
    });

    $(function () {
        $(document)
            .on('hidden.bs.toast', '.toast', function () {
                $(this).removeClass('hide');
            })
            .on('inserted.bs.tooltip', '.is-invalid', function () {
                $('#' + $(this).attr('aria-describedby')).addClass('is-invalid');
            });
    });
})(jQuery);
