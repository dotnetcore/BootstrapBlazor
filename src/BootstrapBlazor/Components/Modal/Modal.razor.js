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
    EventHandler.on(el, 'hide.bs.modal', e => {
        if (e.target === el) {
            modal.hiddenTask = new Promise(resolve => modal.resolveHidden = resolve)
        }
    })
    EventHandler.on(el, 'hidden.bs.modal', async e => {
        e.stopPropagation();
        const resolveHidden = modal.resolveHidden
        modal.hiddenTask = null
        modal.resolveHidden = null
        try {
            await invoke.invokeMethodAsync(closeCallback)
        }
        finally {
            resolveHidden?.()
        }
    })
    EventHandler.on(window, 'popstate', modal.pop)

    modal.show = () => {
        const dialogs = el.querySelectorAll('.modal-dialog')
        const dialog = [...dialogs].reverse().find(d => !d.classList.contains('d-none'))
        if (!dialog) {
            return
        }

        backupBodyStyle(modal);
        if (!modal.modal) {
            // Handle nested dismissal before Bootstrap's native listeners.
            modal.handlerKeyboardAndBackdrop()
            modal.modal = bootstrap.Modal.getOrCreateInstance(el)
            // hack: fix focusin event
            modal.modal._focustrap._handleFocusin = e => { }
        }

        const multiple = dialogs.length > 1
        modal.modal._dialog = dialog
        modal.modal._config.keyboard = !multiple && el.getAttribute('data-bs-keyboard') === 'true'
        modal.modal._config.backdrop = multiple || el.getAttribute('data-bs-backdrop') === 'static' ? 'static' : true

        if (!modal.modal._isShown) {
            modal.modal.show()
        }
        else if (multiple && !modal.modal._isTransitioning) {
            modal.invoke.invokeMethodAsync(modal.shownCallback)
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

    modal.close = async () => {
        const close = await invoke.invokeMethodAsync("BeforeCloseCallback");
        if (close) {
            modal.hide();
        }
    }

    modal.handlerKeyboardAndBackdrop = () => {
        if (!modal.hook_keyboard_backdrop) {
            modal.hook_keyboard_backdrop = true;

            const isMultiple = () => el.querySelectorAll('.modal-dialog').length > 1
            modal.handlerKeydown = e => {
                if (isMultiple() && e.key === 'Escape' && el.getAttribute('data-bs-keyboard') === 'true') {
                    e.stopImmediatePropagation()
                }
            }
            modal.handlerMousedown = e => {
                if (isMultiple() && e.target === el && el.getAttribute('data-bs-backdrop') !== 'static') {
                    e.stopImmediatePropagation()
                }
            }
            EventHandler.on(el, 'keydown', modal.handlerKeydown)
            EventHandler.on(el, 'mousedown', modal.handlerMousedown)

            modal.handlerEscape = async e => {
                if (isMultiple() && e.key === 'Escape') {
                    const keyboard = el.getAttribute('data-bs-keyboard')
                    if (keyboard === 'true') {
                        modal.close();
                    }
                }
            }

            EventHandler.on(document, 'keyup', modal.handlerEscape)
            EventHandler.on(el, 'click', e => {
                if (isMultiple() && e.target.closest('.modal-dialog') === null) {
                    const backdrop = el.getAttribute('data-bs-backdrop')
                    if (backdrop !== 'static') {
                        modal.close();
                    }
                    else if (modal.modal) {
                        const dialogs = [...el.querySelectorAll('.modal-dialog')].filter(d => !d.classList.contains('d-none'));
                        if (dialogs.length > 0) {
                            modal.modal._dialog = dialogs[dialogs.length - 1];
                            modal.modal._triggerBackdropTransition();
                        }
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
    else if (method === 'waitForHide') {
        return modal.modal?._isShown ? null : modal.hiddenTask
    }
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if (modal) {
        EventHandler.off(modal.el, 'shown.bs.modal')
        EventHandler.off(modal.el, 'hide.bs.modal')
        EventHandler.off(modal.el, 'hidden.bs.modal')
        EventHandler.off(modal.el, 'click')

        if (modal.hook_keyboard_backdrop) {
            EventHandler.off(document, 'keyup', modal.handlerEscape)
            EventHandler.off(modal.el, 'keydown', modal.handlerKeydown)
            EventHandler.off(modal.el, 'mousedown', modal.handlerMousedown)
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
        modal.resolveHidden?.()
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
