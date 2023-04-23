import Data from "../../modules/data.js"
import DropdownBase from "../../modules/base-dropdown.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return;
    }

    const config = {
        class: 'popover-dropdown',
        toggleClass: '[data-bs-toggle="bb.dropdown"]',
        dropdown: '.filter-item',
        dismiss: el.getAttribute('data-bb-dismiss')
    }
    const dropdown = DropdownBase.init(el)
    const filter = { el, config, dropdown }
    Data.set(id, filter)

    EventHandler.on(dropdown.toggleMenu, 'click', config.dismiss, () => {
        if (dropdown.popover) {
            dropdown.popover.hide()
        }
    })

    const buttons = dropdown.toggleMenu.querySelectorAll(config.dismiss)
    EventHandler.on(dropdown.toggleMenu, 'keyup', e => {
        if (e.key === 'Escape') {
            buttons.item(0).click()
        } else if (e.key === 'Enter') {
            buttons.item(1).click()
        }
    });
}

export function dispose(id) {
    const filter = Data.get(id)
    Data.remove(id)

    if (filter) {
        const dropdown = filter.dropdown
        const config = filter.config
        EventHandler.off(dropdown.toggleMenu, 'click', config.dismiss)
        DropdownBase.dispose(dropdown)
    }
}
