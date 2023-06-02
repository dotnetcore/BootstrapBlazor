import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.on(el, 'webkitAnimationEnd', e => {
            console.log(el, e)
            invoke.invokeMethodAsync(callback);
        })
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el) {
        EventHandler.off(el, 'webkitAnimationEnd')
    }
}
