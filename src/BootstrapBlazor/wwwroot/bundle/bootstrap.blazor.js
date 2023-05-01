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
        }
    });

    $.fn.extend({
        drag: function (start, move, end) {
            var $this = $(this);

            var handleDragStart = function (e) {
                e.preventDefault();
                e.stopPropagation();

                document.addEventListener('mousemove', handleDragMove);
                document.addEventListener('touchmove', handleDragMove);
                document.addEventListener('mouseup', handleDragEnd);
                document.addEventListener('touchend', handleDragEnd);

                if ($.isFunction(start)) {
                    start.call($this, e);
                }
            };

            var handleDragMove = function (e) {
                if (e.touches && e.touches.length > 1) {
                    return;
                }

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

    var load = function (targetName, callback, interval) {
        if (!interval) {
            interval = 100;
        }
        if (!window[targetName]) {
            var handler = window.setInterval(function () {
                if (!!window[targetName]) {
                    window.clearInterval(handler);

                    callback();
                }
            }, interval);
        }
        else {
            callback();
        }
    };

    var addScript = function (content) {
        // content 文件名
        const links = [...document.getElementsByTagName('script')];
        var link = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('script');
            link.setAttribute('src', content);
            document.body.appendChild(link);
        }
    };

    var removeScript = function (content) {
        const links = [...document.getElementsByTagName('script')];
        var nodes = links.filter(function (link) {
            return link.src.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.body.removeChild(nodes[index]);
        }
    }

    var addLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var link = links.filter(function (link) {
            return link.href.indexOf(href) > -1;
        });
        if (link.length === 0) {
            link = document.createElement('link');
            link.setAttribute('href', href);
            link.setAttribute("rel", "stylesheet");
            document.getElementsByTagName("head")[0].appendChild(link);
        }
    }

    var removeLink = function (href) {
        const links = [...document.getElementsByTagName('link')];
        var nodes = links.filter(function (link) {
            return link.href.indexOf(content) > -1;
        });
        for (var index = 0; index < nodes.length; index++) {
            document.getElementsByTagName("head")[0].removeChild(nodes[index]);
        }
    }

    window.BootstrapBlazorModules = {
        load: load,
        addScript: addScript,
        removeScript: removeScript,
        addLink: addLink,
        removeLink: removeLink
    };
})(jQuery);

(function ($) {
    /**
     * Grid
     * @param {any} element
     * @param {any} options
     */
    var Grid = function (element, options) {
        this.$element = $(element);
        var colSpan = this._getColSpan(this.$element);
        var rowType = this.$element.data('type');
        var itemsPerRow = parseInt(this.$element.data('items'));
        if (isNaN(itemsPerRow)) itemsPerRow = 12;

        this.options = $.extend({ rowType, itemsPerRow, colSpan }, options);
        this.layout();
    };

    Grid.VERSION = "5.1.0";
    Grid.Author = 'argo@163.com';
    Grid.DATA_KEY = "lgb.grid";

    $.extend(Grid.prototype, {
        layout: function () {
            this._layout_column(null);
            this.$element.removeClass('d-none');
        },
        _layout_column: function ($target) {
            var $el = this.$element;
            var rowType = this.options.rowType;
            var itemsPerRow = this.options.itemsPerRow;
            var isLabel = false;
            var $groupCell = null;
            var that = this;
            var $div = $('<div class="row g-3"></div>');
            if (rowType === "inline") $div.addClass('form-inline');

            $el.children().each(function (index, ele) {
                var $ele = $(ele);
                var isRow = $ele.data('toggle') === 'row';
                var colSpan = that._getColSpan($ele);
                if (isRow) {
                    $('<div></div>').addClass(that._calc(colSpan)).appendTo($div).append($ele);
                }
                else {
                    isLabel = $ele.prop('tagName') === 'LABEL';

                    // 如果有 Label 表示在表单内
                    if (isLabel) {
                        if ($groupCell === null) {
                            $groupCell = $('<div></div>').addClass(that._calc(colSpan));
                        }
                        $groupCell.append($ele);
                    }
                    else {
                        isLabel = false;
                        if ($groupCell == null) {
                            $groupCell = $('<div></div>').addClass(that._calc(colSpan));
                        }
                        $groupCell.append($ele);
                        if ($target == null) $groupCell.appendTo($div);
                        else $groupCell.appendTo($target);
                        $groupCell = null;
                    }
                }
            });

            if ($target == null) {
                $el.append($div);
            }
        },
        _layout_parent_row: function () {
            var uid = this.$element.data('target');
            var $target = $('[data-uid="' + uid + '"]');
            var $row = $('<div class="row"></div>').appendTo($target);
            this._layout_column($row);
        },
        _calc: function (colSpan) {
            var itemsPerRow = this.options.itemsPerRow;
            if (colSpan > 0) itemsPerRow = itemsPerRow * colSpan;
            var ret = "col-12";
            if (itemsPerRow !== 12) {
                ret = "col-12 col-sm-" + itemsPerRow;
            }
            return ret;
        },
        _getColSpan: function ($el) {
            var colSpan = parseInt($el.data('colspan'));
            if (isNaN(colSpan)) colSpan = 0;
            return colSpan;
        }
    });

    function GridPlugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(Grid.DATA_KEY);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(Grid.DATA_KEY, data = new Grid(this, options));
        });
    }

    $.fn.grid = GridPlugin;
    $.fn.grid.Constructor = Grid;
})(jQuery);

