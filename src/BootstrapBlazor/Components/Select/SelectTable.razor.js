import { getWidth } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const setWidth = () => {
        const minWidth = parseFloat(el.dataset.bbMinWidth || '602');
        let width = getWidth(el);
        if (width < minWidth) {
            width = minWidth;
        }
        const dropdown = el.querySelector('.dropdown-table') || document.querySelector('.popover-dropdown .dropdown-table');
        if (dropdown) {
            dropdown.style.setProperty('--bb-dropdown-table-width', `${width - 2}px`);
        }
    }

    const popover = Popover.init(el, {
        initCallback: () => {
            setWidth();
            const dropdown = el.querySelector('.dropdown-table');
            if (dropdown) {
                dropdown.style.setProperty('position', 'fixed');
                dropdown.style.setProperty('visibility', 'hidden');
                dropdown.style.setProperty('display', 'block');

                const wrapper = dropdown.querySelector('.table-wrapper');
                const headerHeight = wrapper.children[0].offsetHeight;
                wrapper.children[1].style.setProperty('height', `calc(100% - ${headerHeight}px)`);

                dropdown.style.removeProperty('display');
                dropdown.style.removeProperty('visibility');
                dropdown.style.removeProperty('position');
            }
        },
        hideCallback: async () => {
            await invoke.invokeMethodAsync("TriggerUpdateSelectedItems");
        }
    });

    const observer = new ResizeObserver(setWidth);
    observer.observe(el)

    const input = el.querySelector(".form-select");
    const selectTable = {
        el,
        input,
        popover,
        observer
    }

    Data.set(id, selectTable);

    EventHandler.on(el, 'click', '.multi-select-close', e => {
        e.preventDefault();
        e.stopPropagation();

        const disabled = el.classList.contains('disabled');
        if (disabled) {
            return;
        }

        const index = e.delegateTarget.getAttribute('data-bb-index');
        if (index) {
            const value = parseInt(index);
            if (value > -1) {
                invoke.invokeMethodAsync("TriggerRemoveItem", value);
            }
        }
    });

    EventHandler.on(input, 'keydown', e => {
        handlerKeydown(selectTable, e);
    });
}

const handlerKeydown = (table, e) => {
    const key = e.key;
    const { el, invoke, popover: { popover: { tip } } } = table;
    if (key === 'Enter') {
        const activeItem = tip.querySelector('.table-fixed-body > table > tbody > tr.active');
        if (activeItem !== null) {
            setTimeout(() => activeItem.click(), 0);
        }
    }
    else if (key === 'ArrowUp' || key === 'ArrowDown') {
        e.preventDefault();
        e.stopPropagation();

        const items = [...tip.querySelectorAll('.table-fixed-body > table > tbody > tr')];
        if (items.length === 0) {
            return;
        }

        let current = tip.querySelector('.active');
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

const scrollIntoView = (el, item) => {
    const behavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
}

export function close(id) {
    const data = Data.get(id)
    if (data) {
        data.popover.popover.hide();
    }
}
export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        const { el, popover, input, observer } = data;
        observer.disconnect();
        Popover.dispose(popover)
        EventHandler.off(el, 'click');
        EventHandler.off(input, 'keydown');
    }
}
