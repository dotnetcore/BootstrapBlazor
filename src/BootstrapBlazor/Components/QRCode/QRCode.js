(function ($) {
    $.extend({
        bb_qrcode: function (el, obj, method, text) {
            var $el = $(el);
            var $qr = $el.find('.qrcode-img');
            $qr.html('');
            if (method === 'generate') {
                qrcode = new QRCode($qr[0], {
                    text: text,
                    width: 128,
                    height: 128,
                    colorDark: '#000000',
                    colorLight: '#ffffff',
                    correctLevel: QRCode.CorrectLevel.H
                });
                obj.invokeMethodAsync('Generated');
            }
        },
    });
})(jQuery);
