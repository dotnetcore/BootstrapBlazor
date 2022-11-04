import DropdownBase from "./base/base-dropdown.js"
import EventHandler from "./base/event-handler.js"

export class TableFilter extends DropdownBase {
    static get Default() {
        return {
            class: 'popover-dropdown',
            toggleClass: '[data-bs-toggle="bb.dropdown"]',
            dropdown: '.filter-item'
        }
    }

    _setListeners() {
        EventHandler.on(this._toggleMenu, 'click', this._config.dismiss, () => {
            if (this._popover) {
                this._popover.hide()
            }
        })

        const buttons = this._toggleMenu.querySelectorAll(this._config.dismiss)
        EventHandler.on(this._toggleMenu, 'keyup', e => {
            if (e.key === 'Escape') {
                buttons.item(0).click()
            } else if (e.key === 'Enter') {
                buttons.item(1).click()
            }
        });

        super._setListeners();
    }

    _isDisabled() {
        return false
    }

    _dispose() {
        EventHandler.off(this._toggleMenu, 'click', this._config.dismiss)
        super._dispose();
    }
}
