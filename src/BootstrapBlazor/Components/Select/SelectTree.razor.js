import { isDisabled } from ".../../modules/utility.js"
import DropdownBase from "../../modules/base-dropdown.js"
import EventHandler from "../../modules/event-handler.js"

export class SelectTree extends DropdownBase {
    _init() {
        this._input = this._element.querySelector('.form-select')

        super._init()
    }

    _setListeners() {
        EventHandler.on(this._toggleMenu, 'click', '.tree-node', e => {
            if (this._isPopover) {
                this._popover.hide()
            } else {
                const dropdown = bootstrap.Dropdown.getInstance(this._toggle)
                if (dropdown) {
                    dropdown.hide()
                }
            }
        })

        super._setListeners()
    }

    _isDisabled() {
        return isDisabled(this._input)
    }

    _dispose() {
        EventHandler.off(this._toggleMenu, 'click', '.tree-node')
        super._dispose();
    }
}
