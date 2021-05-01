(function ($) {
    $.extend({
        bb_side_menu_collapsed: function (el) {
            var $el = $(el);
            if ($el.hasClass('is-collapsed')) {
                $el.find('.collapse').each(function (index, ele) {
                    var $ele = $(ele);
                    if ($ele.data('bs.collapse')) {
                        $ele.collapse('dispose');
                    }
                    $ele.removeAttr('data-parent').removeClass('collapse show');
                    var $link = $ele.prev();
                    if ($link.hasClass('nav-link')) {
                        $link.attr('href', "#").removeAttr('data-toggle').removeAttr('aria-expanded').removeClass('collapsed');
                    }
                });
            }
            else {
                $.bb_side_menu(el);
            }
        },
        bb_side_menu_expand: function (el, expand) {
            if (expand) {
                $(el).find('.collapse').collapse('show');
            }
            else {
                $(el).find('.collapse').collapse('hide');
                var handler = window.setTimeout(function () {
                    window.clearTimeout(handler);
                    $.bb_auto_expand($(el));
                }, 400);
            }
        },
        bb_auto_expand: function ($el) {
            // 自动展开
            var actives = $el.find('.nav-link.expand')
                .map(function (index, ele) {
                    return $(ele).removeClass('expand');
                })
                .toArray();
            var $link = $el.find('.active');
            do {
                var $ul = $link.parentsUntil('.submenu.collapse').parent();
                if ($ul.length === 1 && $ul.not('.show')) {
                    $link = $ul.prev();
                    if ($link.length !== 0) {
                        actives.push($link);
                    }
                }
                else {
                    $link = null;
                }
            }
            while ($link != null && $link.length > 0);

            while (actives.length > 0) {
                $link = actives.shift();
                $link.trigger('click');
            }
        },
        bb_init_side_menu: function ($el) {
            var accordion = $el.hasClass('accordion');
            var $root = $el.children('.submenu');
            $root.find('.submenu').each(function (index, ele) {
                var $ul = $(this);
                $ul.addClass('collapse').removeClass('d-none');
                if (accordion) {
                    var $li = $ul.parentsUntil('.submenu')
                    if ($li.prop('nodeName') === 'LI') {
                        var rootId = $li.parent().attr('id');
                        $ul.attr('data-parent', '#' + rootId);
                    }
                }
                else {
                    $ul.removeAttr('data-parent');
                }

                var ulId = $ul.attr('id');
                var $link = $ul.prev();
                $link.attr('data-toggle', 'collapse');
                $link.attr('href', '#' + ulId);
            });
            var collapses = $root.find('.collapse');
            collapses.each(function (index, ele) {
                var $ele = $(ele);
                if ($ele.data('bs.collapse')) {
                    $ele.collapse('dispose');
                }
                var parent = '';
                if (accordion) parent = $ele.attr('data-parent');
                $ele.collapse({ parent: parent, toggle: false });
            });
        },
        bb_side_menu: function (el) {
            var $el = $(el);

            // 初始化组件
            $.bb_init_side_menu($el);

            // 自动展开
            $.bb_auto_expand($(el));
        }
    });
})(jQuery);
