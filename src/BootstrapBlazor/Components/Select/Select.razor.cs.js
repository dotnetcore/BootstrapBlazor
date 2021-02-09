(function ($) {
    $.extend({
        bb_select: function (el, obj, method) {
            var $el = $(el);
            var $search = $el.find('input.search-text');
            if ($search.length > 0) {
                $el.on('shown.bs.dropdown', function () {
                    $search.focus();
                });
            }

            $el.on('keyup', function (e) {
                var $this = $(this);
                if ($this.hasClass('show')) {
                    var $items = $this.find('.dropdown-menu.show > .dropdown-item');
                    var $activeItem = $items.filter(function (index, ele) {
                        return $(ele).hasClass('active');
                    });

                    if (e.key === "ArrowUp") {
                        $activeItem.removeClass('active');
                        var $prev = $activeItem.prev();
                        if ($prev.length === 0) {
                            $prev = $items.last();
                        }
                        $prev.addClass('active');
                    }
                    else if (e.key === "ArrowDown") {
                        $activeItem.removeClass('active');
                        var $next = $activeItem.next();
                        if ($next.length === 0) {
                            $next = $items.first();
                        }
                        $next.addClass('active');
                    }
                    else if (e.key === "Enter") {
                        $this.removeClass('show').find('.show').removeClass('show');
                        obj.invokeMethodAsync(method, $activeItem.index());
                    }
                }
            });
        }
    });
})(jQuery);
