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

    const selectTable = {
        el,
        input: el.querySelector(".form-select"),
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
        data.observer.disconnect();
        Popover.dispose(data.popover)
        EventHandler.off(data.el, 'click');
    }
}
