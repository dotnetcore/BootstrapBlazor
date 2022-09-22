(function ($) {
    $.extend({
        bb_multi_select: function (el, method) {
            var $el = $(el);
            var $input = $el.find('.dropdown-toggle');
            var menu = ".dropdown-menu";
            if (method === 'init') {
                $input.popover({
                    toggle: 'multi-select'
                })
                    .on('show.bs.popover', function () {
                        var disabled = $input.hasClass("disabled");
                        if (!disabled) {
                            this.setAttribute('aria-expanded', 'true');
                        }
                        return !disabled;
                    })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $body = $pop.find('.popover-body');
                            if ($body.length === 0) {
                                $body = $('<div class="popover-body"></div>').appendTo($pop);
                            }
                            $body.addClass('show').append($el.find(menu));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find(menu);
                            $el.append($picker);

                            this.setAttribute('aria-expanded', 'false');
                        }
                    });
            }
            else {
                $input.popover(method);
            }
        }
    });
})(jQuery);
