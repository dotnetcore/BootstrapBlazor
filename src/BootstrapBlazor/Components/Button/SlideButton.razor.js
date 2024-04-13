import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

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
    EventHandler.on(list, 'click', '.slide-item', e => {
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

export function dispose(id) {
    const slide = Data.get(id)
    Data.remove(id)

    if (slide) {
        EventHandler.off(slide.button, 'click')
        EventHandler.off(slide.list, 'click', '.btn-close')
        EventHandler.off(slide.list, 'click', '.slide-item')
    }
}

const reset = slide => {
    const { el, button, list } = slide

    list.classList.add('invisible')
    list.removeAttribute('style')

    const maxHeight = parseFloat(getComputedStyle(el).getPropertyValue('--bb-slide-list-height'))
    const height = list.getBoundingClientRect().height
    if (height < maxHeight) {
        list.style.setProperty('--bb-slide-list-height', `${height}px`)
    }
    if (list.classList.contains('is-horizontal')) {
        list.style.setProperty('--bb-slide-list-width-collapsed', '0')
    }
    else {
        list.style.setProperty('--bb-slide-list-height-collapsed', '0')
    }

    const placement = el.getAttribute('data-bb-placement') || 'auto'
    const offset = parseFloat(el.getAttribute('data-bb-offset') || '8')
    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth
    const listWidth = list.offsetWidth
    const listHeight = list.offsetHeight
    setVerticalPlacement(list, placement, offset, buttonHeight, buttonWidth, listWidth)
    setHorizontalPlacement(list, placement, offset, buttonHeight, buttonWidth, listHeight)

    list.style.setProperty('transition', 'width .3s ease-in-out, height .3s ease-in-out')
    list.classList.remove('invisible')
}

const setVerticalPlacement = (list, placement, offset, buttonHeight, buttonWidth, listWidth) => {
    if (placement === 'auto' || placement === 'top' || placement === 'top-start') {
        list.style.setProperty('bottom', `${buttonHeight + offset}px`)
        list.style.setProperty('left', '0')
    }
    else if (placement === 'top-center') {
        list.style.setProperty('bottom', `${buttonHeight + offset}px`)
        list.style.setProperty('left', `${(buttonWidth - listWidth) / 2}px`)
    }
    else if (placement === 'top-end') {
        list.style.setProperty('bottom', `${buttonHeight + offset}px`)
        list.style.setProperty('right', '0')
    }
    else if (placement === 'bottom' || placement === 'bottom-start') {
        list.style.setProperty('top', `${buttonHeight + offset}px`)
        list.style.setProperty('left', '0')
    }
    else if (placement === 'bottom' || placement === 'bottom-center') {
        list.style.setProperty('top', `${buttonHeight + offset}px`)
        list.style.setProperty('left', `${(buttonWidth - listWidth) / 2}px`)
    }
    else if (placement === 'bottom' || placement === 'bottom-end') {
        list.style.setProperty('top', `${buttonHeight + offset}px`)
        list.style.setProperty('right', '0')
    }
}

const setHorizontalPlacement = (list, placement, offset, buttonHeight, buttonWidth, listHeight) => {
    if (placement === 'left') {
        list.style.setProperty('top', `${(buttonHeight - listHeight) / 2}px`)
        list.style.setProperty('right', `${buttonWidth + offset}px`)
    }
    else if (placement === 'left-start') {
        list.style.setProperty('top', '0')
        list.style.setProperty('right', `${buttonWidth + offset}px`)
    }
    else if (placement === 'left-end') {
        list.style.setProperty('bottom', '0')
        list.style.setProperty('right', `${buttonWidth + offset}px`)
    }
    else if (placement === 'right') {
        list.style.setProperty('top', `${(buttonHeight - listHeight) / 2}px`)
        list.style.setProperty('left', `${buttonWidth + offset}px`)
    }
    else if (placement === 'right-start') {
        list.style.setProperty('top', '0')
        list.style.setProperty('left', `${buttonWidth + offset}px`)
    }
    else if (placement === 'right-end') {
        list.style.setProperty('bottom', '0')
        list.style.setProperty('left', `${buttonWidth + offset}px`)
    }
}

const closePopup = e => {
    document.querySelectorAll('.slide-button').forEach(el => {
        if (e.target.closest('.slide-button') !== el) {
            const list = el.querySelector('.slide-list')
            if (list.classList.contains('show')) {
                const autoClose = el.getAttribute('data-bb-auto-close') === 'true'
                if (autoClose) {
                    list.classList.remove('show')
                }
            }
        }
    })
}
