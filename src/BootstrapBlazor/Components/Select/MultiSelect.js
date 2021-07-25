(function ($) {
    $.extend({
        bb_multi_select: function (el, obj, method) {
            $(el).data('bb_multi_select', { obj: obj, method: method });
        },
    });

    $(function () {
        $(document).on('click', function (e) {
            var $el = $(e.target);

            // 处理 MultiSelect 弹窗
            var $select = $el.closest('.multi-select');
            $('.multi-select.show').each(function () {
                if ($select.length === 0 || this != $select[0]) {
                    var select = $(this).data('bb_multi_select');
                    select.obj.invokeMethodAsync(select.method);
                }
            });
        });
    });
})(jQuery);
