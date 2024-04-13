import { getHeight, getInnerHeight } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const search = el.querySelector("input.search-text")
    const popover = Popover.init(el)

    const shown = () => {
        if (search) {
            search.focus();
        }
        const prev = popover.toggleMenu.querySelector('.dropdown-item.preActive')
        if (prev) {
            prev.classList.remove('preActive')
        }
        scrollToActive(popover.toggleMenu, prev)
    }

    const keydown = e => {
        if (popover.toggleElement.classList.contains('show')) {
            const items = popover.toggleMenu.querySelectorAll('.dropdown-item:not(.search, .disabled)')
            let activeItem = popover.toggleMenu.querySelector('.dropdown-item.preActive')
            if (activeItem == null) activeItem = popover.toggleMenu.querySelector('.dropdown-item.active')

            if (activeItem) {
                if (items.length > 1) {
                    activeItem.classList.remove('preActive')
                    if (e.key === "ArrowUp") {
                        do {
                            activeItem = activeItem.previousElementSibling
                        }
                        while (activeItem && !activeItem.classList.contains('dropdown-item'))
                        if (!activeItem) {
                            activeItem = items[items.length - 1]
                        }
                        activeItem.classList.add('preActive')
                        scrollToActive(popover.toggleMenu, activeItem)
                        e.preventDefault()
                        e.stopPropagation()
                    }
                    else if (e.key === "ArrowDown") {
                        do {
                            activeItem = activeItem.nextElementSibling
                        }
                        while (activeItem && !activeItem.classList.contains('dropdown-item'))
                        if (!activeItem) {
                            activeItem = items[0]
                        }
                        activeItem.classList.add('preActive')
                        scrollToActive(popover.toggleMenu, activeItem)
                        e.preventDefault()
                        e.stopPropagation()
                    }
                }

                if (e.key === "Enter") {
                    popover.toggleMenu.classList.remove('show')
                    let index = indexOf(el, activeItem)
                    invoke.invokeMethodAsync(method, index)
                }
            }
        }
    }

    EventHandler.on(el, 'shown.bs.dropdown', shown);
    EventHandler.on(el, 'keydown', keydown)

    const select = {
        el,
        popover
    }
    Data.set(id, select)
}


export function dispose(id) {
    const select = Data.get(id)
    Data.remove(id)

    if (select) {
        EventHandler.off(select.el, 'shown.bs.dropdown')
        EventHandler.off(select.el, 'keydown')
        Popover.dispose(select.popover)
    }
}


function scrollToActive(el, activeItem) {
    if (!activeItem) {
        activeItem = el.querySelector('.dropdown-item.active')
    }

    if (activeItem) {
        const innerHeight = getInnerHeight(el)
        const itemHeight = getHeight(activeItem);
        const index = indexOf(el, activeItem)
        const margin = itemHeight * index - (innerHeight - itemHeight) / 2;
        if (margin >= 0) {
            el.scrollTo(0, margin);
        }
        else {
            el.scrollTo(0, 0);
        }
    }
}

function indexOf(el, element) {
    const items = el.querySelectorAll('.dropdown-item')
    return Array.prototype.indexOf.call(items, element)
}
