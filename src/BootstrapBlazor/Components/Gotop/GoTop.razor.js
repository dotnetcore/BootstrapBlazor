import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, target) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const go = { el, target }
    Data.set(id, go)

    go.tip = new bootstrap.Tooltip(el)
    EventHandler.on(el, 'click', e => {
        e.preventDefault();
        const element = document.querySelector(target) || window
        element.scrollTop = 0
        go.tip.hide()
    })
}

export function dispose(id) {
    const go = Data.get(id)
    Data.remove(id)

    if (go) {
        EventHandler.off(go.el, 'click')
        go.tip.dispose()
        delete go.tip
    }
}
