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

export function layout_init() {
    console.log('begin layout init...');

    var handler = setInterval(() => {
        console.log('fetch header...');

        const layout = document.querySelector('.layout');
        if (layout) {
            console.log('fetch header... done');
            clearInterval(handler);

            const bar = layout.querySelector('.layout-header-bar');
            if (bar) {
                console.log('find bar... done');

                bootstrap.Tooltip.getOrCreateInstance(bar);
                EventHandler.on(bar, 'click', e => {
                    layout.classList.toggle('is-collapsed');
                });
            }
        }
    }, 50);
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
