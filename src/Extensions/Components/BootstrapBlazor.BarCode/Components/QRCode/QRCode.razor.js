import {addScript} from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

const generate = b => {
    if (b._qrcode === undefined) {
        const config = {
            ...{
                width: 128,
                height: 128,
                colorDark: '#000000',
                colorLight: '#ffffff'
            },
            ...{
                height: b._height,
                width: b._height,
                text: b._content,
                colorDark: b._colorDark,
                colorLight: b._colorLight,
                correctLevel: QRCode.CorrectLevel.H
            }
        }
        b._qrcode = new QRCode(b._qr, config)
    } else {
        b._qrcode.clear()
        b._qrcode.makeCode(b._content)
    }
    b._invoker.invokeMethodAsync(b._invokerMethod)
}

const clear = b => {
    b._qrcode.clear()
    const img = b._qr.querySelector('img')
    img.src = ''
}

export async function init(el, invoker, content, callback) {
    await addScript('./_content/BootstrapBlazor.BarCode/qrcode.min.js')

    const b = {}
    Data.set(el, b)

    b._invoker = invoker
    b._invokerMethod = callback
    b._element = document.getElementById(el)
    b._qr = b._element.querySelector('.qrcode-img')
    b._content = content
    b._height = b._element.getAttribute('data-bb-width')
    b._colorLight = b._element.getAttribute('data-bb-color-light')
    b._colorDark = b._element.getAttribute('data-bb-color-dark')

    generate(b)
}

export function update(el, content) {
    const b = Data.get(el)

    b._content = content
    b._height = b._element.getAttribute('data-bb-width')
    b._colorLight = b._element.getAttribute('data-bb-color-light')
    b._colorDark = b._element.getAttribute('data-bb-color-dark')

    if (b._content != null && b._content.length > 0) {
        generate(b)
    } else {
        clear(b)
    }
}

export function dispose(el) {
    Data.remove(el)
}
