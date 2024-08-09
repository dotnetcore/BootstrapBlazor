import { setIndeterminate } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.on(el, 'click', async e => {
        e.preventDefault();

        const ret = await invoke.invokeMethodAsync(options.callback);
        if (ret) {

        }
    })
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.off(el, 'click');
}

export { setIndeterminate }
