import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"

export class BlazorQRCode extends BlazorComponent {
    _init() {
        // arguments list: methodName content callbackFn
        BootstrapBlazorModules.addScript('./_content/BootstrapBlazor.BarCode/modules/qrcode.min.js')

        const handler = window.setInterval(() => {
            if (typeof QRCode === 'function') {
                window.clearInterval(handler);

                dowork()
            }
        }, 100)

        this._qr = this._element.querySelector(this._config.QRSELECTOR)
        this._invokerName = this._config.arguments[3]

        const dowork = () => {
            this._qr.innerHTML = ''
            this._execute(this._config.arguments)
        }
    }

    _execute(args) {
        const obj = args[0]
        const method = args[1]
        const text = args[2]
        if (method === 'generate') {
            if (this._qrcode === undefined) {
                const config = {
                    ...this._config,
                    ...{
                        height: this._config.width,
                        text: text,
                        correctLevel: QRCode.CorrectLevel.H
                    }
                }
                this._qrcode = new QRCode(this._qr, config)
            }
            else {
                this._qrcode.clear()
                this._qrcode.makeCode(text)
            }
            obj.invokeMethodAsync(this._invokerName)
        }
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
