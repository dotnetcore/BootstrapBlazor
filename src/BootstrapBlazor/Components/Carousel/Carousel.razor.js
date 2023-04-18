import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const options = { delay: 10 }
    const carousel = {
        element: el,
        controls: el.querySelectorAll('[data-bs-slide]'),
        carousel: new bootstrap.Carousel(el, options)
    }
    Data.set(id, carousel)

    EventHandler.on(el, 'mouseenter', () => {
        carousel.enterHandler = setTimeout(() => {
            clearTimeout(carousel.enterHandler)
            carousel.enterHandler = null
            el.classList.add('hover')
        }, options.delay)
    })

    EventHandler.on(el, 'mouseleave', () => {
        carousel.leaveHandler = setTimeout(() => {
            window.clearTimeout(carousel.leaveHandler)
            carousel.leaveHandler = null
            el.classList.remove('hover')
        }, options.delay)
    })
}

export function dispose(id) {
    const carousel = Data.get(id)
    Data.remove(id)

    if (carousel.carousel !== null) {
        carousel.carousel.dispose()
    }
    if (carousel.enterHandler) {
        window.clearTimeout(carousel.enterHandler)
    }
    if (carousel.leaveHandler) {
        window.clearTimeout(carousel.leaveHandler)
    }
    EventHandler.off(carousel.element, 'mouseenter')
    EventHandler.off(carousel.element, 'mouseleave')
}
