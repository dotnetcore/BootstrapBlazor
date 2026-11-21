import { isElement } from "./utility.js"

export async function toggle(options) {
    let el = null;
    options = options || {};
    if (options.id) {
        el = document.getElementById(options.id);
    }
    else if (options.selector) {
        el = document.querySelector(options.selector);
    }
    else if (isElement(options.element)) {
        el = options.element;
    }
    else {
        el = document.documentElement
    }

    if (el !== null) {
        if (isFullscreen()) {
            await exitFullscreen(el);
        }
        else {
            await enterFullscreen(el);
        }
    }
}

const enterFullscreen = async el => {
    await el.requestFullscreen();
}

const exitFullscreen = async el => {
    await document.exitFullscreen()
}

const isFullscreen = () => {
    return document.fullscreenElement !== null;
}

export function supportsPopover(el) {
    return typeof el.showPopover === 'function' && typeof el.hidePopover === 'function'
}

const modalHosts = []
const overlayChangeEvent = 'bb.overlaychange'

export function setModalOverlay(host, open) {
    const index = modalHosts.indexOf(host)
    if (index !== -1) {
        modalHosts.splice(index, 1)
    }
    if (open) {
        modalHosts.push(host)
    }
    document.dispatchEvent(new Event(overlayChangeEvent))
}

export function attachOverlay(el) {
    const placeholder = document.createComment('fullscreen-overlay')
    el.before(placeholder)

    const move = parent => {
        const open = el.hasAttribute('popover') && el.matches(':popover-open')
        if (open) {
            el.hidePopover()
        }

        if (parent) {
            parent.append(el)
        }
        else if (placeholder.parentNode) {
            placeholder.after(el)
        }

        if (open && el.isConnected) {
            el.showPopover()
        }
    }

    const update = () => {
        const host = [...modalHosts].reverse().find(host => host.isConnected && host.open)
        const target = host ?? document.fullscreenElement
        if (target && !target.contains(placeholder)) {
            if (el.parentNode !== target) {
                move(target)
            }
        }
        else if (placeholder.parentNode && el.parentNode !== placeholder.parentNode) {
            move(null)
        }
    }

    document.addEventListener('fullscreenchange', update)
    document.addEventListener(overlayChangeEvent, update)
    update()

    return () => {
        document.removeEventListener('fullscreenchange', update)
        document.removeEventListener(overlayChangeEvent, update)
        if (placeholder.parentNode && el.parentNode !== placeholder.parentNode) {
            move(null)
        }
        placeholder.remove()
    }
}
