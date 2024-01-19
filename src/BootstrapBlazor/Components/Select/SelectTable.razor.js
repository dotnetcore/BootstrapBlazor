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

                const wrapper = dropdown.querySelector('.table-wrapper');
                wrapper.children[1].style.setProperty('height', 'calc(100% - 39px)');
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
