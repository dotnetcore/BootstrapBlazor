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
            if (modal.modal) {
                modal.modal._dialog.remove()
                modal.modal.dispose()
                modal.modal = null
                document.body.classList.remove('modal-open');
                document.body.style.paddingRight = '';
                document.body.style.overflow = '';
            }
        }
    }
    Data.set(id, modal)

    EventHandler.on(el, 'shown.bs.modal', () => {
        invoke.invokeMethodAsync(shownCallback)
    })
    EventHandler.on(el, 'hidden.bs.modal', e => {
        e.stopPropagation();
        if (modal.draggable) {
            modal.dialog.style.width = ''
            modal.dialog.style.margin = ''

            EventHandler.off(modal.dialog, 'mousedown')
            EventHandler.off(modal.dialog, 'touchstart')
        }
        invoke.invokeMethodAsync(closeCallback)
    })
    EventHandler.on(window, 'popstate', modal.pop)

    modal.show = () => {
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
        }
    }

    modal.hide = () => {
        if (el.children.length === 1) {
            modal.modal.hide();
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
        if (modal.modal) {
            modal.modal.dispose()
        }
    }
}
