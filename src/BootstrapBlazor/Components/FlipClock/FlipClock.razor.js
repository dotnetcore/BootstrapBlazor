import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, invoke, method) {

}

export function dispose(id) {
    const clock = Data.get(id)
    if (clock) {
    }
}
