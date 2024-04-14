import { getDescribedElement, getDescribedOwner, hackPopover, isDisabled } from "./utility.js"
import EventHandler from "./event-handler.js"

const Popover = {
    init(el, config) {
        const popover = {
            ...{
                el,
                class: 'popover-dropdown',
                toggleClass: '.dropdown-toggle',
                dropdownSelector: '.dropdown-menu',
                isDisabled: () => {
                    return isDisabled(el) || isDisabled(el.parentNode) || isDisabled(el.querySelector('.form-control'))
                },
                initCallback: null
            },
            ...config || {}
        }
        popover.toggleElement = el.querySelector(popover.toggleClass)
        popover.isPopover = popover.toggleElement.getAttribute('data-bs-toggle') === 'bb.dropdown'
        popover.toggleMenu = el.querySelector(popover.dropdownSelector)
        popover.isShown = () => {
            return popover.popover && popover.popover._isShown();
        }

        popover.setCustomClass = () => {
            const extraClass = popover.toggleElement.getAttribute('data-bs-custom-class')
            if (extraClass) {
                popover.toggleMenu.classList.add(...extraClass.split(' '))
            }
        }

        popover.hide = () => {
            if (popover.isShown()) {
                popover.popover.hide();
            }
        }

        popover.show = () => {
            if (!popover.isShown()) {
                popover.popover.show();
            }
        }

        popover.toggle = () => {
            if (popover.isShown()) {
                popover.popover.hide();
            }
            else {
                popover.popover.show();
            }
        }

        if (popover.isPopover) {
            popover.hasDisplayNone = false;

            const showPopover = e => {
                const disabled = popover.isDisabled();

                if (!disabled) {
                    el.classList.add('show');
                }
                if (disabled) {
                    e.preventDefault();
                }
            }

            const insertedPopover = () => {
                const popoverBody = getDescribedElement(popover.toggleElement)
                if (popoverBody) {
                    let body = popoverBody.querySelector('.popover-body')
                    if (!body) {
                        body = document.createElement('div')
                        body.classList.add('popover-body')
                        popoverBody.append(body)
                    }
                    body.classList.add('show')
                    const content = popover.toggleMenu
                    if (content.classList.contains("d-none")) {
                        popover.hasDisplayNone = true;
                        content.classList.remove("d-none")
                    }
                    body.append(content)
                }
            }

            const hidePopover = e => {
                e.stopPropagation()

                if (popover.hasDisplayNone) {
                    popover.toggleMenu.classList.add("d-none");
                }
                el.classList.remove('show');
                el.append(popover.toggleMenu);
            }

            const active = e => {
                if (!popover.isDisabled()) {
                    popover.popover = bootstrap.Popover.getInstance(popover.toggleElement);
                    if (!popover.popover) {
                        popover.popover = new bootstrap.Popover(popover.toggleElement)
                        hackPopover(popover.popover, popover.class)
                        if (popover.initCallback) {
                            popover.initCallback();
                        }
                        popover.popover.toggle()
                    }
                }

                if (popover.clickToggle) {
                    popover.clickToggle(e)
                }
            }

            const closePopover = e => {
                const selector = `.${popover.class}.show`;
                const el = e.target;
                if (el.closest(selector)) {
                    return;
                }
                const owner = getDescribedElement(el.closest(popover.toggleClass));
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
            EventHandler.on(el, 'click', popover.toggleClass, active)
            EventHandler.on(popover.toggleMenu, 'click', '.dropdown-item', e => {
                if (popover.popover._config.autoClose !== 'outside') {
                    popover.hide()
                }
            })

            if (!window.bb_dropdown) {
                window.bb_dropdown = true

                EventHandler.on(document, 'click', closePopover);
            }

            // update handler
            if (popover.toggleMenu) {
                const observer = new ResizeObserver(() => {
                    popover.popover && popover.popover.update()
                });
                observer.observe(popover.toggleMenu)
                popover.observer = observer
            }

        }
        else {
            const show = e => {
                if (popover.isDisabled()) {
                    e.preventDefault()
                }
                popover.setCustomClass()
            }

            EventHandler.on(el, 'show.bs.dropdown', show)
        }

        return popover
    },

    dispose(popover) {
        if (popover.isPopover) {
            popover.observer.disconnect()
            delete popover.observer

            if (popover.popover) {
                popover.popover.dispose()
                delete popover.popover
            }
            EventHandler.off(popover.el, 'show.bs.popover')
            EventHandler.off(popover.el, 'inserted.bs.popover')
            EventHandler.off(popover.el, 'hide.bs.popover')
            EventHandler.off(popover.el, 'click', '.dropdown-toggle')
            EventHandler.off(popover.toggleMenu, 'click', '.dropdown-item')
        }
        else {
            EventHandler.off(popover.el, 'show.bs.dropdown')
        }
    }
}

export default Popover
