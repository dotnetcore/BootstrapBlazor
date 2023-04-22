import { getDescribedElement, getDescribedOwner, isDisabled } from "./utility.js"
import Data from "./data.js"
import EventHandler from "./event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const confirm = {
        el,
        popover: bootstrap.Popover.getOrCreateInstance(el),
        config: {
            class: 'popover-confirm',
            dismiss: '.popover-confirm-buttons > div',
            popoverSelector: '.popover-confirm.show'
        },
        container = el.querySelector('[data-bb-toggle="confirm"]')
    }
    Data.set(id, config)

    confirm.show = e => {
        const disabled = isDisabled(el);
        if (disabled) {
            e.preventDefault()
        }
    }
    confirm.inserted = () => {
        const popover = getDescribedElement(el)
        const children = cofirm.container.children
        const len = children.length
        for (let i = 0; i < len; i++) {
            popover.appendChild(children[0])
        }
    }
    confirm.hide = () => {
        const popover = getDescribedElement(el)
        popover.classList.remove('show')

        const children = popover.children
        const len = children.length
        for (let i = 1; i < len; i++) {
            confirm.container.appendChild(children[1])
        }
    }

    EventHandler.on(el, 'show.bs.popover', confirm.show)
    EventHandler.on(el, 'inserted.bs.popover', confirm.inserted)
    EventHandler.on(el, 'hide.bs.popover', confirm.hide)

    if (confirm.config.dismiss != null) {
        EventHandler.on(document, 'click', confirm.config.dismiss, () => confirm.popover.hide())
    }

    confirm.checkCancel = el => {
        // check button
        let self = el === el || el.closest('.dropdown-toggle') === el
        self = self && confirm.popover._isShown()

        // check popover
        self = self || el.closest(confirm.config.popoverSelector)
        return self
    }

    confirm.closeConfirm = e => {
        const el = e.target
        if (!confirm.checkCancel(el)) {
            document.querySelectorAll(confirm.config.popoverSelector).forEach(function (ele) {
                const element = getDescribedOwner(ele)
                if (element) {
                    const popover = bootstrap.Popover.getInstance(element);
                    if (popover) {
                        popover.hide()
                    }
                }
            })
        }
    }

    if (!window.bb_confirm) {
        window.bb_confirm = true

        EventHandler.on(document, 'click', confirm.closeConfirm);
    }
}

const toggle = id => {
    const confirm = Data.get(id)
    if (confiirm.popover) {
        confirm.popover.toggle()
    }
}

export function showConfirm(id) {

}

export function submit(id) {

}

export function dispose(id) {
    const confirm = Data.get(id)
    Data.remove(id)

    if (confirm.popover) {
        confirm.popover.dispose()
    }
    if (confirm.config.dismiss != null) {
        EventHandler.off(document, 'click', confirm.config.dismiss)
    }
}
}
