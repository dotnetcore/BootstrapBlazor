import { drag, getHeight, getWidth } from "../../modules/utility.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

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

    const resizer = el.querySelector('.modal-resizer')
    if(resizer) {
        const dialog = el.querySelector('.modal-dialog')
        drag(resizer,
            e => {
                dialog.originX = e.clientX || e.touches[0].clientX;
                dialog.originY = e.clientY || e.touches[0].clientY;

                const rect = dialog.getBoundingClientRect()
                dialog.dialogWidth = rect.width
                dialog.dialogHeight = rect.height

                dialog.style.maxWidth = 'auto'
                dialog.style.width = `${dialog.dialogWidth}px`
                dialog.style.height = `${dialog.dialogHeight}px`
                dialog.classList.add('is-resize')
            },
            e => {
                if (dialog.classList.contains('is-resize')) {
                    const eventX = e.clientX || e.changedTouches[0].clientX;
                    const eventY = e.clientY || e.changedTouches[0].clientY;

                    let newValX = dialog.dialogWidth + Math.ceil(eventX - dialog.originX);
                    let newValY = dialog.dialogHeight + Math.ceil(eventY - dialog.originY);

                    if (newValX > window.innerWidth) {
                        newValX = window.innerWidth
                    }
                    if (newValY > window.innerHeight) {
                        newValY = window.innerHeight
                    }

                    dialog.style.maxWidth = `${newValX}px`
                    dialog.style.width = `${newValX}px`
                    dialog.style.height = `${newValY}px`
                }
            },
            () => {
                dialog.classList.remove('is-drag')
            })
    }

    EventHandler.on(el, 'shown.bs.modal', () => {
        invoke.invokeMethodAsync(shownCallback)
    })
    EventHandler.on(el, 'hide.bs.modal', e => {
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
                modal.modal = bootstrap.Modal.getOrCreateInstance(el, { focus: false })
            }
            modal.modal._config.keyboard = el.getAttribute('data-bs-keyboard') === 'true'
            modal.modal._config.backdrop = backdrop
            modal.modal.show()
        } else {
            modal.invoke.invokeMethodAsync(modal.shownCallback)

            modal.modal._config.keyboard = false
            modal.modal._config.backdrop = 'static'

            modal.handlerKeyboardAndBackdrop()
        }

        modal.dialog = dialogs[dialogs.length - 1]
        modal.draggable = modal.dialog.classList.contains('is-draggable')
        if (modal.draggable) {
            modal.disposeDrag()

            modal.originX = 0;
            modal.originY = 0;
            modal.dialogWidth = 0;
            modal.dialogHeight = 0;
            modal.pt = { top: 0, left: 0 };

            modal.header = modal.dialog.querySelector('.modal-header')
            drag(modal.header,
                e => {
                    if (e.target.closest('.modal-header-buttons')) {
                        return true
                    }
                    modal.originX = e.clientX || e.touches[0].clientX;
                    modal.originY = e.clientY || e.touches[0].clientY;

                    const rect = modal.dialog.querySelector('.modal-content').getBoundingClientRect()
                    modal.dialogWidth = rect.width
                    modal.dialogHeight = rect.height
                    modal.pt.top = rect.top
                    modal.pt.left = rect.left

                    modal.dialog.style.margin = `${modal.pt.top}px 0 0 ${modal.pt.left}px`
                    modal.dialog.style.width = `${modal.dialogWidth}px`
                    modal.dialog.classList.add('is-drag')
                },
                e => {
                    if (modal.dialog.classList.contains('is-drag')) {
                        const eventX = e.clientX || e.changedTouches[0].clientX;
                        const eventY = e.clientY || e.changedTouches[0].clientY;

                        let newValX = modal.pt.left + Math.ceil(eventX - modal.originX);
                        let newValY = modal.pt.top + Math.ceil(eventY - modal.originY);

                        if (newValX <= 0) newValX = 0;
                        if (newValY <= 0) newValY = 0;

                        if (newValX + modal.dialogWidth < window.innerWidth) {
                            modal.dialog.style.marginLeft = `${newValX}px`
                        }
                        if (newValY + modal.dialogHeight < window.innerHeight) {
                            modal.dialog.style.marginTop = `${newValY}px`
                        }
                    }
                },
                () => {
                    modal.dialog.classList.remove('is-drag')
                })
        }
    }

    modal.hide = () => {
        if (el.children.length === 1) {
            modal.modal.hide()
        } else {
            modal.invoke.invokeMethodAsync(modal.closeCallback)
        }
    }

    modal.toggle = () => {
        if (modal.modal) {
            modal.modal.toggle()
        } else {
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
    } else if (method === 'hide') {
        modal.hide()
    } else if (method === 'toggle') {
        modal.toggle()
    }
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if(modal) {
        if (modal.draggable) {
            modal.disposeDrag()
        }

        EventHandler.off(modal.el, 'shown.bs.modal')
        EventHandler.off(modal.el, 'hide.bs.modal')
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
