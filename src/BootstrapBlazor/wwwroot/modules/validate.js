import Data from "./data.js"

export function execute(id, title) {
    const el = document.getElementById(id)
    const v = {
        el,
        tip: new bootstrap.Tooltip(el, { title })
    }
    Data.set(id, v)

    v.tip._config.customClass = "is-invalid"
    if (!v.tip._isShown()) {
        v.tip.show()
    }
}

export function dispose(id) {
    const v = Data.get(id) || {}
    Data.remove(id)

    if (v.tip) {
        const delay = 10
        const handler = setTimeout(() => {
            clearTimeout(handler)
            if (v.tip) {
                v.tip.dispose()
                delete v.tip
            }
        }, delay)
    }
}
