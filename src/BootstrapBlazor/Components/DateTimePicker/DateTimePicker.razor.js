import Data from "../../modules/Data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const dismissSelector = el.getAttribute("data-bb-dismiss")
    const popover = Popover.init(el);
    const dateTimePicker = {
        el,
        dismissSelector,
        popover
    }

    if (dismissSelector) {
        EventHandler.on(popover.toggleMenu, 'click', dismissSelector, () => {
            popover.hide()
        })
    }

    Data.set(id, dateTimePicker)
}

export function hide(id) {
    const data = Data.get(id)
    if (data) {
        data.popover.hide()
    }
}

export function dispose(id) {
    const data = Data.get(id)
    if (data) {
        if (data.dismissSelector) {
            EventHandler.off(data.popover.toggleMenu, 'click', data.dismissSelector)
        }
        Popover.dispose(data.el)
    }
    Data.remove(id)
}
