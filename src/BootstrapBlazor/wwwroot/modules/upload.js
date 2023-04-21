import { ImagePreviewer } from "../Components/ImagePreviewer/ImagePreviewer.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const upload = {
        el,
        browserClass: '.btn-browser',
        inputFile: el.querySelector('[type="file"]'),
        preventFn: e => {
            e.preventDefault()
        }
    }
    Data.set(id, upload)

    EventHandler.on(el, 'click', upload.browserClass, () => {
        upload.inputFile.click()
    })

    //阻止浏览器默认行为
    EventHandler.on(document, "dragleave", preventFn)
    EventHandler.on(document, 'drop', preventFn)
    EventHandler.on(document, 'dragenter', preventFn)
    EventHandler.on(document, 'dragover', preventFn)

    EventHandler.on(el, 'drop', e => {
        try {
            //获取文件对象
            const fileList = e.dataTransfer.files

            //检测是否是拖拽文件到页面的操作
            if (fileList.length === 0) {
                return false;
            }

            upload.inputFile.files = e.dataTransfer.files;
            const event = new Event('change', { bubbles: true });
            upload.inputFile.dispatchEvent(event);
        } catch (e) {
            console.error(e);
        }
    })

    EventHandler.on(el, 'paste', e => {
        upload.inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        upload.inputFile.dispatchEvent(event);
    });

    EventHandler.on(el, 'click', '.btn-zoom', e => {
        if (!upload.previewer) {
            //const previewerId = document.getElementById(this._config.previewerId)
            //this._previewer = ImagePreviewer.getOrCreateInstance(previewerId)
        }
        const button = e.delegateTarget
        const buttons = [...el.querySelectorAll('.btn-zoom')]
        //this._previewer.show(buttons.indexOf(button))
    })
}

//_execute(args) {
//    const tooltipId = args[0]
//    const method = args[1]
//    if (method === 'disposeTooltip' && tooltipId) {
//        const element = document.getElementById(tooltipId)
//        if (element) {
//            const tooltip = bootstrap.Tooltip.getInstance(element)
//            if (tooltip) {
//                tooltip.dispose()
//            }
//        }
//    }
//}

export function dispose(id) {
    const upload = Data.get(id)
    Data.remove(id)

    EventHandler.off(el, 'click', upload.browserClass)
    EventHandler.off(document, 'dragleave', upload.preventFn);
    EventHandler.off(document, 'drop', upload.preventFn);
    EventHandler.off(document, 'dragenter', upload.preventFn);
    EventHandler.off(document, 'dragover', upload.preventFn);
    EventHandler.off(el, 'drop');
    EventHandler.off(el, 'paste');
}
