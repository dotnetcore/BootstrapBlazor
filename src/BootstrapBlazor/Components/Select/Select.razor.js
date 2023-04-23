import { getHeight, getInnerHeight } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import DropdownBase from "../../modules/base-dropdown.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    if (el == null) {
        return;
    }
    const select = {
        el, invoke, callback,
        search: el.querySelector('input.search-text'),
        dropdown: DropdownBase.init(el)
    }
    Data.set(id, select)

    select.scrollToActive = activeItem => {
        if (!activeItem) {
            activeItem = select.toggleMenu.querySelector('.dropdown-item.active')
        }

        if (activeItem) {
            const innerHeight = getInnerHeight(select.toggleMenu)
            const itemHeight = getHeight(activeItem);
            const index = select.indexOf(activeItem)
            const margin = itemHeight * index - (innerHeight - itemHeight) / 2;
            if (margin >= 0) {
                select.toggleMenu.scrollTo(0, margin);
            } else {
                select.toggleMenu.scrollTo(0, 0);
            }
        }
    }

    select.indexOf = element => {
        const items = select.toggleMenu.querySelectorAll('.dropdown-item')
        return Array.prototype.indexOf.call(items, element)
    }

    const shown = () => {
        if (select.search) {
            select.search.focus();
        }
        const prev = select.toggleMenu.querySelector('.dropdown-item.preActive')
        if (prev) {
            prev.classList.remove('preActive')
        }
        select.scrollToActive()
    }

    const keydown = e => {
        e.stopPropagation()

        if (select.toggle.classList.contains('show')) {
            const items = select.toggleMenu.querySelectorAll('.dropdown-item:not(.search, .disabled)')
            let activeItem = select.toggleMenu.querySelector('.dropdown-item.preActive')
            if (activeItem == null) activeItem = select.toggleMenu.querySelector('.dropdown-item.active')

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
                        select.scrollToActive(activeItem)
                    } else if (e.key === "ArrowDown") {
                        do {
                            activeItem = activeItem.nextElementSibling
                        }
                        while (activeItem && !activeItem.classList.contains('dropdown-item'))
                        if (!activeItem) {
                            activeItem = items[0]
                        }
                        activeItem.classList.add('preActive')
                        select.scrollToActive(activeItem)
                    }
                }

                if (e.key === "Enter") {
                    select.toggleMenu.classList.remove('show')
                    let index = select.indexOf(activeItem)
                    select.invoke.invokeMethodAsync(select.callback, index)
                }
            }
        }
    }

    EventHandler.on(select.element, 'shown.bs.dropdown', shown);
    EventHandler.on(select.element, 'keydown', keydown)
}

export function dispose(id) {
    const select = Data.get(id)

    if (select) {
        EventHandler.off(select.el, 'shown.bs.dropdown')
        EventHandler.off(select.el, 'keydown')

        if (select.dropdown) {
            DropdownBase.dispose(select.dropdown)
        }
    }
}
