import Data from "../../modules/data.js"

const showTooltip = (id, title) => {
    const el = document.getElementById(id)

    if (el) {
        const tooltip = bootstrap.Tooltip.getOrCreateInstance(el, {
            title: title
        })
        if (tooltip._config.title !== title) {
            tooltip._config.title = title
        }
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

const share = context => {
    navigator.share(context)
}

export {
    share,
    showTooltip,
    removeTooltip,
    dispose
}
