import Data from "./data.js"
import EventHandler from "./event-handler.js"

export function init(id, invoke, callback) {
    const resp = {
        invoke,
        callback,
        currentBreakpoint: getResponsive(),
        fn: () => {
            let lastBreakpoint = getResponsive()
            if (lastBreakpoint !== resp.currentBreakpoint) {
                resp.currentBreakpoint = lastBreakpoint
                invoke.invokeMethodAsync(callback, lastBreakpoint)
            }
        }
    }
    Data.set(id, resp)
    invoke.invokeMethodAsync(callback, resp.currentBreakpoint)
    EventHandler.on(window, 'resize', resp.fn)
}

export function dispose(id) {
    const resp = Data.get(id)
    Data.remove(id)

    if (resp) {
        EventHandler.off(window, 'resize', resp.fn)
    }
}

export function getResponsive() {
    return window.getComputedStyle(document.body, ':before').content.replace(/\"/g, '')
}
