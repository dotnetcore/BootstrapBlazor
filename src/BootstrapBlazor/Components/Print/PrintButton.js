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
                $modalBody.find(":text, :checkbox, :radio").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');
                    if (!id) {
                        $el.attr('id', $.getUID());
                    }
                });
                var printContenxt = $modalBody.html();
                var $body = $('body').addClass('bb-printview-open');
                var $dialog = $('<div></div>').addClass('bb-printview').html(printContenxt).appendTo($body);

                // assign value
                $dialog.find(":input").each(function (index, el) {
                    var $el = $(el);
                    var id = $el.attr('id');

                    if ($el.attr('type') === 'checkbox') {
                        $el.prop('checked', $('#' + id).prop('checked'));
                    }
                    else {
                        $el.val($('#' + id).val());
                    }
                });

                window.setTimeout(function () {
                    window.print();
                    $body.removeClass('bb-printview-open')
                    $dialog.remove();
                }, 50);
            }
            else {
                window.setTimeout(function () {
                    window.print();
                }, 50);
            }
        }
    });
})(jQuery);
