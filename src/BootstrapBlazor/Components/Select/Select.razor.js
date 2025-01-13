import { debounce, getHeight, getInnerHeight, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"
import Input from "../../modules/input.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    const { confirmMethodCallback, searchMethodCallback } = options;
    if (el == null) {
        return
    }

    const search = el.querySelector(".search-text")
    const popover = Popover.init(el)

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

    if (popover.isPopover) {
        EventHandler.on(el, 'shown.bs.popover', shown);
    }
    else {
        EventHandler.on(el, 'shown.bs.dropdown', shown);
    }

    const input = el.querySelector(`#${id}_input`);
    EventHandler.on(input, 'keydown', keydown)

    const onSearch = debounce(async v => {
        search.parentElement.classList.add('l');
        await invoke.invokeMethodAsync(searchMethodCallback, v);
        search.parentElement.classList.remove('l');
    });
    if (search) {
        Input.composition(search, onSearch);
        EventHandler.on(search, 'keydown', keydown);
    }

    const select = {
        el,
        search,
        popover
    }
    Data.set(id, select);
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
    const select = Data.get(id)
    Data.remove(id)

    if (select) {
        const { el, popover, search } = select
        EventHandler.off(el, 'shown.bs.dropdown')
        EventHandler.off(el, 'keydown');
        Popover.dispose(popover);

        if (search) {
            Input.dispose(search);
            EventHandler.off(search, 'keydown');
        }
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
