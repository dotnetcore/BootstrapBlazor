import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    const cm = {
        element: el
    }
    Data.set(id, cm)

    if (el) {
        window.bb = window.bb || {}
        if (window.bb.cancelContextMenuHandler === undefined) {
            window.bb.contextMenus = []
            window.bb.cancelContextMenuHandler = e => {
                const menu = document.querySelector('.bb-cm.show')
                if (menu) {
                    const zoneId = menu.getAttribute('data-bb-zone-id')
                    if (zoneId) {
                        const zone = document.getElementById(zoneId)
                        if (zone) {
                            menu.classList.remove('show')
                            zone.appendChild(menu)
                        }
                    }
                }
            }
            EventHandler.on(document, 'click', window.bb.cancelContextMenuHandler)
        }

        window.bb.contextMenus.push(el)
        EventHandler.on(el, 'click', e => e.stopPropagation())
    }
}

export function show(id, event) {
    const menu = document.getElementById(id)
    menu.style.top = `${event.clientY}px`
    menu.style.left = `${event.clientX}px`

    const body = document.body
    body.appendChild(menu)

    menu.classList.add('show')
}

export function dispose(id) {
    const cm = Data.get(id)
    Data.remove(id)

    if (cm) {
        const el = cm.el
        EventHandler.off(el, 'click')

        window.bb = window.bb || { contextMenus: [] }
        window.bb.contextMenus.pop(el)
        if (window.bb.contextMenus.length === 0) {
            if (window.bb.cancelContextMenuHandler) {
                EventHandler.off(document, 'click', window.bb.cancelContextMenuHandler)
            }
        }
    }
}
