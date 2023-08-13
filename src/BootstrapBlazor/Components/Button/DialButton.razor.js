import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
}

export function dispose(id) {
    const dial = Data.get(id)
    Data.remove(id)

    if (slide) {
    }
}
