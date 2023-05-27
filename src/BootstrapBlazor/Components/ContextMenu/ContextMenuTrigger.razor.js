import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, invoker, callback) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const trigger = { el, invoke, callback }
    Data.set(id, trigger)

    EventHandler.on(el, 'oncontextmenu', e => {
        console.log(e)
    })
}

export function dispose(id) {
    const trigger = Data.get(id)
    Data.remove(id)

    if (trigger) {
        const el = trigger.el
        EventHandler.off(el, 'oncontextmenu')
    }
}
