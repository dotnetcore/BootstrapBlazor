(function ($) {
    $.extend({
        bb_tooltip: function (id, method, title, placement, html, trigger) {
            var ele = document.getElementById(id);
            var instance = bootstrap.Tooltip.getInstance(ele);
            if (method === 'dispose') {
                if (instance) {
                    instance.dispose();
                }
            }
            else {
                var op = { html: html, sanitize: !html, title: title, placement: placement, trigger: trigger };
                instance = new bootstrap.Tooltip(ele, op);
                var $ele = $(ele);
                if (method === 'enable') {
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
                else {
                    $ele.tooltip(method);
                }
            }
        },
    });

    $(function () {
        $(document)
            .on('inserted.bs.tooltip', '.is-invalid', function () {
                $('#' + $(this).attr('aria-describedby')).addClass('is-invalid');
            });
    });
})(jQuery);
