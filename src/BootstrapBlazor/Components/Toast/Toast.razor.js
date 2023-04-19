import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const toast = {
        element: el,
        invoke,
        callback,
        toast: new bootstrap.Toast(el),
        showProgress: () => {
            return toast.toast._config.autohide
        }
    }
    Data.set(id, toast)

    if (toast.showProgress()) {
        toast.progressElement = toast.element.querySelector('.toast-progress')
        const delay = toast.toast._config.delay / 1000
        toast.progressElement.style.transition = `width linear ${delay}s`
    }

    EventHandler.on(toast.element, 'shown.bs.toast', () => {
        if (toast.showProgress()) {
            toast.progressElement.style.width = '100%'
        }
    })
    EventHandler.on(toast.element, 'hidden.bs.toast', () => {
        toast.invoke.invokeMethodAsync(toast.callback)
    })
    toast.toast.show()
}

export function dispose(id) {
    const toast = Data.get(id)
    Data.remove(id)

    EventHandler.off(toast.element, 'shown.bs.toast')
    EventHandler.off(toast.element, 'hidden.bs.toast')

    if (toast.toast) {
        toast.toast.dispose()
    }
}
