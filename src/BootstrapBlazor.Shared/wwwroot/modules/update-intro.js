import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"

export class UpdateIntro extends BlazorComponent {
    _init() {
        this._key = `bb_intro_popup:${this._config.version}`
        this._check()
        this._setListeners()
    }

    _check() {
        const width = window.innerWidth
        if (width >= 768) {
            const isShown = localStorage.getItem(this._key)
            if (!isShown) {
                this._slideToggle()

                // clean
                for (let index = localStorage.length; index > 0; index--) {
                    const k = localStorage.key(index - 1);
                    if (k.indexOf('bb_intro_popup:') > -1) {
                        localStorage.removeItem(k);
                    }
                }
            }
        }
    }

    _slideToggle() {
        this._element.classList.toggle('show')
    }

    _close() {
        localStorage.setItem(this._key, 'false')
        this._slideToggle()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', '.blazor-intro-button', () => {
            this._close()
        })
    }

    _dispose() {
        EventHandler.off(this._element, 'click', '.blazor-intro-button');
    }
}
