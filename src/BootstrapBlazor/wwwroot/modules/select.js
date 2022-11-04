import DropdownBase from "./base/base-dropdown.js"
import EventHandler from "./base/event-handler.js"
import { getHeight, getInnerHeight } from "./base/utility.js"

export class Select extends DropdownBase {
    _init() {
        // el, obj, method
        this._search = this._element.querySelector('input.search-text')
        this._invoker = this._config.arguments[0]
        this._invokeMethodName = this._config.arguments[1]

        super._init()
    }

    _setListeners() {
        super._setListeners()

        const shown = () => {
            if (this._search) {
                this._search.focus();
            }
            const prev = this._toggleMenu.querySelector('.dropdown-item.preActive')
            if (prev) {
                prev.classList.remove('preActive')
            }
            this._scrollToActive()
        }

        const keydown = e => {
            e.stopPropagation()

            if (this._toggle.classList.contains('show')) {
                const items = this._toggleMenu.querySelectorAll('.dropdown-item:not(.search, .disabled)')
                let activeItem = this._toggleMenu.querySelector('.dropdown-item.preActive') 
                if (activeItem == null) activeItem = this._toggleMenu.querySelector('.dropdown-item.active')

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
                            this._scrollToActive(activeItem)
                        } else if (e.key === "ArrowDown") {
                            do {
                                activeItem = activeItem.nextElementSibling
                            }
                            while (activeItem && !activeItem.classList.contains('dropdown-item'))
                            if (!activeItem) {
                                activeItem = items[0]
                            }
                            activeItem.classList.add('preActive')
                            this._scrollToActive(activeItem)
                        }
                    }

                    if (e.key === "Enter") {
                        this._toggleMenu.classList.remove('show')
                        let index = this._indexOf(activeItem)
                        this._invoker.invokeMethodAsync(this._invokeMethodName, index)
                    }
                }
            }
        }

        EventHandler.on(this._element, 'shown.bs.dropdown', shown);
        EventHandler.on(this._element, 'keydown', keydown)
    }

    _indexOf(element) {
        const items = this._toggleMenu.querySelectorAll('.dropdown-item')
        return Array.prototype.indexOf.call(items, element)
    }

    _scrollToActive(activeItem) {
        if (!activeItem) {
            activeItem = this._toggleMenu.querySelector('.dropdown-item.active')
        }

        if (activeItem) {
            const innerHeight = getInnerHeight(this._toggleMenu)
            const itemHeight = getHeight(activeItem);
            const index = this._indexOf(activeItem)
            const margin = itemHeight * index - (innerHeight - itemHeight) / 2;
            if (margin >= 0) {
                this._toggleMenu.scrollTo(0, margin);
            } else {
                this._toggleMenu.scrollTo(0, 0);
            }
        }
    }

    _dispose() {
        EventHandler.off(this._element, 'shown.bs.dropdown')
        EventHandler.off(this._element, 'keydown')

        super._dispose()
    }
}
