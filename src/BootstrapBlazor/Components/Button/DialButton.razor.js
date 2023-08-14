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
        toggle(el, list)
    })

    EventHandler.on(list, 'animationend', e => {
        if (list.classList.contains('closing')) {
            e.target.style.setProperty('visibility', 'hidden')

            const items = [...list.querySelectorAll('.dial-item')];
            if (items.indexOf(e.target) === items.length - 1) {
                list.classList.remove('closing')
                list.classList.remove('show')
                items.forEach(item => {
                    item.style.removeProperty('visibility')
                })
            }
        }
        e.target.style.removeProperty('animation')
    })

    if (!window.bb_dial_button) {
        window.bb_dial_button = true

        EventHandler.on(document, 'click', e => closePopup(e));
    }
}

export function update(id) {
    const dial = Data.get(id)
    if (dial) {
        reset(dial)
    }
}

export function close(id) {
    const slide = Data.get(id)
    if (slide) {
        toggle(slide.el, slide.list)
    }
}

export function dispose(id) {
    const dial = Data.get(id)
    Data.remove(id)

    if (dial) {
        EventHandler.off(dial.button, 'click')
        EventHandler.off(dial.list, 'animationend')
    }
}

const animate = (item, value, delay, fn) => {
    const handler = setTimeout(() => {
        clearTimeout(handler)
        if (fn) {
            fn()
        }
        item.style.setProperty('animation', value)
    }, delay)
}

const toggle = (el, list) => {
    const items = list.querySelectorAll('.dial-item')
    if (list.classList.contains('show')) {
        list.classList.add('closing')
        for (let index = 0; index < items.length; index++) {
            const item = items[items.length - index - 1]
            animate(item, 'var(--bb-dial-list-animation-duration) cubic-bezier(0, 0, 0.58, 1) 0s 1 normal none running FadeOut', index * 100)
        }
    }
    else {
        list.classList.add('show')
        items.forEach((item, index) => {
            item.style.setProperty('visibility', 'hidden')
            animate(item, 'var(--bb-dial-list-animation-duration) cubic-bezier(0.42, 0, 1, 1) 0s 1 normal none running FadeIn', index * 100, () => {
                item.style.removeProperty('visibility')
            })
        })
    }
}

const reset = slide => {
    const isRadial = slide.el.classList.contains('is-radial')
    if (isRadial) {
        resetRadial(slide)
    }
    else {
        resetLinear(slide)
    }
}

