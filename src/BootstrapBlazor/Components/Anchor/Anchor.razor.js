import { getDescribedElement, getOverflowParent } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const targetSelector = 'data-bb-target'
    const containerSelector = 'data-bb-container'
    const offsetSelector = 'data-bb-offset'
    const animation = el.getAttribute('data-bb-animation')

    EventHandler.on(el, 'click', e => {
        e.preventDefault()
        const target = getDescribedElement(el, targetSelector)
        if (target) {
            const container = getDescribedElement(el, containerSelector) || getOverflowParent(target)
            const containerRect = container.getBoundingClientRect()
            const rect = target.getBoundingClientRect()
            let margin = rect.top - containerRect.top + container.scrollTop
            let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '')
            if (marginTop) {
                margin = margin - parseInt(marginTop)
            }
            const offset = el.getAttribute(offsetSelector)
            if (offset) {
                margin = margin - parseInt(offset)
            }
            if (animation && 'style' in container) {
                container.style.scrollBehavior = 'smooth'
            }
            container.scrollTo(0, margin)
        }
    })
}

export function dispose(id) {
    const el = document.getElementById(id)
    EventHandler.off(el, 'click')
}
