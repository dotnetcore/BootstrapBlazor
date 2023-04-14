import Data from '../../../BootstrapBlazor/modules/data.js'

export class BlazorQRCode extends BlazorComponent {
    _init() {
        // arguments list: methodName content callbackFn
        BootstrapBlazorModules.addScript('./_content/BootstrapBlazor.BarCode/modules/qrcode.min.js')

        this._invoker = this._config.arguments[0]
        this._content = this._config.arguments[1]
        this._invokerMethod = this._config.arguments[2]
        this._qr = this._element.querySelector(this._config.QRSELECTOR)

        const handler = window.setInterval(() => {
            if (typeof QRCode === 'function') {
                window.clearInterval(handler)

                dowork()
            }
        }, 100)

        const dowork = () => {
            this._generate()
        }
    }

    _execute(args) {
        this._content = args[1]
        if (this._content.length > 0) {
            this._generate()
        }
        else {
            this._clear()
        }
    }

    _clear() {
        this._qrcode.clear()
        const img = this._qr.querySelector('img')
        img.src = ''
    }

    _generate() {
        if (this._qrcode === undefined) {
            const config = {
                ...this._config,
                ...{
                    height: this._config.width,
                    text: this._content,
                    correctLevel: QRCode.CorrectLevel.H
                }
            }
            this._qrcode = new QRCode(this._qr, config)
        }
        else {
            this._qrcode.clear()
            this._qrcode.makeCode(this._content)
        }
        this._invoker.invokeMethodAsync(this._invokerMethod)
    }

    static get Default() {
        return {
            width: 128,
            height: 128,
            colorDark: '#000000',
            colorLight: '#ffffff',
            QRSELECTOR: '.qrcode-img'
        }
    }
} 
