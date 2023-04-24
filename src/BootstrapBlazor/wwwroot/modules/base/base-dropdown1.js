import EventHandler from "./event-handler.js"
import { isDisabled } from "./index.js"
import { getDescribedElement, getDescribedOwner, isFunction } from "./utility.js"

export default {
    init(el) {
        const dropdown = {
            el,
            class: 'popover-dropdown',
            toggleClass: '.dropdown-toggle',
            dropdown: '.dropdown-menu',
            isDisabled: () => {
                return isDisabled(el) || isDisabled(el.parentNode) || isDisabled(el.querySelector('.form-control'))
            }
        }
        dropdown.toggleElement = el.querySelector(dropdown.toggleClass)
        dropdown.isPopover = dropdown.toggleElement.getAttribute('data-bs-toggle') === 'bb.dropdown'
        dropdown.toggleMenu = el.querySelector(dropdown.dropdown)
        dropdown.isShown = () => {
            return dropdown.popover && dropdown.popover._isShown();
        }

        dropdown.setCustomClass = () => {
            const extraClass = dropdown.toggleElement.getAttribute('data-bs-custom-class')
            if (extraClass) {
                dropdown.toggleMenu.classList.add(...extraClass.split(' '))
            }
        }

        dropdown.hide = () => {
            if (dropdown.isShown()) {
                dropdown.popover.hide();
            }
        }

        dropdown.show = () => {
            if (!dropdown.isShown()) {
                dropdown.popover.show();
            }
        }

        dropdown.toggle = () => {
            if (dropdown.isShown()) {
                dropdown.popover.hide();
            } else {
                dropdown.popover.show();
            }
        }

        if (dropdown.isPopover) {
            dropdown.hasDisplayNone = false;

            const showPopover = e => {
                const disabled = dropdown.isDisabled();

                if (!disabled) {
                    el.classList.add('show');
                }
                if (disabled) {
                    e.preventDefault();
                }
            }

            const insertedPopover = () => {
                const popover = getDescribedElement(dropdown.toggleElement)
                if (popover) {
                    let body = popover.querySelector('.popover-body')
                    if (!body) {
                        body = document.createElement('div')
                        body.classList.add('popover-body')
                        popover.append(body)
                    }
                    body.classList.add('show')
                    const content = dropdown.toggleMenu
                    if (content.classList.contains("d-none")) {
                        dropdown.hasDisplayNone = true;
                        content.classList.remove("d-none")
                    }
                    body.append(content)
                }
            }

            const hidePopover = e => {
                e.stopPropagation()

                if (dropdown.hasDisplayNone) {
                    dropdown.toggleMenu.classList.add("d-none");
                }
                el.classList.remove('show');
                el.append(dropdown.toggleMenu);
            }

            const active = e => {
                if (!dropdown.isDisabled()) {
                    dropdown.popover = bootstrap.Popover.getInstance(dropdown.toggleElement);
                    if (!dropdown.popover) {
                        dropdown.popover = new bootstrap.Popover(dropdown.toggleElement)
                        hackPopover(dropdown.popover, dropdown.class)
                        dropdown.popover.toggle()
                    }
                }

                if (dropdown.clickToggle) {
                    dropdown.clickToggle(e)
                }
            }

            const closePopover = e => {
                const selector = `.${dropdown.class}.show`;
                const el = e.target;
                if (el.closest(selector)) {
                    return;
                }
                const owner = getDescribedElement(el.closest(dropdown.toggleClass));
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

            EventHandler.on(el, 'show.bs.popover', showPopover)
            EventHandler.on(el, 'inserted.bs.popover', insertedPopover)
            EventHandler.on(el, 'hide.bs.popover', hidePopover)
            EventHandler.on(el, 'click', dropdown.toggleClass, active)
            EventHandler.on(dropdown.toggleMenu, 'click', '.dropdown-item', e => {
                if (dropdown.popover._config.autoClose !== 'outside') {
                    dropdown.hide()
                }
            })

            if (!window.bb_dropdown) {
                window.bb_dropdown = true

                EventHandler.on(document, 'click', closePopover);
            }
        }
        else {
            const show = e => {
                if (dropdown.isDisabled()) {
                    e.preventDefault()
                }
                dropdown.setCustomClass()
            }

            EventHandler.on(el, 'show.bs.dropdown', show)
        }

        return dropdown
    },

    dispose(dropdown) {
        if (dropdown.isPopover) {
            EventHandler.off(dropdown.el, 'show.bs.popover')
            EventHandler.off(dropdown.el, 'inserted.bs.popover')
            EventHandler.off(dropdown.el, 'hide.bs.popover')
            EventHandler.off(dropdown.el, 'click', '.dropdown-toggle')
            EventHandler.off(dropdown.toggleMenu, 'click', '.dropdown-item')
        }
        else {
            EventHandler.off(dropdown.el, 'show.bs.dropdown')
        }
    }
}
