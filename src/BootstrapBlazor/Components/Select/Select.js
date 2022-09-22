(function ($) {
    $.extend({
        bb_select: function (el, obj, method, init) {
            var $el = $(el);
            var $input = $el.find('.dropdown-toggle');
            if (init === 'init') {
                var $search = $el.find('input.search-text');

                var bb_scrollToActive = function ($el) {
                    var $menu = $el.find('.dropdown-menu');
                    var $activeItem = $menu.children('.preActive');
                    if ($activeItem.length === 0) {
                        $activeItem = $menu.children('.active');
                        $activeItem.addClass('preActive');
                    }
                    if ($activeItem.length === 1) {
                        var height = $menu.innerHeight();
                        var itemHeight = $activeItem.outerHeight();
                        var index = $activeItem.index() + 1;
                        var margin = 11 + itemHeight * index - height;
                        if (margin >= 0) {
                            $menu.scrollTop(margin);
                        }
                        else {
                            $menu.scrollTop(0);
                        }
                    }
                };

                $el.on('show.bs.dropdown', function (e) {
                    if ($el.find('.form-select').prop('disabled')) {
                        e.preventDefault();
                    }
                });

                $el.on('shown.bs.dropdown', function () {
                    if ($search.length > 0) {
                        $search.focus();
                    }
                    bb_scrollToActive($(this));
                });

                $el.on('keyup', function (e) {
                    e.stopPropagation();

                    var $this = $(this);
                    if ($this.find('.dropdown-toggle').hasClass('show')) {
                        var $items = $this.find('.dropdown-menu.show > .dropdown-item').not('.disabled, .search');

                        var $activeItem = $items.filter(function (index, ele) {
                            return $(ele).hasClass('preActive');
                        });

                        if ($items.length > 1) {
                            if (e.key === "ArrowUp") {
                                $activeItem.removeClass('preActive');
                                var $prev = $activeItem.prev().not('.disabled, .search');
                                if ($prev.length === 0) {
                                    $prev = $items.last();
                                }
                                $prev.addClass('preActive');
                                bb_scrollToActive($this);
                            }
                            else if (e.key === "ArrowDown") {
                                $activeItem.removeClass('preActive');
                                var $next = $activeItem.next().not('.disabled, .search');
                                if ($next.length === 0) {
                                    $next = $items.first();
                                }
                                $next.addClass('preActive');
                                bb_scrollToActive($this);
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

                var menu = ".dropdown-menu";
                $input.popover({
                    toggle: 'dropdown'
                })
                    .on('show.bs.popover', function () {
                        var disabled = $(this).find('.form-select').is(":disabled");
                        if (!disabled) {
                            this.setAttribute('aria-expanded', 'true');
                            this.classList.add('show');
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
                            this.classList.remove('show');
                        }
                    });
            }
            else {
                $input.popover(init);
            }
        },
        bb_select_tree(el) {
            var $el = $(el);
            $el.trigger('click');
        }
    });
})(jQuery);
