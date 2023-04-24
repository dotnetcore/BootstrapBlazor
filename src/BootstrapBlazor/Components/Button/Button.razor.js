import Data from "../../modules/data.js"

const showTooltip = (id, title) => {
    const el = document.getElementById(id)

    if (el) {
        bootstrap.Tooltip.getOrCreateInstance(el, {
            title: title
        })
    }
}

const removeTooltip = id => {
    const el = document.getElementById(id)

    if (el) {
        const tip = bootstrap.Tooltip.getInstance(el)
        if (tip) {
            tip.dispose()
        }
    }
}

const dispose = id => {
    removeTooltip(id)
}

export {
    showTooltip,
    removeTooltip,
    dispose
}
