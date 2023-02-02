import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class RibbonTab extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokerMethod = this._config.arguments[1]
        this._setListeners()
    }

    _setListeners() {
        this._handlerClick = e => {
            const isFloat = this._element.classList.contains('is-float')
            if (isFloat) {
                const expanded = this._element.classList.contains('is-expand')
                if (expanded) {
                    const ribbonBody = e.target.closest('.ribbon-body');
                    if (ribbonBody) {
                        this._invoker.invokeMethodAsync(this._invokerMethod)
                    }
                    else {
                        const ribbonTab = e.target.closest('.ribbon-tab')
                        if (ribbonTab !== this._element) {
                            this._invoker.invokeMethodAsync(this._invokerMethod)
                        }
                    }
                }
            }
        }
        EventHandler.on(document, 'click', this._handlerClick)
    }

    _dispose() {
        EventHandler.off(document, 'click', this._handlerClick)
    }
}
