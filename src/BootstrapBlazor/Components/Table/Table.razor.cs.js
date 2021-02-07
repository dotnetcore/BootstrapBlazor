(function () {
    $.extend({
        bb_table_search: function (el, obj, searchMethod, clearSearchMethod) {
            $(el).data('bb_table_search', { obj: obj, searchMethod, clearSearchMethod });
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
        bb_table_load: function (el, method) {
            var $el = $(el);
            var $loader = $el.find('.table-loader');
            if (method === 'show')
                $loader.addClass('show');
            else
                $loader.removeClass('show');
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
    });
})(jQuery);
