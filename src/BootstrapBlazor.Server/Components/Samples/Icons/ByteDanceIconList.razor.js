import { copy } from "../../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    const faList = {
        element: el
    }
    Data.set(id, faList)

    EventHandler.on(el, 'click', 'a', e => {
        e.preventDefault()
        e.stopPropagation()

        const span = e.delegateTarget.querySelector('span')
        const name = span.innerHTML;
        faList.copy(e.delegateTarget, name)
    })

    faList.copy = (element, text) => {
        copy(text)

        faList.tooltip = bootstrap.Tooltip.getInstance(element)
        if (faList.tooltip) {
            faList.reset(element)
        }
        else {
            faList.show(element)
        }
    }

    faList.show = element => {
        faList.tooltip = new bootstrap.Tooltip(element, {
            title: faList.element.getAttribute('data-bb-title')
        })
        faList.tooltip.show()
        faList.tooltipHandler = setTimeout(() => {
            clearTimeout(faList.tooltipHandler)
            if (faList.tooltip) {
                faList.tooltip.dispose()
            }
        }, 1000)
    }

    faList.reset = element => {
        if (faList.tooltipHandler) {
            clearTimeout(faList.tooltipHandler)
        }
        if (faList.tooltip) {
            faList.tooltipHandler = setTimeout(() => {
                clearTimeout(faList.tooltipHandler)
                faList.tooltip.dispose()
                faList.show()
            }, 10)
        }
        else {
            faList.show(element)
        }
    }
}

export function dispose(id) {
    const faList = Data.get(id)
    Data.remove(id)

    if (faList) {
        EventHandler.off(faList.element, 'click', 'a')
    }
}
