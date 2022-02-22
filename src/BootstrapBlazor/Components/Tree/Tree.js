(function ($) {
    $.extend({
        bb_tree: function (el) {
            var $el = $(el);
            $el.find('.tree-content').hover(function () {
                $(this).parent().addClass('hover');
            }, function () {
                $(this).parent().removeClass('hover');
            });

            // 支持 Radio
            $el.on('click', '.tree-node', function () {
                var $node = $(this);
                var $prev = $node.prev();
                var $radio = $prev.find('[type="radio"]');
                if ($radio.attr('disabeld') !== 'disabled') {
                    $radio.trigger('click');
                }
            });
        }
    });
})(jQuery);
