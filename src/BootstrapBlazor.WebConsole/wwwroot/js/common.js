(function ($) {
    $(function () {
        $(document)
            .on('click', '.card-footer-control', function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.toggleClass('show');
                $this.prev().toggle('show');

                // 更改自身状态
                var text = $this.hasClass('show') ? "隐藏代码" : "显示代码";
                $this.find('span').text(text);
            });
    });
})(jQuery);
