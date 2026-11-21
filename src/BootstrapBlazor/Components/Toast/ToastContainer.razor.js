import Data from "../../modules/data.js"
import { attachOverlay, supportsPopover } from "../../modules/fullscreen.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el) {
        if (!supportsPopover(el)) {
            el.removeAttribute('popover')
        }
        Data.set(id, { detachOverlay: attachOverlay(el) })

        toggle(id, false)
    }
}

export function toggle(id, show) {
    const el = document.getElementById(id)
    if (!el || !el.hasAttribute('popover') || !supportsPopover(el)) {
        return
    }

    if (show && !el.matches(':popover-open')) {
        el.showPopover()
    }
    else if (!show && el.matches(':popover-open')) {
        el.hidePopover()
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el && el.hasAttribute('popover') && supportsPopover(el) && el.matches(':popover-open')) {
        el.hidePopover()
    }
    Data.get(id)?.detachOverlay()
    Data.remove(id)
}
