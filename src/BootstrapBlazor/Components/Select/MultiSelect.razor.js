import { debounce, isDisabled, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const { confirmMethodCallback, searchMethodCallback, toggleRow } = options;
    const search = el.querySelector(".search-text");
    const itemsElement = el.querySelector('.multi-select-items');
    const popover = Popover.init(el, {
        itemsElement,
        closeButtonSelector: '.multi-select-close'
    })

    const ms = {
        el, invoke,
        itemsElement,
        closeButtonSelector: '.multi-select-close',
        search,
        popover
    }

    const onSearch = debounce(async v => {
        search.parentElement.classList.add('l');
        await invoke.invokeMethodAsync('TriggerOnSearch', v);
        search.parentElement.classList.remove('l');
    });

    const shown = () => {
        if (search) {
            search.focus();
        }
        const active = popover.toggleMenu.querySelector('.dropdown-item.active');
        if (active) {
            scrollIntoView(el, active);
        }
    }

    const keydown = e => {
        const menu = popover.toggleMenu;
        const key = e.key;
        if (key === "Enter" || key === 'NumpadEnter') {
            if (popover.isPopover) {
                popover.hide();
            }
            else {
                menu.classList.remove('show');
            }
            const activeItem = menu.querySelector('.active');
            let index = indexOf(menu, activeItem)
            invoke.invokeMethodAsync(confirmMethodCallback, index)
        }
        else if (key === 'ArrowUp' || key === 'ArrowDown') {
            e.preventDefault();

            if (menu.querySelector('.dropdown-virtual')) {
                return;
            }

            const items = [...menu.querySelectorAll('.dropdown-item:not(.disabled)')];
            if (items.length > 0) {
                let current = menu.querySelector('.active');
                if (current !== null) {
                    current.classList.remove('active');
                }

                let index = current === null ? -1 : items.indexOf(current);
                index = key === 'ArrowUp' ? index - 1 : index + 1;
                if (index < 0) {
                    index = items.length - 1;
                }
                else if (index > items.length - 1) {
                    index = 0;
                }
                current = items[index];
                current.classList.add('active');
                scrollIntoView(el, current);
            }
        }
    }

    Input.composition(search, onSearch);
    EventHandler.on(search, 'keydown', keydown);

    if (popover.isPopover) {
        EventHandler.on(el, 'shown.bs.popover', shown);
    }
    else {
        EventHandler.on(el, 'shown.bs.dropdown', shown);
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
            const ret = await invoke.invokeMethodAsync('TriggerEditTag', e.target.value);
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

    const { search, popover } = ms;
    if (!popover.isPopover) {
        EventHandler.off(ms.itemsElement, 'click', ms.closeButtonSelector)
    }
    Popover.dispose(ms.popover);

    if (search) {
        Input.dispose(search);
        EventHandler.off(search, 'keydown');
    }
}

function indexOf(el, element) {
    const items = el.querySelectorAll('.dropdown-item')
    return Array.prototype.indexOf.call(items, element)
}

const scrollIntoView = (el, item) => {
    const behavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
}
