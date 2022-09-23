(function ($) {
    $.extend({
        bb_iconList: function (el, obj, method, showDialog, copy) {
            var $el = $(el);
            $el.find('[data-bs-spy="scroll"]').scrollspy();
            $el.on('click', '.nav-link', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var targetId = $(this).attr('href');
                var $target = $(targetId);
                var container = window;
                var margin = $target.offset().top;
                var marginTop = $target.css('marginTop').replace('px', '');
                if (marginTop) {
                    margin = margin - parseInt(marginTop);
                }
                $(container).scrollTop(margin);
            });
            $el.on('click', '.icons-body a', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var $this = $(this);
                var $i = $(this).find('i');
                var text = $i.attr('class');
                obj.invokeMethodAsync(method, text);
                var dialog = $el.hasClass('is-dialog');
                if (dialog) {
                    obj.invokeMethodAsync(showDialog, text);
                }
                else if (copy) {
                    $.bb_copyIcon($this, text);
                }
            });
        },
        bb_iconDialog: function (el) {
            var $el = $(el);
            $el.on('click', 'button', function (e) {
                var $this = $(this);
                var text = $this.prev().text();
                $.bb_copyIcon($this, text);
            });
        },
        bb_copyIcon: function ($this, text) {
            $.bb_copyText(text);
            $this.tooltip({
                title: 'Copied!'
            });
            $this.tooltip('show');
            var handler = window.setTimeout(function () {
                window.clearTimeout(handler);
                $this.tooltip('dispose');
            }, 1000);
        },
        bb_scrollspy: function () {
            var $el = $('.icon-list [data-bs-spy="scroll"]');
            $el.scrollspy('refresh');
        }
    });
})(jQuery);
