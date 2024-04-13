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

    //阻止浏览器默认行为
    EventHandler.on(document, "dragleave", preventHandler)
    EventHandler.on(document, 'drop', preventHandler)
    EventHandler.on(document, 'dragenter', preventHandler)
    EventHandler.on(document, 'dragover', preventHandler)

    EventHandler.on(el, 'drop', e => {
        try {
            //获取文件对象
            const fileList = e.dataTransfer.files

            //检测是否是拖拽文件到页面的操作
            if (fileList.length === 0) {
                return false
            }

            inputFile.files = e.dataTransfer.files
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

    EventHandler.on(el, 'click', '.btn-zoom', e => {
        const previewId = el.getAttribute('data-bb-previewer-id');
        const prev = Data.get(previewId)
        const button = e.delegateTarget
        const buttons = [...el.querySelectorAll('.btn-zoom')]
        prev.viewer.updatePrevList([...el.querySelectorAll('.upload-body img')].map(v => v.src))
        prev.viewer.show(buttons.indexOf(button))
    })
}

export function dispose(id) {
    const upload = Data.get(id)
    Data.remove(id)

    const el = upload.el
    const preventHandler = upload.preventHandler
    if (upload) {
        EventHandler.off(el, 'click')
        EventHandler.off(el, 'drop')
        EventHandler.off(el, 'paste')
        EventHandler.off(document, 'dragleave', preventHandler)
        EventHandler.off(document, 'drop', preventHandler)
        EventHandler.off(document, 'dragenter', preventHandler)
        EventHandler.off(document, 'dragover', preventHandler)
    }
}
