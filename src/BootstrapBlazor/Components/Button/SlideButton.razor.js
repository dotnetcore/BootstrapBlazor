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
    const offset = parseFloat(el.getAttribute('data-bb-offset') || '4')
    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth
    const listHeight = list.offsetHeight
    const listWidth = list.offsetWidth
    let style = null
    if (placement === 'auto' || placement === 'top' || placement === 'top-start') {
        style = `bottom: ${buttonHeight + offset}px; left: 0;`
    }
    else if (placement === 'top-center') {
        style = `bottom: ${buttonHeight + offset}px; left: ${(buttonWidth - listWidth) / 2}px;`
    }
    else if (placement === 'top-end') {
        style = `bottom: ${buttonHeight + offset}px; right: 0;`
    }
    else if (placement === 'bottom' || placement === 'bottom-start') {
        style = `top: ${buttonHeight + offset}px; left: 0;`
    }
    else if (placement === 'bottom' || placement === 'bottom-center') {
        style = `top: ${buttonHeight + offset}px; left: ${(buttonWidth - listWidth) / 2}px;`
    }
    else if (placement === 'bottom' || placement === 'bottom-end') {
        style = `top: ${buttonHeight + offset}px; right: 0;`
    }
    list.setAttribute('style', style)
}
