import { Dropdown } from "./dropdown.js"
import { isDisabled } from "./base/index.js"

export class MultiSelect extends Dropdown {
    _isDisabled() {
        return isDisabled(this._toggle)
    }
}
