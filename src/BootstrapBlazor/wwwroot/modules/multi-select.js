import DropdownBase from "./base/base-dropdown.js"
import EventHandler from "./base/event-handler.js"
import { isDisabled } from "./base/index.js"

export class MultiSelect extends DropdownBase {
    _init() {
        this._config._itemsElement = this._element.querySelector('.multi-select-items')
        this._config._closeButtonSelector = '.multi-select-close'
        this._invoker = this._config.arguments[0]
        this._invokerMethod = this._config.arguments[1]

        super._init()
    }

    _isDisabled() {
        return isDisabled(this._toggle)
    }

    _clickToggle() {
        const element = event.target.closest(this._config._closeButtonSelector);
        if (element) {
            event.stopPropagation()

            this._invoker.invokeMethodAsync(this._invokerMethod, element.getAttribute('data-bb-val'))
        }
    }

    _setListeners() {
        if (!this._isPopover) {
            EventHandler.on(this._config._itemsElement, 'click', this._config._closeButtonSelector, () => {
                const dropdown = bootstrap.Dropdown.getInstance(this._toggle)
                if (dropdown && dropdown._isShown()) {
                    dropdown.hide()
                }
            })
        }

        super._setListeners()
    }

    _dispose() {
        if (!this._isPopover) {
            EventHandler.off(this._config._itemsElement, 'click', this._config._closeButtonSelector)
        }

        super._dispose()
    }
}
