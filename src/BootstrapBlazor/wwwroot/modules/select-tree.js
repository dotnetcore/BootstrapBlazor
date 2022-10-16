import { DropdownBase } from "./base/base-dropdown.js";
import { isDisabled } from "./base/index.js";

export class SelectTree extends DropdownBase {
    _init() {
        // el, obj, method
        this._input = this._element.querySelector('.form-select')

        super._init()
    }

    _isDisabled() {
        return isDisabled(this._input)
    }
}
