(function ($) {
    $.extend({
        bb_image_load_async: function (el, url) {
            if (url) {
                var $el = $(el);
                var $img = $el.children('img');
                $img.attr('src', url);
            }
        },
        bb_image_preview: function (el, prevList) {
            var $el = $(el);
            var $wrapper = $el.children('.bb-viewer-wrapper');

            // 消除 body 滚动条
            var $body = $('body').addClass('is-img-preview');

            var $prevImg = $wrapper.find('.bb-viewer-canvas > img');

            var $closeButton = $el.find('.bb-viewer-close');
            var $prevButton = $el.find('.bb-viewer-prev');
            var $nextButton = $el.find('.bb-viewer-next');
            var $zoomOut = $el.find('.fa-search-plus');
            var $zoomIn = $el.find('.fa-search-minus');
            var $fullScreen = $el.find('.btn-full-screen');
            var $zoomOut = $el.find('.fa-search-plus');
            var $rotateLeft = $el.find('.fa-rotate-left');
            var $rotateRight = $el.find('.fa-rotate-right');
            var $fullScreen = $el.find('.bb-viewer-full-screen');
            var $mask = $el.find('.bb-viewer-mask');

            // 移动到文档结尾
            $wrapper.appendTo('body').addClass('show');

            // 防止重复注册事件
            if ($wrapper.hasClass('init')) {
                return;
            }

            $wrapper.addClass('init');

            // 关闭按钮处理事件
            $closeButton.on('click', function () {
                $('body').removeClass('is-img-preview');
                // 恢复 Image
                resetImage();
                $wrapper.removeClass('show').appendTo($el);
            });

            // 上一张按钮处理事件
            $prevButton.on('click', function () {
                index--;
                if (index < 0) {
                    index = prevList.length - 1;
                }
                updateImage(index);
            });

            // 下一张按钮处理事件
            $nextButton.on('click', function () {
                index++;
                if (index >= prevList.length) {
                    index = 0;
                }
                updateImage(index);
            });

            // 更新预览图片数据源
            var index = 0;
            var updateImage = function (index) {
                resetImage();
                var url = prevList[index];
                $wrapper.find('.bb-viewer-canvas > img').attr('src', url);
            }

            // 全屏/恢复按钮功能
            $fullScreen.on('click', function () {
                resetImage();
                $wrapper.toggleClass('active');
            });

            // 放大功能
            $zoomOut.on('click', function () {
                processImage(function (scale) {
                    return scale + 0.2;
                })
            });

            // 缩小功能
            $zoomIn.on('click', function () {
                processImage(function (scale) {
                    return Math.max(0.2, scale - 0.2);
                })
            });

            // 左旋转功能
            $rotateLeft.on('click', function () {
                processImage(null, function (rotate) {
                    return rotate - 90;
                })
            });

            // 右旋转功能
            $rotateRight.on('click', function () {
                processImage(null, function (rotate) {
                    return rotate + 90;
                })
            });

            // 鼠标放大缩小
            $prevImg.on('mousewheel DOMMouseScroll', function (e) {
                e.preventDefault();
                var wheel = e.originalEvent.wheelDelta || -e.originalEvent.detail;
                var delta = Math.max(-1, Math.min(1, wheel));
                if (delta > 0) {
                    // 放大
                    processImage(function (scale) {
                        return scale + 0.015;
                    })
                }
                else {
                    // 缩小
                    processImage(function (scale) {
                        return Math.max(0.195, scale - 0.015);
                    })
                }
            });

            var originX = 0;
            var originY = 0;
            var pt = { top: 0, left: 0 };
            $prevImg.drag(
                function (e) {
                    originX = e.clientX || e.touches[0].clientX;
                    originY = e.clientY || e.touches[0].clientY;

                    // 偏移量
                    pt.top = parseInt($prevImg.css('marginTop').replace("px", ""));
                    pt.left = parseInt($prevImg.css('marginLeft').replace("px", ""));

                    this.addClass('is-drag');
                },
                function (e) {
                    if (this.hasClass('is-drag')) {
                        var eventX = e.clientX || e.changedTouches[0].clientX;
                        var eventY = e.clientY || e.changedTouches[0].clientY;

                        newValX = pt.left + Math.ceil(eventX - originX);
                        newValY = pt.top + Math.ceil(eventY - originY);

                        this.css({ "marginLeft": newValX });
                        this.css({ "marginTop": newValY });
                    }
                },
                function (e) {
                    this.removeClass('is-drag');
                }
            );

            // 点击遮罩关闭功能
            $mask.on('click', function () {
                $closeButton.trigger('click');
            });

            // 增加键盘支持
            $(document).on("keydown", function (e) {
                console.log(e.key);
                if (e.key === "ArrowUp") {
                    $zoomOut.trigger('click');
                }
                else if (e.key === "ArrowDown") {
                    $zoomIn.trigger('click');
                }
                else if (e.key === "ArrowLeft") {
                    $prevButton.trigger('click');
                }
                else if (e.key === "ArrowRight") {
                    $nextButton.trigger('click');
                }
                else if (e.key === "Escape") {
                    $closeButton.trigger('click');
                }
            });

            var getScale = function (v) {
                var scale = 1;
                if (v) {
                    var arr = v.split(' ');
                    scale = parseFloat(arr[0].split('(')[1]);
                }
                return scale;
            };

            var getRotate = function (v) {
                var rotate = 0;
                if (v) {
                    var arr = v.split(' ');
                    scale = parseFloat(arr[1].split('(')[1]);
                }
                return scale;
            };

            var processImage = function (scaleCallback, rotateCallback) {
                var transform = $prevImg[0].style.transform;
                var scale = getScale(transform);
                var rotate = getRotate(transform);
                if (scaleCallback) {
                    scale = scaleCallback(scale);
                }
                if (rotateCallback) {
                    rotate = rotateCallback(rotate);
                }
                $prevImg.css('transform', 'scale(' + scale + ') rotate(' + rotate + 'deg)');

                var left = getValue($prevImg[0].style.marginLeft);
                var top = getValue($prevImg[0].style.marginTop);
                $prevImg.css('marginLeft', left + 'px');
                $prevImg.css('marginTop', top + 'px');
            };

            var resetImage = function () {
                $prevImg.addClass('transition-none');
                $prevImg.css('transform', 'scale(1) rotate(0deg)');
                $prevImg.css('marginLeft', '0px');
                $prevImg.css('marginTop', '0px');
                window.setTimeout(function () {
                    $prevImg.removeClass('transition-none');
                }, 300);
            }

            var getValue = function (v) {
                var value = 0;
                if (v) {
                    value = parseFloat(v);
                }
                return value;
            };

            $prevImg.touchScale(function (scale) {
                processImage(function () {
                    return scale;
                });
            });
        }
    });
})(jQuery);