(function ($) {
    /**
     * Tab
     * @param {any} element
     * @param {any} options
     */
    var Tab = function (element, options) {
        this.$element = $(element);
        this.$header = this.$element.children('.tabs-header');
        this.$wrap = this.$header.children('.tabs-nav-wrap');
        this.$scroll = this.$wrap.children('.tabs-nav-scroll');
        this.$tab = this.$scroll.children('.tabs-nav');
        this.options = $.extend({}, options);
        this.init();
    };

    Tab.VERSION = "5.1.0";
    Tab.Author = 'argo@163.com';
    Tab.DATA_KEY = "lgb.Tab";

    var _proto = Tab.prototype;
    _proto.init = function () {
        var that = this;
        $(window).on('resize', function () {
            that.resize();
        });
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

        var $bar = this.$tab.children('.tabs-active-bar');
        var $activeTab = this.$tab.children('.tabs-item.active');
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

    $.fn.lgbTab = TabPlugin;
    $.fn.lgbTab.Constructor = Tab;
})(jQuery);

(function () {
    $.extend({
        bb_cascader_hide: function (el) {
            const dropdownEl = document.getElementById(el);
            const dropdown = bootstrap.Dropdown.getInstance(dropdownEl);
            dropdown.hide();
        }
    });
})();

(function ($) {
    $.extend({
        bb_collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.children('.accordion-item'), function () {
                var $item = $(this);
                var $body = $item.children('.accordion-collapse');
                var id = $body.attr('id');
                if (!id) {
                    id = $.getUID();
                    $body.attr('id', id);
                    if (parent != null) {
                        $body.attr('data-bs-parent', parent);
                    }

                    var $button = $item.find('[data-bs-toggle="collapse"]');
                    $button.attr('data-bs-target', '#' + id).attr('aria-controls', id);
                }
            });

            $el.find('.tree .tree-item > .fa').on('click', function (e) {
                var $parent = $(this).parent();
                $parent.find('[data-bs-toggle="collapse"]').trigger('click');
            });

            // support menu component
            if ($el.parent().hasClass('menu')) {
                $el.on('click', '.nav-link:not(.collapse)', function () {
                    var $this = $(this);
                    $el.find('.active').removeClass('active');
                    $this.addClass('active');

                    // parent
                    var $card = $this.closest('.accordion');
                    while ($card.length > 0) {
                        $card.children('.accordion-header').find('.nav-link').addClass('active');
                        $card = $card.parent().closest('.accordion');
                    }
                });
            }
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_gotop: function (el, target) {
            var $el = $(el);
            var tooltip = $el.tooltip();
            $el.on('click', function (e) {
                e.preventDefault();
                $(target || window).scrollTop(0);
                tooltip.tooltip('hide');
            });
        }
    });
})(jQuery);

