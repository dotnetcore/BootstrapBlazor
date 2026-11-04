import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"
import Dialog from "../../modules/dialog.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const mediaQuery = window.matchMedia('(min-width: 768px)');
    const panel = el.querySelector(el.getAttribute('data-bb-dropdown'));
    const dialogBody = el.querySelector('.datetime-picker-dialog-body');
    const picker = createPicker(el, invoke, options, mediaQuery, panel, dialogBody);
    const onModeChanged = () => reset(id);
    const data = { el, invoke, options, picker, mediaQuery, panel, dialogBody, onModeChanged };
    data.input = handlerInput(data);
    Data.set(id, data);
    EventHandler.on(mediaQuery, 'change', onModeChanged);
}

const createPicker = (el, invoke, options, mediaQuery, panel, dialogBody) => {
    if (dialogBody && !mediaQuery.matches) {
        dialogBody.append(panel);
        return createDialog(el, invoke, options);
    }

    el.append(panel);
    return createPopover(el, invoke, options);
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
    popover.mode = 'popover';
    return popover;
}

const createDialog = (el, invoke, options) => {
    const isDisabled = () => el.classList.contains('disabled') || el.querySelector('.form-control')?.disabled;
    return Dialog.init(el, {
        isDisabled,
        hideCallback: () => {
            if (invoke) {
                invoke.invokeMethodAsync(options.triggerHideCallback);
            }
        }
    });
}

const handlerInput = picker => {
    const input = picker.el.querySelector('.datetime-picker-input');
    if (input) {
        EventHandler.on(input, 'keydown', e => {
            if (e.key === 'Tab' && picker.picker.mode === 'popover' && picker.picker.isShown()) {
                picker.picker.hide();
            }
        });
        EventHandler.on(input, 'keyup', e => {
            if (e.key === 'Escape') {
                picker.picker.hide();
                input.blur();
            }
            else if (e.key === 'Tab' && picker.picker.mode === 'popover') {
                picker.picker.show();
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
        const { el, input, invoke, options, mediaQuery, panel, dialogBody } = picker;
        disposePicker(picker.picker);

        picker.picker = createPicker(el, invoke, options, mediaQuery, panel, dialogBody);
        if (input !== el.querySelector('.datetime-picker-input')) {
            disposeInput(input);
            picker.input = handlerInput(picker);
        }
    }
}

const disposePicker = picker => {
    if (picker.mode === 'dialog') {
        Dialog.dispose(picker);
    }
    else {
        if (picker.popover?._isShown()) {
            picker.hide();
        }
        Popover.dispose(picker);
    }
}

export function hide(id) {
    const data = Data.get(id)
    if (data) {
        data.picker.hide()
    }
}

export function dispose(id) {
    const data = Data.get(id)
    Data.remove(id)

    if (data) {
        const { input, picker, mediaQuery, onModeChanged, panel, dialogBody } = data;
        EventHandler.off(mediaQuery, 'change', onModeChanged);
        disposeInput(input);
        disposePicker(picker);
        if (dialogBody) {
            dialogBody.append(panel);
        }
    }
}
