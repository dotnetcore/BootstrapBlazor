import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const layout = {
        handler: () => {
            var width = window.innerWidth
            invoke.invokeMethodAsync(callback, width)
        }
    }
    Data.set(id, layout)

    const tooltip = document.querySelector('.layout-header [data-bs-toggle="tooltip"]')
    if (tooltip) {
        layout.tooltip = bootstrap.Tooltip.getOrCreateInstance(tooltip)
    }

    layout.handler();
    EventHandler.on(window, 'resize', layout.handler);
}

export function dispose(id) {
    const layout = Data.get(id)
    Data.remove(id)

    if (layout) {
        EventHandler.off(window, 'resize', layout.handler)

        if (layout.tooltip) {
            layout.tooltip.dispose()
        }
    }
}