(function ($) {
    // private functions
    var backup = function (index) {
        var input = this;
        if (index !== undefined) {
            input.prevValues[index] = $($(input).find(".ipv4-cell")[index]).val();
        } else {
            $(input).find(".ipv4-cell").each(function (i, cell) {
                input.prevValues[i] = $(cell).val();
            });
        }
    };

    var revert = function (index) {
        var input = this;
        if (index !== undefined) {
            $($(input).find(".ipv4-cell")[index]).val(input.prevValues[index]);
        } else {
            $(input).find(".ipv4-cell").each(function (i, cell) {
                $(cell).val(input.prevValues);
            });
        }
    };

    var selectCell = function (index) {
        var input = this;
        if (index === undefined && index < 0 && index > 3) return;
        $($(input).find(".ipv4-cell")[index]).focus();
    };

    var isValidIPStr = function (ipString) {
        if (typeof ipString !== "string") return false;

        var ipStrArray = ipString.split(".");
        if (ipStrArray.length !== 4) return false;

        return ipStrArray.reduce(function (prev, cur) {
            if (prev === false || cur.length === 0) return false;
            return (Number(cur) >= 0 && Number(cur) <= 255) ? true : false;
        }, true);
    };

    var getCurIPStr = function () {
        var str = "";
        this.find(".ipv4-cell").each(function (index, element) {
            str += (index == 0) ? $(element).val() : "." + $(element).val();
        });
        return str;
    };

    // function for text input cell
    var getCursorPosition = function () {
        var cell = this;
        if ('selectionStart' in cell) {
            // Standard-compliant browsers
            return cell.selectionStart;
        } else if (document.selection) {
            // IE
            cell.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -cell.value.length);
            return sel.text.length - selLen;
        }
        throw new Error("cell is not an input");
    };

    $.extend({
        bb_ipv4_input: function (ele) {
            var $ele = $(ele);
            ele.prevValues = [];

            $ele.find(".ipv4-cell").focus(function () {
                $(this).select(); // input select all when tab in
                $ele.toggleClass("selected", true);
            });

            $ele.find(".ipv4-cell").focusout(function () {
                $ele.toggleClass("selected", false);
            });

            $ele.find(".ipv4-cell").each(function (index, cell) {
                $(cell).keydown(function (e) {
                    if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {	// numbers, backup last status
                        backup.call(ele, index);
                    }
                    else if (e.keyCode == 37 || e.keyCode == 39) {	// left key ,right key
                        if (e.keyCode == 37 && getCursorPosition.call(cell) === 0) {
                            selectCell.call(ele, index - 1);
                            e.preventDefault();
                        }
                        else if (e.keyCode == 39 && getCursorPosition.call(cell) === $(cell).val().length) {
                            selectCell.call(ele, index + 1);
                            e.preventDefault();
                        }
                    }
                    else if (e.keyCode == 9) {	// allow tab
                    }
                    else if (e.keyCode == 8 || e.keyCode == 46) {	// allow backspace, delete
                    }
                    else {
                        e.preventDefault();
                    }
                });

                $(cell).keyup(function (e) {
                    if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {	// numbers
                        var val = $(this).val();
                        var num = Number(val);

                        if (num > 255)
                            revert.call(ele, index);
                        else if (val.length > 1 && val[0] === "0")
                            revert.call(ele, index);
                        else if (val.length === 3)
                            selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'Backspace') {
                        if ($(this).val() === '') {
                            $(this).val('0');
                            var i = index - 1;
                            if (i > -1) {
                                selectCell.call(ele, i)
                            }
                            else {
                                selectCell.call(ele, 0)
                            }
                        }
                    }
                    if (e.key === '.') {
                        selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'ArrowRight') {
                        selectCell.call(ele, index + 1)
                    }
                    if (e.key === 'ArrowLeft') {
                        selectCell.call(ele, index - 1)
                    }
                });
            });
        }
    });
}(jQuery));

