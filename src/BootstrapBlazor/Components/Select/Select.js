(function ($) {
    $.extend({
        bb_select: function (el, obj, method) {
            var $el = $(el);
            var $search = $el.find('input.search-text');

            $el.on('show.bs.dropdown', function (e) {
                if ($el.find('.form-select').prop('disabled')) {
                    e.preventDefault();
                }
            });

            $el.on('shown.bs.dropdown', function () {
                if ($search.length > 0) {
                    $search.focus();
                }
            });

            $el.on('keyup', function (e) {
                var $this = $(this);
                if ($this.find('.dropdown-toggle').hasClass('show')) {
                    var $items = $this.find('.dropdown-menu.show > .dropdown-item').not('.disabled, .search');

                    var $activeItem = $items.filter(function (index, ele) {
                        return $(ele).hasClass('active');
                    });

                    if ($items.length > 1) {
                        if (e.key === "ArrowUp") {
                            $activeItem.removeClass('active');
                            var $prev = $activeItem.prev().not('.disabled, .search');
                            if ($prev.length === 0) {
                                $prev = $items.last();
                            }
                            $prev.addClass('active');
                        }
                        else if (e.key === "ArrowDown") {
                            $activeItem.removeClass('active');
                            var $next = $activeItem.next().not('.disabled, .search');
                            if ($next.length === 0) {
                                $next = $items.first();
                            }
                            $next.addClass('active');
                        }
                    }

                    if (e.key === "Enter") {
                        $this.find('.show').removeClass('show');
                        var index = $activeItem.index();
                        if ($this.find('.search').length > 0) {
                            index--;
                        }
                        obj.invokeMethodAsync(method, index);
                    }
                }
            });

            $el.on('click', '.dropdown-item.disabled', function (e) {
                e.stopImmediatePropagation();
            });
        }
    });
})(jQuery);
