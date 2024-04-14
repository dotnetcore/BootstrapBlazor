import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    EventHandler.on(el, 'hidden.bs.collapse', e => {
        e.preventDefault()
        e.stopPropagation()
        invoke.invokeMethodAsync(callback, true)
    })
    EventHandler.on(el, 'shown.bs.collapse', e => {
        e.preventDefault()
        e.stopPropagation()
        invoke.invokeMethodAsync(callback, false)
    })
    Data.set(id, { el, invoke, callback })
}

export function dispose(id) {
    const card = Data.get(id)
    Data.remove(id)

    if (card === null) {
        return
    }

    EventHandler.off(card.el, 'hidden.bs.collapse')
    EventHandler.off(card.el, 'shown.bs.collapse')
}
