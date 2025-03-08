import Data from "../../modules/data.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const popover = Popover.init(el, {
        dropdownSelector: el.getAttribute('data-bb-dropdown'),
        isDisabled: () => {
            return el.classList.contains('disabled');
        },
        hideCallback: () => {
            if (invoke) {
                invoke.invokeMethodAsync(options.triggerHideCallback);
            }
        }
    });
    const dateTimePicker = {
        el,
        popover
    }
    Data.set(id, dateTimePicker)
}

export function hide(id) {
    const data = Data.get(id)
    if (data) {
        data.popover.hide()
    }
}

export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        Popover.dispose(data.popover)
    }
}
