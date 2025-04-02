import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import { registerBootstrapBlazorModule } from "../../modules/utility.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const rt = {
        element: el, invoke, callback,
        handlerClick: e => {
            [...document.querySelectorAll('.ribbon-header.is-float.is-expand')].forEach(headerEl => {
                const tabId = headerEl.parentElement.getAttribute("id");
                const tab = Data.get(tabId);
                const { element, invoke, callback } = tab;

                const ribbonBody = e.target.closest('.ribbon-body');
                if (ribbonBody) {
                    invoke.invokeMethodAsync(callback);
                }
                else {
                    const ribbonTab = e.target.closest('.ribbon-tab')
                    if (ribbonTab !== element) {
                        invoke.invokeMethodAsync(callback);
                    }
                }
            });
        }
    }
    Data.set(id, rt)

    registerBootstrapBlazorModule('RibbonTab', id, () => {
        EventHandler.on(document, 'click', rt.handlerClick)
    });
}

export function dispose(id) {
    const rt = Data.get(id)
    Data.remove(id)

    const { RibbonTab } = window.BootstrapBlazor;
    RibbonTab.dispose(id, () => {
        EventHandler.off(document, 'click', rt.handlerClick)
    });
}
