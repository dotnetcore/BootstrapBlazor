import { getDescribedElement, getDescribedOwner, hackPopover, isDisabled } from "../../modules/utility.js"
import { showTooltip, removeTooltip } from "./Button.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const config = {
    class: 'popover-confirm',
    dismiss: '.popover-confirm-buttons > div',
    popoverSelector: '.popover-confirm.show'
}

export function init(id) {
    const el = document.getElementById(id)
    if (el == null) {
        return
    }

    const confirm = {
        el,
        container: el.querySelector('[data-bb-toggle="confirm"]')
    }
    Data.set(id, confirm)

    confirm.show = e => {
        const disabled = isDisabled(el);
        if (disabled) {
            e.preventDefault()
        }
    }
    confirm.inserted = () => {
        const popover = getDescribedElement(el)
        const children = confirm.container.children
        const len = children.length
        for (let i = 0; i < len; i++) {
            popover.appendChild(children[i])
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

        const handler = setTimeout(() => {
            clearTimeout(handler);
            const hasConfirm = el.hasAttribute('data-bb-confirm');
            if (hasConfirm) {
                confirm.popover.dispose();
                delete confirm.popover;
            }
        }, 50);
    }

    EventHandler.on(el, 'show.bs.popover', confirm.show)
    EventHandler.on(el, 'inserted.bs.popover', confirm.inserted)
    EventHandler.on(el, 'hide.bs.popover', confirm.hide)

    if (config.dismiss != null) {
        confirm.dismissHandler = e => {
            const ele = e.target.closest(config.popoverSelector)
            if (ele) {
                const element = getDescribedOwner(ele)
                if (element) {
                    const popover = bootstrap.Popover.getInstance(element);
                    if (popover) {
                        popover.hide()
                    }
                }
            }
        }
        EventHandler.on(document, 'click', config.dismiss, confirm.dismissHandler)
    }

    confirm.checkCancel = el => {
        // check button
        let self = el === confirm.el || el.closest('.dropdown-toggle') === confirm.el
        self = self && confirm.popover && confirm.popover._isShown()

        // check popover
        self = self || el.closest('.pop-confirm') || el.closest(config.popoverSelector)
        return self
    }

    confirm.closeConfirm = e => {
        const el = e.target
        if (!confirm.checkCancel(el)) {
            document.querySelectorAll(config.popoverSelector).forEach(function (ele) {
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
        window.bb_confirm = {
            handle: false,
            items: []
        }
    }
    if (!window.bb_confirm.handle) {
        window.bb_confirm.handle = true
        EventHandler.on(document, 'click', confirm.closeConfirm);
    }
    window.bb_confirm.items.push(id)
}

export function showConfirm(id) {
    const confirm = Data.get(id)

    if (confirm && !confirm.popover) {
        confirm.popover = new bootstrap.Popover(confirm.el);
        hackPopover(confirm.popover, config.class)
        confirm.popover.show()
    }

    // close other confirm
    document.querySelectorAll(config.popoverSelector).forEach(el => {
        const owner = getDescribedOwner(el)
        if (owner !== confirm.el) {
            const id = owner.getAttribute('id')
            if (id) {
                const p = Data.get(id)
                if (p) {
                    p.hide()
                }
            }
        }
    })
}

export function submit(id) {
    const el = document.getElementById(id)

    if (el) {
        const form = el.closest('form');
        if (form !== null) {
            const button = document.createElement('button');
            button.setAttribute('type', 'submit');
            button.setAttribute('hidden', 'true');
            form.append(button);
            button.click();
            button.remove();
        }
    }
}
export function dispose(id) {
    const confirm = Data.get(id)
    Data.remove(id)

    if (confirm) {
        window.bb_confirm.items.pop(id)
        if (window.bb_confirm.items.length === 0) {
            delete window.bb_confirm
            EventHandler.off(document, 'click', confirm.closeConfirm)
        }
        if (confirm.popover) {
            confirm.popover.dispose()
        }
        if (config.dismiss) {
            EventHandler.off(document, 'click', config.dismiss, confirm.dismissHandler)
        }
    }
}

export {
    showTooltip,
    removeTooltip
}
