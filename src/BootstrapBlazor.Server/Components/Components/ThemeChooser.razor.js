import { insertAfter } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const themeList = el.querySelector('.theme-list')

    Data.set(id, { el });

    EventHandler.on(el, 'click', () => {
        themeList.classList.toggle('is-open')
    })
}

export function dispose(id) {
    const theme = Data.get(id)
    Data.remove(id)

    if (theme) {
        EventHandler.off(theme.el, 'click')
    }
}
