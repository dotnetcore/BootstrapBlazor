import { copy } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const dialog = {
        element: el
    }
    Data.set(id, dialog)

    EventHandler.on(el, 'click', 'button', e => {
        const text = e.delegateTarget.parentNode.querySelector('.is-display').textContent
        if (text) {
            dialog.copy(e.delegateTarget, text)
        }
    })

    dialog.copy = (element, text) => {
        copy(text)

        dialog.tooltip = bootstrap.Tooltip.getInstance(element)
        if (dialog.tooltip) {
            dialog.reset(element)
        }
        else {
            dialog.show(element)
        }
    }

    dialog.show = element => {
        dialog.tooltip = new bootstrap.Tooltip(element, {
            title: dialog.element.getAttribute('data-bb-title')
        })
        dialog.tooltip.show()
        dialog.tooltipHandler = setTimeout(() => {
            clearTimeout(dialog.tooltipHandler);
            if (dialog.tooltip) {
                dialog.tooltip.dispose();
            }
        }, 1000);
    }

    dialog.reset = element => {
        if (dialog.tooltipHandler) {
            clearTimeout(dialog.tooltipHandler)
        }
        if (dialog.tooltip) {
            dialog.tooltipHandler = setTimeout(() => {
                clearTimeout(dialog.tooltipHandler)
                dialog.tooltip.dispose();
                dialog.show()
            }, 10)
        }
        else {
            dialog.show(element)
        }
    }
}

export function dispose(id) {
    const dialog = Data.get(id)
    Data.remove(id)
    EventHandler.off(dialog.element, 'click', 'button')
}
