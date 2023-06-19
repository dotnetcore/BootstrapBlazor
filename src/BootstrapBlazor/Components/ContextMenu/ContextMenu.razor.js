import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"
import { createPopper, computePosition } from '../../modules/floating-ui.js'

export function init(id) {
    const el = document.getElementById(id)
    const cm = {
        element: el
    }
    Data.set(id, cm)

    if (el) {
        window.bb = window.bb || {}
        if (bb.cancelContextMenuHandler === undefined) {
            bb.contextMenus = []
            bb.cancelContextMenuHandler = e => {
                const menu = document.querySelector('.bb-cm.show')
                if (menu) {
                    hide(menu)
                }
            }
            EventHandler.on(document, 'click', bb.cancelContextMenuHandler)
            EventHandler.on(document, 'contextmenu', bb.cancelContextMenuHandler)
        }

        bb.contextMenus.push(el)
    }
}

export function show(id, event) {
    const menu = document.getElementById(id)
    if (menu === null) {
        return
    }

    const body = document.body
    body.appendChild(menu)

    const zone = getZone(menu)
    createPopper(zone, menu, () => showContextMenu(zone, menu, event))
}

export function dispose(id) {
    const cm = Data.get(id)
    Data.remove(id)

    if (cm) {
        const el = cm.el

        window.bb = window.bb || { contextMenus: [] }
        const index = bb.contextMenus.indexOf(el)
        if (index > -1) {
            bb.contextMenus.splice(index, 1)
        }

        if (bb.contextMenus.length === 0) {
            if (bb.cancelContextMenuHandler) {
                EventHandler.off(document, 'click', bb.cancelContextMenuHandler)
                EventHandler.off(document, 'contextmenu', bb.cancelContextMenuHandler)
            }
        }
    }
}

const showContextMenu = async (zone, menu, event) => {
    const vl = createVirtualElement(zone, event)

    const pos = await computePosition(vl, menu, {
        placement: 'bottom-start'
    })

    Object.assign(menu.style, {
        left: `${pos.x}px`,
        top: `${pos.y}px`,
    })
    menu.classList.add('show')
}

const createVirtualElement = (zone, event) => {
    const rect = zone.getBoundingClientRect()

    if (event.lastRect === void 0) {
        Object.assign(event, {
            lastRect: {
                x: rect.x,
                y: rect.y
            }
        })
    }
    const top = event.clientY + rect.y - event.lastRect.y
    const left = event.clientX + rect.x - event.lastRect.x
    return {
        getBoundingClientRect() {
            return {
                x: left,
                y: top,
                top: top,
                bottom: top,
                left: left,
                right: left,
                width: 0,
                height: 0,
            };
        }
    }
}

const hide = menu => {
    const zone = getZone(menu)
    if (zone) {
        menu.classList.remove('show')
        zone.appendChild(menu)
    }
}

const getZone = menu => {
    let zone = null
    const zoneId = menu.getAttribute('data-bb-zone-id')
    if (zoneId) {
        zone = document.getElementById(zoneId)
    }
    return zone
}
