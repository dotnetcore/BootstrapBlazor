import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import { setModalOverlay } from "../../modules/fullscreen.js"

export function init(id, invoke, shownCallback, closeCallback) {
    const el = document.getElementById(id)
    const host = el.closest('dialog')
    const modal = {
        el,
        host,
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
        if (host.open) {
            host.close()
        }
        setModalOverlay(host, false)
        restoreBodyStyle(modal)
        invoke.invokeMethodAsync(closeCallback)
    })
    EventHandler.on(window, 'popstate', modal.pop)
    EventHandler.on(host, 'cancel', e => {
        e.preventDefault()
        if (el.getAttribute('data-bs-keyboard') === 'true'
            && modal.modal?._isShown && !modal.modal._isTransitioning) {
            modal.close()
        }
    })
    EventHandler.on(host, 'click', e => {
        if ((e.target === host || e.target === modal.modal?._backdrop._element)
            && el.getAttribute('data-bs-backdrop') !== 'static') {
            modal.close()
        }
    })
    modal.movePopover = e => {
        const tip = document.getElementById(e.target.getAttribute('aria-describedby'))
        if (tip && !host.contains(tip)) {
            host.append(tip)
        }
    }
    EventHandler.on(host, 'inserted.bs.tooltip', modal.movePopover)
    EventHandler.on(host, 'inserted.bs.popover', modal.movePopover)

    modal.show = () => {
        const dialogs = el.querySelectorAll('.modal-dialog')
        if (dialogs.length === 0) {
            return
        }

        const opening = !host.open
        if (opening) {
            host.showModal()
        }

        backupBodyStyle(modal);
        if (!modal.modal) {
            modal.modal = bootstrap.Modal.getOrCreateInstance(el)
            modal.modal._backdrop._config.rootElement = host
            // hack: fix focusin event
            modal.modal._focustrap._handleFocusin = e => { }
        }

        const multiple = dialogs.length > 1
        modal.modal._dialog = dialogs[0]
        modal.modal._config.keyboard = !multiple && el.getAttribute('data-bs-keyboard') === 'true'
        modal.modal._config.backdrop = multiple || el.getAttribute('data-bs-backdrop') === 'static' ? 'static' : true

        if (multiple) {
            modal.handlerKeyboardAndBackdrop()
        }

        if (!modal.modal._isShown) {
            modal.modal.show()
            if (opening) {
                if (modal.modal._isShown) {
                    setModalOverlay(host, true)
                }
                else {
                    if (host.open) {
                        host.close()
                    }
                    restoreBodyStyle(modal)
                }
            }
        }
        else if (multiple && !modal.modal._isTransitioning) {
            modal.invoke.invokeMethodAsync(modal.shownCallback)
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
        if (modal.modal?._isShown) {
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

            EventHandler.on(el, 'click', e => {
                if (e.target.closest('.modal-dialog') === null) {
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
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if (modal) {
        EventHandler.off(modal.el, 'shown.bs.modal')
        EventHandler.off(modal.el, 'hidden.bs.modal')
        EventHandler.off(modal.el, 'click')
        EventHandler.off(modal.host, 'cancel')
        EventHandler.off(modal.host, 'click')
        EventHandler.off(modal.host, 'inserted.bs.tooltip', modal.movePopover)
        EventHandler.off(modal.host, 'inserted.bs.popover', modal.movePopover)

        EventHandler.off(window, 'popstate', modal.pop)
        const dialog = modal.modal;
        if (dialog) {
            if (dialog._isShown || dialog._isTransitioning) {
                dialog._backdrop._config.isAnimated = false;
                dialog._hideModal();
            }

            restoreBodyStyle(modal);
            dialog.dispose()
        }

        if (modal.host.open) {
            modal.host.close()
        }
        setModalOverlay(modal.host, false)
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
        modal.originalStyle = null;
    }
}
