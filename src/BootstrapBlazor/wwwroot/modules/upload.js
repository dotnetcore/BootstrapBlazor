import Data from "./data.js"
import EventHandler from "./event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const preventHandler = e => e.preventDefault()
    const upload = { el, preventHandler }
    Data.set(id, upload)

    const inputFile = el.querySelector('[type="file"]')
    EventHandler.on(el, 'click', '.btn-browser', () => {
        inputFile.click()
    })

    EventHandler.on(el, 'click', '.upload-drop-body', () => {
        inputFile.click()
    })

    EventHandler.on(document, "dragleave", preventHandler)
    EventHandler.on(document, 'drop', preventHandler)
    EventHandler.on(document, 'dragenter', preventHandler)
    EventHandler.on(document, 'dragover', preventHandler)

    EventHandler.on(el, 'drop', e => {
        try {
            const fileList = e.dataTransfer.files
            if (fileList.length === 0) {
                return false
            }

            inputFile.files = fileList
            const event = new Event('change', { bubbles: true })
            inputFile.dispatchEvent(event)
        } catch (e) {
            console.error(e)
        }
    })

    EventHandler.on(el, 'paste', e => {
        inputFile.files = e.clipboardData.files
        const event = new Event('change', { bubbles: true })
        inputFile.dispatchEvent(event)
    })

    const getIndex = target => {
        let index = 0;
        let button = target;
        if (button.tagName === 'IMG') {
            button = button.closest('.upload-item').querySelector('.btn-zoom');
        }
        if (button) {
            const buttons = [...el.querySelectorAll('.btn-zoom')]
            index = buttons.indexOf(button);
        }
        return index;
    };

    EventHandler.on(el, 'click', '.btn-zoom, .upload-item-body-image', e => {
        const prev = Data.get(el.getAttribute('data-bb-previewer-id'));
        prev.viewer.updatePrevList([...el.querySelectorAll('.upload-body img')].map(v => v.src));
        prev.viewer.show(getIndex(e.delegateTarget));
    })
}

export function dispose(id) {
    const upload = Data.get(id)
    Data.remove(id)

    if (upload) {
        const { el, preventHandler } = upload;

        EventHandler.off(el, 'click')
        EventHandler.off(el, 'drop')
        EventHandler.off(el, 'paste')
        EventHandler.off(document, 'dragleave', preventHandler)
        EventHandler.off(document, 'drop', preventHandler)
        EventHandler.off(document, 'dragenter', preventHandler)
        EventHandler.off(document, 'dragover', preventHandler)
    }
}
