(function ($) {
    $.extend({
        bb_filter: function (el, obj, method) {
            $(el).data('bb_filter', { obj: obj, method: method });
        }
    });

    $(function () {
        $(document).on('click', function (e) {
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
        });

        $(document).on('keyup', function (e) {
            if (e.key === 'Enter') {
                // 关闭 TableFilter 过滤面板
                var $filter = $('.table-filter .table-filter-item.show:first');
                var bb = $filter.data('bb_filter');
                if (bb) {
                    $filter.removeClass('show');
                    bb.obj.invokeMethodAsync('ConfirmByKey');
                }
            }
            else if (e.key === 'Escape') {
                // 关闭 TableFilter 过滤面板
                var $filter = $('.table-filter .table-filter-item.show:first');
                var bb = $filter.data('bb_filter');
                if (bb) {
                    $filter.removeClass('show');
                    bb.obj.invokeMethodAsync('EscByKey');
                }
            }
        });
    });
})(jQuery);
