import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const popover = Popover.init(el)
    const selectTree = {
        el,
        input: el.querySelector(".form-select"),
        popover
    }

    EventHandler.on(popover.toggleMenu, 'click', '.tree-node', e => {
        if (popover.isPopover) {
            popover.hide()
        }
        else {
            const dropdown = bootstrap.Dropdown.getInstance(popover.toggleElement)
            if (dropdown) {
                dropdown.hide()
            }
        }
    })
    Data.set(id, selectTree)
}

export function dispose(id) {
    const data = Data.get(id)
    if (data) {
        EventHandler.off(data.popover.toggleMenu, 'click', '.tree-node')
        Popover.dispose(data.popover)
    }
    Data.remove(id)
}
