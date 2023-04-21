import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const rt = {
        element: el, invoke, callback,
        handlerClick: e => {
            const isFloat = el.classList.contains('is-float')
            if (isFloat) {
                const expanded = el.classList.contains('is-expand')
                if (expanded) {
                    const ribbonBody = e.target.closest('.ribbon-body');
                    if (ribbonBody) {
                        invoke.invokeMethodAsync(callback)
                    }
                    else {
                        const ribbonTab = e.target.closest('.ribbon-tab')
                        if (ribbonTab !== el) {
                            invoke.invokeMethodAsync(callback)
                        }
                    }
                }
            }
        }
    }
    Data.set(id, rt)

    EventHandler.on(document, 'click', rt.handlerClick)
}

export function dispose(id) {
    const rt = Data.get(id)
    Data.remove(id)

    EventHandler.off(document, 'click', rt.handlerClick)
}
