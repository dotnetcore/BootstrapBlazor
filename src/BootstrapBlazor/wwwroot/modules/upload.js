import Data from "./data.js"
import EventHandler from "./event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const preventHandler = e => e.preventDefault();
    const body = el.querySelector('.upload-drop-body');
    const inputFile = el.querySelector('[type="file"]');
    const upload = { el, body, preventHandler, inputFile };
    Data.set(id, upload)
        
    EventHandler.on(el, 'click', '.btn-browser', () => {
        //上传按钮禁用时不触发文件选择框
        if (el.querySelector('.btn-browser').classList.contains('disabled'))
            return;
        inputFile.click()
    })
    EventHandler.on(inputFile, 'change', e => {
        upload.files = e.delegateTarget.files;
    });

    EventHandler.on(document, "dragleave", preventHandler)
    EventHandler.on(document, 'drop', preventHandler)
    EventHandler.on(document, 'dragenter', preventHandler)
    EventHandler.on(document, 'dragover', preventHandler)

    EventHandler.on(body, 'dragenter', e => {
        el.classList.add('dropping');
    })

    EventHandler.on(body, 'dragleave', e => {
        el.classList.remove('dropping');
    });

    EventHandler.on(body, 'drop', e => {
        el.classList.remove('dropping');

        if (el.classList.contains('disabled')) {
            return;
        }
        try {
            const fileList = e.dataTransfer.files
            if (fileList.length === 0) {
                return false
            }

            inputFile.files = fileList
            const event = new Event('change', { bubbles: true })
            inputFile.dispatchEvent(event)
        }
        catch (e) {
            console.error(e)
        }
    })

    EventHandler.on(el, 'paste', e => {
        if (el.classList.contains('disabled')) {
            return;
        }

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

export function preview(previewerId, index) {
    const prev = Data.get(previewerId);
    if (prev) {
        prev.viewer.show(index);
    }
}

export function getPreviewUrl(id, fileName) {
    let url = '';
    const upload = Data.get(id);
    const { files, inputFile } = upload;
    const file = [...(files || inputFile.files)].find(v => v.name === fileName);
    if (file) {
        url = URL.createObjectURL(file);
    }
    return url;
}

export function dispose(id) {
    const upload = Data.get(id)
    Data.remove(id)

    if (upload) {
        const { el, body, preventHandler, inputFile } = upload;

        EventHandler.off(document, 'dragleave', preventHandler)
        EventHandler.off(document, 'drop', preventHandler)
        EventHandler.off(document, 'dragenter', preventHandler)
        EventHandler.off(document, 'dragover', preventHandler)

        EventHandler.off(el, 'click')
        EventHandler.off(el, 'paste')

        EventHandler.off(inputFile, 'change');

        EventHandler.off(body, 'dragleave')
        EventHandler.off(body, 'drop')
        EventHandler.off(body, 'dragenter')
    }
}
