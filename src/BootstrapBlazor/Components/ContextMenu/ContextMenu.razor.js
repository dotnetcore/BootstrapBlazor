import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import { createPopper, computePosition } from '../../modules/floating-ui.js'
import { registerBootstrapBlazorModule } from "../../modules/utility.js"

export function init(id) {
    const el = document.getElementById(id)

    if (el) {
        const cm = { el, zone: getZone(el) };
        Data.set(id, cm);

        registerBootstrapBlazorModule("ContextMenu", id, context => {
            if (context.cancelContextMenuHandler === void 0) {
                context.cancelContextMenuHandler = e => {
                    const menu = document.querySelector('.bb-cm.show')
                    if (menu) {
                        const menuId = menu.getAttribute('id')
                        const cm = Data.get(menuId)
                        if (cm.popper) {
                            cm.popper()
                        }

                        menu.classList.remove('show')
                        const zone = getZone(menu)
                        if (zone) {
                            zone.appendChild(menu)
                        }
                    }
                }
            }
            EventHandler.on(document, 'click', context.cancelContextMenuHandler)
            EventHandler.on(document, 'contextmenu', context.cancelContextMenuHandler)
        })
    }
}

export function show(id, event) {
    const cm = Data.get(id)

    if (cm) {
        const el = cm.el
        const zone = cm.zone

        const body = document.body
        body.appendChild(el)

        if (cm.popper) {
            cm.popper()
        }

        cm.popper = createPopper(zone, el, () => showContextMenu(zone, el, event))
    }
}

export function dispose(id) {
    const cm = Data.get(id)
    Data.remove(id)

    if (cm) {
        const el = cm.el
        const popper = cm.popper
        if (popper) {
            cm.popper()
        }

        const { ContextMenu } = window.BootstrapBlazor;
        ContextMenu.dispose(id, context => {
            EventHandler.off(document, 'click', context.cancelContextMenuHandler)
            EventHandler.off(document, 'contextmenu', context.cancelContextMenuHandler)
        });
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

const getZone = menu => {
    let zone = null
    const zoneId = menu.getAttribute('data-bb-zone-id')
    if (zoneId) {
        zone = document.getElementById(zoneId)
    }
    return zone
}
