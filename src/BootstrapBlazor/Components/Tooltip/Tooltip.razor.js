import { hackTooltip } from "../../modules/utility.js"
import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el) {
        hackTooltip();

        const fallbackPlacements = (el.getAttribute('data-bs-fallbackPlacements') || 'top,right,bottom,left').split(',');
        const tip = {
            tooltip: new bootstrap.Tooltip(el, {
                sanitize: el.getAttribute('data-bs-sanitize') !== 'false',
                title: () => {
                    return el.getAttribute('data-bs-original-title')
                },
                fallbackPlacements: fallbackPlacements
            })
        }
        Data.set(id, tip)
    }
}

export function dispose(id) {
    const tip = Data.get(id)
    Data.remove(id)

    if (tip && tip.tooltip) {
        const timeout = tip.tooltip._config.trigger.includes("focus") ? 300 : 0
        const handler = setTimeout(() => {
            clearTimeout(handler)
            if (tip.tooltip) {
                tip.tooltip.dispose()
                delete tip.tooltip
            }
        }, timeout)
    }
}
