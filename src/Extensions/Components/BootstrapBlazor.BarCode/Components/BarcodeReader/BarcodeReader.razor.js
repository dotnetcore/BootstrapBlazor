import '../../zxing.min.js'
import { vibrate } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

export async function init(el, invoker) {
    const b = {}
    Data.set(el, b)

    b._reader = new ZXing.BrowserMultiFormatReader()
    b._element = el
    b._scanType = b._element.getAttribute('data-bb-scan')
    b._invoker = invoker

    if (b._scanType === 'Camera') {
        b.scan = () => {
            b._invoker.invokeMethodAsync("Start")
            const deviceId = b._element.getAttribute('data-bb-deviceid')
            const video = b._element.querySelector('video').getAttribute('id')
            b._reader.decodeFromVideoDevice(deviceId, video, (result, err) => {
                if (result) {
                    vibrate()
                    b._invoker.invokeMethodAsync("GetResult", result.text)

                    const autoStop = b._element.getAttribute('data-bb-autostop') === 'true'
                    if (autoStop) {
                        b.stop()
                    }
                }
                if (err && !(err instanceof ZXing.NotFoundException)) {
                    console.error(err)
                    b._invoker.invokeMethodAsync('GetError', err)
                }
            })
        }

        b.stop = () => {
            b._reader.reset();
            b._invoker.invokeMethodAsync("Stop")
        }

        b._reader.getVideoInputDevices().then(videoInputDevices => {
            b._invoker.invokeMethodAsync("InitDevices", videoInputDevices).then(() => {
                const autoStart = b._element.getAttribute('data-bb-autostart') === 'true'
                if (autoStart && videoInputDevices.length > 0) {
                    const button = b._element.querySelector('[data-bb-method="scan"]')
                    button.click()
                }
            })
        })
    } else {
        const scanImageHandler = () => {
            const files = b._reader.file.files
            if (files.length === 0) {
                return
            }
            const reader = new FileReader()
            reader.onloadend = e => {
                b._reader.decodeFromImageUrl(e.target.result).then(result => {
                    if (result) {
                        vibrate()
                        console.log(result.text);
                        b._invoker.invokeMethodAsync('GetResult', result.text)
                    }
                }).catch((err) => {
                    if (err) {
                        console.log(err)
                        b._invoker.invokeMethodAsync('GetError', err.message)
                    }
                })
            }
            reader.readAsDataURL(files[0])
        }

        const resetFile = () => {
            let file = b._element.querySelector('[type="file"]')
            if (file) {
                EventHandler.off(file, 'change');
                file.remove()
            }
            file = document.createElement('input')
            file.setAttribute('type', 'file')
            file.setAttribute('hidden', 'true')
            file.setAttribute('accept', 'image/*')
            b._element.append(file)
            EventHandler.on(file, 'change', scanImageHandler)
            b._reader.file = file
            return file
        }

        b.scanImage = () => {
            let file = resetFile()
            file.click()
        }
    }

    const eventHandler = e => {
        let button = e.delegateTarget
        const method = button.getAttribute('data-bb-method')
        const fn = b[method]
        if (typeof fn === 'function') {
            fn()
        }
    }

    EventHandler.on(b._element, 'click', '[data-bb-method]', eventHandler)
}

export function dispose(el) {
    const b = Data.get(el)
    Data.remove(el)

    if (b) {
        EventHandler.off(b._element, 'click', '[data-bb-method]')
        b._reader.reset()
    }
}
