import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

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

const toggle = (el, list) => {
    const items = list.querySelectorAll('.dial-item')
    if (items.length > 0) {
        const duration = parseInt(el.getAttribute('data-bb-duration') || '400')
        const interval = duration / (items.length + 1)

        if (list.classList.contains('show')) {
            animateClose(el, list, items, interval)
        }
        else {
            animateOpen(el, list, items, interval)
        }
    }
}

const animateOpen = (el, list, items, interval) => {
    items.forEach(item => {
        item.style.setProperty('visibility', 'hidden')
    })
    list.classList.add('show')

    const run = item => {
        let start = void 0
        const step = ts => {
            if (start === void 0) {
                start = ts
                item.style.removeProperty('visibility')
                item.style.setProperty('animation', '200ms cubic-bezier(0.42, 0, 1, 1) 0s 1 normal none running FadeIn')
            }
            const elapsed = ts - start;
            if (elapsed < 200) {
                requestAnimationFrame(step);
            }
            else {
                item.style.removeProperty('animation')
            }
        }
        requestAnimationFrame(step)
    }

    const animateItem = index => {
        const handler = setTimeout(() => {
            clearTimeout(handler)
            if (items.length > index) {
                const item = items[index]
                run(item)
                animateItem(index + 1)
            }
        }, interval);
    }

    animateItem(0);
}

const animateClose = (el, list, items, interval) => {
    list.classList.add('closing')
    items[0].setAttribute('bb-animate', 'true')

    const run = item => {
        let start = void 0
        const step = ts => {
            if (start === void 0) {
                start = ts
                item.style.setProperty('animation', '200ms cubic-bezier(0, 0, 0.58, 1) 0s 1 normal none running FadeOut')
            }
            const elapsed = ts - start;
            if (elapsed < 200) {
                requestAnimationFrame(step);
            }
            else {
                item.style.removeProperty('animation')

                if (item.getAttribute('bb-animate') === 'true') {
                    list.classList.remove('closing')
                    list.classList.remove('show')
                    item.removeAttribute('bb-animate')
                    items.forEach(item => {
                        item.style.removeProperty('visibility')
                    })
                }
                else {
                    item.style.setProperty('visibility', 'hidden')
                }
            }
        }
        requestAnimationFrame(step)
    }

    const animateItem = index => {
        const handler = setTimeout(() => {
            clearTimeout(handler)
            if (index > -1) {
                const item = items[index]
                run(item)
                animateItem(index - 1)
            }
        }, interval);
    }
    animateItem(items.length - 1);
}

const reset = dial => {
    const isRadial = dial.el.classList.contains('is-radial')
    if (isRadial) {
        resetRadial(dial)
    }
    else {
        resetLinear(dial)
    }
}

const resetRadial = slide => {
    const { el, button, list } = slide
    const placement = el.getAttribute('data-bb-placement') || 'middle-center'

    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth

    const dialRadius = parseFloat(el.getAttribute('data-bb-radius') || '150')
    list.setAttribute('style', '')
    list.style.setProperty('--bb-dial-radial-radius', `${dialRadius}px`)

    if (placement === 'top-center') {
        list.style.setProperty('--bb-dial-list-horizontal-offset', `calc(${buttonWidth / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-width))`)
    }
    else if (placement === 'middle-start') {
        list.style.setProperty('--bb-dial-list-vertical-offset', `calc(${buttonHeight / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-height))`)
    }
    else if (placement === 'middle-center') {
        list.style.setProperty('--bb-dial-list-vertical-offset', `calc(${buttonHeight / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-height))`)
        list.style.setProperty('--bb-dial-list-horizontal-offset', `calc(${buttonWidth / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-width))`)
    }
    else if (placement === 'middle-end') {
        list.style.setProperty('--bb-dial-list-vertical-offset', `calc(${buttonHeight / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-height))`)
    }
    else if (placement === 'bottom-center') {
        list.style.setProperty('--bb-dial-list-horizontal-offset', `calc(${buttonWidth / 2}px - (var(--bb-dial-radial-radius) + 1.5 * var(--bb-dial-item-width))`)
    }

    const items = list.querySelectorAll('.dial-item')
    if (items.length > 0) {
        for (let index = 0; index < items.length; index++) {
            const item = items[index]
            item.setAttribute('style', '')
            if (placement === 'top-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(90 / (items.length - 1)) * index}deg`)
            }
            else if (placement === 'top-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(180 / (items.length - 1)) * index}deg`)
            }
            else if (placement === 'top-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(90 / (items.length - 1)) * index + 90}deg`)
            }
            else if (placement === 'middle-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(180 / (items.length - 1)) * index - 90}deg`)
            }
            else if (placement === 'middle-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(360 / items.length) * index}deg`)
            }
            else if (placement === 'middle-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(180 / (items.length - 1)) * index + 90}deg`)
            }
            else if (placement === 'bottom-start') {
                item.style.setProperty('--bb-dial-item-angle', `${(90 / (items.length - 1)) * index - 90}deg`)
            }
            else if (placement === 'bottom-center') {
                item.style.setProperty('--bb-dial-item-angle', `${(180 / (items.length - 1)) * index - 180}deg`)
            }
            else if (placement === 'bottom-end') {
                item.style.setProperty('--bb-dial-item-angle', `${(90 / (items.length - 1)) * index - 180}deg`)
            }
        }
    }
}

const resetLinear = dial => {
    const { el, button, list } = dial
    const placement = el.getAttribute('data-bb-placement') || 'middle-end'
    let offset = parseFloat(el.getAttribute('data-bb-offset') || '8')

    const buttonHeight = button.offsetHeight
    const buttonWidth = button.offsetWidth

    const listStyle = getComputedStyle(list)
    const listHeight = parseFloat(listStyle.height)
    const listWidth = parseFloat(listStyle.width)

    list.querySelectorAll('.dial-item').forEach(item => {
        item.removeAttribute('style')
    })

    list.setAttribute('style', '')
    if (placement.startsWith('middle')) {
        list.style.setProperty('--bb-dial-list-vertical-offset', `${(buttonHeight - listHeight) / 2}px`)
        list.style.setProperty('--bb-dial-list-horizontal-offset', `${buttonWidth + offset}px`)
    }
    else {
        list.style.setProperty('--bb-dial-list-vertical-offset', `${buttonHeight + offset}px`)
        list.style.setProperty('--bb-dial-list-horizontal-offset', `${(buttonWidth - listWidth) / 2}px`)
    }
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
