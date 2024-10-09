import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const { method } = options;
    EventHandler.on(el, 'hidden.bs.dropdown', e => {
        const item = e.target;
        const items = [...el.querySelectorAll("[data-bs-toggle=\"dropdown\"]")];
        const index = items.indexOf(item);
        invoke.invokeMethodAsync(method, index);
    });
}

export function dispose(id) {
    const dw = Data.get(id)
    Data.remove(id);

    EventHandler.off(el, 'hidden.bs.dropdown');
}
