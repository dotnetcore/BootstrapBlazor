import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const { invoke, method, delay = 10 } = options;
    const carousel = {
        element: el,
        controls: el.querySelectorAll('[data-bs-slide]'),
        carousel: new bootstrap.Carousel(el, { delay })
    }
    Data.set(id, carousel)

    EventHandler.on(el, 'mouseenter', () => {
        carousel.enterHandler = setTimeout(() => {
            clearTimeout(carousel.enterHandler)
            carousel.enterHandler = null
            el.classList.add('hover')
        }, delay)
    })

    EventHandler.on(el, 'mouseleave', () => {
        carousel.leaveHandler = setTimeout(() => {
            window.clearTimeout(carousel.leaveHandler)
            carousel.leaveHandler = null
            el.classList.remove('hover')
        }, delay)
    })

    if (method) {
        const slides = el.querySelectorAll('.carousel-item');
        EventHandler.on(el, 'slid.bs.carousel', e => {
            const active = el.querySelector('.carousel-item.active');
            if (active) {
                const index = [...slides].indexOf(active);
                invoke.invokeMethodAsync(method, index);
            }
        })
    }
}

export function dispose(id) {
    const carousel = Data.get(id)
    Data.remove(id)

    if (carousel === null) {
        return
    }

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
    EventHandler.off(carousel.element, 'slid.bs.carousel')
}
