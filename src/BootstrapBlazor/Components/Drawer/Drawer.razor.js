import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)
    const dw = {
        element: el,
        body: document.querySelector('body'),
        drawerBody: el.querySelector('.drawer-body')
    }
    Data.set(id, dw)
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
}