(function(e){var t=function(t){var n=this;t!==undefined?n.prevValues[t]=e(e(n).find(".ipv4-cell")[t]).val():e(n).find(".ipv4-cell").each(function(t,r){n.prevValues[t]=e(r).val()})},n=function(t){var n=this;t!==undefined?e(e(n).find(".ipv4-cell")[t]).val(n.prevValues[t]):e(n).find(".ipv4-cell").each(function(t,r){e(r).val(n.prevValues)})},r=function(t){var n=this;if(t===undefined&&t<0&&t>3)return;e(e(n).find(".ipv4-cell")[t]).focus()},i=function(e){if(typeof e!="string")return!1;var t=e.split(".");return t.length!==4?!1:t.reduce(function(e,t){return e===!1||t.length===0?!1:Number(t)>=0&&Number(t)<=255?!0:!1},!0)},s=function(){var t="";return this.find(".ipv4-cell").each(function(n,r){t+=n==0?e(r).val():"."+e(r).val()}),t},o=function(){var e=this;if("selectionStart"in e)return e.selectionStart;if(document.selection){e.focus();var t=document.selection.createRange(),n=document.selection.createRange().text.length;return t.moveStart("character",-e.value.length),t.text.length-n}throw new Error("cell is not an input")};e.fn.ipv4_input=function(u,a){this.each(function(){if(e(this).hasClass("ipv4-input"))return;var i=this;i.prevValues=[],e(i).toggleClass("ipv4-input",!0),e(i).html('<input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" /><label class="ipv4-dot">.</label><input type="text" class="ipv4-cell" />'),e(this).find(".ipv4-cell").focus(function(){e(this).select(),e(i).toggleClass("selected",!0)}),e(this).find(".ipv4-cell").focusout(function(){e(i).toggleClass("selected",!1)}),e(this).find(".ipv4-cell").each(function(s,u){e(u).keydown(function(n){n.keyCode>=48&&n.keyCode<=57||n.keyCode>=96&&n.keyCode<=105?t.call(i,s):n.keyCode==37||n.keyCode==39?n.keyCode==37&&o.call(u)===0?(r.call(i,s-1),n.preventDefault()):n.keyCode==39&&o.call(u)===e(u).val().length&&(r.call(i,s+1),n.preventDefault()):n.keyCode!=9&&n.keyCode!=8&&n.keyCode!=46&&n.preventDefault()}),e(u).keyup(function(t){if(t.keyCode>=48&&t.keyCode<=57||t.keyCode>=96&&t.keyCode<=105){var o=e(this).val(),u=Number(o);u>255?n.call(i,s):o.length>1&&o[0]==="0"?n.call(i,s):o.length===3&&r.call(i,s+1)}})})});var f=function(t,n){t=="rwd"&&(n===undefined?this.toggleClass("rwd"):this.toggleClass("rwd",n));if(t=="value"){if(n===undefined)return s.call(this);if(!i(n))throw new Error("invalid ip address");var r=n.split(".");this.find(".ipv4-cell").each(function(t,n){e(n).val(r[t])})}return t=="valid"?i(s.call(this)):(t=="clear"&&this.find(".ipv4-cell").each(function(t,n){e(n).val("")}),this)},l=this;if(e.type(u)==="object"){var c=u;for(var h in c)f.call(this,h,u[h])}else l=f.call(this,u,a);return l}})(jQuery);
(function ($) {
    $.extend({
        bb_printview: function (el) {
            var $el = $(el);
            var $modalBody = $el.parentsUntil('.modal-content').parent().find('.modal-body');
            if ($modalBody.length > 0) {
                $modalBody.find(":text, :checkbox, :radio").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');
                    if (!id) {
                        $el.attr('id', $.getUID());
                    }
                });
                var printContenxt = $modalBody.html();
                var $body = $('body').addClass('bb-printview-open');
                var $dialog = $('<div></div>').addClass('bb-printview').html(printContenxt).appendTo($body);

                // assign value
                $dialog.find(":input").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');

                    if ($el.attr('type') === 'checkbox') {
                        $el.prop('checked', $('#' + id).prop('checked'));
                    }
                    else {
                        $el.val($('#' + id).val());
                    }
                });

                window.setTimeout(function () {
                    window.print();
                    $body.removeClass('bb-printview-open')
                    $dialog.remove();
                }, 50);
            }
            else {
                window.setTimeout(function () {
                    window.print();
                }, 50);
            }
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_reconnect: function () {
            var reconnectHandler = window.setInterval(function () {
                var $com = $('#components-reconnect-modal');
                if ($com.length > 0) {
                    var cls = $com.attr("class");
                    if (cls === 'components-reconnect-show') {
                        window.clearInterval(reconnectHandler);

                        async function attemptReload() {
                            await fetch('');
                            location.reload();
                        }
                        attemptReload();
                        setInterval(attemptReload, 5000);
                    }
                }
            }, 2000);
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_row: function (el) {
            var $el = $(el);
            $el.grid();
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_split: function (el) {
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
                    splitWidth = $split.innerWidth();
                    splitHeight = $split.innerHeight();
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

                    $split.children().children('.split-left').css({ 'flex-basis': newVal.toString() + '%' });
                    $split.children().children('.split-right').css({ 'flex-basis': (100 - newVal).toString() + '%' });
                    $split.attr('data-split', newVal);
                },
                function (e) {
                    $split.toggleClass('dragging');
                });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tab: function (el) {
            var $el = $(el);
            var handler = window.setInterval(function () {
                if ($el.is(':visible')) {
                    window.clearInterval(handler);
                    $el.lgbTab('active');
                }
            }, 200);
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_setTitle: function (title) {
            document.title = title;
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tree: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });

            // 支持 Radio
            $el.on('click', '.tree-node', function () {
                var $node = $(this);
                var $prev = $node.prev();
                var $radio = $prev.find('[type="radio"]');
                if ($radio.attr('disabeld') !== 'disabled') {
                    $radio.trigger('click');
                }
            });
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_tree_view: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });

            // 支持 Radio
            $el.on('click', '.tree-node', function () {
                var $node = $(this);
                var $prev = $node.prev();
                var $radio = $prev.find('[type="radio"]');
                if ($radio.attr('disabeld') !== 'disabled') {
                    $radio.trigger('click');
                }
            });
        }
    });
})(jQuery);
