(function ($) {
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
            if (window.Toasts === undefined) window.Toasts = [];

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

            var tooltip = function () {
                $ele.find('.is-tips').tooltip({
                    container: 'body',
                    title: function () {
                        return $(this).text();
                    }
                });
            }

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

                var calcPosition = function () {
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
                };

                // 点击 filter 小按钮时计算弹出位置
                $ele.find('.filterable .fa-filter').on('click', function () {
                    calcPosition.call(this);
                });

                tooltip();

                $ele.children('.table-scroll').scroll(function () {
                    $ele.find('.table-filter-item.show').each(function () {
                        var fieldName = $(this).attr('data-field');
                        var filter = $ele.find('.fa-filter[data-field="' + fieldName + '"]')[0];
                        calcPosition.call(filter);
                    });
                });

                $.bb_table_resize($ele);
            }
            else if (method === 'width') {
                var width = 0;
                if (args) width = $ele.outerWidth(true);
                else width = $(window).outerWidth(true);
                return width;
            }
            else if (method === 'tooltip') {
                tooltip();
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
                                                    context.invoke('editor.pasteHTML', result);
                                                });
                                            }
                                        });
                                        this.$button = button.render();
                                        return this.$button;
                                    });
                            }
                            $.extend($.summernote.plugins, pluginObj);
                        })(result[i], result[i].buttonName);
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
        },
        bb_drawer: function (el, open) {
            var $el = $(el);
            if (open) {
                $el.addClass('is-open');
                $('body').addClass('overflow-hidden');
            }
            else {
                if ($el.hasClass('is-open')) {
                    $el.removeClass('is-open').addClass('is-close');
                    var handler = window.setTimeout(function () {
                        window.clearTimeout(handler);
                        $el.removeClass('is-close');
                        $('body').removeClass('overflow-hidden');
                    }, 350);
                }
            }
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
