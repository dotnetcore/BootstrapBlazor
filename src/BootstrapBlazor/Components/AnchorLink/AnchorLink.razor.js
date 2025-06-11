import { copy } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const anchorLink = {
        element: el
    }
    Data.set(id, anchorLink)

    if (el) {
        EventHandler.on(el, 'click', e => {
            e.preventDefault()
            const href = location.origin + location.pathname + '#' + id
            copy(href)

            const title = el.getAttribute('data-bb-title')
            if (title) {
                const tooltip = bootstrap.Tooltip.getOrCreateInstance(el, { title })
                tooltip.show()

                anchorLink.tooltip = tooltip
                anchorLink.handler = setTimeout(() => {
                    clearTimeout(anchorLink.handler)
                    tooltip.dispose()
                }, 1000)
            }
        })
    }
}

export function dispose(id) {
    const anchorLink = Data.get(id)
    Data.remove(id)

    if (anchorLink) {
        EventHandler.off(anchorLink.element, 'click')

        if (anchorLink.handler) {
            clearTimeout(anchorLink.handler)
        }
        if (anchorLink.tooltip) {
            anchorLink.tooltip.dispose()
        }
    }
}
