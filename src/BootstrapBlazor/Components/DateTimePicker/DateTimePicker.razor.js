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
        picker.modalHidden = () => restorePanel(picker);
        EventHandler.on(modal, 'hidden.bs.modal', picker.modalHidden);
    }

    picker.parentModal = picker.el.closest('.modal.show');
    picker.modalOpen = true;
    picker.el.classList.add('show');
    body.append(picker.popover.toggleMenu);
    initSidebar(picker);
}

const initSidebar = picker => {
    const panel = picker.popover.toggleMenu;
    picker.sidebarInertItems = new Set();
    const update = () => {
        picker.sidebarInertItems.forEach(el => el.inert = false);
        picker.sidebarInertItems.clear();
        const content = panel.querySelector('.picker-panel-sidebar-content');
        if (!content || !content.getClientRects().length) {
            return;
        }

        picker.sidebarResizeObserver.observe(content);
        const viewport = content.parentElement;
        const button = viewport.nextElementSibling;
        let row = content;
        while (row.children.length === 1) {
            row = row.firstElementChild;
        }

        const items = [...row.children].map(el => ({ el, rect: el.getBoundingClientRect() }))
            .filter(item => item.rect.height > 0);
        const top = content.getBoundingClientRect().top;
        const firstTop = Math.min(...items.map(item => item.rect.top));
        const firstRow = items.filter(item => Math.abs(item.rect.top - firstTop) < 1);
        const bottom = firstRow.length
            ? Math.max(...firstRow.map(item => item.rect.bottom))
            : content.getBoundingClientRect().bottom;
        const height = Math.ceil(bottom - top);
        const overflow = content.scrollHeight > height + 1;
        const expanded = overflow && button.getAttribute('aria-expanded') === 'true';
        viewport.style.setProperty('--bb-picker-sidebar-row-height', `${height}px`);
        viewport.classList.toggle('is-expanded', expanded);
        button.hidden = !overflow;
        button.setAttribute('aria-expanded', `${expanded}`);
        for (const { el, rect } of items) {
            if (!expanded && rect.top >= bottom && !el.inert) {
                el.inert = true;
                picker.sidebarInertItems.add(el);
            }
        }
    };

    picker.sidebarResizeObserver = new ResizeObserver(update);
    picker.sidebarResizeObserver.observe(panel);
    picker.sidebarMutationObserver = new MutationObserver(update);
    picker.sidebarMutationObserver.observe(panel, { childList: true, subtree: true, characterData: true });
    EventHandler.on(panel, 'click', '.picker-panel-sidebar-toggle', e => {
        const button = e.delegateTarget;
        button.setAttribute('aria-expanded', `${button.getAttribute('aria-expanded') !== 'true'}`);
        update();
    });
    update();
}

const disposeSidebar = picker => {
    picker.sidebarResizeObserver?.disconnect();
    picker.sidebarMutationObserver?.disconnect();
    const panel = picker.popover.toggleMenu;
    EventHandler.off(panel, 'click', '.picker-panel-sidebar-toggle');
    picker.sidebarInertItems?.forEach(el => el.inert = false);
    const button = panel.querySelector('.picker-panel-sidebar-toggle');
    if (button) {
        button.setAttribute('aria-expanded', 'false');
        button.hidden = true;
        button.previousElementSibling.classList.remove('is-expanded');
    }
}

const restorePanel = picker => {
    disposeSidebar(picker);
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
            EventHandler.off(data.modal, 'hidden.bs.modal', data.modalHidden);
            if (data.modalOpen) {
                restorePanel(data);
            }
        }
        disposeSidebar(data);
        data.el.append(popover.toggleMenu);
        Popover.dispose(popover)
    }
}
