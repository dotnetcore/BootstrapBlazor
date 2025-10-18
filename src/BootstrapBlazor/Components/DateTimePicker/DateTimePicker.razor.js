import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
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
    const input = el.querySelector('.datetime-picker-input');
    const dateTimePicker = {
        input,
        popover
    }
    Data.set(id, dateTimePicker);

    EventHandler.on(input, 'keydown', e => {
        if (e.key === 'Tab' && popover.isShown()) {
            popover.hide();
        }
    });
    EventHandler.on(input, 'keyup', e => {
        if (e.key === 'Escape') {
            popover.hide();
            input.blur();
        }
        else if (e.key === 'Tab') {
            popover.show();
        }
    });
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
        const { input, popover } = data;
        EventHandler.off(input, 'keydown');
        EventHandler.off(input, 'keyup');
        Popover.dispose(popover)
    }
}
