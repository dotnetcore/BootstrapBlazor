import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }
    const popover = Popover.init(el, {
        isDisabled: () => {
            return el.querySelector('.dropdown-toggle').getAttribute('disabled') === 'disabled'
        }
    })

    const dropdown = { popover }
    Data.set(id, dropdown)
}

export function dispose(id) {
    const dropdown = Data.get(id)
    Data.remove(id)

    if (dropdown) {
        Popover.dispose(dropdown.popover)
    }
}
