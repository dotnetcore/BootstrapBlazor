import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version"
import Viewer from "./viewer.js?v=$version"

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
        if (!upload.viewer) {
            const previewId = el.getAttribute('data-bb-previewer-id')
            const viewEl = document.getElementById(previewId)
            upload.viewer = Viewer.init(viewEl, [])
            upload.viewEl = viewEl
        }
        const button = e.delegateTarget
        const buttons = [...el.querySelectorAll('.btn-zoom')]
        upload.viewer.updatePrevList([...el.querySelectorAll('.upload-body img')].map(v => v.src))
        upload.viewer.show(buttons.indexOf(button))
    })
}

export function dispose(id) {
    const upload = Data.get(id)
    Data.remove(id)

    const el = upload.el
    const preventHandler = upload.preventHandler
    if (upload) {
        if (upload.viewer) {
            upload.viewer.dispose(upload.viewEl)
        }
        EventHandler.off(el, 'click')
        EventHandler.off(el, 'drop')
        EventHandler.off(el, 'paste')
        EventHandler.off(document, 'dragleave', preventHandler)
        EventHandler.off(document, 'drop', preventHandler)
        EventHandler.off(document, 'dragenter', preventHandler)
        EventHandler.off(document, 'dragover', preventHandler)
    }
}
