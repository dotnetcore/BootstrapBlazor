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

    $.blazorCulture = {
        get: () => {
            return window.localStorage['BlazorCulture'];
        },
        set: (value) => {
            window.localStorage['BlazorCulture'] = value;
        }
    };

    $.generatefile = (fileName, bytesBase64, contenttype) => {
        var link = document.createElement('a');
        link.download = fileName;
        link.href = 'data:' + contenttype + ';base64,' + bytesBase64;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };

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

    /**
     * Tab
     * @param {any} element
     * @param {any} options
     */
    var Tab = function (element, options) {
        this.$element = $(element);
        this.$header = this.$element.find('.tabs-header');
        this.$wrap = this.$header.find('.tabs-nav-wrap');
        this.$scroll = this.$wrap.find('.tabs-nav-scroll');
        this.$tab = this.$scroll.find('.tabs-nav');
        this.options = $.extend({}, options);
        this.init();
    };

    Tab.VERSION = "3.1.0";
    Tab.Author = 'argo@163.com';
    Tab.DATA_KEY = "lgb.Tab";

    var _proto = Tab.prototype;
    _proto.init = function () {
        var that = this;
        $(window).on('resize', function () {
            //that.fixSize();
            that.resize();
        });
        //this.fixSize();
        this.active();
    };

    _proto.fixSize = function () {
        var height = this.$element.height();
        var width = this.$element.width();
        this.$element.css({ 'height': height + 'px', 'width': width + 'px' });
    }

    _proto.resize = function () {
        this.vertical = this.$element.hasClass('tabs-left') || this.$element.hasClass('tabs-right');
        this.horizontal = this.$element.hasClass('tabs-top') || this.$element.hasClass('tabs-bottom');

        var $lastItem = this.$tab.find('.tabs-item:last');
        if ($lastItem.length > 0) {
            if (this.vertical) {
                this.$wrap.css({ 'height': this.$element.height() + 'px' });
                var tabHeight = this.$tab.height();
                var itemHeight = $lastItem.position().top + $lastItem.outerHeight();
                if (itemHeight < tabHeight) this.$wrap.removeClass("is-scrollable");
                else this.$wrap.addClass('is-scrollable');
            }
            else {
                this.$wrap.removeAttr('style');
                var tabWidth = this.$tab.width();
                var itemWidth = $lastItem.position().left + $lastItem.outerWidth();
                if (itemWidth < tabWidth) this.$wrap.removeClass("is-scrollable");
                else this.$wrap.addClass('is-scrollable');
            }
        }
    }

    _proto.active = function () {
        // check scrollable
        this.resize();

        var $bar = this.$element.find('.tabs-active-bar');
        var $activeTab = this.$element.find('.tabs-item.is-active');
        if ($activeTab.length === 0) return;

        if (this.vertical) {
            //scroll
            var top = $activeTab.position().top;
            var itemHeight = top + $activeTab.outerHeight();
            var scrollTop = this.$scroll.scrollTop();
            var scrollHeight = this.$scroll.outerHeight();
            var marginTop = itemHeight - scrollTop - scrollHeight;
            if (marginTop > 0) {
                this.$scroll.scrollTop(scrollTop + marginTop);
            }
            else {
                var marginBottom = top - scrollTop;
                if (marginBottom < 0) {
                    this.$scroll.scrollTop(scrollTop + marginBottom);
                }
            }
            $bar.css({ 'width': '2px', 'transform': 'translateY(' + top + 'px)' });
        }
        else {
            // scroll
            var left = $activeTab.position().left;
            var itemWidth = left + $activeTab.outerWidth();
            var scrollLeft = this.$scroll.scrollLeft();
            var scrollWidth = this.$scroll.width();
            var marginLeft = itemWidth - scrollLeft - scrollWidth;
            if (marginLeft > 0) {
                this.$scroll.scrollLeft(scrollLeft + marginLeft);
            }
            else {
                var marginRight = left - scrollLeft;
                if (marginRight < 0) {
                    this.$scroll.scrollLeft(scrollLeft + marginRight);
                }
            }
            var width = $activeTab.width();
            var itemLeft = left + parseInt($activeTab.css('paddingLeft'));
            $bar.css({ 'width': width + 'px', 'transform': 'translateX(' + itemLeft + 'px)' });
        }
    };

    function TabPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Tab.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Tab.DATA_KEY, data = new Tab(this, options));
            if (typeof option === 'string') {
                if (/active/.test(option))
                    data[option].apply(data);
            }
        });
    }

    $.fn.tab = TabPlugin;
    $.fn.tab.Constructor = Tab;
    /*end tab*/

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
        var originX = 0;
        var originY = 0;
        var trail = [];

        this.$slider.drag(
            function (e) {
                that.$barText.addClass('d-none');
                originX = e.clientX || e.touches[0].clientX;
                originY = e.clientY || e.touches[0].clientY;
            },
            function (e) {
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
            },
            function (e) {
                var eventX = e.clientX || e.changedTouches[0].clientX;
                that.$footer.removeClass('is-move');

                var offset = Math.ceil((that.options.width - 40 - 20) / (that.options.width - 40) * (eventX - originX) + 3);
                that.verify(offset, trail);
            }
        );

        this.$refresh.on('click', function () {
            that.options.barText = that.$barText.attr('data-text');
        });
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
            if (that.$element.hasClass('is-disabled') || that.isstack || $(this).hasClass('is-upload') || $(this).hasClass('is-uploading')) return;

            that.$prev = $(this);
            that.$file.trigger('click');
        });
        this.$element.on('click', '.btn-upload', function () {
            if (that.$element.hasClass('is-disabled')) return;

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
            if (that.$element.hasClass('is-disabled')) return;

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
        });

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

    _proto.resetall = function () {
        // 服务器端调用重置组件
        if (this.iswall || this.multiple) {
            this.$element.find('.upload-body .upload-prev.is-load, .upload-body .upload-prev.is-file').remove();
        }
        else {
            var $prev = this.$element.find('.upload-body .upload-prev');
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
                            var f = that.files.findIndex(function (v, index) {
                                return v.file.name === origin;
                            });
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
                else if (that.isstack) {
                    $prev.addClass('is-invalid is-invalid-file');
                    $prev.find('.upload-prev-invalid-file .file-name').html(file.name);
                    $prev.find('.upload-prev-invalid-file .file-error').html(result.message);
                }
                else {
                    $prev.removeClass('is-upload is-active').addClass('is-invalid');
                    that.showError($prev, result.message);
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
                        if (!$prev.hasClass("is-file")) $prev.find('img').attr('src', v.prevUrl + "?v=" + $.getUID());
                        $prev.attr('data-file', v.fileName);
                        that.options.remoteObj.obj.invokeMethodAsync(that.options.remoteObj.complete, file.name, v.prevUrl);
                    }

                    if (that.iscard) {
                        var fileSize = that.calcSize(file.size);
                        $prev.find('.upload-list-item-size').html("(" + fileSize + ")");
                    }

                });
                failed = false;
            }
        }
        catch (ex) {
            console.log(ex);
        }

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

        // setHeader
        var methodName = this.options.remoteObj.setHeaders;
        this.options.remoteObj.obj.invokeMethodAsync(methodName).then(function (headers) {
            if ($.isArray(headers)) {
                $.each(headers, function () {
                    xhr.setRequestHeader(this.name, this.value);
                })
            }
            xhr.send(fd);

            $prev.removeClass('is-active').addClass('is-uploading');
        });
    };

    function UploaderPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Uploader.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Uploader.DATA_KEY, data = new Uploader(this, options));

            // 支持 reset 方法
            if (typeof option === 'string') {
                if (/Reset/.test(option))
                    data['resetall'].apply(data);
            }
        });
    }

    $.fn.uploader = UploaderPlugin;
    $.fn.uploader.Constructor = Uploader;
    /*end upload*/

    $.fn.extend({
        drag: function (star, move, end) {
            var $this = $(this);

            var handleDragStart = function (e) {
                e.stopPropagation();

                document.addEventListener('mousemove', handleDragMove);
                document.addEventListener('touchmove', handleDragMove);
                document.addEventListener('mouseup', handleDragEnd);
                document.addEventListener('touchend', handleDragEnd);

                if ($.isFunction(star)) {
                    star.call($this, e);
                }
            };

            var handleDragMove = function (e) {
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
        }
    });

    $.extend({
        html5edit: function (el, options) {
            if (!$.isFunction($.fn.summernote)) {
                return;
            }

            var $this = $(el);
            var op = typeof options == 'object' && options;

            if (/destroy|hide/.test(options)) {
                return $this.toggleClass('open').summernote(options);
            }
            else if (typeof options == 'string') {
                return $this.hasClass('open') ? $this.summernote(options) : $this.html();
            }

            op = $.extend({ focus: true, lang: 'zh-CN', height: 80, dialogsInBody: true }, op);

            // div 点击事件
            $this.on('click', op, function (event, args) {
                var $this = $(this).tooltip('hide');
                var op = $.extend({ placeholder: $this.attr('placeholder') }, event.data, args || {});
                op.obj.invokeMethodAsync('GetToolBar').then(result => {
                    var $toolbar = $this.toggleClass('open').summernote($.extend({
                        callbacks: {
                            onChange: function (htmlString) {
                                op.obj.invokeMethodAsync(op.method, htmlString);
                            }
                        },
                        toolbar: result
                    }, op))
                        .next().find('.note-toolbar')
                        .on('click', 'button[data-method]', { note: $this, op: op }, function (event) {
                            var $btn = $(this);
                            switch ($btn.attr('data-method')) {
                                case 'submit':
                                    $btn.tooltip('dispose');
                                    var $note = event.data.note.toggleClass('open');
                                    var htmlString = $note.summernote('code');
                                    $note.summernote('destroy');
                                    event.data.op.obj.invokeMethodAsync(event.data.op.method, htmlString);
                                    break;
                            }
                        });
                    var $done = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn btn btn-sm note-btn-close" tabindex="-1" data-method="submit" title="完成" data-placement="bottom"><i class="fa fa-check"></i></button></div>').appendTo($toolbar).find('button').tooltip({ container: 'body' });
                    $('body').find('.note-group-select-from-files [accept="image/*"]').attr('accept', 'image/bmp,image/png,image/jpg,image/jpeg,image/gif');
                });

            }).tooltip({ title: '点击展开编辑' });

            if (op.value) $this.html(op.value);
            if ($this.hasClass('open')) {
                // 初始化为 editor
                $this.trigger('click', { focus: false });
            }
            return this;
        },
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
        showMessage: function (el, obj, method) {
            if (!window.Messages) window.Messages = [];
            Messages.push(el);

            var $el = $(el);
            var autoHide = $el.attr('data-autohide') !== 'false';
            var delay = parseInt($el.attr('data-delay'));
            var autoHideHandler = null;

            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    // auto close
                    autoHideHandler = window.setTimeout(function () {
                        window.clearTimeout(autoHideHandler);
                        $el.close();
                    }, delay);
                }
                $el.addClass('show');
            }, 50);

            $el.close = function () {
                if (autoHideHandler != null) {
                    window.clearTimeout(autoHideHandler);
                }
                $el.removeClass('show');
                var hideHandler = window.setTimeout(function () {
                    window.clearTimeout(hideHandler);

                    // remove Id
                    Messages.remove(el);
                    if (Messages.length === 0) {
                        // call server method prepare remove dom
                        obj.invokeMethodAsync(method);
                    }
                }, 500);
            };

            $el.on('click', '.close', function (e) {
                e.preventDefault();
                e.stopPropagation();

                $el.close();
            });
        },
        bb_pop: function (el, method) {
            var $el = $(el);
            if (method === 'init') {
                $el.appendTo($('body'));
            }
            else if (method === 'dispose') {
                $el.remove();
            }
        },
        showToast: function (el, toast, method) {
            // 记录 Id
            Toasts.push(el);

            // 动画弹出
            var $toast = $(el);

            // check autohide
            var autoHide = $toast.attr('data-autohide') !== 'false';
            var delay = parseInt($toast.attr('data-delay'));

            $toast.addClass('d-block');
            var autoHideHandler = null;
            var showHandler = window.setTimeout(function () {
                window.clearTimeout(showHandler);
                if (autoHide) {
                    $toast.find('.toast-progress').css({ 'width': '100%', 'transition': 'width ' + delay / 1000 + 's linear' });

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
                    Toasts.remove($toast[0]);
                    if (Toasts.length === 0) {
                        // call server method prepare remove dom
                        toast.invokeMethodAsync(method);
                    }
                }, 500);
            });
        },
        bb_carousel: function (ele) {
            var $ele = $(ele).carousel();

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

            var isDisabled = $slider.find('.disabled').length > 0;
            if (!isDisabled) {
                var originX = 0;
                var curVal = 0;
                var newVal = 0;
                var slider_width = $slider.innerWidth();
                $slider.find('.slider-button-wrapper').drag(
                    function (e) {
                        originX = e.clientX || e.touches[0].clientX;
                        curVal = parseInt($slider.attr('aria-valuetext'));
                        $slider.find('.slider-button-wrapper, .slider-button').addClass('dragging');
                    },
                    function (e) {
                        var eventX = e.clientX || e.changedTouches[0].clientX;

                        newVal = Math.ceil((eventX - originX) * 100 / slider_width) + curVal;

                        if (newVal <= 0) newVal = 0;
                        if (newVal >= 100) newVal = 100;

                        $slider.find('.slider-bar').css({ "width": newVal.toString() + "%" });
                        $slider.find('.slider-button-wrapper').css({ "left": newVal.toString() + "%" });
                        $slider.attr('aria-valuetext', newVal.toString());

                        slider.invokeMethodAsync(method, newVal);
                    },
                    function (e) {
                        $slider.find('.slider-button-wrapper, .slider-button').removeClass('dragging');

                        slider.invokeMethodAsync(method, newVal);
                    });
            }
        },
        bb_tooltip: function (id, method, title, placement, html, trigger) {
            var op = { html: html, sanitize: !html, title: title, placement: placement, trigger: trigger };
            var $ele = $('#' + id);
            if (method === "") {
                if ($ele.data('bs.tooltip')) $ele.tooltip('dispose');
                $ele.tooltip(op);
            }
            else if (method === 'enable') {
                if ($ele.data('bs.tooltip')) $ele.tooltip('dispose');
                $ele.tooltip(op);
                var $ctl = $ele.parents('form').find('.is-invalid:first');
                if ($ctl.prop("nodeName") === 'INPUT') {
                    if ($ctl.prop('readonly')) {
                        $ctl.trigger('focus');
                    }
                    else {
                        $ctl.focus();
                    }
                }
                else if ($ctl.prop("nodeName") === 'DIV') {
                    $ctl.trigger('focus');
                }
            }
            else if (method === "dispose") {
                if ($ele.data('bs.tooltip')) $ele.tooltip(method);
            }
            else {
                if (!$ele.data('bs.tooltip')) $ele.tooltip(op);
                $ele.tooltip(method);
            }
        },
        bb_popover: function (id, method, title, content, placement, html, trigger) {
            var $ele = $('#' + id);
            var op = { html: html, sanitize: false, title: title, content: content, placement: placement, trigger: trigger };
            if (method === "") {
                if ($ele.data('bs.popover')) $ele.popover('dispose');
                $ele.popover(op);
            }
            else if (method === "dispose") {
                if ($ele.data('bs.popover')) $ele.popover(method);
            }
            else {
                if (!$ele.data('bs.popover')) $ele.popover(op);
                $ele.popover(method);
            }
        },
        bb_confirm: function (id) {
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
        bb_modal: function (el, method) {
            var $el = $(el);

            if (method === 'dispose') {
                $el.remove();
            }
            else if (method === 'init') {
                if ($el.closest('.swal').length === 0) {
                    // move self end of the body
                    $('body').append($el);

                    // monitor mousedown ready to drag dialog
                    var originX = 0;
                    var originY = 0;
                    var dialogWidth = 0;
                    var dialogHeight = 0;
                    var pt = { top: 0, left: 0 };
                    var $dialog = null;
                    $el.find('.is-draggable .modal-header').drag(
                        function (e) {
                            originX = e.clientX || e.touches[0].clientX;
                            originY = e.clientY || e.touches[0].clientY;

                            // 弹窗大小
                            $dialog = this.closest('.modal-dialog');
                            dialogWidth = $dialog.width();
                            dialogHeight = $dialog.height();

                            // 偏移量
                            pt.top = parseInt($dialog.css('marginTop').replace("px", ""));
                            pt.left = parseInt($dialog.css('marginLeft').replace("px", ""));

                            // 移除 Center 样式
                            $dialog.css({ "marginLeft": pt.left, "marginTop": pt.top });
                            $dialog.removeClass('modal-dialog-centered');

                            // 固定大小
                            $dialog.css("width", dialogWidth);
                            this.addClass('is-drag');
                        },
                        function (e) {
                            var eventX = e.clientX || e.changedTouches[0].clientX;
                            var eventY = e.clientY || e.changedTouches[0].clientY;

                            newValX = pt.left + Math.ceil(eventX - originX);
                            newValY = pt.top + Math.ceil(eventY - originY);

                            if (newValX <= 0) newValX = 0;
                            if (newValY <= 0) newValY = 0;

                            if (newValX + dialogWidth < $(window).width()) {
                                if ($dialog != null) {
                                    $dialog.css({ "marginLeft": newValX });
                                }
                            }
                            if (newValY + dialogHeight < $(window).height()) {
                                if ($dialog != null) {
                                    $dialog.css({ "marginTop": newValY });
                                }
                            }
                        },
                        function (e) {
                            this.removeClass('is-drag');
                        }
                    );
                }
                $el.on('shown.bs.modal', function () {
                    $(document).one('keyup', function (e) {
                        if (e.key === 'Escape') {
                            var $dialog = $el.find('.modal-dialog');
                            var method = $dialog.data('bb_dotnet_invoker');
                            if (method != null) {
                                method.invokeMethodAsync('Close');
                            }
                        }
                    });
                });
            }
            else {
                $el.modal(method);
            }
        },
        bb_dialog: function (el, obj, method) {
            var $el = $(el);
            if (method === 'init') {
                $el.data('bb_dotnet_invoker', obj);
            }
        },
        bb_filter: function (el, obj, method) {
            $(el).data('bb_filter', { obj: obj, method: method });
        },
        bb_table_resize: function ($ele) {
            var resizer = $ele.find('.col-resizer');
            if (resizer.length > 0) {
                var eff = function (toggle) {
                    var $span = $(this);
                    var $th = $span.closest('th');
                    if (toggle) $th.addClass('border-resize');
                    else $th.removeClass('border-resize');

                    var index = $th.index();
                    var $tbody = $th.closest('.table-resize').find('tbody');
                    var $tds = $tbody.find('tr').each(function () {
                        var $td = $(this.children[index]);
                        if (toggle) $td.addClass('border-resize');
                        else $td.removeClass('border-resize');
                    });
                    return index;
                };

                var colWidth = 0;
                var tableWidth = 0;
                var colIndex = 0;
                var originalX = 0;

                resizer.each(function () {
                    $(this).drag(
                        function (e) {
                            colIndex = eff.call(this, true);
                            var width = $ele.find('table colgroup col')[colIndex].width;
                            if (width) {
                                colWidth = parseInt(width);
                            }
                            else {
                                colWidth = $(this).closest('th').width();
                            }
                            tableWidth = $(this).closest('table').width();
                            originalX = e.clientX;
                        },
                        function (e) {
                            $ele.find('table colgroup').each(function (index, colgroup) {
                                var col = $(colgroup).find('col')[colIndex];
                                var marginX = e.clientX - originalX;
                                col.width = colWidth + marginX;

                                if (index === 0)
                                    $(colgroup).closest('table').width(tableWidth + marginX);
                            });
                        },
                        function () {
                            eff.call(this, false);
                        }
                    );
                });
            }
        },
        bb_table: function (el, method, args) {
            var $ele = $(el);

            var btn = $ele.find('.btn-col');
            if (!btn.hasClass('init')) {
                btn.addClass('init');
                btn.on('click', function () {
                    var $menu = $(this).next();
                    $menu.toggleClass('show');
                });
            }

            if (method === 'fixTableHeader') {
                var $thead = $ele.find('.table-fixed-header');
                var $body = $ele.find('.table-fixed-body');
                $body.on('scroll', function () {
                    var left = $body.scrollLeft();
                    $thead.scrollLeft(left);
                });
                var $fs = $ele.find('.fixed-scroll');
                if ($fs.length === 1) {
                    var $prev = $fs.prev();
                    while ($prev.length === 1) {
                        if ($prev.hasClass('fixed-right') && !$prev.hasClass('modified')) {
                            var margin = $prev.css('right');
                            margin = margin.replace('px', '');
                            if ($.browser.versions.mac) {
                                margin = (parseFloat(margin) - 2) + 'px';
                            }
                            else if ($.browser.versions.mobile) {
                                margin = (parseFloat(margin) - 17) + 'px';
                            }
                            $prev.css({ 'right': margin }).addClass('modified');
                            $prev = $prev.prev();
                        }
                        else {
                            break;
                        }
                    }

                    if ($.browser.versions.mobile) {
                        $fs.remove();
                    }
                }

                // 固定表头的最后一列禁止列宽调整
                $ele.find('.col-resizer:last').remove();
                $.bb_table_resize($ele);
            }
            else if (method === 'init') {
                // sort
                var $tooltip = $ele.find('.table-cell.is-sort .table-text');
                var tooltipTitle = { unset: "点击升序", sortAsc: "点击降序", sortDesc: "取消排序" };

                $tooltip.each(function () {
                    var $sortIcon = $(this).parent().find('.fa:last');
                    if ($sortIcon.length > 0) {
                        var defaultTitle = tooltipTitle.unset;
                        if ($sortIcon.hasClass('fa-sort-asc')) defaultTitle = tooltipTitle.sortAsc;
                        else if ($sortIcon.hasClass('fa-sort-desc')) defaultTitle = tooltipTitle.sortDesc;
                        $(this).tooltip({
                            container: 'body',
                            title: defaultTitle
                        });
                    }
                });

                $tooltip.on('click', function () {
                    var $this = $(this);
                    var $fa = $this.parent().find('.fa:last');
                    var sortOrder = 'sortAsc';
                    if ($fa.hasClass('fa-sort-asc')) sortOrder = "sortDesc";
                    else if ($fa.hasClass('fa-sort-desc')) sortOrder = "unset";
                    var $tooltip = $('#' + $this.attr('aria-describedby'));
                    if ($tooltip.length > 0) {
                        var $tooltipBody = $tooltip.find(".tooltip-inner");
                        $tooltipBody.html(tooltipTitle[sortOrder]);
                        $this.attr('data-original-title', tooltipTitle[sortOrder]);
                    }
                });

                // filter
                var $toolbar = $ele.find('.table-toolbar');
                var marginTop = 0;
                if ($toolbar.length > 0) marginTop = $toolbar.height();

                // 点击 filter 小按钮时计算弹出位置
                $ele.find('.filterable .fa-filter').on('click', function () {
                    // position
                    var position = $(this).position();
                    var field = $(this).attr('data-field');
                    var $body = $ele.find('.table-filter-item[data-field="' + field + '"]');
                    var th = $(this).closest('th');
                    var left = th.outerWidth() + th.position().left - $body.outerWidth() / 2;
                    var marginRight = 0;
                    if (th.hasClass('sortable')) marginRight = 24;
                    if (th.hasClass('filterable')) marginRight = marginRight + 12;

                    // 判断是否越界
                    var scrollLeft = th.closest('table').parent().scrollLeft();
                    var margin = th.offset().left + th.outerWidth() - marginRight + $body.outerWidth() / 2 - $(window).width();
                    marginRight = marginRight + scrollLeft;
                    if (margin > 0) {
                        left = left - margin - 16;

                        // set arrow
                        $arrow = $body.find('.card-arrow');
                        $arrow.css({ 'left': 'calc(50% - 0.5rem + ' + (margin + 16) + 'px)' });
                    }
                    $body.css({ "top": position.top + marginTop + 50, "left": left - marginRight });
                });

                $ele.find('.is-tips').tooltip({
                    container: 'body',
                    title: function () {
                        return $(this).text();
                    }
                });
                $.bb_table_resize($ele);
            }
            else if (method === 'width') {
                var width = 0;
                if (args) width = $ele.outerWidth(true);
                else width = $(window).outerWidth(true);
                return width;
            }
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
                    // handler disabled event
                    if ($(this).hasClass('disabled')) return;

                    e.stopImmediatePropagation();
                    var $input = $(this).parents('.datetime-picker-bar').find('.datetime-picker-input');
                    $input.trigger('click');
                });

                $('.disabled .cell').on('click', function (e) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                });
            }
            else $input.popover(method);
        },
        bb_datetimeRange: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-placement') || 'auto';
            var $input = $el.find('.datetime-range-bar');
            if (!method) {
                $input.popover({
                    toggle: 'datetime-range',
                    placement: placement,
                    template: '<div class="popover popover-datetime-range" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'
                })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            $pop.find('.popover-body').append($el.find('.datetime-range-body').addClass('show'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.datetime-range-body');
                            $pop.find('.popover-body').append($picker.clone());
                            $el.append($picker.removeClass('show'));
                        }
                    });

                $el.find('.is-clear').on('click', function () {
                    $input.popover('hide');
                });
            }
            else $input.popover(method);
        },
        bb_tab: function (el) {
            $(el).tab('active');
        },
        captcha: function (el, obj, method, options) {
            options.remoteObj = { obj, method };
            $(el).sliderCaptcha(options);
        },
        uploader: function (el, obj, complete, check, del, failed, setHeaders) {
            if (complete) {
                options = {};
                options.remoteObj = { obj, complete, check, del, failed, setHeaders };
                $(el).uploader(options);
            }
            else {
                $(el).uploader(obj);
            }
        },
        collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.children('.card').children('.collapse-item'), function () {
                var $item = $(this);
                var id = $item.attr('id');
                if (!id) {
                    id = $.getUID();
                    $item.attr('id', id);
                    if (parent != null) $item.attr('data-parent', parent);

                    var $button = $item.prev().find('[data-toggle="collapse"]');
                    $button.attr('data-target', '#' + id).attr('aria-controls', id);
                }
            });

            $el.find('.tree .tree-item > .fa').on('click', function (e) {
                var $parent = $(this).parent();
                $parent.find('[data-toggle="collapse"]').trigger('click');
            });

            // support menu component
            if ($el.parent().hasClass("menu")) {
                $el.on('click', '.nav-link:not(.collapse)', function () {
                    var $this = $(this);
                    $el.find('.active').removeClass('active');
                    $this.addClass("active");

                    // parent
                    var $card = $this.closest('.card');
                    while ($card.length > 0) {
                        $card.children('.card-header').children('.card-header-wrapper').find('.nav-link').addClass('active');
                        $card = $card.parent().closest('.card');
                    }
                });
            }
        },
        rate: function (el, obj, method) {
            var $el = $(el);
            $el.val = parseInt($el.attr('aria-valuenow'));
            var reset = function () {
                var $items = $el.find('.rate-item');
                $items.each(function (i) {
                    if (i > $el.val) $(this).removeClass('is-on');
                    else $(this).addClass('is-on');
                });
            };

            $el.on('mouseenter', '.rate-item', function () {
                if (!$el.hasClass('disabled')) {
                    var $items = $el.find('.rate-item');
                    var index = $items.toArray().indexOf(this);
                    $items.each(function (i) {
                        if (i > index) $(this).removeClass('is-on');
                        else $(this).addClass('is-on');
                    });
                }
            });
            $el.on('mouseleave', function () {
                if (!$el.hasClass('disabled')) {
                    reset();
                }
            });
            $el.on('click', '.rate-item', function () {
                if (!$el.hasClass('disabled')) {
                    var $items = $el.find('.rate-item');
                    $el.val = $items.toArray().indexOf(this);
                    $el.attr('aria-valuenow', $el.val + 1);
                    obj.invokeMethodAsync(method, $el.val + 1);
                }
            });
        },
        footer: function (el, target) {
            var $el = $(el);
            var tooltip = $el.tooltip();
            $el.on('click', function (e) {
                e.preventDefault();
                $(target || window).scrollTop(0);
                tooltip.tooltip('hide');
            });
        },
        bb_anchor: function (el) {
            var $el = $(el);
            $el.on('click', function (e) {
                e.preventDefault();
                var $target = $($el.data('target'));
                var container = $el.data('container');
                if (!container) {
                    container = window;
                }
                var margin = $target.offset().top;
                var marginTop = $target.css("marginTop").replace('px', '');
                if (marginTop) {
                    margin = margin - parseInt(marginTop);
                }
                var offset = $el.data('offset');
                if (offset) {
                    margin = margin - parseInt(offset);
                }
                $(container).scrollTop(margin);
            });
        },
        bb_editor: function (el, obj, attrMethod, callback, method, height, value) {
            var invoker = function () {
                var editor = el.getElementsByClassName("editor-body");

                if (obj === 'code') {
                    if ($(editor).hasClass('open')) {
                        $(editor).summernote('code', value);
                    }
                    else {
                        $(editor).html(value);
                    }
                }
                else {
                    var option = { obj: obj, method: method, height: height };
                    if (value) option.value = value;

                    $.html5edit(editor, option);
                }
            }

            if (attrMethod !== "") {
                obj.invokeMethodAsync(attrMethod).then(result => {
                    for (var i in result) {
                        (function (plugin, pluginName) {
                            if (pluginName == null) {
                                return;
                            }
                            pluginObj = {};
                            pluginObj[pluginName] = function (context) {
                                var ui = $.summernote.ui;
                                context.memo('button.' + pluginName,
                                    function () {
                                        var button = ui.button({
                                            contents: '<i class="' + plugin.iconClass + '"></i>',
                                            container: "body",
                                            tooltip: plugin.tooltip,
                                            click: function () {
                                                obj.invokeMethodAsync(callback, pluginName).then(result => {
                                                    context.invoke('editor.insertText', result);
                                                });
                                            }
                                        });
                                        this.$button = button.render();
                                        return this.$button;
                                    });
                            }
                            $.extend($.summernote.plugins, pluginObj);
                        })(result[i], result[i].pluginItemName);
                    }
                    invoker();
                });
            }
            else {
                invoker();
            }
        },
        split: function (el) {
            var $split = $(el);

            var splitWidth = $split.innerWidth();
            var splitHeight = $split.innerHeight();
            var curVal = 0;
            var newVal = 0;
            var originX = 0;
            var originY = 0;
            var isVertical = !$split.children().hasClass('is-horizontal');

            $split.children().children('.split-bar').drag(
                function (e) {
                    if (isVertical) {
                        originY = e.clientY || e.touches[0].clientY;
                        curVal = $split.children().children('.split-left').innerHeight() * 100 / splitHeight;
                    }
                    else {
                        originX = e.clientX || e.touches[0].clientX;
                        curVal = $split.children().children('.split-left').innerWidth() * 100 / splitWidth;
                    }
                    $split.toggleClass('dragging');
                },
                function (e) {
                    if (isVertical) {
                        var eventY = e.clientY || e.changedTouches[0].clientY;
                        newVal = Math.ceil((eventY - originY) * 100 / splitHeight) + curVal;
                    }
                    else {
                        var eventX = e.clientX || e.changedTouches[0].clientX;
                        newVal = Math.ceil((eventX - originX) * 100 / splitWidth) + curVal;
                    }

                    if (newVal <= 0) newVal = 0;
                    if (newVal >= 100) newVal = 100;

                    $split.children().children('.split-left').css({ "flex-basis": newVal.toString() + "%" });
                    $split.children().children('.split-right').css({ "flex-basis": (100 - newVal).toString() + "%" });
                    $split.attr('data-split', newVal);
                },
                function (e) {
                    $split.toggleClass('dragging');
                });
        },
        bb_layout: function (refObj, method) {
            $('.layout-header').find('[data-toggle="tooltip"]').tooltip();

            $(window).on('resize', function () {
                calcWindow();
            });

            var calcWindow = function () {
                var width = $(window).width();
                refObj.invokeMethodAsync(method, width);
            }

            calcWindow();
        },
        bb_scroll: function (el, force) {
            var $el = $(el);

            // 移动端不需要修改滚动条
            // 苹果系统不需要修改滚动条
            var mobile = $(window).width() < 768 || navigator.userAgent.match(/Macintosh/);
            if (force || !mobile) {
                var autoHide = $el.attr('data-hide');
                var height = $el.attr('data-height');
                var width = $el.attr('data-width');

                var option = {
                    alwaysVisible: autoHide !== "true",
                };

                if (!height) height = "auto";
                if (height !== "") option.height = height;
                if (!width) option.width = width;
                $el.slimScroll(option);
            }
            else {
                $el.addClass('is-phone');
            }
        },
        markdown: function (el, method) {
            var key = 'bb_editor';
            var $el = $(el);
            if (method) {
                var editor = $el.data(key);
                if (editor) {
                    var result = editor[method]();
                    console.log(result);
                    return result;
                }
            }
            else {
                var id = $.getUID();
                $el.attr('id', id);
                var editor = editormd(id, {
                    saveHTMLToTextarea: true,
                    path: "/lib/"
                });
                $el.data(key, editor);
            }
        },
        bb_console_log: function (el) {
            var $el = $(el);
            var $body = $el.find('[data-scroll="auto"]');
            if ($body.length > 0) {
                var $win = $body.find('.console-window');
                $body.scrollTop($win.height());
            }
        },
        bb_multi_select: function (el, obj, method) {
            $(el).data('bb_multi_select', { obj: obj, method: method });
        },
        bb_tree: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });
        },
        bb_barcode: function (el, obj, method, auto) {
            var $el = $(el);
            var codeReader = new ZXing.BrowserMultiFormatReader();

            if ($el.attr('data-scan') === 'Camera') {
                codeReader.getVideoInputDevices().then((videoInputDevices) => {
                    obj.invokeMethodAsync("InitDevices", videoInputDevices).then(() => {
                        if (auto && videoInputDevices.length > 0) {
                            var button = $el.find('button[data-method="scan"]');
                            var data_method = $el.attr('data-scan');
                            if (data_method === 'Camera') button.trigger('click');
                        }
                    });
                });
            }

            $el.on('click', 'button[data-method]', function () {
                var data_method = $(this).attr('data-method');
                if (data_method === 'scan') {
                    obj.invokeMethodAsync("Start");
                    var deviceId = $el.find('.dropdown-item.active').attr('data-val');
                    var video = $el.find('video').attr('id');
                    codeReader.decodeFromVideoDevice(deviceId, video, (result, err) => {
                        if (result) {
                            $.bb_vibrate();
                            console.log(result.text);
                            obj.invokeMethodAsync("GetResult", result.text);

                            var autostop = $el.attr('data-autostop') === 'true';
                            if (autostop) {
                                codeReader.reset();
                            }
                        }
                        if (err && !(err instanceof ZXing.NotFoundException)) {
                            console.error(err)
                            obj.invokeMethodAsync("GetError", err);
                        }
                    });
                }
                else if (data_method === 'scanImage') {
                    codeReader = new ZXing.BrowserMultiFormatReader();
                    $el.find(':file').remove();
                    var $img = $('.scanner-image');
                    var $file = $('<input type="file" hidden accept="image/*">');
                    $el.append($file);

                    $file.on('change', function () {
                        if (this.files.length === 0) {
                            return;
                        }
                        var reader = new FileReader();
                        reader.onloadend = function (e) {
                            $img.attr('src', e.target.result);
                            codeReader.decodeFromImage($img[0]).then((result) => {
                                if (result) {
                                    $.bb_vibrate();
                                    console.log(result.text);
                                    obj.invokeMethodAsync("GetResult", result.text);
                                }
                            }).catch((err) => {
                                if (err) {
                                    console.log(err)
                                    obj.invokeMethodAsync("GetError", err.message);
                                }
                            })
                        };
                        reader.readAsDataURL(this.files[0]);
                    })
                    $file.trigger('click');
                }
                else if (data_method === 'close') {
                    codeReader.reset();
                    obj.invokeMethodAsync("Stop");
                }
            });
        },
        bb_camera: function (el, obj, method, auto) {
            var $el = $(el);
            navigator.mediaDevices.enumerateDevices().then(function (videoInputDevices) {
                var videoInputs = videoInputDevices.filter(function (device) {
                    return device.kind === 'videoinput';
                });
                obj.invokeMethodAsync("InitDevices", videoInputs).then(() => {
                    if (auto && videoInputs.length > 0) {
                        $el.find('button[data-method="play"]').trigger('click');
                    }
                });

                // handler button click event
                var video = $el.find('video')[0];
                var canvas = $el.find('canvas')[0];
                var context = canvas.getContext('2d');
                var mediaStreamTrack;

                $el.on('click', 'button[data-method]', function () {
                    var data_method = $(this).attr('data-method');
                    if (data_method === 'play') {
                        var front = $(this).attr('data-camera');
                        var deviceId = $el.find('.dropdown-item.active').attr('data-val');
                        var constrains = { video: { facingMode: front }, audio: false };
                        if (deviceId !== "") {
                            constrains.video.deviceId = { exact: deviceId };
                        }
                        navigator.mediaDevices.getUserMedia(constrains).then(stream => {
                            video.srcObject = stream;
                            video.play();
                            mediaStreamTrack = stream.getTracks()[0];
                            obj.invokeMethodAsync("Start");
                        }).catch(err => {
                            console.log(err)
                            obj.invokeMethodAsync("GetError", err.message)
                        });
                    }
                    else if (data_method === 'stop') {
                        video.pause();
                        video.srcObject = null;
                        mediaStreamTrack.stop();
                        obj.invokeMethodAsync("Stop");
                    }
                    else if (data_method === 'capture') {
                        context.drawImage(video, 0, 0, 300, 200);
                        var url = canvas.toDataURL();
                        console.log(url);
                        obj.invokeMethodAsync("Capture");

                        var $img = $el.find('img');
                        if ($img.length === 1) {
                            $img.attr('src', url);
                        }

                        var link = $el.find('a.download');
                        link.attr('href', url);
                        link.attr('download', new Date().format('yyyyMMddHHmmss') + '.png');
                        link[0].click();
                    }
                });
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
        bb_qrcode: function (el) {
            var $el = $(el);
            var $qr = $el.find('.qrcode-img');
            $qr.html('');
            var method = "";
            var obj = null;
            if (arguments.length === 2) method = arguments[1];
            else {
                method = arguments[2];
                obj = arguments[1];
            }
            if (method === 'generate') {
                var text = $el.find('.qrcode-text').val();
                qrcode = new QRCode($qr[0], {
                    text: text,
                    width: 128,
                    height: 128,
                    colorDark: "#000000",
                    colorLight: "#ffffff",
                    correctLevel: QRCode.CorrectLevel.H
                });
                obj.invokeMethodAsync("Generated");
            }
        },
        bb_select: function (id) {
            var $el = $('#' + id);
            var $search = $el.find('input.search-text');
            if ($search.length > 0) {
                $el.on('shown.bs.dropdown', function () {
                    $search.focus();
                });
            }
        }
    });

    $(function () {
        new MutationObserver((mutations, observer) => {
            if (document.querySelector('#components-reconnect-modal h5 a')) {
                function attemptReload() {
                    fetch('').then(() => {
                        location.reload();
                    });
                }
                observer.disconnect();
                attemptReload();
                setInterval(attemptReload, 10000);
            }
        }).observe(document.body, { childList: true, subtree: true });

        $(document)
            .on('hidden.bs.toast', '.toast', function () {
                $(this).removeClass('hide');
            })
            .on('inserted.bs.tooltip', '.is-invalid', function () {
                $('#' + $(this).attr('aria-describedby')).addClass('is-invalid');
            });

        // popover confirm
        $.fn.popover.Constructor.prototype.isWithContent = function () {
            var components = ['', 'confirm', 'datetime-picker', 'datetime-range'];
            var toggle = this.config.toggle;
            return components.indexOf(toggle) || this.getTitle() || this._getContent();
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
            if ($el.parents('.popover-datetime-range.show').length === 0) {
                $('.popover-datetime-range.show').each(function (index, ele) {
                    var pId = this.getAttribute('id');
                    if (pId) {
                        var $input = $('[aria-describedby="' + pId + '"]');
                        if ($el.parents('.datetime-range-bar').attr('aria-describedby') !== pId) $input.popover('hide');
                    }
                });
            }

            // table filter
            // 处理 Filter 中的 DateTimePicker 点击
            var $target = $(e.target);
            var $pd = $target.closest('.popover-datetime');
            if ($pd.length == 1) {
                var pid = $pd.attr('id');
                var $el = $('[aria-describedby="' + pid + '"]');
                if ($el.closest('.datetime-picker').hasClass('is-filter')) {
                    return;
                }
            }

            var $filter = $target.closest('.table-filter-item');
            if ($filter.length == 0) {
                $('.table-filter-item.show').each(function (index) {
                    var filter = $(this).data('bb_filter');
                    filter.obj.invokeMethodAsync(filter.method);
                })
            }

            // 处理 MultiSelect 弹窗
            var $select = $target.closest('.multi-select');
            $('.multi-select.show').each(function () {
                if ($select.length === 0 || this != $select[0]) {
                    var select = $(this).data('bb_multi_select');
                    select.obj.invokeMethodAsync(select.method);
                }
            });

            // 处理 Table ColumnList
            var $btn = $target.closest('.btn-col.init');
            if (!$btn.hasClass('init')) {
                var $menu = $target.closest('.dropdown-menu.dropdown-menu-right.show');
                if ($menu.length === 0) {
                    $('.table-toolbar-button .dropdown-menu.show').removeClass('show');
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

        $(document).on('keyup', function (e) {
            if (e.key === 'Enter') {
                // 关闭 TableFilter 过滤面板
                var bb = $('.table-filter .table-filter-item.show:first').data('bb_filter');
                if (bb) {
                    bb.obj.invokeMethodAsync('ConfirmByKey');
                }
            }
            else if (e.key === 'Escape') {
                // 关闭 TableFilter 过滤面板
                var bb = $('.table-filter .table-filter-item.show:first').data('bb_filter');
                if (bb) {
                    bb.obj.invokeMethodAsync('EscByKey');
                }
            }
        });
    });
})(jQuery);
