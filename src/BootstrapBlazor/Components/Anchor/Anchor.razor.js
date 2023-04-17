import { getDescribedElement, getWindowScroll } from "../../modules/utility.js"
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
            const container = getDescribedElement(el, containerSelector) || document.defaultView
            const rect = target.getBoundingClientRect()
            let margin = rect.top
            let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '')
            if (marginTop) {
                margin = margin - parseInt(marginTop)
            }
            const offset = el.getAttribute(offsetSelector)
            if (offset) {
                margin = margin - parseInt(offset)
            }
            let winScroll = container
            if (winScroll.scrollTop === undefined) {
                winScroll = getWindowScroll(container)
            }
            if (animation) {
                const top = margin + winScroll.scrollTop
                margin = winScroll.scrollTop
                const step = (top - margin) / 10
                let handler = window.setInterval(() => {
                    if (margin === top) {
                        window.clearInterval(handler)
                        handler = null
                    }
                    else {
                        margin += step
                        if (step > 0 && margin >= top) {
                            margin = top
                        }
                        else if (step < 0 && margin <= top) {
                            margin = top
                        }
                        container.scrollTo(0, margin)
                    }
                }, 10)
            }
            else {
                container.scrollTo(0, margin + winScroll.scrollTop)
            }
        }
    })
}

export function dispose(id) {
    const el = document.getElementById(id)
    EventHandler.off(el, 'click')
}
