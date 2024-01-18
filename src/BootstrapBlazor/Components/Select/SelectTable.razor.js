import Data from "../../modules/data.js?v=$version"
import Popover from "../../modules/base-popover.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const popover = Popover.init(el);
    const selectTable = {
        el,
        input: el.querySelector(".form-select"),
        popover
    }

    Data.set(id, selectTable)
}

export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        Popover.dispose(data.popover)
    }
}
