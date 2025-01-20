import { debounce } from "./utility.js"
import Input from "./input.js"
import EventHandler from "./event-handler.js"

export function registerSearch(el, options) {
    Input.composition(search, onSearch);
    EventHandler.on(search, 'keydown', keydown);

    if (popover.isPopover) {
        EventHandler.on(el, 'shown.bs.popover', shown);
    }
    else {
        EventHandler.on(el, 'shown.bs.dropdown', shown);
    }
}

export function unregisterSearch(el) {
    Input.dispose(search);
    EventHandler.off(search, 'keydown');

    if (popover.isPopover) {
        EventHandler.off(el, 'shown.bs.popover');
    }
    else {
        EventHandler.off(el, 'shown.bs.dropdown');
    }
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

const indexOf = (el, element) => {
    const items = el.querySelectorAll('.dropdown-item')
    return Array.prototype.indexOf.call(items, element)
}

const scrollIntoView = (el, item) => {
    const behavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
}
