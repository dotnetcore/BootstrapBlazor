import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"
import { computePosition, flip, shift, offset, hide as hide$1, autoUpdate } from '../../js/floating-ui.dom.esm.js'

const hide = menu => {
    const zoneId = menu.getAttribute('data-bb-zone-id')
    if (zoneId) {
        const zone = document.getElementById(zoneId)
        if (zone) {
            menu.classList.remove('show')
            zone.appendChild(menu)
        }
    }
}

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

    const zoneId = menu.getAttribute('data-bb-zone-id')
    if (zoneId) {
        const zone = document.getElementById(zoneId)
        if (zone) {
            menu.classList.add('show')

            autoUpdate(zone, menu, () => showContextMenu(zone, menu, event))
        }
    }
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
        placement: 'bottom-start',
        middleware: [
            offset(),
            hide$1(),
            flip(),
            shift({ padding: 5 }),
        ],
    })

    Object.assign(menu.style, {
        left: `${pos.x}px`,
        top: `${pos.y}px`,
    });
}

const createVirtualElement = (zone, event) => {
    const rect = zone.getBoundingClientRect()
    if (event.zoneY === undefined) {
        event.zoneY = rect.y
    }
    if (event.zoneX === undefined) {
        event.zoneX = rect.x
    }
    const top = event.clientY + rect.y - event.zoneY
    const left = event.clientX + rect.x - event.zoneX
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
