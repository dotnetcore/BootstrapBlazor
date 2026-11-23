import EventHandler from "./event-handler.js"
import { computePosition, createPopper, flip, hide, offset, shift, size } from "./floating-ui.js"
import { supportsPopover } from "./fullscreen.js"
import { hackTooltip } from "./utility.js"

const cancelClose = popup => {
    clearTimeout(popup.closeTimer)
    popup.closeTimer = null
}

const close = (popup, depth = 0) => {
    cancelClose(popup)
    while (popup.path.length > depth) {
        const entry = popup.path.pop()
        entry.cleanup?.()
        if (supportsPopover(entry.panel) && entry.panel.matches(':popover-open')) {
            entry.panel.hidePopover()
        }
        entry.panel.classList.remove('show')
        Object.assign(entry.panel.style, {
            left: '',
            top: '',
            maxHeight: '',
            maxWidth: '',
            visibility: ''
        })
        entry.panel.removeAttribute('data-bb-placement')
        entry.trigger.classList.remove('menu-open')
        entry.trigger.setAttribute('aria-expanded', 'false')
    }
}

const getLinks = panel => {
    const list = panel.querySelector(':scope > .menu-popup-content') ?? panel
    return [...list.querySelectorAll(':scope > li > .nav-link:not(.disabled)')]
}

const getDepth = (popup, link) => {
    const panel = link.closest('.menu-popup')
    return panel ? popup.path.findIndex(entry => entry.panel === panel) + 1 : 0
}

const open = (popup, trigger, focus = false) => {
    cancelClose(popup)
    const panel = document.getElementById(trigger.getAttribute('data-bb-menu-popup'))
    if (!panel || !popup.element.contains(panel) || trigger.classList.contains('disabled')) {
        return
    }

    const depth = getDepth(popup, trigger)
    const current = popup.path[depth]
    if (current?.trigger === trigger) {
        if (focus) {
            getLinks(panel)[0]?.focus()
        }
        return
    }

    close(popup, depth)
    const entry = { trigger, panel, focus }
    popup.path.push(entry)
    trigger.classList.add('menu-open')
    trigger.setAttribute('aria-expanded', 'true')
    panel.style.visibility = 'hidden'
    panel.classList.add('show')
    if (supportsPopover(panel)) {
        panel.showPopover()
    }
    else {
        panel.removeAttribute('popover')
    }

    const arrowElement = panel.querySelector(':scope > .menu-popup-arrow')
    const update = async () => {
        const side = getComputedStyle(trigger).direction === 'rtl' ? 'left' : 'right'
        const position = await computePosition(trigger, panel, {
            placement: arrowElement ? side : `${side}-start`,
            strategy: 'fixed',
            middleware: [
                offset(arrowElement ? 10 : 4),
                flip(),
                shift({ padding: 5, crossAxis: true }),
                size({
                    padding: 5,
                    apply({ availableHeight, availableWidth, elements }) {
                        if (!popup.path.includes(entry)) {
                            return
                        }
                        Object.assign(elements.floating.style, {
                            maxHeight: `${Math.max(0, availableHeight)}px`,
                            maxWidth: `${Math.max(0, availableWidth)}px`
                        })
                    }
                }),
                hide()
            ]
        })
        if (!popup.path.includes(entry) || !panel.isConnected) {
            return
        }
        if (position.middlewareData.hide?.referenceHidden) {
            close(popup, popup.path.indexOf(entry))
            return
        }
        Object.assign(panel.style, {
            left: `${position.x}px`,
            top: `${position.y}px`,
            visibility: ''
        })
        if (arrowElement) {
            const side = position.placement.split('-')[0]
            panel.setAttribute('data-bb-placement', side)
        }
        if (entry.focus) {
            entry.focus = false
            getLinks(panel)[0]?.focus()
        }
    }
    entry.cleanup = createPopper(trigger, panel, update)
}

const scheduleClose = popup => {
    cancelClose(popup)
    popup.closeTimer = setTimeout(() => close(popup), 150)
}

const getLink = (popup, target) => {
    if (target.closest('.widget')) {
        return null
    }
    const link = target.closest('.nav-link')
    return link?.closest('.menu') === popup.element ? link : null
}

