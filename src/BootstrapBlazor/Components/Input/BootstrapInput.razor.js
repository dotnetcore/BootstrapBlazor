import EventHandler from "../../modules/event-handler.js"

export function handleKeyup(el, invoke, enter, enterCallbackMethod, esc, escCallbackMethod) {
    EventHandler.on(el, 'keyup', e => {
        if (enter && e.key === 'Enter') {
            invoke.invokeMethodAsync(enterCallbackMethod, el.value);
        }
        else if (esc && e.key === 'Escape') {
            invoke.invokeMethodAsync(escCallbackMethod);
        }
    });
}

export function select(el) {
    el.select()
}

export function selectAllByFocus(el) {
    EventHandler.on(el, 'focus', () => {
        el.select();
    });
}

export function selectAllByEnter(el) {
    EventHandler.on(el, 'keyup', e => {
        if (e.key === 'Enter') {
            el.select();
        }
    });
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.off(el, 'keyup')
        EventHandler.off(el, 'focus')
    }
}
