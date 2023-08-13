import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const button = el.querySelector('.btn-slide')
    const list = el.querySelector('.slide-list')
    const slide = { el, button, list }
    Data.set(id, slide);
    reset(slide)

    EventHandler.on(button, 'click', () => {
        list.classList.toggle('show')
    })
    EventHandler.on(list, 'click', '.btn-close', e => {
        e.stopPropagation()
        list.classList.remove('show')
    })

    if (!window.bb_slide_button) {
        window.bb_slide_button = true

        EventHandler.on(document, 'click', e => closePopup(e));
    }
}

export function update(id) {
    const slide = Data.get(id)
    if (slide) {
        reset(slide)
    }
}

export function close(id) {
    const slide = Data.get(id)
    if (slide) {
        slide.list.classList.remove('show')
    }
}

export function dispose(id) {
    const slide = Data.get(id)
    Data.remove(id)

    if (slide) {
        EventHandler.off(slide.button, 'click')
        EventHandler.off(slide.list, 'click', '.btn-close')
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
    else if (placement === 'left') {
        style = `top: ${(buttonHeight - listHeight) / 2}px; right: ${buttonWidth + offset}px;`
    }
    else if (placement === 'left-start') {
        style = `top: 0; right: ${buttonWidth + offset}px;`
    }
    else if (placement === 'left-end') {
        style = `bottom: 0; right: ${buttonWidth + offset}px;`
    }
    else if (placement === 'right') {
        style = `top: ${(buttonHeight - listHeight) / 2}px; left: ${buttonWidth + offset}px;`
    }
    else if (placement === 'right-start') {
        style = `top: 0; left: ${buttonWidth + offset}px;`
    }
    else if (placement === 'right-end') {
        style = `bottom: 0; left: ${buttonWidth + offset}px;`
    }

    if (style) {
        list.setAttribute('style', style)
    }
    list.classList.remove('d-none')
}

const closePopup = e => {
    document.querySelectorAll('.slide-button').forEach(el => {
        if (e.target.closest('.slide-button') !== el) {
            const list = el.querySelector('.slide-list')
            if (list && list.classList.contains('show')) {
                const autoClose = el.getAttribute('data-bb-auto-close') === 'true'
                if (autoClose) {
                    list.classList.remove('show')
                }
            }
        }
    })
}
