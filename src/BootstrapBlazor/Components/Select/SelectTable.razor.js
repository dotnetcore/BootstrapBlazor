import { getWidth } from "../../modules/utility.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import Popover from "../../modules/base-popover.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const popover = Popover.init(el, {
        initCallback: () => {
            const width = getWidth(el);
            const dropdown = el.querySelector('.dropdown-table');
            if (dropdown) {
                dropdown.style.setProperty('--bb-dropdown-table-width', `${width}px`);

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
    const selectTable = {
        el,
        input: el.querySelector(".form-select"),
        popover
    }

    Data.set(id, selectTable)
}

export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        Popover.dispose(data.popover)
    }
}
