import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class Toast extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokeMethodName = this._config.arguments[1]
        this._toast = bootstrap.Toast.getOrCreateInstance(this._element)

        if (this._showProgress()) {
            this._progressElement = this._element.querySelector('.toast-progress')
            const delay = this._toast._config.delay / 1000
            this._progressElement.style.transition = `width linear ${delay}s`
        }

        this._setListeners()
        this._toast.show()
    }

    _setListeners() {
        EventHandler.on(this._element, 'shown.bs.toast', () => {
            if (this._showProgress()) {
                this._progressElement.style.width = '100%'
            }
        })
        EventHandler.on(this._element, 'hidden.bs.toast', () => {
            this._invoker.invokeMethodAsync(this._invokeMethodName)
        })
    }

    _showProgress() {
        return this._toast._config.autohide
    }

    _dispose() {
        EventHandler.off(this._element, 'shown.bs.toast')
        EventHandler.off(this._element, 'hidden.bs.toast')
        if (this._toast) {
            this._toast.dispose()
        }
    }
}
