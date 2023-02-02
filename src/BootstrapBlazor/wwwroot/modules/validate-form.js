import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class ValidateForm extends BlazorComponent {
    _init() {
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'keydown', e => {
            if (e.target.nodeName !== 'TEXTAREA') {
                const dissubmit = this._element.getAttribute('data-bb-dissubmit') === 'true'
                if (e.keyCode === 13 && dissubmit) {
                    e.preventDefault()
                    e.stopPropagation()
                }
            }
        })
    }

    _dispose() {
        EventHandler.off(this._element, 'keydown')
    }
}
