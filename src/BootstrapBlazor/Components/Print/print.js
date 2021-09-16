(function ($) {
    $.extend({
        bb_printview: function (el) {
            var $modalHeader = $(el).parent();
            var inHeader = $modalHeader.hasClass('modal-header');
            var inFooter = $modalHeader.hasClass('modal-footer');
            var $modalBody = null;
            if (inHeader) {
                $modalBody = $modalHeader.next();
            }
            else if (inFooter) {
                $modalBody = $modalHeader.parentsUntil('.modal-content').parent().find('.modal-body');
            }
            if ($modalBody != null && $modalBody.length > 0) {
                var printContenxt = $modalBody.html();
                var $body = $('body').addClass('bb-printview-open');
                var $dialog = $('<div></div>').addClass('bb-printview').html(printContenxt).appendTo($body);

                // create mask
                var $mask = $('<div class="bb-print-mask"></div>').appendTo($body);
                window.print();
                $body.removeClass('bb-printview-open')
                $dialog.remove();
                $mask.remove();
            }
            else {
                window.print();
            }
        }
    });
})(jQuery);
