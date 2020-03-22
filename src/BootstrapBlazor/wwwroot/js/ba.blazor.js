(function ($) {
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
        activeMenu: function (id) {
            var $curMenu = $('.sidebar .active').first();

            // set website title
            $('head title').text($curMenu.text());
            this.resetTab(id);
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
        initDocument: function () {
            $('body').removeClass('trans-mute');
            $('[data-toggle="tooltip"]').tooltip();
            $('.sidebar').addNiceScroll().autoScrollSidebar();
        },
        toggleModal: function (modalId) {
            $(modalId).modal('toggle');
        },
        showToast: function (title, message, cate) {
            var cateToCss = function (c) {
                var ret = "";
                switch (c) {
                    case "Success":
                        ret = "fa fa-check-circle text-success";
                        break;
                    case "Information":
                        ret = "fa fa-exclamation-triangle text-info";
                        break;
                    case "Error":
                    default:
                        ret = "fa fa-times-circle text-danger";
                        break;
                }
                return ret;
            }
            var toastTemplate = '<div class="toast fade toast-bottom-right" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="true" data-delay="4200">';
            toastTemplate += '<div class="toast-header">';
            toastTemplate += '<div class="toast-bar"><i class="' + cateToCss(cate) + '"></i></div>';
            toastTemplate += '<strong class="mr-auto">' + title + '</strong>';
            toastTemplate += '<button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">';
            toastTemplate += '<span aria-hidden="true">&times;</span>';
            toastTemplate += '</button>';
            toastTemplate += '</div><div class="toast-body">';
            toastTemplate += message;
            toastTemplate += '</div>';
            toastTemplate += '<div class="toast-progress"></div>';
            toastTemplate += '</div>';

            // 利用 js 生成一个临时 toast 弹窗后自我销毁
            var $toast = $(toastTemplate).appendTo('body');
            var handler = window.setTimeout(function () {
                window.clearTimeout(handler);
                $toast.toast('show');
            }, 200);

            var handlerDismiss = window.setTimeout(function () {
                window.clearTimeout(handlerDismiss);
                //$toast.remove();

                // 回调重新排列方法
            }, 4400);
        },
        tooltip: function (id, method) {
            var $ele = $(id);
            if (method === 'enable') {
                $ele.tooltip();
                $ele.parents('form').find('.invalid:first').focus();
            }
            else $ele.tooltip(method);
        },
        submitForm: function (btn) {
            $(btn).parent().prev().find('form :submit').click();
        },
        toggleBlazor: function (show) {
            var $blazor = $('header .nav .dropdown-mvc').parent();
            if (show) $blazor.removeClass('d-none');
            else $blazor.addClass('d-none');
        },
        setWebSettings: function (showSidebar, showCardTitle, fixedTableHeader) {
            var $tabContent = $('section .tab-content');
            if (showCardTitle) $tabContent.removeClass('no-card-header');
            else $tabContent.addClass('no-card-header');
        },
        resetTableWidth: function (source, target) {
            // 设置表格宽度
            target.width(source.width());

            // 设置各列宽度
            var $heads = target.find('th');
            source.find('th').each(function (index, element) {
                var header = $heads.get(index);
                $(header).width($(element).width());
            });
        },
        resetTableHeight: function (source) {
            var table = source;
            var height = source.parents('.bootstrap-table').position().top;
            height = $(window).height() - height - 184 - 51 - 45 - 37;
            table.height(height);
        },
        initTable: function (id, firstRender) {
            var $table = $('#' + id);
            var $fixedBody = $table.parents('.fixed-table-body');

            // 固定表头设置
            if ($fixedBody.length === 1) {
                if (firstRender) {
                    // calc height
                    $.resetTableHeight($fixedBody);

                    // modify scroll
                    $table.parent().overlayScrollbars({
                        className: 'os-theme-dark',
                        scrollbars: {
                            autoHide: 'leave',
                            autoHideDelay: 100
                        }
                    });
                }

                var $tableContainer = $table.parents('.table-wrapper');
                var $tableHeader = $tableContainer.find('.fixed-table-header table');
                $.resetTableWidth($table, $tableHeader);

                if (firstRender) {
                    $tableContainer.removeClass('table-fixed').find('.fixed-table-body').removeClass('invisible');

                    $(window).on('resize', function () {
                        $.resetTableWidth($table, $tableHeader);
                        $.resetTableHeight($fixedBody);
                    });
                }
            }

            // set search toolbar
            if (firstRender) {
                var $search = $table.parents('.bootstrap-table').find('.fixed-table-toolbar').find('.search');
                if ($search.length === 1) {
                    $searchInput = $search.find('.search-input').tooltip({
                        sanitize: false,
                        title: '<div class="search-input-tooltip">输入任意字符串全局搜索 </br> <kbd>Enter</kbd> 搜索 <kbd>ESC</kbd> 清除搜索</div>',
                        html: true
                    });

                    // 支持键盘回车搜索
                    $searchInput.on('keyup', function (event) {
                        if (event.keyCode === 13 || event.keyCode === 27) {
                            // ENTER
                            var $buttons = $(this).next();
                            var $search = $buttons.find(':first');
                            if ($search.length === 1) {
                                if (event.keyCode === 13) {
                                    $search.trigger('click');
                                }
                                else $search.next().trigger('click');
                            }
                        }
                    });
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
    });
})(jQuery);
