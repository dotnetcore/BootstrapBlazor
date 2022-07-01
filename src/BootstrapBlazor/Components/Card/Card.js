(function ($) {
    $.extend({
        bb_card_collapse: function (el) {
            var $ele = $(el);
            var status = $ele.attr("data-bs-collapsed") === "true";
            if (status) {
                $ele.removeClass('is-open');
                $ele.closest('.card').find('.card-body').css({ display: "none" });
            }
            $ele.on('click', function (e) {
                if (e.target.nodeName === 'BUTTON') {
                    return;
                }
                var parentButton = $(e.target).closest('button');
                if (parentButton.length !== 0) {
                    return;
                }
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
