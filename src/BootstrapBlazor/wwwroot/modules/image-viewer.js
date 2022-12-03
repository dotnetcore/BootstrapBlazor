import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { ImagePreviewer } from "./image-previewer.js"

export class ImageViewer extends BlazorComponent {
    _init() {
        this._img = this._element.querySelector('img')
        this._prevList = this._config.arguments || []

        if (this._img && this._config.async) {
            this._img.setAttribute('src', this._config.arguments[0])
        }

        this._setListeners()
    }

    _setListeners() {

        if (this._prevList.length > 0) {
            EventHandler.on(this._img, 'click', () => {
                if (!this._previewer) {
                    const previewerElement = document.getElementById(this._config.previewerId)
                    if (previewerElement) {
                        this._previewer = ImagePreviewer.getOrCreateInstance(previewerElement)
                    }
                }
                this._previewer.show()
            })
        }
    }

    _execute(args) {
        EventHandler.off(this._img, 'click')

        this._prevList = args
        this._setListeners()
    }

    _dispose() {
        EventHandler.off(this._img, 'click')

        if (this._previewer) {
            this._previewer.dispose()
            this._previewer = null
        }
    }
}
