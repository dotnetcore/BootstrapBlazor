import { addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

const generate = (b, content) => {
    b._qrcode.clear()
    b._qrcode.makeCode(content)

    b._invoker.invokeMethodAsync(b._invokerMethod)
}

const clear = b => {
    b._qrcode.clear()
    const img = b._qr.querySelector('img')
    if (img) {
        img.src = ''
    }
}

export async function init(el, invoker, content, callback) {
    await addScript('./_content/BootstrapBlazor.BarCode/qrcode.min.js')

    const b = {}
    Data.set(el, b)

    b._invoker = invoker
    b._invokerMethod = callback
    b._element = el
    b._qr = b._element.querySelector('.qrcode-img')

    b._height = b._element.getAttribute('data-bb-width')
    b._colorLight = b._element.getAttribute('data-bb-color-light')
    b._colorDark = b._element.getAttribute('data-bb-color-dark')

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
            colorDark: b._colorDark,
            colorLight: b._colorLight,
            correctLevel: QRCode.CorrectLevel.H
        }
    }
    b._qrcode = new QRCode(b._qr, config)

    if (content && content.length > 0) {
        generate(b, content)
    }
}

export function update(el, content) {
    const b = Data.get(el)

    if (content && content.length > 0) {
        generate(b, content)
    } else {
        clear(b)
    }
}

export function dispose(el) {
    Data.remove(el)
}
