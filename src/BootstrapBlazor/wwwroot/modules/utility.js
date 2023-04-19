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

const getWidth = (element, self = false) => {
    let width = element.offsetWidth
    if (self) {
        const styles = getComputedStyle(element)
        const borderLeftWidth = parseFloat(styles.borderLeftWidth)
        const borderRightWidth = parseFloat(styles.borderRightWidth)
        const paddingLeft = parseFloat(styles.paddingLeft)
        const paddingRight = parseFloat(styles.paddingRight)
        width = width - borderLeftWidth - borderRightWidth - paddingLeft - paddingRight
    }
    return width
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

const getInnerWidth = element => getWidth(element, true)

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
    let link = links.filter(function (link) {
        return link.src.indexOf(content) > -1
    })
    let done = link.length > 0;
    if (link.length === 0) {
        link = document.createElement('script')
        link.setAttribute('src', content)
        document.body.appendChild(link)
        link.onload = () => {
            done = true
        }
    }
    return new Promise((resolve, reject) => {
        const handler = setInterval(() => {
            if (done) {
                clearInterval(handler)
                resolve()
            }
        }, 20)
    })
}

const removeScript = content => {
    const links = [...document.getElementsByTagName('script')]
    const nodes = links.filter(function (link) {
        return link.src.indexOf(content) > -1
    })
    for (let index = 0; index < nodes.length; index++) {
        document.body.removeChild(nodes[index])
    }
}

const addLink = href => {
    const links = [...document.getElementsByTagName('link')]
    let link = links.filter(function (link) {
        return link.href.indexOf(href) > -1
    })
    let done = link.length > 0;
    if (link.length === 0) {
        link = document.createElement('link')
        link.setAttribute('href', href)
        link.setAttribute("rel", "stylesheet")
        document.getElementsByTagName("head")[0].appendChild(link)
        link.onload = () => {
            done = true
        }
    }
    return new Promise((resolve, reject) => {
        const handler = setInterval(() => {
            if (done) {
                clearInterval(handler)
                resolve()
            }
        }, 20)
    })
}

const removeLink = href => {
    const links = [...document.getElementsByTagName('link')]
    const nodes = links.filter(function (link) {
        return link.href.indexOf(href) > -1
    })
    for (let index = 0; index < nodes.length; index++) {
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

const drag = (element, start, move, end) => {
    const handleDragStart = e => {
        let notDrag = false
        if (isFunction(start)) {
            notDrag = start(e) || false
        }

        if (!notDrag) {
            e.preventDefault()
            e.stopPropagation()

            document.addEventListener('mousemove', handleDragMove)
            document.addEventListener('touchmove', handleDragMove)
            document.addEventListener('mouseup', handleDragEnd)
            document.addEventListener('touchend', handleDragEnd)
        }
    }

    const handleDragMove = e => {
        if (e.touches && e.touches.length > 1) {
            return;
        }

        if (isFunction(move)) {
            move(e)
        }
    }

    const handleDragEnd = e => {
        if (isFunction(end)) {
            end(e)
        }

        const handler = window.setTimeout(() => {
            window.clearTimeout(handler)
            document.removeEventListener('mousemove', handleDragMove)
            document.removeEventListener('touchmove', handleDragMove)
            document.removeEventListener('mouseup', handleDragEnd)
            document.removeEventListener('touchend', handleDragEnd)
        }, 10)
    }

    element.addEventListener('mousedown', handleDragStart)
    element.addEventListener('touchstart', handleDragStart)
}

const getDescribedElement = (element, selector = 'aria-describedby') => {
    if (isElement(element)) {
        let id = element.getAttribute(selector)
        if (id) {
            if (id.indexOf('.') === -1) {
                id = `#${id}`
            }
            return document.querySelector(id)
        }
    }
    return null
}

const isElement = object => {
    if (!object || typeof object !== 'object') {
        return false
    }

    if (typeof object.jquery !== 'undefined') {
        object = object[0]
    }

    return typeof object.nodeType !== 'undefined'
}

const getElement = object => {
    // it's a jQuery object or a node element
    if (isElement(object)) {
        return object.jquery ? object[0] : object
    }

    if (typeof object === 'string' && object.length > 0) {
        return document.querySelector(object)
    }

    return null
}

const getElementById = object => {
    if (typeof object === 'string' && object.length > 0 && object.substring(0, 1) !== '.' && object.substring(0, 1) !== '#') {
        object = `#${object}`
    }

    return getElement(object);
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

const getTransitionDurationFromElement = (element) => {
    if (!element) {
        return 0
    }

    // Get transition-duration of the element
    let { transitionDuration, transitionDelay } = window.getComputedStyle(element)

    const floatTransitionDuration = Number.parseFloat(transitionDuration)
    const floatTransitionDelay = Number.parseFloat(transitionDelay)

    // Return 0 if element or transition duration is not found
    if (!floatTransitionDuration && !floatTransitionDelay) {
        return 0
    }

    // If multiple durations are defined, take the first
    transitionDuration = transitionDuration.split(',')[0]
    transitionDelay = transitionDelay.split(',')[0]

    return (Number.parseFloat(transitionDuration) + Number.parseFloat(transitionDelay)) * 1000
}

const isVisible = element => {
    if (!isElement(element) || element.getClientRects().length === 0) {
        return false
    }

    const elementIsVisible = getComputedStyle(element).getPropertyValue('visibility') === 'visible'
    // Handle `details` element as its content may falsie appear visible when it is closed
    const closedDetails = element.closest('details:not([open])')

    if (!closedDetails) {
        return elementIsVisible
    }

    if (closedDetails !== element) {
        const summary = element.closest('summary')
        if (summary && summary.parentNode !== closedDetails) {
            return false
        }

        if (summary === null) {
            return false
        }
    }

    return elementIsVisible
}

const isDisabled = element => {
    if (!element || element.nodeType !== Node.ELEMENT_NODE) {
        return true
    }

    if (element.classList.contains('disabled')) {
        return true
    }

    if (typeof element.disabled !== 'undefined') {
        return element.disabled
    }

    return element.hasAttribute('disabled') && element.getAttribute('disabled') !== 'false'
}

export {
    addLink,
    addScript,
    copy,
    drag,
    insertBefore,
    insertAfter,
    isDisabled,
    isElement,
    isFunction,
    isVisible,
    getElement,
    getElementById,
    getDescribedElement,
    getHeight,
    getInnerHeight,
    getInnerWidth,
    getTargetElement,
    getTransitionDelayDurationFromElement,
    getWidth,
    getWindow,
    getWindowScroll,
    removeLink,
    removeScript,
    vibrate
}
