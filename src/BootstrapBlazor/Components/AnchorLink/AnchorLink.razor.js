import { copy } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const anchorLink = {}
    Data.set(id, anchorLink)

    const el = document.getElementById(id)
    const title = el.getAttribute('data-bb-title')
    anchorLink._element = el

    EventHandler.on(el, 'click', e => {
        e.preventDefault()
        const href = location.origin + location.pathname + '#' + id
        copy(href)

        if (title) {
            const tooltip = bootstrap.Tooltip.getOrCreateInstance(el, { title })
            tooltip.show()

            anchorLink._tooltip = tooltip
            anchorLink.handler = setTimeout(() => {
                clearTimeout(anchorLink.handler)
                tooltip.dispose()
            }, 1000)
        }
    })
}

export function dispose(id) {
    const anchorLink = Data.get(id)

    EventHandler.off(anchorLink._element, 'click')

    if (anchorLink.handler) {
        clearTimeout(anchorLink.handler)
    }
    if (anchorLink.tooltip) {
        anchorLink.tooltip.dispose()
    }
}
