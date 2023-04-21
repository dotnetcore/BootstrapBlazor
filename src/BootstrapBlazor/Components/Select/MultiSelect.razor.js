import DropdownBase from "./base/base-dropdown.js"
import { isDisabled } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const ms = {
        el, invoke, callback,
        itemsElement: el.querySelector('.multi-select-items'),
        closeButtonSelector: '.multi-select-close'
    }

    super._init()

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

export function dispose(id) {
    if (!this._isPopover) {
        EventHandler.off(this._config._itemsElement, 'click', this._config._closeButtonSelector)
    }

    super._dispose()
}
