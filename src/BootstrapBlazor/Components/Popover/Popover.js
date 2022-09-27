(function ($) {
    $.extend({
        bb_confirm_submit: function (id) {
            let $ele = $('#' + id);
            let $submit = $('<button type="submit" hidden />');
            $submit.appendTo($ele.parent());
            $submit.trigger('click');
            $submit.remove();
        },
        bb_popover: function (id, method, title, content, placement, html, trigger, css) {
            let ele = document.getElementById(id);
            let instance = bootstrap.Popover.getInstance(ele);
            if (instance) {
                instance.dispose();
            }
            if (method !== 'dispose') {
                let op = {html, sanitize: false, title, content, placement, trigger};
                if (css !== '') {
                    op.customClass = css;
                }
                instance = new bootstrap.Popover(ele, op);
                if (method !== '') {
                    $(ele).popover(method);
                }
            }
        }
    });
})(jQuery);
