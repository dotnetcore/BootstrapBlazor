import { copy } from "../../BootstrapBlazor/modules/utility.js"
import Data from "../../BootstrapBlazor/modules/data.js"
import EventHandler from "../../BootstrapBlazor/modules/event-handler.js"

export function init(id, invoke, updateMethod, showDialogMethod, copyIcon) {
    const el = document.getElementById(id);
    const faList = {
        element: el,
        invoke,
        updateMethod,
        showDialogMethod,
        copyIcon
    }
    Data.set(id, faList)

    if (el.classList.contains('is-catalog')) {
        faList.body = el.querySelector('.icons-body')
        faList.target = faList.body.getAttribute('data-bs-target')
        faList.scrollspy = new bootstrap.ScrollSpy(faList.body, {
            target: faList.target
        })
        faList.base = el.querySelector('#bb-fa-top')
    }

    EventHandler.on(el, 'click', '.nav-link', e => {
        e.preventDefault()
        e.stopPropagation()

        const targetId = e.delegateTarget.getAttribute('href')
        const target = el.querySelector(targetId)

        const rect = target.getBoundingClientRect()
        let margin = rect.top
        let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '')
        if (marginTop) {
            margin = margin - parseInt(marginTop)
        }
        const offset = el.getAttribute('bb-data-offset')
        if (offset) {
            margin = margin - parseInt(offset)
        }
        margin = margin - faList.base.getBoundingClientRect().top
        faList.body.scrollTo(0, margin)
    })

    EventHandler.on(el, 'click', '.icons-body a', e => {
        e.preventDefault()
        e.stopPropagation()

        const i = e.delegateTarget.querySelector('i')
        const css = i.getAttribute('class')
        faList.invoke.invokeMethodAsync(faList.updateMethod, css)
        const dialog = el.classList.contains('is-dialog')
        if (dialog) {
            faList.invoke.invokeMethodAsync(faList.showDialogMethod, css)
        } else if (faList.copyIcon) {
            faList.copy(e.delegateTarget, css)
        }
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
        EventHandler.off(faList.element, 'click', '.nav-link')
        EventHandler.off(faList.element, 'click', '.icons-body a')

        if (faList.scrollspy) {
            faList.scrollspy.dispose()
        }
    }
}
