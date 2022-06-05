(function ($) {
    $.extend({
        bb_ribbon: function (id, obj, method) {
            if (window.bb_ribbons === undefined) {
                window.bb_ribbons = {};
            }
            window.bb_ribbons[id] = { obj, method };
        },
    });

    $(function () {
        $(document)
            .on('click', function (e) {
                var $ele = $(e.target);
                var $ribbon = $('.ribbon-tab');
                if ($ribbon.hasClass('is-expand')) {
                    var parent = $ele.closest('.ribbon-tab').length === 0;
                    if (parent) {
                        $ribbon.toArray().forEach(function (item) {
                            var id = item.id;
                            if (id) {
                                var ribbon = window.bb_ribbons[id];
                                if (ribbon) {
                                    ribbon.obj.invokeMethodAsync(ribbon.method);
                                }
                            }
                        });
                    }
                }
            });
    });
})(jQuery);
