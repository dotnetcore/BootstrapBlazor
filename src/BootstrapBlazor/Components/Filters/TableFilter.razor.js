import DropdownBase from "../../modules/base/base-dropdown1.js"
import EventHandler from "../../modules/event-handler.js"
import Data from "../../modules/data.js"


const Default = {
    class: 'popover-dropdown',
    toggleClass: '[data-bs-toggle="bb.dropdown"]',
    dropdown: '.filter-item'
}

export function init(id) {
    const el = document.getElementById(id)

    if (el == null) {
        return;
    }

    const action = el.querySelector(Default.dropdown)
    const dismiss = el.getAttribute("data-bb-dismiss")
    const dropdown = DropdownBase.init(el);
    const tableFilter = {
        el,
        dismiss,
        action,
        dropdown
    }

    EventHandler.on(action, 'click', dismiss, () => {
        if (dropdown.popover) {
            dropdown.popover.hide()
        }
    })

    const buttons = el.querySelectorAll(dismiss)
    EventHandler.on(el, 'keyup', e => {
        if (e.key === 'Escape') {
            buttons.item(0).click()
        } else if (e.key === 'Enter') {
            buttons.item(1).click()
        }
    });

    Data.set(id, tableFilter)
}

export function isDisabled() {
    return false
}

export function dispose(id) {
    const data = Data.get(id)
    if (data.action) {
        EventHandler.off(data.action, 'click', data.dismiss)
        DropdownBase.dispose(data.dropdown)
    }
}
