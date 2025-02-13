import { select, handleKeyUp, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    var el = document.getElementById(id);
    const text = {
        prevMethod: '',
        element: el
    }

    Data.set(id, text);
    EventHandler.on(el, 'keydown', e => {
        if (e.key === "Enter" || e.key === "NumpadEnter") {
            const useShiftEnter = el.getAttribute('data-bb-shift-enter') === 'true';
            const shiftKey = e.shiftKey;
            if (!shiftKey || !useShiftEnter) {
                e.preventDefault();
            }
        }
    });
}

export function execute(id, method, position) {
    const text = Data.get(id)

    if (text) {
        const autoScroll = text.element.getAttribute('data-bb-scroll') === 'auto'
        if (method === 'update') {
            method = text.prevMethod
        }
        if (method === 'toTop') {
            position = 0;
        }
        if (autoScroll || method === 'toBottom') {
            position = text.element.scrollHeight
        }

        if (!isNaN(position)) {
            text.element.scrollTop = position;
        }

        if (method !== 'update') {
            text.prevMethod = method;
        }
    }
}

export function dispose(id) {
    const text = Data.get(id);
    Data.remove(id);

    if (text) {
        EventHandler.off(text.element);
    }
}

export { select, handleKeyUp, selectAllByFocus, selectAllByEnter }
