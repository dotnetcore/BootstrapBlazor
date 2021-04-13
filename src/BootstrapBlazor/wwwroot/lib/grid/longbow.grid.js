(function ($) {
    /**
     * Grid
     * @param {any} element
     * @param {any} options
     */
    var Grid = function (element, options) {
        this.$element = $(element);
        var rowType = this.$element.data('type');
        var colSpan = this._getColSpan(this.$element);
        var itemsPerRow = parseInt(this.$element.data('items'));
        if (isNaN(itemsPerRow)) itemsPerRow = 12;

        this.options = $.extend({ rowType, itemsPerRow, colSpan }, options);
        this.layout();
    };

    Grid.VERSION = "3.1.0";
    Grid.Author = 'argo@163.com';
    Grid.DATA_KEY = "lgb.grid";

    $.extend(Grid.prototype, {
        layout: function () {
            if (this.options.colSpan !== 0 || this.$element.data('target')) {
                this._layout_parent_row();
            }
            else {
                this._layout_column(null);
            }

            this.$element.removeClass('d-none');
        },
        _layout_column: function ($target) {
            var $el = this.$element;
            var rowType = this.options.rowType;
            var itemsPerRow = this.options.itemsPerRow;
            var $div = $('<div></div>').addClass(this.options.rowType);
            var isLabel = false;
            var $groupCell = null;
            var that = this;

            $el.children().each(function (index, ele) {
                var $ele = $(ele);
                var isRow = $ele.data('toggle') === 'row';
                if (isRow) {
                    var colSpan = that._getColSpan($ele);
                    var uId = $.getUID();

                    // 设置目标地址元素
                    $ele.attr('data-target', uId);
                    $('<div></div>').attr('data-uid', uId).addClass(that._calc(colSpan)).appendTo($div);
                }
                else {
                    isLabel = $ele.prop('tagName') === 'LABEL';

                    // 如果有 Label 表示在表单内
                    if (isLabel) {
                        if ($groupCell === null) {
                            $groupCell = $('<div></div>').addClass('form-group').addClass(that._calc());
                        }
                        $groupCell.append($ele);
                    }
                    else {
                        isLabel = false;
                        if ($groupCell == null) {
                            $groupCell = $('<div></div>').addClass(that._calc());
                        }
                        $groupCell.append($ele);
                        if ($target == null) $groupCell.appendTo($div);
                        else $groupCell.appendTo($target);
                        $groupCell = null;
                    }
                }
            });

            $el.append($div);
        },
        _layout_parent_row: function () {
            var uid = this.$element.data('target');
            var $target = $('[data-uid="' + uid + '"]');
            var $row = $('<div></div>').addClass(this.options.rowType).appendTo($target);
            this._layout_column($row);
        },
        _calc: function (colSpan) {
            var itemsPerRow = this.options.itemsPerRow;
            if (colSpan > 0) itemsPerRow = itemsPerRow * colSpan;
            var ret = "col-12";
            if (itemsPerRow !== 12) {
                ret = "col-sm-12 col-md-" + itemsPerRow;
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
