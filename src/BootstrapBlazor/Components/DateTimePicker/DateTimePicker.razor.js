import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const popover = createPopover(el, invoke, options);
    const input = handlerInput(el, popover);
    Data.set(id, { el, input, invoke, options, popover });
}

const createPopover = (el, invoke, options) => {
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
    return popover;
}

const handlerInput = (el, popover) => {
    const input = el.querySelector('.datetime-picker-input');
    if (input) {
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
    return input;
}

const disposeInput = input => {
    if (input) {
        EventHandler.off(input, 'keydown');
        EventHandler.off(input, 'keyup');
    }
}

export function reset(id) {
    const picker = Data.get(id);
    if (picker) {
        const { el, input, popover, invoke, options } = picker;
        disposeInput(input);
        Popover.dispose(popover);

        picker.popover = createPopover(el, invoke, options);
        picker.input = handlerInput(picker.el, picker.popover);
    }
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
        disposeInput(input);
        Popover.dispose(popover)
    }
}
