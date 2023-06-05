import { drag } from "../../modules/utility.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    const modal = {
        el
    }
    Data.set(id, modal)

    modal.disposeDrag = () => {
        if (modal.header) {
            EventHandler.off(modal.header, 'mousedown')
            EventHandler.off(modal.header, 'touchstart')
            modal.header = null
        }
    }

    const resize = el.querySelector('.modal-resizer')
    if (resize) {
        modal.resize = resize

        modal.disposeResizeDrag = () => {
            EventHandler.off(resize, 'mousedown')
            EventHandler.off(resize, 'touchstart')
            modal.resize = null
        }

        const dialog = el.querySelector('.modal-content')
        drag(resize,
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
                    el.style.setProperty('--bs-modal-width', `${newValX}px`)
                }
            },
            () => {
                dialog.classList.remove('is-resize')
            }
        )
    }
}

export function dispose(id) {
    const modal = Data.get(id)
    Data.remove(id)

    if (modal) {
        if (modal.draggable) {
            modal.disposeDrag()
        }

        if (modal.resize) {
            modal.disposeResizeDrag()
        }
    }
}
