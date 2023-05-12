import { getHeight, getInnerHeight } from "../../modules/utility.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"
import Popover from "../../modules/base-popover.js?v=$version"

export function init(id, method, obj) {
    const el = document.getElementById(id)

    if (el == null) {
        return
    }

    const search = el.querySelector("input.search-text")
    const popover = Popover.init(el)
    const select = {
        el,
        search,
        method,
        obj,
        popover
    }

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
        e.preventDefault()
        e.stopPropagation()

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
                    }
                }

                if (e.key === "Enter") {
                    popover.toggleMenu.classList.remove('show')
                    let index = indexOf(el, activeItem)
                    obj.invokeMethodAsync(method, index)
                }
            }
        }
    }

    EventHandler.on(el, 'shown.bs.dropdown', shown);
    EventHandler.on(el, 'keydown', keydown)

    Data.set(id, select)
}


export function dispose(id) {
    const data = Data.get(id)
    if (data) {
        EventHandler.off(data.el, 'shown.bs.dropdown')
        EventHandler.off(data.el, 'keydown')
        Popover.dispose(data.popover)
    }
    Data.remove(id)
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