const keydown = (popup, event) => {
    const link = getLink(popup, event.target)
    if (!link || event.altKey || event.ctrlKey || event.metaKey) {
        return
    }
    const links = getLinks(link.closest('ul'))
    const index = links.indexOf(link)
    const depth = getDepth(popup, link)
    const rtl = getComputedStyle(link).direction === 'rtl'
    const openKey = rtl ? 'ArrowLeft' : 'ArrowRight'
    const closeKey = rtl ? 'ArrowRight' : 'ArrowLeft'
    let handled = true
    popup.keyboard = true
    cancelClose(popup)

    if (event.key === openKey && link.hasAttribute('data-bb-menu-popup')) {
        open(popup, link, true)
    }
    else if (event.key === closeKey || event.key === 'Escape') {
        const entry = popup.path[Math.max(0, depth - 1)]
        close(popup, Math.max(0, depth - 1))
        entry?.trigger.focus()
    }
    else if (['ArrowDown', 'ArrowUp', 'Home', 'End'].includes(event.key)) {
        close(popup, depth)
        const next = event.key === 'Home' ? 0
            : event.key === 'End' ? links.length - 1
                : (index + (event.key === 'ArrowDown' ? 1 : -1) + links.length) % links.length
        links[next]?.focus()
    }
    else if (event.key === ' ' || event.key === 'Enter' && link.hasAttribute('data-bb-menu-popup')) {
        if (link.hasAttribute('data-bb-menu-popup')) {
            open(popup, link, true)
        }
        else {
            link.click()
        }
    }
    else {
        handled = false
    }
    if (handled) {
        event.preventDefault()
    }
}

const create = element => {
    const popup = { element, path: [], tooltips: new Map(), closeTimer: null, keyboard: false }
    popup.enter = event => {
        if (event.pointerType === 'touch') {
            return
        }
        cancelClose(popup)
        popup.keyboard = false
        const link = getLink(popup, event.target)
        if (link && !link.classList.contains('disabled')) {
            if (link.hasAttribute('data-bb-menu-popup')) {
                open(popup, link)
            }
            else {
                close(popup, getDepth(popup, link))
            }
        }
    }
    popup.leave = event => {
        if (!element.contains(event.relatedTarget) && !popup.keyboard) {
            scheduleClose(popup)
        }
    }
    popup.focus = () => cancelClose(popup)
    popup.blur = event => {
        if (!element.contains(event.relatedTarget)) {
            scheduleClose(popup)
        }
    }
    popup.click = event => {
        const link = getLink(popup, event.target)
        if (link && !link.classList.contains('disabled')) {
            if (link.hasAttribute('data-bb-menu-popup')) {
                event.preventDefault()
                open(popup, link)
            }
            else {
                close(popup)
                popup.tooltips.get(link)?.hide()
            }
        }
    }
    popup.outside = event => {
        if (!element.contains(event.target)) {
            close(popup)
        }
    }
    popup.keydown = event => keydown(popup, event)
    EventHandler.on(element, 'pointerover', popup.enter)
    EventHandler.on(element, 'pointerleave', popup.leave)
    EventHandler.on(element, 'focusin', popup.focus)
    EventHandler.on(element, 'focusout', popup.blur)
    EventHandler.on(element, 'click', popup.click)
    EventHandler.on(element, 'keydown', popup.keydown)
    EventHandler.on(document, 'pointerdown', popup.outside)
    return popup
}

export default {
    refresh(menu) {
        if (!menu.element.classList.contains('is-collapsed')) {
            this.dispose(menu)
            return
        }
        const popup = menu.popup ??= create(menu.element)
        const invalid = popup.path.findIndex(entry => !menu.element.contains(entry.trigger)
            || !menu.element.contains(entry.panel)
            || entry.trigger.classList.contains('disabled')
            || entry.trigger.getAttribute('data-bb-menu-popup') !== entry.panel.id)
        if (invalid !== -1) {
            close(popup, invalid)
        }
        popup.path.forEach(entry => {
            entry.trigger.classList.add('menu-open')
            entry.trigger.setAttribute('aria-expanded', 'true')
        })
        for (const [link, tooltip] of popup.tooltips) {
            if (!menu.element.contains(link) || !link.hasAttribute('data-bb-menu-tooltip')) {
                tooltip.dispose()
                popup.tooltips.delete(link)
            }
        }
        menu.element.querySelectorAll('[data-bb-menu-tooltip]').forEach(link => {
            if (!popup.tooltips.has(link)) {
                hackTooltip()
                popup.tooltips.set(link, new bootstrap.Tooltip(link, {
                    title: () => link.getAttribute('data-bb-menu-tooltip'),
                    placement: 'right',
                    trigger: 'hover focus',
                    container: 'body',
                    html: false
                }))
            }
        })
    },
    dispose(menu) {
        const popup = menu.popup
        if (!popup) {
            return
        }
        close(popup)
        popup.tooltips.forEach(tooltip => tooltip.dispose())
        EventHandler.off(popup.element, 'pointerover', popup.enter)
        EventHandler.off(popup.element, 'pointerleave', popup.leave)
        EventHandler.off(popup.element, 'focusin', popup.focus)
        EventHandler.off(popup.element, 'focusout', popup.blur)
        EventHandler.off(popup.element, 'click', popup.click)
        EventHandler.off(popup.element, 'keydown', popup.keydown)
        EventHandler.off(document, 'pointerdown', popup.outside)
        delete menu.popup
    }
}
