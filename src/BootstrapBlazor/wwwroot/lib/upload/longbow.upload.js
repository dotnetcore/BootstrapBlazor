(function ($) {
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
})(jQuery);
