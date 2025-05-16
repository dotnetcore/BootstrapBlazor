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
    Data.set(id, toast);

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

export function update(id) {
    const t = Data.get(id);
    const { element, toast } = t;
    const autoHide = element.getAttribute('data-bs-autohide') !== 'false';
    if(autoHide) {
        const delay = parseInt(element.getAttribute('data-bs-delay'));
        const progressElement = element.querySelector('.toast-progress');

        toast._config.autohide = autoHide;
        toast._config.delay = delay;

        progressElement.style.width = '100%';
        progressElement.style.transition = `width linear ${delay / 1000}s`;
        EventHandler.on(progressElement, 'transitionend', e => {
           toast.hide();
        });
    }
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
