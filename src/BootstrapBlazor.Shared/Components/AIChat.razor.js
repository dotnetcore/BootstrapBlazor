import { insertAfter } from "../../../_content/BootstrapBlazor/modules/utility.js?v=$version"
import Data from "../../../_content/BootstrapBlazor/modules/data.js?v=$version"
import EventHandler from "../../../_content/BootstrapBlazor/modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const menu = el.querySelector('.chat-menu')

    const ai = { el, menu }
    Data.set(id, ai);

    EventHandler.on(el, 'click', () => {
        menu.classList.toggle('is-open')
    })
}

export function dispose(id) {
    const ai = Data.get(id)
    Data.remove(id)

    if (ai) {
        EventHandler.off(ai.el, 'click')
    }
}
