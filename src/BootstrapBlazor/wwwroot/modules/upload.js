import EventHandler from "./base/event-handler.js"
import BlazorComponent from "./base/blazor-component.js"

class Upload extends BlazorComponent {
    _init() {
        this._inputFile = this._element.querySelector('[type="file"]')
        this._browserButton = this._element.querySelector('.btn-browser')

        this._setListeners()
    }

    _setListeners() {
        if (this._browserButton !== null) {
            EventHandler.on(this._browserButton, 'click', () => {
                this._inputFile.click()
            })
        }

        //阻止浏览器默认行为
        EventHandler.on(document, "dragleave", e => {
            e.preventDefault()
        })
        EventHandler.on(document, 'drop', e => {
            e.preventDefault()
        })
        EventHandler.on(document, 'dragenter', e => {
            e.preventDefault()
        })
        EventHandler.on(document, 'dragover', e => {
            e.preventDefault()
        })

        EventHandler.on(this._element, 'drop', e => {
            try {
                //获取文件对象
                const fileList = e.dataTransfer.files

                //检测是否是拖拽文件到页面的操作
                if (fileList.length === 0) {
                    return false;
                }

                this._inputFile.files = e.dataTransfer.files;
                const event = new Event('change', {bubbles: true});
                this._inputFile.dispatchEvent(event);
            } catch (e) {
                console.error(e);
            }
        })

        EventHandler.on(this._element, 'paste', e => {
            this._inputFile.files = e.clipboardData.files;
            const event = new Event('change', {bubbles: true});
            this._inputFile.dispatchEvent(event);
        });
    }

    _dispose() {
        EventHandler.off(this._browserButton, 'click')
        EventHandler.off(document, 'dragleave');
        EventHandler.off(document, 'drop');
        EventHandler.off(document, 'dragenter');
        EventHandler.off(document, 'dragover');
        EventHandler.off(this._element, 'drop');
        EventHandler.off(this._element, 'paste');
    }

    static get NAME() {
        return 'upload'
    }
}

export {
    Upload
}
