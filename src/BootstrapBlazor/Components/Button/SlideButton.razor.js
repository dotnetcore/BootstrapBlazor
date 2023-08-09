import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const button = document.getElementById(`${id}_button`)
    const list = el.querySelector('.slide-list')
    const slide = { el, button, list }
    Data.set(id, slide);

    EventHandler.on(button, 'click', () => {
        if (!list.classList.contains('show')) {
            reset(el, button, list)
        }
        list.classList.toggle('show')
    })
}

export function dispose(id) {
    const slide = Data.get(id)
    Data.remove(id)

    if (slide) {
        EventHandler.off(slide.button, 'click')
    }
}

const reset = (el, button, list) => {
    const placement = el.getAttribute('data-bb-placement') || 'auto'
    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth
    const listHeight = list.offsetHeight
    const listWidth = list.offsetWidth
    const offset = 8
    if (placement === 'top' || placement === 'top-start') {
        list.setAttribute('style', `bottom: ${buttonHeight + offset}px; left: 0;`)
    }
    else if (placement === 'top-center') {
        list.setAttribute('style', `bottom: ${buttonHeight + offset}px; left: ${(buttonWidth - listWidth) / 2}px;`)
    }
    else if (placement === 'top-end') {
        list.setAttribute('style', `bottom: ${buttonHeight + offset}px; right: 0;`)
    }
    else if (placement === 'left') {
        const width = button.offsetWidth
        const listWidth = list.offsetWidth
        list.setAttribute('style', `left: ${0 - width - listWidth - 8}px;`)
    }
}
