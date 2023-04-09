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
    if (link.length === 0) {
        link = document.createElement('script')
        link.setAttribute('src', content)
        document.body.appendChild(link)
    }
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
    if (link.length === 0) {
        link = document.createElement('link')
        link.setAttribute('href', href)
        link.setAttribute("rel", "stylesheet")
        document.getElementsByTagName("head")[0].appendChild(link)
    }
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

    EventHandler.on(element, 'mousedown', handleDragStart)
    EventHandler.on(element, 'touchstart', handleDragStart)
}

export {
    addLink,
    addScript,
    copy,
    drag,
    insertBefore,
    insertAfter,
    isFunction,
    getHeight,
    getInnerHeight,
    getInnerWidth,
    getWidth,
    getWindow,
    getWindowScroll,
    removeLink,
    removeScript
}
