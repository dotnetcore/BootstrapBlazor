(function ($) {
    $.extend({
        bb_rate: function (el, obj, method) {
            var $el = $(el);
            $el.val = parseInt($el.attr('aria-valuenow'));
            var reset = function () {
                var $items = $el.find('.rate-item');
                $items.each(function (i) {
                    if (i > $el.val) $(this).removeClass('is-on');
                    else $(this).addClass('is-on');
                });
            };

            $el.on('mouseenter', '.rate-item', function () {
                if (!$el.hasClass('disabled')) {
                    var $items = $el.find('.rate-item');
                    var index = $items.toArray().indexOf(this);
                    $items.each(function (i) {
                        if (i > index) $(this).removeClass('is-on');
                        else $(this).addClass('is-on');
                    });
                }
            });
            $el.on('mouseleave', function () {
                if (!$el.hasClass('disabled')) {
                    reset();
                }
            });
            $el.on('click', '.rate-item', function () {
                if (!$el.hasClass('disabled')) {
                    var $items = $el.find('.rate-item');
                    $el.val = $items.toArray().indexOf(this);
                    $el.attr('aria-valuenow', $el.val + 1);
                    obj.invokeMethodAsync(method, $el.val + 1);
                }
            });
        }
    });
})(jQuery);
