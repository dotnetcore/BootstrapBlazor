(function ($) {
    $.extend({
        bb_multi_select: function (el, method) {
            var input = el.querySelector('.dropdown-toggle');
            var isBootstrapDrop = input.getAttribute('data-bs-toggle') === 'dropdown';
            if (!isBootstrapDrop) {
                var p = bb.Popover.getOrCreateInstance(el.querySelector('.dropdown-toggle'), {
                    css: 'popover-multi-select'
                });
                if (method) {
                    p.invoke(method);
                }
            }
        }
    });
})(jQuery);
