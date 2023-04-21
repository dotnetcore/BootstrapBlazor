import { getDescribedElement, getDescribedOwner, isDisabled, isFunction } from "./utility.js"
import Data from "./data.js"
import EventHandler from "./event-handler.js"

export default {
    init(id) {
        const el = document.getElementById(id)
        const dropdown = {
            el,
            class: 'popover-dropdown',
            toggleClass: '.dropdown-toggle',
            dropdown: '.dropdown-menu',
            isDisabled: () => {
                return isDisabled(el) || isDisabled(el.parentNode) || isDisabled(el.querySelector('.form-control'))
            }
        }
        dropdown.toggle = el.querySelector(el.toggleClass)
        dropdown.isPopover = dropdown.toggle.getAttribute('data-bs-toggle') === 'bb.dropdown'
        dropdown.toggleMenu = el.querySelector(dropdown.dropdown)
        dropdown.hackPopover = () => {
            if (dropdown.popover) {
                dropdown.popover._isWithContent = () => true

                const getTipElement = dropdown.popover._getTipElement
                const fn = tip => {
                    tip.classList.add(dropdown.config.class)
                }
                dropdown.popover._getTipElement = () => {
                    let tip = getTipElement.call(dropdown.popover)
                    fn(tip)
                    return tip
                }
            }
        }

        dropdown.isShown = () => {
            return dropdown.popover && dropdown.popover._isShown();
        }

        dropdown.setCustomClass = () => {
            const extraClass = dropdown.toggle.getAttribute('data-bs-custom-class')
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
                const popover = getDescribedElement(dropdown.toggle)
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

            const active = () => {
                if (!dropdown.isDisabled()) {
                    dropdown.popover = bootstrap.Popover.getInstance(dropdown.toggle);
                    if (!dropdown.popover) {
                        dropdown.popover = new bootstrap.Popover(dropdown.toggle)
                        dropdown.hackPopover()
                        dropdown.popover.toggle()
                    }
                }

                dropdown.clickToggle()
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
    },

    dispose(id) {
        const dropdown = Data.get(id)
        Data.remove(id)

        if (dropdown.isPopover) {
            EventHandler.off(dropdown.el, 'show.bs.popover')
            EventHandler.off(dropdown.el, 'inserted.bs.popover')
            EventHandler.off(dropdown.el, 'hide.bs.popover')
            EventHandler.off(dropdown.el, 'click', '.dropdown-toggle')
            EventHandler.off(dropdown.toggleMenu, 'click', '.dropdown-item')
        }
        else {
            EventHandler.off(el, 'show.bs.dropdown')
        }
    }
}
