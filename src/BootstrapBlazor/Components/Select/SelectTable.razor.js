import { getWidth } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const setWidth = () => {
        const minWidth = parseFloat(el.dataset.bbMinWidth || '580');
        let width = getWidth(el);
        if (width < minWidth) {
            width = minWidth;
        }
        const dropdown = el.querySelector('.dropdown-table') || document.querySelector('.popover-dropdown .dropdown-table');
        if (dropdown) {
            dropdown.style.setProperty('--bb-dropdown-table-width', `${width}px`);
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

    Data.set(id, selectTable)
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
    }
}
