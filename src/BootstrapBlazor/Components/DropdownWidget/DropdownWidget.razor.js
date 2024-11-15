import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const invokeMethod = e => {
        const item = e.target;
        const items = [...el.querySelectorAll("[data-bs-toggle=\"dropdown\"]")];
        const index = items.indexOf(item);
        invoke.invokeMethodAsync(method, index, e.type === 'shown.bs.dropdown');
    }

    const { method } = options;
    EventHandler.on(el, 'shown.bs.dropdown', invokeMethod);
    EventHandler.on(el, 'hidden.bs.dropdown', invokeMethod);
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el) {
        EventHandler.off(el, 'shown.bs.dropdown');
        EventHandler.off(el, 'hidden.bs.dropdown');
    }
}
