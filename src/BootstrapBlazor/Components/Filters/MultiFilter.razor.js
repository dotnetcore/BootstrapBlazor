import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, options) {
    const el = document.getElementById(id)
    if (el == null) {
        return;
    }

    const { invoker, callback, alwaysTrigger } = options;
    const filterEl = el.closest('.filter-icon');
    if (filterEl) {
        const popoverEl = filterEl.querySelector('i[data-bs-toggle="bb.dropdown"]');
        if (popoverEl) {
            EventHandler.on(popoverEl, 'show.bs.popover', () => {
                if (alwaysTrigger === false) {
                    EventHandler.off(popoverEl, 'show.bs.popover');
                }
                invoker.invokeMethodAsync(callback);
            });
            Data.set(id, popoverEl);
        }
    }
}

export function dispose(id) {
    const popoverEl = Data.get(id)
    Data.remove(id)

    if (popoverEl) {
        EventHandler.off(popoverEl, 'show.bs.popover');
    }
}
