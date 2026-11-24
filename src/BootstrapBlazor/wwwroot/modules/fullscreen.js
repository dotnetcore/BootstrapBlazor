import { getDescribedElement, getDescribedOwner, isElement } from "./utility.js"

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

export function getFullscreenContainer(reference, container) {
    const fullscreen = document.fullscreenElement
    return fullscreen?.contains(reference) && !fullscreen.contains(container) ? fullscreen : container
}

const floatingOverlayContainers = new WeakMap()

const moveFloatingOverlay = (reference, overlay) => {
    const original = floatingOverlayContainers.get(overlay) ?? overlay.parentElement
    const container = original?.isConnected ? original : document.body
    const target = getFullscreenContainer(reference, container)
    if (target === overlay.parentElement) {
        return false
    }

    if (target === container) {
        floatingOverlayContainers.delete(overlay)
    }
    else {
        floatingOverlayContainers.set(overlay, container)
    }

    const focused = document.activeElement
    target.append(overlay)
    if (overlay.contains(focused)) {
        focused.focus({ preventScroll: true })
    }
    return true
}

const mountFloatingOverlay = ({ target }) => {
    if (document.fullscreenElement?.contains(target)) {
        const overlay = getDescribedElement(target)
        if (overlay) {
            moveFloatingOverlay(target, overlay)
        }
    }
}

document.addEventListener('inserted.bs.tooltip', mountFloatingOverlay)
document.addEventListener('inserted.bs.popover', mountFloatingOverlay)
document.addEventListener('fullscreenchange', () => {
    document.querySelectorAll('.tooltip.show, .popover.show').forEach(overlay => {
        const reference = getDescribedOwner(overlay)
        if (!reference) {
            return
        }

        const component = overlay.classList.contains('popover') ? window.bootstrap?.Popover : window.bootstrap?.Tooltip
        const instance = component?.getInstance(reference)
        if (instance?.tip === overlay && moveFloatingOverlay(reference, overlay)) {
            instance?._popper?.setOptions({})
        }
    })
})

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
        const focused = document.activeElement
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
        if (el.contains(focused)) {
            focused.focus({ preventScroll: true })
        }
    }

    const update = () => {
        const fullscreen = document.fullscreenElement
        if (!el.parentNode || !placeholder.isConnected || el.contains(fullscreen)) {
            return
        }

        const host = [...modalHosts].reverse().find(host => host.isConnected && host.open)
        const target = host ?? fullscreen
        if (el.contains(target)) {
            return
        }

        if (target && !target.contains(placeholder)) {
            if (el.parentNode !== target) {
                move(target)
            }
        }
        else if (el.parentNode !== placeholder.parentNode) {
            move(null)
        }
    }

    document.addEventListener('fullscreenchange', update)
    document.addEventListener(overlayChangeEvent, update)
    update()

    return () => {
        document.removeEventListener('fullscreenchange', update)
        document.removeEventListener(overlayChangeEvent, update)
        if (el.parentNode && el.parentNode !== placeholder.parentNode) {
            if (placeholder.isConnected) {
                move(null)
            }
            else {
                el.remove()
            }
        }
        placeholder.remove()
    }
}
