export function bb_qrcode(el, method, text, obj) {
    BootstrapBlazorModules.addScript('./_content/BootstrapBlazor.BarCode/modules/qrcode.min.js');

    const handler = window.setInterval(function () {
        if (typeof QRCode === 'function') {
            window.clearInterval(handler);

            dowork();
        }
    }, 100);

    const defaultConfig = {
        width: 128,
        height: 128,
        colorDark: '#000000',
        colorLight: '#ffffff'
    };

    const dowork = function () {
        const qr = el.querySelector('.qrcode-img');
        qr.innerHTML = '';
        if (method === 'generate') {
            const config = {
                ...defaultConfig,
                ...{
                    width: el.getAttribute('data-bb-width'),
                    height: el.getAttribute('data-bb-width'),
                    colorDark: el.getAttribute('data-bb-dark-color'),
                    colorLight: el.getAttribute('data-bb-light-color'),
                    text: text,
                    correctLevel: QRCode.CorrectLevel.H
                }
            }
            new QRCode(qr, config);
            obj.invokeMethodAsync('Generated');
        }
    };
}
