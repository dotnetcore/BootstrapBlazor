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
    EventHandler.on(el, 'click', '.dropdown-toggle', e => {
        const mobile = el.getAttribute('data-bb-mobile');
        if (mobile !== 'false') {
            e.preventDefault();
            e.stopImmediatePropagation();
            if (mobile === 'true') {
                requestModal(Data.get(el.id));
            }
        }
    });
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

const requestModal = picker => {
    if (!picker.el.classList.contains('disabled') && !picker.modalOpen && !picker.modalOpening) {
        picker.modalOpening = true;
        picker.invoke.invokeMethodAsync(picker.options.triggerShowModalCallback).finally(() => {
            picker.modalOpening = false;
        });
    }
}

export function showModal(id, modalId) {
    const picker = Data.get(id);
    const modal = document.getElementById(modalId);
    const body = modal?.querySelector('.datetime-picker-modal-body');
    if (!picker || !body) {
        throw new Error(`DateTimePicker modal is not ready: ${id}`);
    }

    if (picker.modal !== modal) {
        picker.modal = modal;
        picker.modalShown = () => {
            if (picker.parentModal) {
                const zIndex = Number.parseInt(getComputedStyle(picker.parentModal).zIndex) || 1055;
                modal.style.zIndex = zIndex + 20;
                bootstrap.Modal.getInstance(modal)._backdrop._getElement().style.zIndex = zIndex + 10;
            }
        };
        picker.modalHidden = () => restorePanel(picker);
        EventHandler.on(modal, 'shown.bs.modal', picker.modalShown);
        EventHandler.on(modal, 'hidden.bs.modal', picker.modalHidden);
    }

    picker.parentModal = picker.el.closest('.modal.show');
    modal.style.zIndex = '';
    picker.modalOpen = true;
    picker.el.classList.add('show');
    body.append(picker.popover.toggleMenu);
}

const restorePanel = picker => {
    picker.el.append(picker.popover.toggleMenu);
    picker.el.classList.remove('show');
    picker.modalOpen = false;

    if (picker.parentModal?.classList.contains('show')) {
        document.body.classList.add('modal-open');
        bootstrap.Modal.getInstance(picker.parentModal)?._focustrap.activate();
    }

    const toggle = picker.popover.toggleElement;
    if (toggle.isConnected && !toggle.disabled) {
        toggle.focus({ preventScroll: true });
    }
}

const handlerInput = (el, popover) => {
    const input = el.querySelector('.datetime-picker-input');
    if (input) {
        EventHandler.on(input, 'keydown', e => {
            if (e.key === 'Tab' && el.getAttribute('data-bb-mobile') === 'false' && popover.isShown()) {
                popover.hide();
            }
        });
        EventHandler.on(input, 'keyup', e => {
            if (e.key === 'Escape') {
                if (el.getAttribute('data-bb-mobile') === 'false') {
                    popover.hide();
                }
                input.blur();
            }
            else if (e.key === 'Tab') {
                const mobile = el.getAttribute('data-bb-mobile');
                if (mobile === 'true') {
                    requestModal(Data.get(el.id));
                }
                else if (mobile === 'false') {
                    popover.show();
                }
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

export function isShown(id) {
    const picker = Data.get(id);
    return !!picker && (picker.modalOpen || picker.modalOpening || picker.el.classList.contains('show'));
}

export function reset(id) {
    const picker = Data.get(id);
    if (picker) {
        const { el, input, popover, invoke, options } = picker;
        disposeInput(input);
        if (!picker.modalOpen && el.classList.contains('show')) {
            popover.hide();
            el.classList.remove('show');
        }

        // The panel may be hosted in the mobile dialog while the trigger is replaced.
        el.append(popover.toggleMenu);
        Popover.dispose(popover);
        picker.popover = createPopover(el, invoke, options);
        picker.input = handlerInput(picker.el, picker.popover);
        if (picker.modalOpen) {
            picker.modal.querySelector('.datetime-picker-modal-body').append(picker.popover.toggleMenu);
        }
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
        if (data.modal) {
            EventHandler.off(data.modal, 'shown.bs.modal', data.modalShown);
            EventHandler.off(data.modal, 'hidden.bs.modal', data.modalHidden);
            if (data.modalOpen) {
                restorePanel(data);
            }
        }
        data.el.append(popover.toggleMenu);
        Popover.dispose(popover)
    }
}
