import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { isDisabled } from "./base/index.js"
import { getDescribedElement, getDescribedOwner } from "./base/utility.js"

export class Confirm extends BlazorComponent {
    static get Default() {
        return {
            class: 'popover-confirm',
            dismiss: '.popover-confirm-buttons > div',
            popoverSelector: '.popover-confirm.show',
            container: '[data-bb-toggle="confirm"]'
        }
    }

    _init() {
        this._popover = bootstrap.Popover.getOrCreateInstance(this._element)
        this._hackPopover()
        this._id = this._element.getAttribute("id")
        this._container = this._element.querySelector(this._config.container)
        this._setListeners()
    }

    _setListeners() {
        this._show = e => {
            const disabled = isDisabled(this._element);
            if (disabled) {
                e.preventDefault()
            }
        }

        this._inserted = () => {
            const popover = getDescribedElement(this._element)

            const children = this._container.children
            const len = children.length
            for (let i = 0; i < len; i++) {
                popover.appendChild(children[0])
            }
        }

        this._hide = () => {
            const popover = getDescribedElement(this._element)
            popover.classList.remove('show')

            const children = popover.children
            const len = children.length
            for (let i = 1; i < len; i++) {
                this._container.appendChild(children[1])
            }
        }

        EventHandler.on(this._element, 'show.bs.popover', this._show)
        EventHandler.on(this._element, 'inserted.bs.popover', this._inserted)
        EventHandler.on(this._element, 'hide.bs.popover', this._hide)

        if (this._config.dismiss != null) {
            EventHandler.on(document, 'click', this._config.dismiss, () => this._popover.hide())
        }

        this._checkCancel = el => {
            // check button
            let self = el === this._element || el.closest('.dropdown-toggle') === this._element
            self = self && this._popover._isShown()

            // check popover
            self = self || el.closest(this._config.popoverSelector)
            return self
        }

        this._closeConfirm = e => {
            const el = e.target
            if (!this._checkCancel(el)) {
                document.querySelectorAll(this._config.popoverSelector).forEach(function (ele) {
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

            EventHandler.on(document, 'click', this._closeConfirm);
        }
    }

    toggle() {
        if (this._popover) {
            this._popover.toggle()
        }
    }

    _dispose() {
        if (this._popover) {
            this._popover.dispose()
        }
        if (this._config.dismiss != null) {
            EventHandler.off(document, 'click', this._config.dismiss)
        }
    }
}
