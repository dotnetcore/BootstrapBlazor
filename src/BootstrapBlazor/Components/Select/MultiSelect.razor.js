import { isDisabled } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const popover = Popover.init(el, {
        itemsElement: el.querySelector('.multi-select-items'),
        closeButtonSelector: '.multi-select-close'
    })

    const ms = {
        el, invoke, method,
        itemsElement: el.querySelector('.multi-select-items'),
        closeButtonSelector: '.multi-select-close',
        popover
    }

    if (!ms.popover.isPopover) {
        EventHandler.on(ms.itemsElement, 'click', ms.closeButtonSelector, () => {
            const dropdown = bootstrap.Dropdown.getInstance(popover.toggleElement)
            if (dropdown && dropdown._isShown()) {
                dropdown.hide()
            }
        })
    }
    ms.popover.clickToggle = e => {
        const element = e.target.closest(ms.closeButtonSelector);
        if (element) {
            e.stopPropagation()

            invoke.invokeMethodAsync(method, element.getAttribute('data-bb-val'))
        }
    }
    ms.popover.isDisabled = () => {
        return isDisabled(ms.popover.toggleElement)
    }

    Data.set(id, ms)
}

export function dispose(id) {
    const ms = Data.get(id)
    Data.remove(id)

    if (!ms.popover.isPopover) {
        EventHandler.off(ms.itemsElement, 'click', ms.closeButtonSelector)
    }
    Popover.dispose(ms.popover)
}
