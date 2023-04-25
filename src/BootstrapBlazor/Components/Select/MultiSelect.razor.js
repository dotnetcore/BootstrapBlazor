import { isDisabled } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import DropdownBase from "../../modules/base-dropdown.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const ms = {
        el, invoke, callback,
        itemsElement: el.querySelector('.multi-select-items'),
        closeButtonSelector: '.multi-select-close',
        dropdown: DropdownBase.init(el)
    }

    if (!ms.dropdown.isPopover) {
        EventHandler.on(ms.itemsElement, 'click', ms.closeButtonSelector, () => {
            const popover = ms.dropdown.popover
            if (popover && popover._isShown()) {
                popover.hide()
            }
        })
    }
    ms.dropdown.clickToggle = e => {
        const element = e.target.closest(ms._closeButtonSelector);
        if (element) {
            e.stopPropagation()

            invoke.invokeMethodAsync(callback, element.getAttribute('data-bb-val'))
        }
    }
    ms.dropdown.isDisabled = () => {
        return isDisabled(ms.dropdown.toggleElement)
    }
}

export function dispose(id) {
    const ms = Data.get(id)
    Data.remove(id)

    if (!ms.dropdonw.isPopover) {
        EventHandler.off(ms.itemsElement, 'click', ms.closeButtonSelector)
    }
    ms.dropdonw.dispose(ms.dropdonw)
}
