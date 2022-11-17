import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class Modal extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokerShownMethod = this._config.arguments[1]
        this._invokerCloseMethod = this._config.arguments[2]
        this._setEventListeners()
    }

    _setEventListeners() {
        EventHandler.on(this._element, 'shown.bs.modal', () => {
            this._invoker.invokeMethodAsync(this._invokerShownMethod)
        })
        EventHandler.on(this._element, 'hide.bs.modal', () => {
            this._invoker.invokeMethodAsync(this._invokerCloseMethod)
        })

        console.log('pop1')

        this._pop = () => {
            console.log('pop2')
            if (this._modal) {
                this._modal._dialog.remove()
                this._modal.dispose()
                this._modal = null
                document.body.classList.remove('modal-open');
                document.body.style.paddingLeft = '';
                document.body.style.paddingRight = '';
                document.body.style.overflow = '';
            }
        }
        EventHandler.on(window, 'popstate', this._pop)
    }

    _execute(args) {
        const method = args[1]
        if (method === 'show') {
            this._show()
        } else if (method === 'hide') {
            this._hide()
        } else if (method === 'toggle') {
            this._toggle()
        }
    }

    _show() {
        const dialogs = this._element.querySelectorAll('.modal-dialog')
        if (dialogs.length === 1) {
            const keyboard = this._element.getAttribute('data-bs-keyboard') === 'true'
            let backdrop = this._element.getAttribute('data-bs-backdrop')
            if (backdrop === null) {
                backdrop = true
            }
            if (!this._modal) {
                this._modal = bootstrap.Modal.getOrCreateInstance(this._element)
            }
            this._modal._config.keyboard = keyboard
            this._modal._config.backdrop = backdrop
            this._modal.show()
        } else {
            this._invoker.invokeMethodAsync(this._invokerShownMethod)
        }
    }

    _hide() {
        const dialogs = this._element.querySelectorAll('.modal-dialog')
        if (dialogs.length === 1) {
            this._modal.hide()
        } else {
            this._invoker.invokeMethodAsync(this._invokerCloseMethod)
        }
    }

    _toggle() {
        if (this._modal) {
            this._modal.toggle()
        } else {
            this._show()
        }
    }

    _dispose() {
        EventHandler.off(this._element, 'shown.bs.modal')
        EventHandler.off(this._element, 'hide.bs.modal')
        EventHandler.off(window, 'popstate', this._pop)
        if (this._modal) {
            this._modal.dispose()
        }
    }
}
