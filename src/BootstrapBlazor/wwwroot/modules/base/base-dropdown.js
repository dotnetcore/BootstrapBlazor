import BlazorComponent from "./blazor-component.js"
import EventHandler from "./event-handler.js"
import { isDisabled } from "./index.js"
import { getDescribedElement, getDescribedOwner, isFunction } from "./utility.js"

export default class DropdownBase extends BlazorComponent {
    static get Default() {
        return {
            class: 'popover-dropdown',
            toggleClass: '.dropdown-toggle',
            dropdown: '.dropdown-menu'
        }
    }

    _init() {
        this._toggle = this._element.querySelector(this._config.toggleClass)
        this._isPopover = this._toggle.getAttribute('data-bs-toggle') === 'bb.dropdown'
        this._toggleMenu = this._element.querySelector(this._config.dropdown)

        this._setListeners()
    }

    _clickToggle() {

    }

    _setListeners() {
        if (this._isPopover) {
            this._hasDisplayNone = false;

            const showPopover = e => {
                const disabled = this._isDisabled();

                if (!disabled) {
                    this._element.classList.add('show');
                }
                if (disabled) {
                    e.preventDefault();
                }
            }

            const insertedPopover = () => {
                const popover = getDescribedElement(this._toggle)
                if (popover) {
                    let body = popover.querySelector('.popover-body')
                    if (!body) {
                        body = document.createElement('div')
                        body.classList.add('popover-body')
                        popover.append(body)
                    }
                    body.classList.add('show')
                    const content = this._toggleMenu
                    if (content.classList.contains("d-none")) {
                        this._hasDisplayNone = true;
                        content.classList.remove("d-none")
                    }
                    body.append(content)
                }
            }

            const hidePopover = e => {
                e.stopPropagation()

                if (this._hasDisplayNone) {
                    this._toggleMenu.classList.add("d-none");
                }
                this._element.classList.remove('show');
                this._element.append(this._toggleMenu);
            }

            const active = () => {
                if (!this._isDisabled()) {
                    this._popover = bootstrap.Popover.getInstance(this._toggle);
                    if (!this._popover) {
                        this._popover = new bootstrap.Popover(this._toggle)
                        this._hackPopover()
                        this._popover.toggle()
                    }
                }

                this._clickToggle()
            }

            const closePopover = e => {
                const selector = `.${DropdownBase.Default.class}.show`;
                const el = e.target;
                if (el.closest(selector)) {
                    return;
                }
                const owner = getDescribedElement(el.closest(DropdownBase.Default.toggleClass));
                document.querySelectorAll(selector).forEach(ele => {
                    if (ele !== owner) {
                        const element = getDescribedOwner(ele);
                        if (element) {
                            const popover = bootstrap.Popover.getInstance(element);
                            if (popover) {
                                popover.hide();
                            }
                        }
                    }
                });
            }

            EventHandler.on(this._element, 'show.bs.popover', showPopover)
            EventHandler.on(this._element, 'inserted.bs.popover', insertedPopover)
            EventHandler.on(this._element, 'hide.bs.popover', hidePopover)
            EventHandler.on(this._element, 'click', this._config.toggleClass, active)
            EventHandler.on(this._toggleMenu, 'click', '.dropdown-item', e => {
                if (this._popover._config.autoClose !== 'outside') {
                    this.hide()
                }
            })

            if (!window.bb_dropdown) {
                window.bb_dropdown = true

                EventHandler.on(document, 'click', closePopover);
            }
        }
        else {
            const show = e => {
                if (this._isDisabled()) {
                    e.preventDefault()
                }
                this._setCustomClass()
            }

            EventHandler.on(this._element, 'show.bs.dropdown', show)
        }
    }

    _setCustomClass() {
        const extraClass = this._toggle.getAttribute('data-bs-custom-class')
        if (extraClass) {
            this._toggleMenu.classList.add(...extraClass.split(' '))
        }
    }

    _isDisabled() {
        return isDisabled(this._element) || isDisabled(this._element.parentNode) || isDisabled(this._element.querySelector('.form-control'))
    }

    _dispose() {
        if (this._isPopover) {
            EventHandler.off(this._element, 'show.bs.popover')
            EventHandler.off(this._element, 'inserted.bs.popover')
            EventHandler.off(this._element, 'hide.bs.popover')
            EventHandler.off(this._element, 'click', '.dropdown-toggle')
            EventHandler.off(this._toggleMenu, 'click', '.dropdown-item')
        }
        else {
            EventHandler.off(this._element, 'show.bs.dropdown')
        }
    }

    _execute(args) {
        if (args.length > 1 && typeof args[0] === 'object') {
            args = [].slice.call(args, 1)
        }
        const fn = this[args[0]]
        if (isFunction(fn)) {
            fn.call(this)
        }
    }

    _isShown() {
        return this._popover && this._popover._isShown();
    }

    hide() {
        if (this._isShown()) {
            this._popover.hide();
        }
    }

    show() {
        if (!this._isShown()) {
            this._popover.show();
        }
    }

    toggle() {
        if (this._isShown()) {
            this._popover.hide();
        } else {
            this._popover.show();
        }
    }
}
