(function ($) {
    $.extend({
        bb_qrcode: function (el) {
            var $el = $(el);
            var $qr = $el.find('.qrcode-img');
            $qr.html('');
            var method = "";
            var obj = null;
            if (arguments.length === 2) method = arguments[1];
            else {
                method = arguments[2];
                obj = arguments[1];
            }
            if (method === 'generate') {
                var text = $el.find('.qrcode-text').val();
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
