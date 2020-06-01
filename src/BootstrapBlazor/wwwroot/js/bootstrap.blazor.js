(function ($) {
    window.Toasts = [];

    window.chartColors = {
        red: 'rgb(255, 99, 132)',
        blue: 'rgb(54, 162, 235)',
        green: 'rgb(75, 192, 192)',
        orange: 'rgb(255, 159, 64)',
        yellow: 'rgb(255, 205, 86)',
        tomato: 'rgb(255, 99, 71)',
        pink: 'rgb(255, 192, 203)',
        violet: 'rgb(238, 130, 238)'
    };

    window.chartOption = {
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Chart'
            },
            tooltips: {
                mode: 'index',
                intersect: false
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: ''
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: ''
                    }
                }]
            }
        }
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
                    star.call(this, e);
                }
            };

            var handleDragMove = function (e) {
                if ($.isFunction(move)) {
                    move.call(this, e);
                }
            };

            var handleDragEnd = function (e) {
                // 结束拖动
                if ($.isFunction(end)) {
                    end.call(this, e);
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
            if (!$.isFunction($.fn.summernote)) return;

            var $this = $(el);
            var op = typeof options == 'object' && options;
            if (/destroy|hide/.test(options)) {
                return $this.toggleClass('open').summernote(options);
            }
            else if (typeof options == 'string') {
                return $this.hasClass('open') ? $this.summernote(options) : $this.html();
            }
            if (!$this.hasClass('open')) {
                op = $.extend({ focus: false, lang: 'zh-CN', height: 80, dialogsInBody: true }, op);
                if (!$this.attr('data-original-title')) $this.on('click', op, function (event) {
                    var $this = $(this).tooltip('hide');
                    var op = $.extend({ placeholder: $this.attr('placeholder') }, event.data);
                    var $toolbar = $this.toggleClass('open').summernote($.extend({}, op, { focus: true }))
                        .next().find('.note-toolbar')
                        .on('click', 'button[data-method]', $this, function (event) {
                            var $btn = $(this);
                            switch ($btn.attr('data-method')) {
                                case 'submit':
                                    $btn.tooltip('dispose');
                                    event.data.toggleClass('open').summernote('destroy');
                                    break;
                            }
                        });
                    var $done = $('<div class="note-btn-group btn-group note-view note-right"><button type="button" class="note-btn note-btn-close" tabindex="-1" data-method="submit" title="完成" data-placement="bottom"><i class="fa fa-check"></i></button></div>').appendTo($toolbar).find('button').tooltip({ container: 'body' });
                    $('body').find('.note-group-select-from-files [accept="image/*"]').attr('accept', 'image/bmp,image/png,image/jpg,image/jpeg,image/gif');
                }).tooltip({ title: '点击展开编辑' });
                if (op.focus) $this.trigger('click');
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
        modal: function (el, method) {
            var $el = $(el);
            $el.modal(method);
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
        getChartOption: function (option) {
            var colors = [];
            for (var name in window.chartColors) colors.push(name);

            var config = {};
            var colorFunc = null;
            if (option.type === 'line') {
                config = $.extend(true, {}, chartOption);
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {
                        backgroundColor: color,
                        borderColor: color
                    });
                }
            }
            else if (option.type === 'bar') {
                config = $.extend(true, {}, chartOption);
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {
                        backgroundColor: Chart.helpers.color(color).alpha(0.5).rgbString(),
                        borderColor: color,
                        borderWidth: 1
                    });
                }
            }
            else if (option.type === 'pie' || option.type === 'doughnut') {
                config = $.extend(true, {}, chartOption);
                colorFunc = function (data) {
                    $.extend(data, {
                        backgroundColor: colors.slice(0, data.data.length).map(function (name) {
                            return chartColors[name];
                        })
                    });
                }

                if (option.type === 'doughnut') {
                    $.extend(config.options, {
                        cutoutPercentage: 50,
                        animation: {
                            animateScale: true,
                            animateRotate: true
                        }
                    });
                }
            }
            else if (option.type === 'bubble') {
                config = $.extend(true, {}, chartOption, {
                    data: {
                        animation: {
                            duration: 10000
                        },
                    },
                    options: {
                        tooltips: {
                            mode: 'point'
                        }
                    }
                });
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {
                        backgroundColor: Chart.helpers.color(color).alpha(0.5).rgbString(),
                        borderWidth: 1,
                        borderColor: color
                    });
                }
            }

            $.each(option.data, function () {
                colorFunc(this);
            });

            return $.extend(true, config, {
                type: option.type,
                data: {
                    labels: option.labels,
                    datasets: option.data
                },
                options: {
                    responsive: option.options.responsive,
                    title: option.options.title,
                    scales: {
                        xAxes: option.options.xAxes.map(function (v) {
                            return {
                                display: option.options.showXAxesLine,
                                scaleLabel: v
                            };
                        }),
                        yAxes: option.options.yAxes.map(function (v) {
                            return {
                                display: option.options.showYAxesLine,
                                scaleLabel: v
                            }
                        })
                    }
                }
            });
        },
        updateChart: function (config, option) {
            if (option.updateMethod === "addDataset") {
                config.data.datasets.push(option.data.datasets.pop());
            }
            else if (option.updateMethod === "removeDataset") {
                config.data.datasets.pop();
            }
            else if (option.updateMethod === "addData") {
                if (config.data.datasets.length > 0) {
                    config.data.labels.push(option.data.labels.pop());
                    config.data.datasets.forEach(function (dataset, index) {
                        dataset.data.push(option.data.datasets[index].data.pop());
                        if (option.type === 'pie' || option.type === 'doughnut') {
                            dataset.backgroundColor.push(option.data.datasets[index].backgroundColor.pop());
                        }
                    });
                }
            }
            else if (option.updateMethod === "removeData") {
                config.data.labels.pop(); // remove the label first

                config.data.datasets.forEach(function (dataset) {
                    dataset.data.pop();
                    if (option.type === 'pie' || option.type === 'doughnut') {
                        dataset.backgroundColor.pop();
                    }
                });
            }
            else if (option.updateMethod === "setAngle") {
                if (option.type === 'doughnut') {
                    if (option.angle === 360) {
                        config.options.circumference = Math.PI;
                        config.options.rotation = -Math.PI;
                    }
                    else {
                        config.options.circumference = 2 * Math.PI;
                        config.options.rotation = -Math.PI / 2;
                    }
                }
            }
            else {
                config.data.datasets.forEach(function (dataset, index) {
                    dataset.data = option.data.datasets[index].data;
                });
            }
        },
        chart: function (el, obj, method, option, updateMethod, type, angle) {
            if ($.isFunction(Chart)) {
                var $el = $(el);
                option.type = type;
                var chart = $el.data('chart');
                if (!chart) {
                    var op = $.getChartOption(option);
                    $el.data('chart', chart = new Chart(el.getElementsByTagName('canvas'), op));
                    $el.removeClass('is-loading').trigger('chart.afterInit');
                    obj.invokeMethodAsync(method);
                }
                else {
                    var op = $.getChartOption(option);
                    op.angle = angle;
                    op.updateMethod = updateMethod;
                    $.updateChart(chart.config, op);
                    chart.update();
                }
            }
        },
        collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.find('.collapse-item'), function () {
                var id = $.getUID();
                var $item = $(this);
                $item.attr('id', id);
                if (parent != null) $item.attr('data-parent', parent);

                var $button = $item.prev().find('[data-toggle="collapse"]');
                $button.attr('data-target', '#' + id).attr('aria-controls', id);

                $button.collapse();

                // expand
                if ($button.parent().hasClass('is-expanded')) {
                    var $collapse = $('#' + id);
                    $collapse.collapse("show");
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
                var $items = $el.find('.rate-item');
                var index = $items.toArray().indexOf(this);
                $items.each(function (i) {
                    if (i > index) $(this).removeClass('is-on');
                    else $(this).addClass('is-on');
                });
            });
            $el.on('mouseleave', function () {
                reset();
            });
            $el.on('click', '.rate-item', function () {
                var $items = $el.find('.rate-item');
                $el.val = $items.toArray().indexOf(this);
                $el.attr('aria-valuenow', $el.val + 1);
                obj.invokeMethodAsync(method, $el.val + 1);
            });
        },
        footer: function (el, obj) {
            var $el = $(el);
            var tooltip = $el.find('[data-toggle="tooltip"]').tooltip();
            $el.find('.footer-top').on('click', function (e) {
                e.preventDefault();
                if (obj === 'window' || obj === 'body') obj = window;
                $(obj).scrollTop(0);
                tooltip.tooltip('hide');
            });
        },
        editor: function (el, isEditor, height) {
            var option = { focus: isEditor };
            if (height) option.height = height;
            $.html5edit(el.getElementsByClassName("editor-body"), option);
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
