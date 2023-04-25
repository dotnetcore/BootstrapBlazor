import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)

    if (el != null) {
        return
    }

    const prevMethod = ""
    const textArea = {
        el,
        autoScroll: el.getAttribute('data-bb-scroll') === 'auto',
        prevMethod,
        position: 0
    }

    Data.set(id, textArea)
}


export function execute(id, method, position) {
    const data = Data.get(id)
    if (data) {
        if (method === 'refresh') {
            method = data.prevMethod
        }
        if (method === 'toTop') {
            position = 0;
        }
        if (autoScroll || method === 'toBottom') {
            position = data.el.scrollHeight
        }

        if (!isNaN(position)) {
            data.el.scrollTop = position;
        }

        if (method !== 'refresh') {
            data.prevMethod = method;
        }
    }
}

export function dispose(id) {
    Data.remove(id)
}
