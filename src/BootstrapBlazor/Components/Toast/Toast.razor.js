import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const progressElement = el.querySelector('.toast-progress')
    const toast = {
        element: el,
        invoke,
        callback,
        toast: new bootstrap.Toast(el),
        showProgress: () => {
            return toast.toast._config.autohide
        },
        progressElement
    }
    Data.set(id, toast);

    if (toast.showProgress()) {
        const delay = toast.toast._config.delay / 1000
        progressElement.style.transition = `width linear ${delay}s`
    }

    EventHandler.on(el, 'shown.bs.toast', () => {
        if (toast.showProgress()) {
            progressElement.style.width = '100%'
        }
    })
    EventHandler.on(el, 'hidden.bs.toast', () => {
        invoke.invokeMethodAsync(toast.callback)
    })
    EventHandler.on(progressElement, 'transitionend', e => {
        if (toast.toast._config.autohide === false) {
            toast.toast.hide();
        }
    });

    toast.toast.show();
}

export function update(id) {
    const t = Data.get(id);
    const { element, toast, progressElement } = t;
    const autoHide = element.getAttribute('data-bs-autohide') !== 'false';
    if (autoHide) {
        const delay = parseInt(element.getAttribute('data-bs-delay'));
        toast._config.delay = delay;

        progressElement.style.width = '100%';
        progressElement.style.transition = `width linear ${delay / 1000}s`;
    }
    else {
        toast._config.autohide = false;
        progressElement.style.removeProperty('width');
        progressElement.style.removeProperty('transition');
    }
}

export function dispose(id) {
    const toast = Data.get(id)
    Data.remove(id)

    const { element, progressElement } = toast;
    EventHandler.off(element, 'shown.bs.toast');
    EventHandler.off(element, 'hidden.bs.toast');
    EventHandler.off(progressElement, 'transitionend');

    if (toast.toast) {
        toast.toast.dispose();
    }
}
