export function bb_qrcode(el, method, text, obj) {
    BootstrapBlazorModules.addScript('./_content/BootstrapBlazor.BarCode/modules/qrcode.min.js');

    const handler = window.setInterval(function () {
        if (typeof QRCode === 'function') {
            window.clearInterval(handler);

            dowork();
        }
    }, 100);

    const dowork = function () {
        var qr = el.querySelector('.qrcode-img');
        qr.innerHTML = '';
        if (method === 'generate') {
            new QRCode(qr, {
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
