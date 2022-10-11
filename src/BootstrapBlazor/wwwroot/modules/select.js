import BlazorComponent from "./base/blazor-component.js";
import EventHandler from "./base/event-handler.js";
import { isDisabled } from "./base/index.js";
import { getHeight, getInnerHeight } from "./base/utility.js";

export class Select extends BlazorComponent {
    _init() {
        // el, obj, method
        this._toggle = this._element.querySelector('.dropdown-toggle')
        this._toggleMenu = this._element.querySelector('.dropdown-menu')
        this._search = this._element.querySelector('input.search-text')
        this._input = this._element.querySelector('.form-select')
        this._invoker = this._config.arguments[0]
        this._invokeMethodName = this._config.arguments[1]
        this._isPopover = this._toggle.getAttribute('data-bs-toggle') === 'bb.dropdown'
        this._setListeners()
    }

    _setListeners() {
        const show = e => {
            if (isDisabled(this._input)) {
                e.preventDefault();
            }
        }

        const shown = () => {
            if (this._search) {
                this._search.focus();
            }
            const prev = this._toggleMenu.querySelector('.dropdown-item.preActive')
            if (prev) {
                prev.classList.remove('preActive')
            }
            this._scrollToActive();
        }

        if(this._isPopover)
        {
            EventHandler.on(this._element, 'inserted.bs.popover', shown);
        }
        else {
            EventHandler.on(this._element, 'show.bs.dropdown', show)
            EventHandler.on(this._element, 'shown.bs.dropdown', shown);
        }

        EventHandler.on(this._element, 'keydown', e => {
            e.stopPropagation()
            e.preventDefault()

            if (this._toggle.classList.contains('show')) {
                const items = this._toggleMenu.querySelectorAll('.dropdown-item:not(.search, .disabled)')
                let activeItem = this._toggleMenu.querySelector('.dropdown-item.preActive') ?? this._toggleMenu.querySelector('.dropdown-item.active')

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
        })
    }

    _indexOf(element) {
        const items = this._toggleMenu.querySelectorAll('.dropdown-item')
        return Array.prototype.indexOf.call(items, element)
    }

    _scrollToActive(activeItem) {
        if (!activeItem) {
            activeItem = this._toggleMenu.querySelector('.dropdown-item.active')
        }

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

    _dispose() {
        if(this._isPopover){
            EventHandler.off(this._element, 'inserted.bs.popover')
        }
        else {
            EventHandler.off(this._element, 'show.bs.dropdown')
            EventHandler.off(this._element, 'shown.bs.dropdown')
        }
        EventHandler.off(this._element, 'keydown')
    }
}
