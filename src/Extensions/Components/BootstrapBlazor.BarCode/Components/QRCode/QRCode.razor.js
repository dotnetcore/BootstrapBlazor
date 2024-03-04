import { addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

const generate = (b, content) => {
    if (!b.qrcode) {
        b.qrcode = new QRCode(b.qr, b.config)
    }
    b.qrcode.makeCode(content)
    b.invoke.invokeMethodAsync(b.callback)
}

const clear = b => {
    if (b.qrcode) {
        b.qrcode.clear()
        delete b.qrcode
        const img = b.qr.querySelector('img')
        if (img) {
            img.remove()
        }
    }
}

export async function init(el, invoke, content, callback) {
    await addScript('./_content/BootstrapBlazor.BarCode/qrcode.min.js')

    const b = {
        el, invoke, callback,
        qr: el.querySelector('.qrcode-img'),
        height: el.getAttribute('data-bb-width'),
        colorLight: el.getAttribute('data-bb-color-light'),
        colorDark: el.getAttribute('data-bb-color-dark')
    }
    Data.set(el, b)

    b.config = {
        ...{
            width: 128,
            height: 128,
            colorDark: '#000000',
            colorLight: '#ffffff'
        },
        ...{
            height: b.height,
            width: b.height,
            colorDark: b.colorDark,
            colorLight: b.colorLight,
            correctLevel: QRCode.CorrectLevel.H
        }
    }

    if (content && content.length > 0) {
        generate(b, content)
    }
}

export function update(el, content) {
    const b = Data.get(el)

    if (content && content.length > 0) {
        generate(b, content)
    }
    else {
        clear(b)
    }
}

export function dispose(el) {
    Data.remove(el)
}
