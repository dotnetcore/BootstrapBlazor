import { isElement, getTransitionDurationFromElement, getElementById } from "./index.js"

const vibrate = () => {
    if ('vibrate' in window.navigator) {
        window.navigator.vibrate([200, 100, 200])
        const handler = window.setTimeout(function () {
            window.clearTimeout(handler)
            window.navigator.vibrate([])
        }, 1000)
    }
}

const isFunction = object => {
    return typeof object === 'function'
}

const copy = (text = '') => {
    if (navigator.clipboard) {
        navigator.clipboard.writeText(text)
    } else {
        const input = document.createElement('input')
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
            return document.querySelector(`[${selector}="${id}"]`)
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

const getHeight = (element, self = false) => {
    let height = element.offsetHeight
    if (self) {
        const styles = getComputedStyle(element)
        const borderTopWidth = parseFloat(styles.borderTopWidth)
        const borderBottomWidth = parseFloat(styles.borderBottomWidth)
        const paddingTop = parseFloat(styles.paddingTop)
        const paddingBottom = parseFloat(styles.paddingBottom)
        height = height - borderBottomWidth - borderTopWidth - paddingTop - paddingBottom
    }
    return height
}

const getInnerHeight = element => getHeight(element, true)

const getWindowScroll = node => {
    const win = getWindow(node)
    const scrollLeft = win.pageXOffset
    const scrollTop = win.pageYOffset
    return {
        scrollLeft: scrollLeft,
        scrollTop: scrollTop
    }
}

const getWindow = node => {
    if (!node) {
        return window
    }

    if (node.toString() !== '[object Window]') {
        const ownerDocument = node.ownerDocument
        return ownerDocument ? ownerDocument.defaultView || window : window
    }

    return node
}

const addScript = content => {
    // content 文件名
    const links = [...document.getElementsByTagName('script')]
    var link = links.filter(function (link) {
        return link.src.indexOf(content) > -1
    })
    if (link.length === 0) {
        link = document.createElement('script')
        link.setAttribute('src', content)
        document.body.appendChild(link)
    }
}

const removeScript = content => {
    const links = [...document.getElementsByTagName('script')]
    var nodes = links.filter(function (link) {
        return link.src.indexOf(content) > -1
    })
    for (var index = 0; index < nodes.length; index++) {
        document.body.removeChild(nodes[index])
    }
}

const addLink = href => {
    const links = [...document.getElementsByTagName('link')]
    var link = links.filter(function (link) {
        return link.href.indexOf(href) > -1
    })
    if (link.length === 0) {
        link = document.createElement('link')
        link.setAttribute('href', href)
        link.setAttribute("rel", "stylesheet")
        document.getElementsByTagName("head")[0].appendChild(link)
    }
}

const removeLink = href => {
    const links = [...document.getElementsByTagName('link')]
    var nodes = links.filter(function (link) {
        return link.href.indexOf(content) > -1
    })
    for (var index = 0; index < nodes.length; index++) {
        document.getElementsByTagName("head")[0].removeChild(nodes[index])
    }
}

const insertBefore = (element, newEl) => {
    if (element) {
        const parentNode = element.parentNode
        if (parentNode) {
            if (element) {
                parentNode.insertBefore(newEl, element)
            }
        }
    }
}

const insertAfter = (element, newEl) => {
    if (element) {
        const parentNode = element.parentNode
        if (parentNode) {
            if (element.nextElementSibling) {
                parentNode.insertBefore(newEl, element.nextElementSibling)
            } else {
                parentNode.appendChild(newEl)
            }
        }
    }
}

const setIndeterminate = (object, state) => {
    const element = getElementById(object)
    if (isElement(element)) {
        element.indeterminate = state;
    }
}

export {
    vibrate,
    copy,
    getDescribedElement,
    getDescribedOwner,
    getTargetElement,
    getTransitionDelayDurationFromElement,
    getHeight,
    getInnerHeight,
    getWindow,
    getWindowScroll,
    isFunction,
    addLink,
    removeLink,
    addScript,
    removeScript,
    insertBefore,
    insertAfter,
    setIndeterminate
}
