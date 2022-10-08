import { isElement, getTransitionDurationFromElement } from "./index.js";

const vibrate = () => {
    if ('vibrate' in window.navigator) {
        window.navigator.vibrate([200, 100, 200])
        const handler = window.setTimeout(function () {
            window.clearTimeout(handler)
            window.navigator.vibrate([])
        }, 1000)
    }
}

const copy = (text = '') => {
    if (navigator.clipboard) {
        navigator.clipboard.writeText(text)
    } else {
        const input = document.createElement('input');
        input.setAttribute('type', 'text')
        input.setAttribute('value', text)
        input.setAttribute('hidden', 'true')
        document.body.appendChild(input)
        input.select()
        document.execCommand('copy')
        document.body.removeChild(input)
    }
}

const getDescribedElement = (element, selector = 'aria-describedby') => {
    if (isElement(element)) {
        const id = element.getAttribute(selector)
        if (id) {
            return document.querySelector(`#${id}`)
        }
    }
    return null
}

const getDescribedOwner = (element, selector = 'aria-describedby') => {
    if (isElement(element)) {
        const id = element.getAttribute('id')
        if (id) {
            return document.querySelector(`[${selector}="${id}"]`);
        }
    }
    return null
}

const getTargetElement = (element, selector = 'data-bs-target') => {
    if (isElement(element)) {
        const id = element.getAttribute(selector)
        if (id) {
            return document.querySelector(id)
        }
    }
    return null
}

const getTransitionDelayDurationFromElement = (element, delay = 80) => {
    return getTransitionDurationFromElement(element) + delay
}

export {
    vibrate,
    copy,
    getDescribedElement,
    getDescribedOwner,
    getTargetElement,
    getTransitionDelayDurationFromElement
}
