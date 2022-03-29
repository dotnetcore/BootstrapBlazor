(function ($) {
    $.extend({
        bb_iconList: function (el, obj, method) {
            var $el = $(el);
            $('body').scrollspy({ offset: 150, target: '.fa-nav' });
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
            $el.on('click', '.icon-item', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var $this = $(this);
                var $i = $(this).find('i');
                var text = $i.attr('class');
                var dialog = $el.hasClass('is-dialog');
                if (dialog) {
                    obj.invokeMethodAsync(method, text);
                }
                else {
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
        }
    });
})(jQuery);
