(function ($) {
    $.extend({
        bb_collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.children('.card').children('.collapse-item'), function () {
                var $item = $(this);
                var id = $item.attr('id');
                if (!id) {
                    id = $.getUID();
                    $item.attr('id', id);
                    if (parent != null) {
                        $item.attr('data-parent', parent);
                    }

                    var $button = $item.prev().find('[data-toggle="collapse"]');
                    $button.attr('data-target', '#' + id).attr('aria-controls', id);
                }
            });

            $el.find('.tree .tree-item > .fa').on('click', function (e) {
                var $parent = $(this).parent();
                $parent.find('[data-toggle="collapse"]').trigger('click');
            });

            // support menu component
            if ($el.parent().hasClass('menu')) {
                $el.on('click', '.nav-link:not(.collapse)', function () {
                    var $this = $(this);
                    $el.find('.active').removeClass('active');
                    $this.addClass('active');

                    // parent
                    var $card = $this.closest('.card');
                    while ($card.length > 0) {
                        $card.children('.card-header').children('.card-header-wrapper')
                            .find('.nav-link').addClass('active');
                        $card = $card.parent().closest('.card');
                    }
                });
            }
        }
    });
})(jQuery);
