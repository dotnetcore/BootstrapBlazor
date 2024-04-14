import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const dismissSelector = el.getAttribute("data-bb-dismiss")
    const popover = Popover.init(el, {
        dropdownSelector: el.getAttribute('data-bb-dropdown'),
        isDisabled: () => {
            return el.classList.contains('disabled');
        }
    });
    const dateTimePicker = {
        el,
        dismissSelector,
        popover
    }
    Data.set(id, dateTimePicker)

    if (dismissSelector) {
        EventHandler.on(popover.toggleMenu, 'click', dismissSelector, () => {
            popover.hide()
        })
    }
}

export function hide(id) {
    const data = Data.get(id)
    if (data) {
        data.popover.hide()
    }
}

export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        if (data.dismissSelector) {
            EventHandler.off(data.popover.toggleMenu, 'click', data.dismissSelector)
        }
        Popover.dispose(data.popover)
    }
}
