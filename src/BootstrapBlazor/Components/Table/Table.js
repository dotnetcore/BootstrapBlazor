(function ($) {
    $.extend({
        bb_table_search: function (el, obj, searchMethod, clearSearchMethod) {
            $(el).data('bb_table_search', { obj: obj, searchMethod, clearSearchMethod });
        },
        bb_table_row_hover: function ($ele) {
            var $toolbar = $ele.find('.table-excel-toolbar');

            var $rows = $ele.find('tbody > tr').each(function (index, row) {
                $(row).hover(
                    function () {
                        var top = $(this).position().top;
                        $toolbar.css({ 'top': top + 'px', 'display': 'block' });
                    },
                    function () {
                        $toolbar.css({ 'top': top + 'px', 'display': 'none' });
                    }
                );
            });
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

                                var $table = $(colgroup).closest('table');
                                if ($table.parent().hasClass('table-fixed-header')) {
                                    $table.width(tableWidth + marginX);
                                }
                                else {
                                    $table.width(tableWidth + marginX - 6);
                                }
                            });
                        },
                        function () {
                            eff.call(this, false);
                        }
                    );
                });
            }
        },
        bb_table_load: function (el, method) {
            var $el = $(el);
            var $loader = $el.find('.table-loader');
            if (method === 'show')
                $loader.addClass('show');
            else
                $loader.removeClass('show');
        },
        bb_table_filter_calc: function ($ele) {
            // filter
            var $toolbar = $ele.find('.table-toolbar');
            var marginTop = 0;
            if ($toolbar.length > 0) marginTop = $toolbar.outerHeight(true);

            // position
            var $this = $(this);
            var position = $this.position();
            var field = $this.attr('data-field');
            var $body = $ele.find('.table-filter-item[data-field="' + field + '"]');
            var $th = $this.closest('th');
            var $thead = $th.closest('thead');
            var rowHeight = $thead.outerHeight(true) - $th.outerHeight(true);
            var left = $th.outerWidth(true) + $th.position().left - $body.outerWidth(true) / 2;
            var marginRight = 0;
            var isFixed = $th.hasClass('fixed');
            if ($th.hasClass('sortable')) marginRight = 24;
            if ($th.hasClass('filterable')) marginRight = marginRight + 12;

            // 判断是否越界
            var scrollLeft = 0;
            if (!isFixed) {
                scrollLeft = $th.closest('table').parent().scrollLeft();
            }
            var margin = $th.offset().left + $th.outerWidth(true) - marginRight + $body.outerWidth(true) / 2 - $(window).width();
            marginRight = marginRight + scrollLeft;
            if (margin > 0) {
                left = left - margin - 16;

                // set arrow
                $arrow = $body.find('.card-arrow');
                $arrow.css({ 'left': 'calc(50% - 0.5rem + ' + (margin + 16) + 'px)' });
            }

            var searchHeight = $ele.find('.table-search').outerHeight(true);
            if (searchHeight === undefined) {
                searchHeight = 0;
            }
            else {
                searchHeight += 8;
            }
            $body.css({ "top": position.top + marginTop + rowHeight + searchHeight + 50, "left": left - marginRight });
        },
        bb_table_filter: function ($ele) {
            // 点击 filter 小按钮时计算弹出位置
            $ele.on('click', '.filterable .fa-filter', function () {
                $.bb_table_filter_calc.call(this, $ele);
            });
        },
        bb_table_getCaretPosition: function (ele) {
            var result = -1;
            var startPosition = ele.selectionStart;
            var endPosition = ele.selectionEnd;
            if (startPosition == endPosition) {
                if (startPosition == ele.value.length)
                    result = 1;
                else if (startPosition == 0) {
                    result = 0;
                }
            }
            return result;
        },
        bb_table_excel_keybord: function ($ele) {
            var isExcel = $ele.find('.table-excel').length > 0;
            if (isExcel) {
                var KeyCodes = {
                    TAB: 9,
                    ENTER: 13,
                    SHIFT: 16,
                    CTRL: 17,
                    ALT: 18,
                    ESCAPE: 27,
                    SPACE: 32,
                    PAGE_UP: 33,
                    PAGE_DOWN: 34,
                    END: 35,
                    HOME: 36,
                    LEFT_ARROW: 37,
                    UP_ARROW: 38,
                    RIGHT_ARROW: 39,
                    DOWN_ARROW: 40
                };

                var setFocus = function ($target) {
                    var handler = window.setTimeout(function () {
                        window.clearTimeout(handler);
                        $target.focus();
                        $target.select();
                    }, 10);
                }

                var activeCell = function ($cells, index) {
                    var ret = false;
                    var td = $cells[index];
                    var $target = $(td).find('input.form-control:not([readonly]');
                    if ($target.length > 0) {
                        setFocus($target);
                        ret = true;
                    }
                    return ret;
                };
                var moveCell = function ($input, keyCode) {
                    var $td = $input.closest('td');
                    var $tr = $td.closest('tr');
                    var $cells = $tr.children('td');
                    var index = $cells.index($td);
                    if (keyCode == KeyCodes.LEFT_ARROW) {
                        while (index-- > 0) {
                            if (activeCell($cells, index)) {
                                break;
                            }
                        }
                    }
                    else if (keyCode == KeyCodes.RIGHT_ARROW) {
                        while (index++ < $cells.length) {
                            if (activeCell($cells, index)) {
                                break;
                            }
                        }
                    }
                    else if (keyCode == KeyCodes.UP_ARROW) {
                        $cells = $tr.prev().children('td');
                        while (index < $cells.length) {
                            if (activeCell($cells, index)) {
                                break;
                            }
                        }
                    }
                    else if (keyCode == KeyCodes.DOWN_ARROW) {
                        $cells = $tr.next().children('td');
                        while (index < $cells.length) {
                            if (activeCell($cells, index)) {
                                break;
                            }
                        }
                    }
                }
                $ele.on('keydown', function (e) {
                    var $input = $(e.target);
                    switch (e.keyCode) {
                        case KeyCodes.UP_ARROW:
                        case KeyCodes.LEFT_ARROW:
                            if ($.bb_table_getCaretPosition(e.target) == 0) {
                                moveCell($input, e.keyCode);
                            }
                            break;
                        case KeyCodes.DOWN_ARROW:
                        case KeyCodes.RIGHT_ARROW:
                            if ($.bb_table_getCaretPosition(e.target) == 1) {
                                moveCell($input, e.keyCode);
                            }
                            break;
                    };
                });
            }
        },
        bb_table_width: function (el, args) {
            var $ele = $(el);
            var width = 0;
            if (args) width = $ele.outerWidth(true);
            else width = $(window).outerWidth(true);
            return width;
        },
        bb_table_tooltip: function (el) {
            var $ele = $(el);
            var $tooltips = $ele.find('.is-tips');
            $tooltips.tooltip('dispose');
            $tooltips.tooltip({
                container: 'body',
                title: function () {
                    return $(this).text();
                }
            });
        },
        bb_table_fixedbody: function ($ele, $body, $thead) {
            // 尝试自适应高度
            if (!$body) {
                $body = $ele.find('.table-fixed-body');
            }
            if (!$thead) {
                $thead = $ele.find('.table-fixed-header');
            }
            var searchHeight = $ele.find('.table-search:first').outerHeight(true);
            if (!searchHeight) {
                searchHeight = 0;
            }
            var paginationHeight = $ele.find('.table-pagination:first').outerHeight(true);
            if (!paginationHeight) {
                paginationHeight = 0;
            }
            var toolbarHeight = $ele.find('.table-toolbar:first').outerHeight(true);
            var bodyHeight = paginationHeight + toolbarHeight + searchHeight;
            if (bodyHeight > 0) {
                if (searchHeight > 0) {
                    //记住历史height，用于展开搜索框时先设置一次高度
                    //再重新计算，避免高度超出父容器，出现滚动条
                    var lastHeight = $body.parent().css("height");
                    $ele.find('.table-search-collapse').each(function () {
                        $(this).data('fixed-height', lastHeight);
                    });
                }
                $body.parent().css({ height: "calc(100% - " + bodyHeight + "px)" });
            }

            var headerHeight = $thead.outerHeight(true);
            if (headerHeight > 0) {
                $body.css({ height: "calc(100% - " + headerHeight + "px)" })
            }
        },
        bb_table: function (el, obj, method, args) {
            var handler = window.setInterval(function () {
                var $table = $(el).find('.table');
                if ($table.length !== 0) {
                    window.clearInterval(handler);
                    $.bb_table_init(el, obj, method, args);
                }
            }, 100);
        },
        bb_table_init: function (el, obj, method, args) {
            var $ele = $(el);
            var fixedHeader = $ele.find('.table-fixed').length > 0;
            if (fixedHeader) {
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
                            if ($.browser.versions.mobile) {
                                margin = (parseFloat(margin) - 6) + 'px';
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

                // 尝试自适应高度
                $.bb_table_fixedbody($ele, $body, $thead);

                // 固定表头的最后一列禁止列宽调整
                $ele.find('.col-resizer:last').remove();
            }

            // sort
            var $tooltip = $ele.find('.table-cell.is-sort .table-text');
            var tooltipTitle = args;

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

            $ele.children('.table-scroll').scroll(function () {
                $ele.find('.table-filter-item.show').each(function () {
                    var fieldName = $(this).attr('data-field');
                    var icon = $ele.find('.fa-filter[data-field="' + fieldName + '"]')[0];
                    $.bb_table_filter_calc.call(icon, $ele);
                });
            });
            $.bb_table_row_hover($ele);

            $.bb_table_tooltip(el);
            $.bb_table_filter($ele);
            $.bb_table_resize($ele);
            $.bb_table_excel_keybord($ele);

            $ele.on('click', '.table-search-collapse', function (e) {
                var $card = $(this).toggleClass('is-open');
                var $body = $card.closest('.card').find('.card-body');
                if ($body.length === 1) {
                    if ($body.is(':hidden')) {
                        //设置历史高度，避免高度超出父容器，出现滚动条
                        if (fixedHeader) {
                            $ele.find('.table-fixed-body')
                                .parent()
                                .css({ height: $card.data('fixed-height') });
                        }
                        $body.parent().toggleClass('collapsed')
                    }
                    $body.slideToggle('fade', function () {
                        var $this = $(this);
                        if ($this.is(':hidden')) {
                            $this.parent().toggleClass('collapsed')
                        }
                        // 尝试自适应高度
                        if (fixedHeader) {
                            $.bb_table_fixedbody($ele);
                        }
                    });
                }
            });
        }
    });

    $(function () {
        $(document).on('keyup', function (e) {
            if (e.key === 'Enter') {
                var $table = $(e.target).closest('.table-container');
                var bb = $table.data('bb_table_search');
                if (bb) {
                    bb.obj.invokeMethodAsync(bb.searchMethod);
                }
            }
            else if (e.key === 'Escape') {
                var $table = $(e.target).closest('.table-container');
                var bb = $table.data('bb_table_search');
                if (bb) {
                    bb.obj.invokeMethodAsync(bb.clearSearchMethod);
                }
            }
        });
        $(document).on('click', function (e) {
            // column list handler
            var $target = $(e.target);

            // skip click dropdown item
            var $menu = $target.closest('.dropdown-menu.show');
            if ($menu.length > 0) {
                return;
            }

            // skip click column list button
            var $button = $target.closest('.btn-col');
            if ($button.length > 0) {
                return;
            }

            $('.table-toolbar > .btn-group > .btn-col > .dropdown-toggle.show').each(function (index, ele) {
                $(ele).trigger('click');
            });
        });
    });
})(jQuery);
