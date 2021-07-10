(function ($) {
    $.extend({
        bb_collapse: function (el) {
            var $el = $(el);
            var parent = null;
            // check accordion
            if ($el.hasClass('is-accordion')) {
                parent = '[' + el.getAttributeNames().pop() + ']';
            }

            $.each($el.children('.accordion-item'), function () {
                var $item = $(this);
                var $body = $item.children('.accordion-collapse');
                var id = $body.attr('id');
                if (!id) {
                    id = $.getUID();
                    $body.attr('id', id);
                    if (parent != null) {
                        $body.attr('data-bs-parent', parent);
                    }

                    var $button = $item.find('[data-bs-toggle="collapse"]');
                    $button.attr('data-bs-target', '#' + id).attr('aria-controls', id);
                }
            });

            $el.find('.tree .tree-item > .fa').on('click', function (e) {
                var $parent = $(this).parent();
                $parent.find('[data-bs-toggle="collapse"]').trigger('click');
            });

            // support menu component
            if ($el.parent().hasClass('menu')) {
                $el.on('click', '.nav-link:not(.collapse)', function () {
                    var $this = $(this);
                    $el.find('.active').removeClass('active');
                    $this.addClass('active');

                    // parent
                    var $card = $this.closest('.accordion');
                    while ($card.length > 0) {
                        $card.children('.accordion-header').find('.nav-link').addClass('active');
                        $card = $card.parent().closest('.accordion');
                    }
                });
            }
        }
    });
})(jQuery);
