(function ($) {
    $.extend({
        bb_timespanPicker: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-bs-placement') || 'auto';
            var $input = $el.find('.timespan-picker-input');
            if (!method) {
                $input.popover({
                    toggle: 'timespan-picker',
                    placement: placement
                })
                    .on('show.bs.popover', function () {
                        var disabled = $(this).parent().hasClass('disabled');
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
                            $body.append($el.find('.timespan-picker').removeClass('d-none'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.timespan-picker');
                            $el.append($picker.addClass('d-none'));
                        }
                    });

                $el.find('.timespan-picker-input-icon').on('click', function (e) {
                    // handler disabled event
                    if ($(this).hasClass('disabled')) return;

                    e.stopImmediatePropagation();
                    var $input = $(this).parents('.timespan-picker-bar').find('.timespan-picker-input');
                    $input.trigger('click');
                });

                $el.find('.date-table .cell').on('click', function (e) {
                    if ($(e.target).parent().parent().hasClass('disabled')) {
                        e.preventDefault();
                        e.stopImmediatePropagation();
                    }
                });
            }
            else $input.popover(method);
        }
    });
})(jQuery);