const resetRadial = slide => {
    const { el, button, list } = slide
    const placement = el.getAttribute('data-bb-placement') || 'middle-center'
    let offset = parseFloat(el.getAttribute('data-bb-offset') || '8')

    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth

    const dialRadius = parseFloat(el.getAttribute('data-bb-radius') || '150')

    list.setAttribute('style', '')
    let area = 90
    if (placement === 'top-start') {
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('top', '0')
        list.style.setProperty('left', '0')
    }
    else if (placement === 'top-center') {
        area = 180
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius * 2}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('top', '0')
        list.style.setProperty('left', `${buttonWidth / 2 - dialRadius}px`)
    }
    else if (placement === 'top-end') {
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('top', '0px')
        list.style.setProperty('right', '0')
    }
    else if (placement === 'middle-start') {
        area = 180
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius * 2}px`)
        list.style.setProperty('top', `${buttonHeight / 2 - dialRadius}px`)
        list.style.setProperty('left', '0')
    }
    else if (placement === 'middle-center') {
        area = 360
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius * 2}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius * 2}px`)
        list.style.setProperty('top', `${buttonHeight / 2 - dialRadius}px`)
        list.style.setProperty('left', `${buttonWidth / 2 - dialRadius}px`)
    }
    else if (placement === 'middle-end') {
        area = 180
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius * 2}px`)
        list.style.setProperty('top', `${buttonHeight / 2 - dialRadius}px`)
        list.style.setProperty('right', '0')
    }
    else if (placement === 'bottom-start') {
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('bottom', '0')
        list.style.setProperty('left', '0')
    }
    else if (placement === 'bottom-center') {
        area = 180
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius * 2}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('bottom', '0')
        list.style.setProperty('left', `${buttonWidth / 2 - dialRadius}px`)
    }
    else if (placement === 'bottom-end') {
        const radialOffset = dialRadius - buttonWidth * 1.5
        list.style.setProperty('--bb-dial-radial-offset', `${radialOffset}px`)
        list.style.setProperty('--bb-dial-list-width', `${dialRadius}px`)
        list.style.setProperty('--bb-dial-list-height', `${dialRadius}px`)
        list.style.setProperty('bottom', '0px')
        list.style.setProperty('right', '0')
    }

    const items = list.querySelectorAll('.dial-item')
    if (items.length > 0) {
        for (let index = 0; index < items.length; index++) {
            const item = items[index]
            item.setAttribute('style', '')

            if (placement === 'top-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index}deg`)
            }
            else if (placement === 'top-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index}deg`)
                item.style.setProperty('left', `calc(${dialRadius - buttonWidth / 2}px - 2 * var(--bb-dial-item-margin))`)
            }
            else if (placement === 'top-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index + 90}deg`)
                item.style.setProperty('right', '0')
            }
            else if (placement === 'middle-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index - 90}deg`)
                item.style.setProperty('top', `calc(${dialRadius - buttonHeight / 2}px - 2 * var(--bb-dial-item-margin))`)
            }
            else if (placement === 'middle-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / items.length) * index}deg`)
                item.style.setProperty('top', `calc(${dialRadius - buttonHeight / 2}px - 2 * var(--bb-dial-item-margin))`)
                item.style.setProperty('left', `calc(${dialRadius - buttonWidth / 2}px - 2 * var(--bb-dial-item-margin))`)
            }
            else if (placement === 'middle-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index + 90}deg`)
                item.style.setProperty('top', `calc(${dialRadius - buttonHeight / 2}px - 2 * var(--bb-dial-item-margin))`)
                item.style.setProperty('right', '0')
            }
            else if (placement === 'bottom-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index - 90}deg`)
                item.style.setProperty('bottom', '0')
                item.style.setProperty('left', '0')
            }
            else if (placement === 'bottom-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index - 180}deg`)
                item.style.setProperty('bottom', '0')
                item.style.setProperty('left', `calc(${dialRadius - buttonWidth / 2}px - 2 * var(--bb-dial-item-margin))`)
            }
            else if (placement === 'bottom-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(area / (items.length - 1)) * index - 180}deg`)
                item.style.setProperty('bottom', '0')
                item.style.setProperty('right', '0')
            }
        }
    }
}

const resetLinear = slide => {
    const { el, button, list } = slide
    const placement = el.getAttribute('data-bb-placement') || 'middle-end'
    let offset = parseFloat(el.getAttribute('data-bb-offset') || '8')

    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth

    const listStyle = getComputedStyle(list)
    const listHeight = parseFloat(listStyle.height)
    const listWidth = parseFloat(listStyle.width)

    list.setAttribute('style', '')
    if (placement.startsWith('middle')) {
        list.style.setProperty('--bb-dial-list-vertical-offset', `${(buttonHeight - listHeight) / 2}px`)
        list.style.setProperty('--bb-dial-list-horizontal-offset', `${buttonWidth + offset}px`)
    }
    else {
        list.style.setProperty('--bb-dial-list-vertical-offset', `${buttonHeight + offset}px`)
        list.style.setProperty('--bb-dial-list-horizontal-offset', `${(buttonWidth - listWidth) / 2}px`)
    }
    list.querySelectorAll('.dial-item').forEach(item => {
        item.removeAttribute('style')
    })
}

const closePopup = e => {
    document.querySelectorAll('.dial-button').forEach(el => {
        if (e.target.closest('.dial-button') !== el) {
            const list = el.querySelector('.dial-list')
            if (list && list.classList.contains('show')) {
                const autoClose = el.getAttribute('data-bb-auto-close') === 'true'
                if (autoClose) {
                    toggle(el, list)
                }
            }
        }
    })
}
