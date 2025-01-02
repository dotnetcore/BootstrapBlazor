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
        const prev = popover.toggleMenu.querySelector('.dropdown-item.preActive')
        if (prev) {
            prev.classList.remove('preActive')
        }
        scrollToActive(popover.toggleMenu, prev)
    }

    const keydown = e => {
        const menu = popover.toggleMenu;
        const key = e.key;
        if (key === "Enter" || key === 'NumpadEnter') {
            menu.classList.remove('show')
            let index = indexOf(el, activeItem)
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
                items[index].classList.add('active');
                const top = getTop(menu, index);
                const hehavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
                menu.scrollTo({ top: top, left: 0, behavior: hehavior });
            }
        }
    }

    EventHandler.on(el, 'shown.bs.dropdown', shown);
    EventHandler.on(el, 'keydown', keydown)

    const onSearch = debounce(async v => {
        search.parentElement.classList.add('l');
        await invoke.invokeMethodAsync(searchMethodCallback, v);
        search.parentElement.classList.remove('l');
    });
    if (search) {
        Input.composition(search, onSearch);
    }

    const select = {
        el,
        search,
        popover
    }
    Data.set(id, select);
}

const getTop = (menu, index) => {
    const styles = getComputedStyle(menu)
    const maxHeight = parseInt(styles.maxHeight) / 2
    const itemHeight = getHeight(menu.querySelector('.dropdown-item'))
    const height = itemHeight * index
    const count = Math.floor(maxHeight / itemHeight);
    let top = 0;
    if (height > maxHeight) {
        top = itemHeight * (index > count ? index - count : index)
    }
    return top;
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
        EventHandler.off(select.el, 'shown.bs.dropdown')
        EventHandler.off(select.el, 'keydown');
        Popover.dispose(select.popover)
        Input.dispose(select.search);
    }
}

function scrollToActive(el, activeItem) {
    const virtualEl = el.querySelector('.dropdown-virtual');

    activeItem ??= el.querySelector('.dropdown-item.active')

    if (activeItem) {
        const innerHeight = getInnerHeight(el)
        const itemHeight = getHeight(activeItem);
        const index = indexOf(el, activeItem)
        const margin = itemHeight * index - (innerHeight - itemHeight) / 2;
        const hehavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';

        const search = el.querySelector('.search');
        if (search.classList.contains('show')) {

        }
        if (margin >= 0) {
            el.scrollTo({ top: margin, left: 0, behavior: hehavior });
        }
        else {
            el.scrollTo({ top: margin, left: 0, behavior: hehavior });
        }
    }
}

function indexOf(el, element) {
    const items = el.querySelectorAll('.dropdown-item')
    return Array.prototype.indexOf.call(items, element)
}
