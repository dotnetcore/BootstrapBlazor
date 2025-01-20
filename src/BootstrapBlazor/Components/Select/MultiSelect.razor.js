import { isDisabled, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import { registerSelect, unregisterSelect } from "../../modules/base-select.js"
import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const { toggleRow, triggerEditTag } = options;
    const search = el.querySelector(".search-text");
    const itemsElement = el.querySelector('.multi-select-items');
    const popover = Popover.init(el, {
        itemsElement,
        closeButtonSelector: '.multi-select-close'
    })
    const ms = {
        el, invoke, options,
        itemsElement,
        closeButtonSelector: '.multi-select-close',
        search,
        keydownEl: [search, itemsElement],
        popover
    }

    EventHandler.on(itemsElement, 'click', '.multi-select-input', e => {
        const handler = setTimeout(() => {
            clearTimeout(handler);
            e.target.focus();
        }, 50);
    });

    EventHandler.on(itemsElement, 'keyup', '.multi-select-input', async e => {
        const triggerSpace = e.target.getAttribute('data-bb-trigger-key') === 'space';
        let submit = false;
        if (triggerSpace) {
            if (e.code === 'Space') {
                submit = true;
            }
        }
        else if (e.code === 'Enter' || e.code === 'NumPadEnter') {
            submit = true;
        }

        if (submit) {
            const ret = await invoke.invokeMethodAsync(triggerEditTag, e.target.value);
            if (ret) {
                e.target.value = '';
            }
        }
    });

    if (!popover.isPopover) {
        EventHandler.on(itemsElement, 'click', ms.closeButtonSelector, () => {
            const dropdown = bootstrap.Dropdown.getInstance(popover.toggleElement)
            if (dropdown && dropdown._isShown()) {
                dropdown.hide()
            }
        })
    }
    popover.clickToggle = e => {
        const element = e.target.closest(ms.closeButtonSelector);
        if (element) {
            e.stopPropagation()

            invoke.invokeMethodAsync(toggleRow, element.getAttribute('data-bb-val'))
        }
    }
    popover.isDisabled = () => {
        return isDisabled(ms.popover.toggleElement)
    }

    Data.set(id, ms);
    registerSelect(ms);
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

    const { popover } = ms;
    if (!popover.isPopover) {
        EventHandler.off(ms.itemsElement, 'click', ms.closeButtonSelector)
    }
    unregisterSelect(ms);
}
