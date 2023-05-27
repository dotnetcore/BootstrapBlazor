import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    const cm = {
        element: el
    }
    Data.set(id, cm)

    if (el) {
        EventHandler.on(el, 'click', e => {
        })
    }
}

export function dispose(id) {
    const cm = Data.get(id)
    Data.remove(id)

    if (cm) {

    }
}
