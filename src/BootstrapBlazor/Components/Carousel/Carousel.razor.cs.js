(function ($) {
    $.extend({
        bb_carousel: function (ele) {
            var $ele = $(ele).carousel();

            // focus event
            var leaveHandler = null;
            $ele.hover(function () {
                if (leaveHandler != null) window.clearTimeout(leaveHandler);

                var $this = $(this);
                var $bar = $this.find('[data-slide]');
                $bar.removeClass('d-none');
                var hoverHandler = window.setTimeout(function () {
                    window.clearTimeout(hoverHandler);
                    $this.addClass('hover');
                }, 10);
            }, function () {
                var $this = $(this);
                var $bar = $this.find('[data-slide]');
                $this.removeClass('hover');
                leaveHandler = window.setTimeout(function () {
                    window.clearTimeout(leaveHandler);
                    $bar.addClass('d-none');
                }, 300);
            });
        }
    });
})(jQuery);
