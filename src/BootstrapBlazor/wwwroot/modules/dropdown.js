import { DropdownBase } from "./base/base-dropdown.js"
import EventHandler from "./base/event-handler.js"

export class Dropdown extends DropdownBase {
    _setListeners() {
        EventHandler.on(this._toggleMenu, 'click', this._config.dismiss, () => {
            this.hide()
        })
        super._setListeners()
    }

    _dispose() {
        EventHandler.off(this._toggleMenu, 'click', this._config.dismiss)
        super._dispose();
    }
}
