import { isDisabled, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const itemsElement = el.querySelector('.multi-select-items');
    const popover = Popover.init(el, {
        itemsElement,
        closeButtonSelector: '.multi-select-close'
    })

    const ms = {
        el, invoke, method,
        itemsElement,
        closeButtonSelector: '.multi-select-close',
        popover
    }

    EventHandler.on(itemsElement, 'change', '.multi-select-input', e => {
        invoke.invokeMethodAsync('TriggerEditTag', e.target.value)
    });

    EventHandler.on(itemsElement, 'click', '.multi-select-input', e => {
        const handler = setTimeout(() => {
            clearTimeout(handler);
            e.target.focus();
        }, 50);
    });

    EventHandler.on(itemsElement, 'keyup', '.multi-select-input', e => {
        const key = e.target.getAttribute('data-bb-trigger-key');
        if (key === 'space') {
            invoke.invokeMethodAsync('TriggerEditTag', e.target.value)
        }
    });

    if (!ms.popover.isPopover) {
        EventHandler.on(itemsElement, 'click', ms.closeButtonSelector, () => {
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

export function show(id) {
    const select = Data.get(id)
    if (select) {
        const delay = getTransitionDelayDurationFromElement(select.popover.toggleElement);
        const handler = setTimeout(() => {
            clearTimeout(handler);
            select.popover.show();
        }, delay);
    }
}

export function hide(id) {
    const select = Data.get(id)
    const delay = getTransitionDelayDurationFromElement(select.popover.toggleElement);
    if (select) {
        const handler = setTimeout(() => {
            clearTimeout(handler);
            select.popover.hide();
        }, delay)
    }
}

export function dispose(id) {
    const ms = Data.get(id)
    Data.remove(id)

    if (!ms.popover.isPopover) {
        EventHandler.off(ms.itemsElement, 'click', ms.closeButtonSelector)
    }
    Popover.dispose(ms.popover)
}
