export function bb_qrcode(el, method, text, obj) {
    BootstrapBlazorModules.addScript('_content/BootstrapBlazor/modules/qrcode.min.js');

    var handler = window.setInterval(function () {
        if ($.isFunction(QRCode)) {
            window.clearInterval(handler);

            dowork();
        }
    }, 100);

    var dowork = function () {
        var $el = $(el);
        var $qr = $el.find('.qrcode-img');
        $qr.html('');
        if (method === 'generate') {
            new QRCode($qr[0], {
                text: text,
                width: 128,
                height: 128,
                colorDark: '#000000',
                colorLight: '#ffffff',
                correctLevel: QRCode.CorrectLevel.H
            });
            obj.invokeMethodAsync('Generated');
        }
    };
};
