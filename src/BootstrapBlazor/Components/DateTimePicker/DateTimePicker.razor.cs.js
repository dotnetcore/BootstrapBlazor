(function ($) {
    $.extend({
        bb_datetimePicker: function (el, method) {
            var $el = $(el);
            var placement = $el.attr('data-placement') || 'auto';
            var $input = $el.find('.datetime-picker-input');
            if (!method) {
                $input.popover({
                    toggle: 'datetime-picker',
                    placement: placement,
                    template: '<div class="popover popover-datetime" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'
                })
                    .on('inserted.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            $pop.find('.popover-body').append($el.find('.date-picker').removeClass('d-none'));
                        }
                    })
                    .on('hide.bs.popover', function () {
                        var pId = this.getAttribute('aria-describedby');
                        if (pId) {
                            var $pop = $('#' + pId);
                            var $picker = $pop.find('.date-picker');
                            $pop.find('.popover-body').append($picker.clone());
                            $el.append($picker.addClass('d-none'));
                        }
                    });

                $('.datetime-picker-input-icon').on('click', function (e) {
                    // handler disabled event
                    if ($(this).hasClass('disabled')) return;

                    e.stopImmediatePropagation();
                    var $input = $(this).parents('.datetime-picker-bar').find('.datetime-picker-input');
                    $input.trigger('click');
                });

                $('.disabled .cell').on('click', function (e) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                });
            }
            else $input.popover(method);
        }
    });

    $(document).on('click', function (e) {
        var $el = $(e.target);

        // datetime picker
        if ($el.parents('.popover-datetime.show').length === 0) {
            $('.popover-datetime.show').each(function (index, ele) {
                var pId = this.getAttribute('id');
                if (pId) {
                    var $input = $('[aria-describedby="' + pId + '"]');
                    if ($el.attr('aria-describedby') !== pId) $input.popover('hide');
                }
            });
        }
    });
})(jQuery);
