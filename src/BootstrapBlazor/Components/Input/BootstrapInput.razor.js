﻿import EventHandler from "../../modules/event-handler.js"

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
            if (enter && (e.key === 'Enter' || e.key === 'NumpadEnter')) {
                invoke.invokeMethodAsync(enterCallbackMethod, { key: e.key, code: e.code, ctrlKey: e.ctrlKey, shiftKey: e.shiftKey, altKey: e.altKey, metaKey: e.metaKey, repeat: e.repeat, type: e.type, location: e.location }, el.value)
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
