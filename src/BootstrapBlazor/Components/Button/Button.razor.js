import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)
    const button = { el }
    Data.set(id, el)
}

export function showTooltip(id, title) {
    const button = Data.get(id)

    button.tooltip = bootstrap.Tooltip.getOrCreateInstance(el, {
        title: title
    })
}

export function removeTooltip(id) {
    const button = Data.get(id)

    if (button.tooltip) {
        button.tooltip.dispose()
        delete button.tooltip
    }
}

export function dispose(id) {
    removeTooltip(id)
    Data.remove(id)
}
