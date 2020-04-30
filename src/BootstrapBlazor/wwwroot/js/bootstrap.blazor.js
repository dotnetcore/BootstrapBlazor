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
        }
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
        slider: function (el, slider, method) {
            var $slider = $(el);
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
                    e.stopPropagation();
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
            if (placement === 'auto') placement = 'top';

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
        confirm: function (id) {
            var $ele = $('[data-target="' + id + '"]');
            var $button = $('#' + id);

            $button.popover({
                toggle: 'confirm',
                html: true,
                sanitize: false,
                content: $ele.find('.popover-body').html()
            });
            $button.popover('show');
        },
        fixTableHeader: function (el) {
            var $ele = $(el);
            var $thead = $ele.find('thead');
            $ele.on('scroll', function () {
                var top = $ele.scrollTop();
                $thead.css({ 'transform': 'translateY(' + top + 'px)' });
            });
        },
        timePicker: function (el) {
            return $(el).find('.time-spinner-item').height();
        },
        datetimePicker: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-placement') || 'auto';
            var $input = $el.find('.datetime-picker-input');
            if (!method) {
                $input.popover({
                    toggle: 'datetime-picker',
                    placement: placement,
                    template: '<div class="popover popover-datetime" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'
                })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            $pop.find('.popover-body').append($el.find('.date-picker').removeClass('d-none'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.date-picker');
                            $pop.find('.popover-body').append($picker.clone());
                            $el.append($picker.addClass('d-none'));
                        }
                    });
                $('.datetime-picker-input-icon').on('click', function (e) {
                    e.stopImmediatePropagation();
                    var $input = $(this).parents('.datetime-picker-bar').find('.datetime-picker-input');
                    $input.trigger('click');
                });
            }
            else $input.popover(method);
        },
        tab: function (el) {
            var $el = $(el);
            var $activeTab = $el.find('.tabs-item.is-active');
            var $bar = $el.find('.tabs-active-bar');
            var width = $activeTab.width();
            var left = $activeTab.position().left + parseInt($activeTab.css('paddingLeft'));
            $bar.css({ 'width': width + 'px', 'transform': 'translateX(' + left + 'px)' });
        },
        captcha: function (el, obj, method, options) {
            options.remoteObj = { obj, method };
            $(el).sliderCaptcha(options);
        },
        uploader: function (el, obj, complete, check, del, failed) {
            options = {};
            options.remoteObj = { obj, complete, check, del, failed };
            $(el).uploader(options);
        }
    });

    /*captch*/
    var SliderCaptcha = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, options);
        this.init();
    };

    SliderCaptcha.VERSION = '3.1.0';
    SliderCaptcha.Author = 'argo@163.com';
    SliderCaptcha.DATA_KEY = "lgb.SliderCaptcha";

    var _proto = SliderCaptcha.prototype;
    _proto.init = function () {
        this.initDOM();
        this.initImg();
        this.bindEvents();
    };

    _proto.initDOM = function () {
        var $canvas = this.$element.find("canvas:first")[0].getContext('2d');
        var $block = this.$element.find("canvas:last")[0];
        var $bar = $block.getContext('2d');
        var $load = this.$element.find(".captcha-load");
        var $footer = this.$element.find('.captcha-footer');
        var $barLeft = $footer.find('.captcha-bar-bg');
        var $slider = this.$element.find('.captcha-bar');
        var $barText = this.$element.find('.captcha-bar-text');
        var $refresh = this.$element.find('.captcha-refresh');
        var barText = $barText.attr('data-text');
        $.extend(this, {
            canvas: $canvas,
            block: $block,
            bar: $bar,
            $load: $load,
            $footer: $footer,
            $barLeft: $barLeft,
            $slider: $slider,
            $barText: $barText,
            $refresh: $refresh,
            barText: barText
        });
    };

    _proto.initImg = function () {
        var drawImg = function (ctx, operation) {
            var l = this.options.sideLength;
            var r = this.options.diameter;
            var PI = Math.PI;
            var x = this.options.offsetX;
            var y = this.options.offsetY;
            ctx.beginPath();
            ctx.moveTo(x, y);
            ctx.arc(x + l / 2, y - r + 2, r, 0.72 * PI, 2.26 * PI);
            ctx.lineTo(x + l, y);
            ctx.arc(x + l + r - 2, y + l / 2, r, 1.21 * PI, 2.78 * PI);
            ctx.lineTo(x + l, y + l);
            ctx.lineTo(x, y + l);
            ctx.arc(x + r - 2, y + l / 2, r + 0.4, 2.76 * PI, 1.24 * PI, true);
            ctx.lineTo(x, y);
            ctx.lineWidth = 2;
            ctx.fillStyle = 'rgba(255, 255, 255, 0.7)';
            ctx.strokeStyle = 'rgba(255, 255, 255, 0.7)';
            ctx.stroke();
            ctx[operation]();
            ctx.globalCompositeOperation = 'destination-over';
        };

        var img = new Image();
        img.src = this.options.imageUrl;
        var that = this;
        img.onload = function () {
            drawImg.call(that, that.canvas, 'fill');
            drawImg.call(that, that.bar, 'clip');

            that.canvas.drawImage(img, 0, 0, that.options.width, that.options.height);
            that.bar.drawImage(img, 0, 0, that.options.width, that.options.height);

            var y = that.options.offsetY - that.options.diameter * 2 - 1;
            var ImageData = that.bar.getImageData(that.options.offsetX - 3, y, that.options.barWidth, that.options.barWidth);
            that.block.width = that.options.barWidth;
            that.bar.putImageData(ImageData, 0, y);
        };
        img.onerror = function () {
            that.$load.text($load.attr('data-failed')).addClass('text-danger');
        }
    };

    _proto.bindEvents = function () {
        var that = this;
        var originX, originY, trail = [],
            isMouseDown = false;

        var handleDragStart = function (e) {
            that.$barText.addClass('d-none');
            originX = e.clientX || e.touches[0].clientX;
            originY = e.clientY || e.touches[0].clientY;
            isMouseDown = true;
        };

        var handleDragMove = function (e) {
            if (!isMouseDown) return false;
            var eventX = e.clientX || e.touches[0].clientX;
            var eventY = e.clientY || e.touches[0].clientY;
            var moveX = eventX - originX;
            var moveY = eventY - originY;
            if (moveX < 0 || moveX + 40 > that.options.width) return false;

            that.$slider.css({ 'left': (moveX - 1) + 'px' });
            var blockLeft = (that.options.width - 40 - 20) / (that.options.width - 40) * moveX;
            that.block.style.left = blockLeft + 'px';

            that.$footer.addClass('is-move');
            that.$barLeft.css({ 'width': (moveX + 4) + 'px' });
            trail.push(Math.round(moveY));
        };

        var handleDragEnd = function (e) {
            if (!isMouseDown) return false;
            isMouseDown = false;

            var eventX = e.clientX || e.changedTouches[0].clientX;
            if (eventX === originX) return false;
            that.$footer.removeClass('is-move');

            var offset = Math.ceil((that.options.width - 40 - 20) / (that.options.width - 40) * (eventX - originX) + 3);
            that.verify(offset, trail);
        };

        this.$slider.on('mousedown', handleDragStart);
        this.$slider.on('touchstart', handleDragStart);
        this.$refresh.on('click', function () {
            that.options.barText = that.$barText.attr('data-text');
        });
        document.addEventListener('mousemove', handleDragMove);
        document.addEventListener('touchmove', handleDragMove);
        document.addEventListener('mouseup', handleDragEnd);
        document.addEventListener('touchend', handleDragEnd);

        document.addEventListener('mousedown', function () { return false; });
        document.addEventListener('touchstart', function () { return false; });
        document.addEventListener('swipe', function () { return false; });
    };

    _proto.verify = function (offset, trails) {
        var remoteObj = this.options.remoteObj.obj;
        var method = this.options.remoteObj.method;
        var that = this;
        remoteObj.invokeMethodAsync(method, offset, trails).then(function (data) {
            if (data) {
                that.$footer.addClass('is-valid');
                that.options.barText = that.$barText.attr('data-text');
            } else {
                that.$footer.addClass('is-invalid');
                setTimeout(function () {
                    that.$refresh.trigger('click');
                    that.options.barText = that.$barText.attr('data-try')
                }, 1000);
            }
        });
    };

    _proto.update = function (option) {
        $.extend(this.options, option);
        this.resetCanvas();
        this.initImg();
        this.resetBar();
    }

    _proto.resetCanvas = function () {
        this.canvas.clearRect(0, 0, this.options.width, this.options.height);
        this.bar.clearRect(0, 0, this.options.width, this.options.height);
        this.block.width = this.options.width;
        this.block.style.left = 0;
        this.$load.text(this.$load.attr('data-load')).removeClass('text-danger');
    };

    _proto.resetBar = function () {
        this.$footer.removeClass('is-invalid is-valid');
        this.$barText.text(this.options.barText).removeClass('d-none');
        this.$slider.css({ 'left': '0px' });
        this.$barLeft.css({ 'width': '0px' });
    };

    function CaptchaPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(SliderCaptcha.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(SliderCaptcha.DATA_KEY, data = new SliderCaptcha(this, options));
            else data.update(options);
        });
    }

    $.fn.sliderCaptcha = CaptchaPlugin;
    $.fn.sliderCaptcha.Constructor = SliderCaptcha;
    /*end captcha*/

    /*upload*/
    var Uploader = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, options);
        this.init();
    };

    Uploader.VERSION = '3.1.0';
    Uploader.Author = 'argo@163.com';
    Uploader.DATA_KEY = "lgb.Uploader";

    var _proto = Uploader.prototype;
    _proto.init = function () {
        var that = this;
        this.$fileItem = this.$element.find('.upload-list .upload-list-item:first');
        this.$file = this.$element.find(':file');
        this.showProgress = this.$element.hasClass('is-progress');
        this.prev = this.$element.hasClass('is-prev');
        this.iscircle = this.$element.hasClass('is-circle');
        this.iswall = this.$element.hasClass('is-wall');
        this.iscard = this.$element.hasClass('is-card');
        this.isstack = this.$element.hasClass('is-stack');
        this.url = this.$element.attr('data-url');
        this.multiple = this.$element.find(':file').attr('multiple') === 'multiple';
        this.files = [];

        if (this.iswall || this.multiple) {
            this.$item = this.$element.find('.upload-prev:first').clone();
        }

        this.$element.on('click', '.upload-prev', function () {
            if (that.isstack || $(this).hasClass('is-upload') || $(this).hasClass('is-uploading')) return;

            that.$prev = $(this);
            that.$file.trigger('click');
        });
        this.$element.on('click', '.btn-upload', function () {
            if (that.isstack) {
                that.$file.trigger('click');
            }
            else {
                $.each(that.files, function () {
                    that.upload(this.file, this.$prev);
                });
            }
            that.files = [];
        });

        this.$element.on('click', '.upload-prev .upload-item-delete, .upload-prev .btn-delete', function (e) {
            var $prev = $(this).parents('.upload-prev');
            var fileName = $prev.attr('data-file');
            if ($prev.hasClass('is-invalid-file')) {
                $prev.remove();
            }
            else {
                $.ajax({
                    url: that.url,
                    method: 'delete',
                    data: JSON.stringify(fileName),
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (result) {
                        if (result) {
                            that.reset.call(that, $prev);
                            var remote = that.options.remoteObj;
                            remote.obj.invokeMethodAsync(remote.del, fileName);
                        }
                    },
                    error: function () {
                        that.failed(e, $prev, { name: fileName });
                    }
                });
            }
        });

        this.$file.on('change', function () {
            that.fileSelected.call(that);
        })

        if (!this.url) this.url = 'api/Upload';
    };

    _proto.reset = function ($prev) {
        if (this.iswall || this.multiple) {
            $prev.remove();
        }
        else {
            $prev.find('img').removeClass('d-block');
            $prev.find('.fa-plus').removeClass('d-none')
            $prev.removeClass('is-upload is-invalid is-valid is-load is-file');
            $prev.removeAttr('data-file');
            $prev.find('.upload-prev-progress-cur').css({ "width": "0" });
            $prev.find('.upload-prev-progress-text').html('0 %');

            if (this.iscircle) {
                this.toggleCircle($prev, false);
            }
        }
    };

    _proto.fileSelected = function () {
        var that = this;

        // handler cancel event
        if (this.$file[0].files.length === 0) {
            return;
        }

        var $prev = this.isstack ? this.$item.clone() : this.$prev;
        if (this.isstack) $prev.addClass('d-flex').insertAfter(this.$element.find('.upload-prev:first'));

        $.each(this.$file[0].files, function (index) {
            var remote = that.options.remoteObj;
            var file = this;
            remote.obj.invokeMethodAsync(remote.check, file.name, file.type, file.size).then(function (result) {
                if (result.result) {
                    $prev.addClass('is-active').removeClass('is-invalid is-valid').tooltip('dispose');

                    if (that.iscircle) that.toggleCircle($prev, true);

                    that.prevFile.call(that, file, $prev);

                    that.setUploadItem.call(that, file, $prev);

                    if (that.prev) {
                        // remove prev file
                        var origin = $prev.attr('data-file');
                        if (origin) {
                            var f = that.files.findIndex(function (v, index) { return v.file.name === origin; });
                            if (f > -1) that.files.splice(f, 1);
                        }
                        $prev.attr('data-file', file.name);
                        that.files.push({ file: file, $prev });
                    }
                    else {
                        that.upload(file, $prev);
                    }

                    if (that.iswall || that.multiple) {
                        $prev = that.$item.clone();
                        if (that.isstack) {
                            if (index + 1 < that.$file[0].files.length) $prev.addClass('d-flex').insertAfter(that.$element.find('.upload-prev:first'));
                        }
                        else {
                            // 照片墙模式
                            $prev.insertAfter(that.$element.find('.upload-prev:last'));
                        }
                    }
                }
                else {
                    if (that.isstack) {
                        $prev.addClass('is-invalid is-invalid-file');
                        $prev.find('.upload-prev-invalid-file .file-name').html(file.name);
                        $prev.find('.upload-prev-invalid-file .file-error').html(result.message);
                    }
                    else {
                        $prev.removeClass('is-upload is-active').addClass('is-invalid');
                        that.showError($prev, result.message);
                    }
                }
            });
        });
    };

    _proto.prevFile = function (file, $prev) {
        if (file.type.startsWith('image')) {
            var reader = new FileReader();
            reader.onloadend = function (e) {
                $prev.removeClass('is-active').addClass('is-load').find('img').attr('src', e.target.result);
            };
            reader.readAsDataURL(file);
        }
        else {
            $prev.addClass('is-file');
            var icon = $prev.find('.upload-file-icon');
            icon.html("");
            icon.append(this.createIcon(file.type));
        }
    };

    _proto.createIcon = function (type) {
        console.log(type);
        var $icon = $('<i class="fa"></fa>');
        if (type === "application/x-zip-compressed") {
            $icon.addClass('fa-file-archive-o');
        }
        else if (type === "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {
            $icon.addClass('fa-file-word-o');
        }
        else if (type === "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
            $icon.addClass('fa-file-excel-o');
        }
        else if (type === "application/vnd.openxmlformats-officedocument.presentationml.presentation") {
            $icon.addClass('fa-file-powerpoint-o');
        }
        else if (type.startsWith("audio")) {
            $icon.addClass('fa-file-audio-o');
        }
        else if (type.startsWith("video")) {
            $icon.addClass('fa-file-video-o');
        }
        else if (type === "application/pdf") {
            $icon.addClass('fa-file-pdf-o');
        }
        else {
            $icon.addClass('fa-file-text-o');
        }
        return $icon;
    }

    _proto.showError = function ($prev, message) {
        $prev.attr('data-original-title', message);
        $prev.attr('data-container', 'body');
        $prev.tooltip('show');
    };

    _proto.setUploadItem = function (file, $prev) {
        if (file) {
            var fileSize = 0;
            if (file.size > 1024 * 1024)
                fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100).toString() + 'MB';
            else
                fileSize = (Math.round(file.size * 100 / 1024) / 100).toString() + 'KB';
            $prev.find('.upload-list-item-text').text(file.name);
            $prev.find('.upload-list-item-size').text(fileSize);
        }
    };

    _proto.calcSize = function (size) {
        var fileSize = "";

        if (size > 1024 * 1024)
            fileSize = (Math.round(size * 100 / (1024 * 1024)) / 100).toString() + ' MB';
        else
            fileSize = (Math.round(size * 100 / 1024) / 100).toString() + ' KB';

        return fileSize;
    };

    _proto.progress = function (evt, $prev) {
        if (evt.lengthComputable) {
            var percentComplete = Math.round(evt.loaded * 100 / evt.total);
            if (this.iscircle) {
                var $progress = $prev.find('.upload-item-circle-progress');
                $progress.attr("stroke-dashoffset", "0");

                var length = parseFloat($progress.attr('data-dash'));
                var len = percentComplete * length / 100;
                $progress.attr("stroke-dasharray", len + "," + length);
            }
            else if (this.showProgress) {
                $prev.find('.upload-prev-progress-text').html(percentComplete + ' %');
                $prev.find('.upload-prev-progress-cur').css({ 'width': percentComplete + '%' });

                // calc rate
                this.calcRate(evt.loaded, evt.total, $prev);
            }
        }
    };

    _proto.calcRate = function (cur, total, $prev) {
        var $el = $prev.find('.upload-list-item-rate');

        if (total > 1 * 1024 * 1024) {

            if (!this.ticks) this.ticks = new Date().getTime();

            var interval = new Date().getTime() - this.ticks;
            if (interval === 0) interval = 1;
            var datas = cur * 1000 / interval;

            var ds = this.calcSize(datas);
            $el.html(ds + "/s");
        }
    };

    _proto.toggleCircle = function ($prev, state) {
        var $label = $prev.find('.upload-item-circle');
        if (state) $label.removeClass('d-none');
        else {
            $label.addClass('d-none');

            var $progress = $prev.find('.upload-item-circle-progress');
            var length = parseFloat($progress.attr('data-dash'));

            $progress.attr("stroke-dashoffset", length);
            $progress.attr("stroke-dasharray", length + "," + length);
        }
    };

    _proto.toggleLabel = function ($prev, state) {
        var $label = $prev.find('.upload-item-label');
        if (state) $label.removeClass('d-none');
        else $label.addClass('d-none');
    };

    _proto.complete = function (evt, $prev, file) {
        var that = this;
        var failed = true;
        try {
            var response = JSON.parse(evt.target.response);
            if ($.isArray(response)) {
                $.each(response, function (index, v) {
                    if (v.originFileName == file.name) {
                        $prev.addClass('is-upload is-valid');
                        $prev.removeClass('is-invalid is-uploading');
                        if (!$prev.hasClass("is-file")) $prev.find('img').attr('src', v.prevUrl);
                        $prev.attr('data-file', v.fileName);
                        that.options.remoteObj.obj.invokeMethodAsync(that.options.remoteObj.complete, file.name);
                    }

                    if (that.iscard) {
                        var fileSize = that.calcSize(file.size);
                        $prev.find('.upload-list-item-size').html("(" + fileSize + ")");
                    }

                });
                failed = false;
            }
        }
        catch (ex) { }

        if (failed) this.failed(evt, $prev, file);
    };

    _proto.failed = function (evt, $prev, file) {
        $prev.addClass("is-invalid");
        var remote = this.options.remoteObj;
        remote.obj.invokeMethodAsync(remote.failed, file.name);
        this.showError($prev, "服务器端错误上传失败");
    };

    _proto.cancel = function (evt) {
        alert("The upload has been canceled by the user or the browser dropped the connection.");
    };

    _proto.upload = function (file, $prev) {
        var that = this;
        var fd = new FormData();
        fd.append(file.name, file);
        var xhr = new XMLHttpRequest();

        if (this.showProgress || this.iscircle) {
            xhr.upload.addEventListener("progress", function (e) {
                that.progress.call(that, e, $prev);
            });
        }

        xhr.addEventListener("error", function (e) {
            that.failed.call(that, e, $prev, file);
        });
        xhr.addEventListener("abort", function (e) {
            that.cancel.call(that, e, $prev, file);
        });
        xhr.addEventListener("load", function (e) {
            that.complete.call(that, e, $prev, file);
        });

        xhr.open("POST", this.url);
        xhr.send(fd);

        $prev.removeClass('is-active').addClass('is-uploading');
    };

    function UploaderPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Uploader.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Uploader.DATA_KEY, data = new Uploader(this, options));
        });
    }

    $.fn.uploader = UploaderPlugin;
    $.fn.uploader.Constructor = Uploader;
    /*end upload*/

    $(function () {
        $(document)
            .on('hidden.bs.toast', '.toast', function () {
                $(this).removeClass('hide');
            })
            .on('inserted.bs.tooltip', '.is-invalid', function () {
                $('#' + $(this).attr('aria-describedby')).addClass('is-invalid');
            });

        // popover confirm
        $.fn.popover.Constructor.prototype.isWithContent = function () {
            var components = ['', 'confirm', 'datetime-picker'];
            var toggle = this.config.toggle;
            return components.indexOf(toggle) || Boolean(this.getTitle());
        }

        var findConfirmButton = function ($el) {
            var button = null;
            var $parent = $el.parents('.popover');
            if ($parent.length > 0) {
                var id = $parent.attr('id');
                button = $('[aria-describedby="' + id + '"]');
            }
            return button;
        };

        $(document).on('click', function (e) {
            // hide popover
            var hide = true;
            var $el = $(e.target);
            // 判断是否点击 popover 内部
            var $confirm = findConfirmButton($el);
            if ($confirm != null) hide = false;
            if (hide) $('[data-toggle="confirm"][aria-describedby^="popover"]').popover('hide');

            // datetime picker
            if ($el.parents('.popover-datetime.show').length === 0) {
                $('.popover-datetime.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }
            else {
                // 处理点击日事件
                var $day = $el.parents('.date-table');
                if ($day.length === 1) {
                    // 点击的是 Day cell
                    var $popover = $el.parents('.popover-datetime.show');
                    var $footer = $popover.find('.picker-panel-footer:visible');
                    if ($footer.length === 0) {
                        var pId = $popover.attr('id');
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                }
            }
        });

        $(document).on('click', '.popover-confirm-buttons .btn', function (e) {
            e.stopPropagation();

            // 确认弹窗按钮事件
            var $confirm = findConfirmButton($(this));
            if ($confirm != null) {
                // 关闭弹窗
                $confirm.popover('hide');

                // remove popover
                var buttonId = $confirm.attr('id');
                $ele = $('[data-target="' + buttonId + '"]');

                var $button = this.getAttribute('data-dismiss') === 'confirm'
                    ? $ele.find('.popover-confirm-buttons .btn:first')
                    : $ele.find('.popover-confirm-buttons .btn:last');
                $button.trigger('click');
            }
        });
    });
})(jQuery);
