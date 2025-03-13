import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, shownCallback, closeCallback) {
    const el = document.getElementById(id)
    const modal = {
        el,
        invoke,
        shownCallback,
        closeCallback,
        pop: () => {
            if (modal) {
                modal.hide();
            }
        },
        originalStyle: null
    }
    Data.set(id, modal)

    EventHandler.on(el, 'shown.bs.modal', () => {
        invoke.invokeMethodAsync(shownCallback)
    })
    EventHandler.on(el, 'hidden.bs.modal', e => {
        e.stopPropagation();
        invoke.invokeMethodAsync(closeCallback)
    })
    EventHandler.on(window, 'popstate', modal.pop)

    modal.show = () => {
        backupBodyStyle(modal);
        const dialogs = el.querySelectorAll('.modal-dialog')
        if (dialogs.length === 1) {
            let backdrop = el.getAttribute('data-bs-backdrop') !== 'static'
            if (!backdrop) {
                backdrop = 'static'
            }
            if (!modal.modal) {
                modal.modal = bootstrap.Modal.getOrCreateInstance(el)
                // hack: fix focusin event
                modal.modal._focustrap._handleFocusin = e => { }
            }
            modal.modal._config.keyboard = el.getAttribute('data-bs-keyboard') === 'true'
            modal.modal._config.backdrop = backdrop
            modal.modal.show()
        }
        else {
            modal.invoke.invokeMethodAsync(modal.shownCallback)

            modal.modal._config.keyboard = false
            modal.modal._config.backdrop = 'static'

            modal.handlerKeyboardAndBackdrop()
            el.classList.add('show');
        }
    }

    modal.hide = () => {
        if (el.children.length === 1) {
            modal.modal.hide();
            restoreBodyStyle(modal);
        }
        else {
            modal.invoke.invokeMethodAsync(modal.closeCallback)
        }
    }

    modal.toggle = () => {
        if (modal.modal) {
            modal.modal.toggle()
        }
        else {
            modal.show()
        }
    }

    modal.handlerKeyboardAndBackdrop = () => {
        if (!modal.hook_keyboard_backdrop) {
            modal.hook_keyboard_backdrop = true;

            modal.handlerEscape = e => {
                if (e.key === 'Escape') {
                    const keyboard = el.getAttribute('data-bs-keyboard')
                    if (keyboard === 'true') {
                        modal.hide()
                    }
                }
            }

            EventHandler.on(document, 'keyup', modal.handlerEscape)
            EventHandler.on(el, 'click', e => {
                if (e.target.closest('.modal-dialog') === null) {
                    const backdrop = el.getAttribute('data-bs-backdrop')
                    if (backdrop !== 'static') {
                        modal.hide()
                    }
                }
            })
        }
    }

    modal.disposeDrag = () => {
        if (modal.header) {
            EventHandler.off(modal.header, 'mousedown')
            EventHandler.off(modal.header, 'touchstart')
            modal.header = null
        }
    }
}

export function execute(id, method) {
    const modal = Data.get(id)
    if (method === 'show') {
        modal.show()
    }
    else if (method === 'hide') {
        modal.hide()
    }
    else if (method === 'toggle') {
        modal.toggle()
    }
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if (modal) {
        EventHandler.off(modal.el, 'shown.bs.modal')
        EventHandler.off(modal.el, 'hidden.bs.modal')
        EventHandler.off(modal.el, 'click')

        if (modal.hook_keyboard_backdrop) {
            EventHandler.off(document, 'keyup', modal.handlerEscape)
        }

        EventHandler.off(window, 'popstate', modal.pop)
        const dialog = modal.modal;
        if (dialog) {
            if (document.body.classList.contains('modal-open')) {
                dialog._backdrop._config.isAnimated = false;
                dialog._hideModal();
            }

            restoreBodyStyle(modal);
            dialog.dispose()
        }
    }
}

const backupBodyStyle = modal => {
    if (modal.originalStyle === null) {
        modal.originalStyle = document.body.style.cssText;
    }
}

const restoreBodyStyle = modal => {
    if (modal.originalStyle !== null) {
        document.body.style.cssText = modal.originalStyle;
        delete modal.originalStyle;
    }
}
