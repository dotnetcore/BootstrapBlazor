import { drag } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const dialog = {
        el
    }
    Data.set(id, dialog)

    dialog.draggable = el.classList.contains('is-draggable')
    if (dialog.draggable) {
        dialog.disposeDrag = () => {
            if (dialog.header) {
                EventHandler.off(dialog.header, 'mousedown')
                EventHandler.off(dialog.header, 'touchstart')
                dialog.header = null
            }
        }

        dialog.originX = 0;
        dialog.originY = 0;
        dialog.dialogWidth = 0;
        dialog.dialogHeight = 0;
        dialog.pt = { top: 0, left: 0 };

        dialog.header = el.querySelector('.modal-header')
        drag(dialog.header,
            e => {
                if (e.target.closest('.modal-header-buttons')) {
                    return true
                }
                dialog.originX = e.clientX || (e.touches != null && e.touches[0].clientX) || 0;
                dialog.originY = e.clientY || (e.touches != null && e.touches[0].clientY) || 0;

                const rect = el.querySelector('.modal-content').getBoundingClientRect()
                dialog.dialogWidth = rect.width
                dialog.dialogHeight = rect.height
                dialog.pt.top = rect.top
                dialog.pt.left = rect.left

                el.style.margin = `${dialog.pt.top}px 0 0 ${dialog.pt.left}px`
                el.style.width = `${dialog.dialogWidth}px`
                el.classList.add('is-drag')
            },
            e => {
                if (el.classList.contains('is-drag')) {
                    const eventX = e.clientX || (e.touches != null && e.touches[0].clientX) || 0;
                    const eventY = e.clientY || (e.touches != null && e.touches[0].clientY) || 0;

                    let newValX = dialog.pt.left + Math.ceil(eventX - dialog.originX);
                    let newValY = dialog.pt.top + Math.ceil(eventY - dialog.originY);

                    if (newValX <= 0) newValX = 0;
                    if (newValY <= 0) newValY = 0;

                    if (newValX + dialog.dialogWidth < window.innerWidth) {
                        el.style.marginLeft = `${newValX}px`
                    }
                    if (newValY + dialog.dialogHeight < window.innerHeight) {
                        el.style.marginTop = `${newValY}px`
                    }
                }
            },
            () => {
                el.classList.remove('is-drag')
            }
        )
    }

    const resize = el.querySelector('.modal-resizer')
    if (resize) {
        dialog.resize = resize
        dialog.disposeResizeDrag = () => {
            EventHandler.off(resize, 'mousedown')
            EventHandler.off(resize, 'touchstart')
            dialog.resize = null
        }

        const content = el.querySelector('.modal-content')
        drag(resize,
            e => {
                content.originX = e.clientX || (e.touches != null && e.touches[0].clientX) || 0;
                content.originY = e.clientY || (e.touches != null && e.touches[0].clientY) || 0;

                const rect = content.getBoundingClientRect()
                content.dialogWidth = rect.width
                content.dialogHeight = rect.height

                content.style.maxWidth = 'auto'
                content.style.width = `${content.dialogWidth}px`
                content.style.height = `${content.dialogHeight}px`
                content.classList.add('is-resize')
            },
            e => {
                if (content.classList.contains('is-resize')) {
                    const eventX = e.clientX || (e.touches != null && e.touches[0].clientX) || 0;
                    const eventY = e.clientY || (e.touches != null && e.touches[0].clientY) || 0;

                    let newValX = content.dialogWidth + Math.ceil(eventX - content.originX);
                    let newValY = content.dialogHeight + Math.ceil(eventY - content.originY);

                    if (newValX > window.innerWidth) {
                        newValX = window.innerWidth
                    }
                    if (newValY > window.innerHeight) {
                        newValY = window.innerHeight
                    }

                    content.style.maxWidth = `${newValX}px`
                    content.style.width = `${newValX}px`
                    content.style.height = `${newValY}px`
                    el.style.setProperty('--bs-modal-width', `${newValX}px`)
                }
            },
            () => {
                content.classList.remove('is-resize')
            }
        )
    }
}

export function dispose(id) {
    const dialog = Data.get(id)
    Data.remove(id)

    if (dialog) {
        if (dialog.draggable) {
            dialog.disposeDrag()
        }

        if (dialog.resize) {
            dialog.disposeResizeDrag()
        }
    }
}
