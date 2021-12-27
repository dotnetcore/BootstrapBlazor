(function ($) {
    $.extend({
        bb_card_collapse: function (el) {
            var $ele = $(el);
            $ele.on('click', function (e) {
                var $card = $(this).toggleClass('is-open');
                var $body = $card.closest('.card').find('.card-body');
                if ($body.length === 1) {
                    if ($body.is(':hidden')) {
                        $body.parent().toggleClass('collapsed')
                    }
                    $body.slideToggle('fade', function () {
                        var $this = $(this);
                        if ($this.is(':hidden')) {
                            $this.parent().toggleClass('collapsed')
                        }
                    });
                }
            });
        }
    });
})(jQuery);