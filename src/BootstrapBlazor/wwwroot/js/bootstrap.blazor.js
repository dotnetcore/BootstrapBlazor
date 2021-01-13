(function ($) {
    $.extend({
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
        footer: function (el, target) {
            var $el = $(el);
            var tooltip = $el.tooltip();
            $el.on('click', function (e) {
                e.preventDefault();
                $(target || window).scrollTop(0);
                tooltip.tooltip('hide');
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
