import { setIndeterminate } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.on(el, 'click', async e => {
        e.stopPropagation();

        const state = el.getAttribute("data-bb-state");
        const trigger = el.getAttribute("data-bb-trigger-before");

        if (state) {
            el.removeAttribute('data-bb-state');
            invoke.invokeMethodAsync(options.syncStateCallback, parseInt(state));

            if (trigger !== "true") {
                return;
            }
        }

        if (trigger === 'true') {
            e.preventDefault();
            await invoke.invokeMethodAsync(options.triggerOnBeforeStateChanged);
            return;
        }

        await invoke.invokeMethodAsync(options.triggerClick);
    });
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.off(el, 'click');
}

export { setIndeterminate }
