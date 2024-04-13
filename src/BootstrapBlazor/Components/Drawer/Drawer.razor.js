import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"

export function init(id) {
    const el = document.getElementById(id)
    const dw = {
        element: el,
        body: document.querySelector('body'),
        drawerBody: el.querySelector('.drawer-body'),
        bar: el.querySelector('.drawer-bar')
    }
    Data.set(id, dw)

    let originX = 0
    let originY = 0;
    let width = 0
    let height = 0;
    let isVertical = false;
    Drag.drag(dw.bar,
        e => {
            isVertical = dw.drawerBody.classList.contains("top") || dw.drawerBody.classList.contains("bottom")
            dw.bar.classList.add('drag')
            if (isVertical) {
                height = parseInt(getComputedStyle(dw.drawerBody).getPropertyValue('--bb-drawer-height'))
                originY = e.clientY || e.touches[0].clientY
            }
            else {
                width = parseInt(getComputedStyle(dw.drawerBody).getPropertyValue('--bb-drawer-width'))
                originX = e.clientX || e.touches[0].clientX
            }
        },
        e => {
            if (isVertical) {
                const eventY = e.clientY || (e.touches.length || e.touches.length > 0 && e.touches[0].clientY)
                const moveY = eventY - originY
                let newHeight = 0;
                if (dw.drawerBody.classList.contains("bottom")) {
                    newHeight = height - moveY
                }
                else {
                    newHeight = height + moveY
                }
                const maxHeight = window.innerHeight;
                if (newHeight > 100 && newHeight < maxHeight) {
                    dw.drawerBody.style.setProperty('--bb-drawer-height', `${newHeight}px`)
                }
            }
            else {
                const eventX = e.clientX || (e.touches.length || e.touches.length > 0 && e.touches[0].clientX)
                const moveX = eventX - originX
                let newWidth = 0;
                if (dw.drawerBody.classList.contains("right")) {
                    newWidth = width - moveX
                }
                else {
                    newWidth = width + moveX
                }
                const maxWidth = window.innerWidth;
                if (newWidth > 100 && newWidth < maxWidth) {
                    dw.drawerBody.style.setProperty('--bb-drawer-width', `${newWidth}px`)
                }
            }
        },
        e => {
            dw.bar.classList.remove('drag')
        }
    )
}

export function execute(id, open) {
    const dw = Data.get(id)
    const el = dw.element
    if (open) {
        el.classList.add('show')
        dw.body.classList.add('overflow-hidden')
        let handler = window.setTimeout(() => {
            dw.drawerBody.classList.add('show')
            window.clearTimeout(handler)
            handler = null
        }, 20);
    }
    else {
        if (el.classList.contains('show')) {
            dw.drawerBody.classList.remove('show')
            let handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                handler = null
                el.classList.remove('show')
                dw.body.classList.remove('overflow-hidden')
            }, 320)
        }
    }
}

export function dispose(id) {
    const dw = Data.get(id)
    const el = dw.element
    if (el.classList.contains('show')) {
        el.classList.remove('show')
        dw.drawerBody.classList.remove('show')
        dw.body.classList.remove('overflow-hidden')
    }
    Data.remove(id)

    if (dw.bar) {
        Drag.dispose(dw.bar)
    }
}
