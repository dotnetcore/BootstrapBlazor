import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)

    if (el) {
        const button = { el }
        Data.set(id, button)
    }
}

export function showTooltip(id, title) {
    const button = Data.get(id)

    if (button) {
        button.tooltip = bootstrap.Tooltip.getOrCreateInstance(button.el, {
            title: title
        })
    }
}

export function removeTooltip(id) {
    const button = Data.get(id)

    if (button && button.tooltip) {
        button.tooltip.dispose()
        delete button.tooltip
    }
}

export function dispose(id) {
    removeTooltip(id)
    Data.remove(id)
}
