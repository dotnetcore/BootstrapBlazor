import { setIndeterminate } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.on(el, 'statechange.bb.checkbox', e => {
        invoke.invokeMethodAsync(method, e.state);
    });
}

export function update(id, state, val) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    setIndeterminate(id, state);
    if (state === false && el.checked !== val) {
        el.checked = val;
    }
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    EventHandler.off(el, 'statechange.bb.checkbox');
}
