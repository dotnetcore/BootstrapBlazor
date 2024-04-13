import Data from "../../modules/data.js"

export function init(id) {
    const text = {
        prevMethod: '',
        element: document.getElementById(id)
    }

    Data.set(id, text)
}

export function execute(id, method, position) {
    const text = Data.get(id)

    if (text) {
        const autoScroll = text.element.getAttribute('data-bb-scroll') === 'auto'
        if (method === 'update') {
            method = text.prevMethod
        }
        if (method === 'toTop') {
            position = 0;
        }
        if (autoScroll || method === 'toBottom') {
            position = text.element.scrollHeight
        }

        if (!isNaN(position)) {
            text.element.scrollTop = position;
        }

        if (method !== 'update') {
            text.prevMethod = method;
        }
    }
}

export function dispose(id) {
    Data.remove(id)
}
