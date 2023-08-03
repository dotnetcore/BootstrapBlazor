import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    EventHandler.on(el, 'click', '.card-collapse-bar', e => {
        invoke.invokeMethodAsync(callback)
    })
    Data.set(id, { el, invoke, callback })
}

export function dispose(id) {
    const card = Data.get(id)
    Data.remove(id)

    if (card === null) {
        return
    }

    EventHandler.off(card.el, 'click', '.card-collapse-bar')
}
