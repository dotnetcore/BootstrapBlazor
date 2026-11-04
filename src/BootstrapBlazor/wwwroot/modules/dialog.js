import EventHandler from "./event-handler.js"

const Dialog = {
    init(el, config) {
        const dialog = el.querySelector('dialog')
        const options = {
            toggleSelector: '.dropdown-toggle',
            isDisabled: () => false,
            hideCallback: null,
            ...(config || {})
        }
        const instance = {
            mode: 'dialog',
            el,
            dialog,
            toggleSelector: options.toggleSelector,
            isShown: () => dialog.open,
            show: () => {
                if (!options.isDisabled() && !dialog.open) {
                    dialog.showModal()
                }
            },
            hide: () => {
                if (dialog.open) {
                    dialog.close()
                }
            },
            toggle: () => {
                if (dialog.open) {
                    instance.hide()
                }
                else {
                    instance.show()
                }
            },
            onToggle: e => {
                if (options.isDisabled()) {
                    e.preventDefault()
                }
                else {
                    instance.toggle()
                }
            },
            onClose: () => {
                if (options.hideCallback) {
                    options.hideCallback()
                }
            },
            onDialogClick: e => {
                if (e.target.closest('.btn-close')?.closest('dialog') === dialog) {
                    instance.hide()
                    return
                }

                if (e.target === dialog) {
                    const rect = dialog.getBoundingClientRect()
                    if (e.clientX < rect.left || e.clientX > rect.right || e.clientY < rect.top || e.clientY > rect.bottom) {
                        instance.hide()
                    }
                }
            }
        }

        EventHandler.on(el, 'click', options.toggleSelector, instance.onToggle)
        EventHandler.on(dialog, 'close', instance.onClose)
        EventHandler.on(dialog, 'click', instance.onDialogClick)

        return instance
    },

    dispose(instance) {
        EventHandler.off(instance.el, 'click', instance.toggleSelector, instance.onToggle)
        EventHandler.off(instance.dialog, 'close', instance.onClose)
        EventHandler.off(instance.dialog, 'click', instance.onDialogClick)
        if (instance.dialog.open) {
            instance.dialog.close()
        }
    }
}

export default Dialog
