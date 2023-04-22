import Data from "./data.js"

export function init(id) {
    const el = document.getElementById(id)
    const tip = {
        tooltip: new bootstrap.Tooltip(el, { sanitize: el.getAttribute('data-bs-sanitize') !== 'false' })
    }
    Data.set(id, tip)
}

export function dispose(id) {
    const tip = Data.get(id)
    Data.remove(id)

    if (tip.tooltip) {
        const delay = 10
        const handler = setTimeout(() => {
            clearTimeout(handler)
            if (tip.tooltip) {
                tip.tooltip.dispose()
                delete tip.tooltip
            }
        }, delay)
    }
}
