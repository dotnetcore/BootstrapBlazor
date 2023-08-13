import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const button = el.querySelector('.btn-dial')
    const list = el.querySelector('.dial-list')
    const slide = { el, button, list }
    Data.set(id, slide);
    reset(slide)

    EventHandler.on(button, 'click', () => {
        list.classList.toggle('show')
    })

    if (!window.bb_slide_button) {
        window.bb_slide_button = true

        EventHandler.on(document, 'click', e => closePopup(e));
    }
}

export function close(id) {
    const slide = Data.get(id)
    if (slide) {
        slide.list.classList.remove('show')
    }
}

export function dispose(id) {
    const dial = Data.get(id)
    Data.remove(id)

    if (slide) {
    }
}

const reset = slide => {
    const { el, button, list } = slide
    const placement = el.getAttribute('data-bb-placement') || 'auto'
    let offset = parseFloat(el.getAttribute('data-bb-offset') || '8')

    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth

    const listStyle = getComputedStyle(list)
    const listHeight = parseFloat(listStyle.height)
    const listWidth = parseFloat(listStyle.width)
    let style = null
    if (placement === 'top') {
        style = `bottom: ${buttonHeight + offset}px; left: 0;`
    }
    else if (placement === 'bottom') {
        style = `top: ${buttonHeight + offset}px; left: 0;`
    }
    else if (placement === 'left') {
        style = `top: ${(buttonHeight - listHeight) / 2}px; right: ${buttonWidth + offset}px;`
    }
    else if (placement === 'auto' || placement === 'right') {
        style = `top: ${(buttonHeight - listHeight) / 2}px; left: ${buttonWidth + offset}px;`
    }

    if (style) {
        list.setAttribute('style', style)
    }
}

const closePopup = e => {
    document.querySelectorAll('.dial-button').forEach(el => {
        if (e.target.closest('.dial-button') !== el) {
            const list = el.querySelector('.dial-list')
            if (list && list.classList.contains('show')) {
                const autoClose = el.getAttribute('data-bb-auto-close') === 'true'
                if (autoClose) {
                    list.classList.remove('show')
                }
            }
        }
    })
}
