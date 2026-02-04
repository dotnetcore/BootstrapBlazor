import EventHandler from "../../modules/event-handler.js"


export function init(id, invoke, method) {
    const el = document.getElementById(id)
    EventHandler.on(el, 'focus', e => {
        const isString = e.target.getAttribute('type') === 'text';
        const isNumber = e.target.classList('class').contains("input-number")
        if (isString && isNumber) {
            let value = e.target.value;
            e.target.setAttribute('type', 'number');
            e.target.value = value.split(',').join('');
            invoke.invokeMethodAsync(method, true);
        }
    });
    EventHandler.on(el, 'blur', e => {
        const isString = e.target.getAttribute('type') === 'number';
        const isNumber = e.target.classList('class').contains("input-number")
        if (isString && isNumber) {
            e.target.setAttribute('type', 'text');
            invoke.invokeMethodAsync(method, false);
        }

    });
}

export function focus(id) {
    const el = document.getElementById(id)
    if (el) {
        el.focus();
    }
}

export function clear(id) {
    const el = document.getElementById(id)
    if (el) {
        el.value = '';
    }
}

export function handleKeyUp(id, invoke, enter, enterCallbackMethod, esc, escCallbackMethod) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.on(el, 'keyup', e => {
            if (enter && (e.key === 'Enter')) {
                const useShiftEnter = el.getAttribute('data-bb-shift-enter') === 'true';
                if (!e.shiftKey && useShiftEnter) {
                    return;
                }
                invoke.invokeMethodAsync(enterCallbackMethod, el.value)
            }
            else if (esc && e.key === 'Escape') {
                invoke.invokeMethodAsync(escCallbackMethod)
            }
        })
    }
}

export function select(id) {
    const el = document.getElementById(id)
    if (el) {
        el.select()
    }
}

export function selectAllByFocus(id) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.on(el, 'focus', () => {
            el.select()
        })
    }
}

export function selectAllByEnter(id) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.on(el, 'keyup', e => {
            if (e.key === 'Enter') {
                el.select()
            }
        })
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.off(el, 'keyup')
        EventHandler.off(el, 'focus')
    }
}
