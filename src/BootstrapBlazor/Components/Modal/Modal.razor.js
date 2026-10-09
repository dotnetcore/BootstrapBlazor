import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, shownCallback, closeCallback) {
    const el = document.getElementById(id)
    const modal = {
        el,
        invoke,
        shownCallback,
        closeCallback,
        pop: () => modal.close(),
        onShown: () => invoke.invokeMethodAsync(shownCallback),
        onHide: e => {
            if (!modal.approvedHide) {
                e.preventDefault();
                modal.close();
            }
        },
        onHidden: e => {
            e.stopPropagation();
            restoreBodyStyle(modal);
            invoke.invokeMethodAsync(closeCallback)
        },
        originalStyle: null,
        showRequest: 0,
        closeRequest: 0
    }
    Data.set(id, modal)

    EventHandler.on(el, 'shown.bs.modal', modal.onShown)
    EventHandler.on(el, 'hide.bs.modal', modal.onHide)
    EventHandler.on(el, 'hidden.bs.modal', modal.onHidden)
    EventHandler.on(window, 'popstate', modal.pop)

    modal.show = async () => {
        const request = ++modal.showRequest;
        if (modal.hiding) {
            await modal.hiding;
        }
        if (Data.get(id) !== modal || request !== modal.showRequest) {
            return;
        }
        const dialogs = el.querySelectorAll('.modal-dialog')
        if (dialogs.length === 0) {
            return;
        }
        backupBodyStyle(modal);
        if (dialogs.length === 1 || !modal.modal?._isShown) {
            const backdrop = el.getAttribute('data-bs-backdrop') === 'static' ? 'static' : true
            if (!modal.modal) {
                modal.modal = bootstrap.Modal.getOrCreateInstance(el)
                // hack: fix focusin event
                modal.modal._focustrap._handleFocusin = e => { }
            }

            modal.modal._dialog = [...dialogs].filter(dialog => !dialog.classList.contains('d-none')).pop() ?? dialogs[0]
            modal.modal._config.keyboard = dialogs.length === 1 && el.getAttribute('data-bs-keyboard') === 'true'
            modal.modal._config.backdrop = dialogs.length === 1 ? backdrop : 'static'
            if (dialogs.length > 1) {
                modal.handlerKeyboardAndBackdrop()
            }
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

    modal.hide = async () => {
        if (modal.modal?._isTransitioning && modal.modal._isShown) {
            await new Promise(resolve => {
                modal.cancelPendingHide = resolve;
                EventHandler.one(el, 'shown.bs.modal', resolve);
            });
            EventHandler.off(el, 'shown.bs.modal', modal.cancelPendingHide);
            delete modal.cancelPendingHide;
            if (Data.get(id) !== modal) {
                return;
            }
        }
        if (el.children.length !== 1) {
            return modal.invoke.invokeMethodAsync(modal.closeCallback)
        }
        if (!modal.modal._isShown) {
            return;
        }
        modal.hiding = new Promise(resolve => {
            const finishHide = () => {
                EventHandler.off(el, 'hidden.bs.modal', finishHide);
                delete modal.hiding;
                delete modal.finishHide;
                resolve();
            };
            modal.finishHide = finishHide;
            EventHandler.one(el, 'hidden.bs.modal', finishHide);
        });
        modal.approvedHide = true;
        try {
            modal.modal.hide();
            if (modal.modal._isShown) {
                throw new Error("The modal hide request was not completed.");
            }
        }
        catch (error) {
            modal.finishHide?.();
            throw error;
        }
        finally {
            modal.approvedHide = false;
        }
    }

    modal.toggle = () => el.classList.contains('show') ? modal.close() : modal.show()

    modal.close = async () => {
        if (!el.classList.contains('show')) {
            return;
        }
        const request = String(++modal.closeRequest);
        if (!await invoke.invokeMethodAsync("BeforeCloseWithRequestCallback", request)) {
            return;
        }
        try {
            return await modal.hide();
        }
        catch (error) {
            if (Data.get(id) === modal) {
                await invoke.invokeMethodAsync("CloseFailedCallback", request);
            }
            throw error;
        }
    }

    modal.handlerKeyboardAndBackdrop = () => {
        if (modal.hook_keyboard_backdrop) {
            return;
        }
        modal.hook_keyboard_backdrop = true;

        modal.handlerEscape = e => {
            if (e.key === 'Escape' && el.querySelectorAll('.modal-dialog').length > 1
                && el.getAttribute('data-bs-keyboard') === 'true') {
                modal.close();
            }
        }

        EventHandler.on(document, 'keyup', modal.handlerEscape)
        EventHandler.on(el, 'click', e => {
            if (el.querySelectorAll('.modal-dialog').length === 1 || e.target.closest('.modal-dialog') !== null) {
                return;
            }
            if (el.getAttribute('data-bs-backdrop') !== 'static') {
                modal.close();
            }
            else if (modal.modal) {
                const dialogs = [...el.querySelectorAll('.modal-dialog')].filter(d => !d.classList.contains('d-none'));
                if (dialogs.length > 0) {
                    modal.modal._dialog = dialogs[dialogs.length - 1];
                    modal.modal._triggerBackdropTransition();
                }
            }
        })
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
        return modal.show()
    }
    else if (method === 'hide') {
        return modal.hide()
    }
    else if (method === 'toggle') {
        return modal.toggle()
    }
}

export function isShown(id) {
    return Data.get(id)?.modal?._isShown ?? false;
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if (!modal) {
        return;
    }
    modal.cancelPendingHide?.();
    const dialog = modal.modal;
    const pendingTransition = modal.hiding ?? (dialog?._isTransitioning && dialog._isShown
        ? new Promise(resolve => EventHandler.one(modal.el, 'shown.bs.modal', resolve))
        : null);
    // Bootstrap animation callbacks still access the instance until shown/hidden.
    EventHandler.off(modal.el, 'shown.bs.modal', modal.onShown)
    EventHandler.off(modal.el, 'hide.bs.modal', modal.onHide)
    EventHandler.off(modal.el, 'hidden.bs.modal', modal.onHidden)
    EventHandler.off(modal.el, 'click')

    if (modal.hook_keyboard_backdrop) {
        EventHandler.off(document, 'keyup', modal.handlerEscape)
    }

    EventHandler.off(window, 'popstate', modal.pop)
    const disposeDialog = () => {
        if (!dialog) {
            return;
        }
        if (document.body.classList.contains('modal-open')) {
            dialog._backdrop._config.isAnimated = false;
            dialog._hideModal();
        }

        restoreBodyStyle(modal);
        dialog.dispose()
    };
    if (pendingTransition) {
        pendingTransition.then(disposeDialog);
    }
    else {
        disposeDialog();
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
