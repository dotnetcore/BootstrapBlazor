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
    const panel = el.querySelector('.datetime-picker-panel');
    const dialogBody = el.querySelector('.datetime-dialog-body');
    const picker = createPicker(el, invoke, options, mediaQuery, panel, dialogBody);
    const onModeChanged = () => syncPickerMode(id);
    const data = { el, invoke, options, picker, mediaQuery, panel, dialogBody, onModeChanged, isPopoverHiding: false };
    data.input = handlerInput(data);
    Data.set(id, data);
    EventHandler.on(mediaQuery, 'change', onModeChanged);
}

const syncPickerMode = id => {
    const data = Data.get(id);
    if (data) {
        const { picker, mediaQuery, dialogBody, isPopoverHiding } = data;
        const isShown = picker.mode === 'dialog' ? picker.isShown() : picker.popover?._isShown();
        if (isShown || isPopoverHiding) {
            return;
        }
        const mode = dialogBody && !mediaQuery.matches ? 'dialog' : 'popover';
        if (picker.mode !== mode) {
            reset(id);
        }
    }
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
        dropdownSelector: '.datetime-picker-panel',
        isDisabled: () => {
            return el.classList.contains('disabled');
        },
        hideCallback: () => {
            const data = Data.get(el.id);
            if (data?.picker === popover) {
                data.isPopoverHiding = true;
                popover.onHidden ??= () => {
                    const current = Data.get(el.id);
                    if (current?.picker === popover) {
                        current.isPopoverHiding = false;
                        syncPickerMode(el.id);
                    }
                };
                EventHandler.one(popover.toggleElement, 'hidden.bs.popover', popover.onHidden);
                if (invoke) {
                    invoke.invokeMethodAsync(options.triggerHideCallback);
                }
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
            syncPickerMode(el.id);
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
        picker.isPopoverHiding = false;
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
        if (picker.onHidden) {
            EventHandler.off(picker.toggleElement, 'hidden.bs.popover', picker.onHidden);
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
