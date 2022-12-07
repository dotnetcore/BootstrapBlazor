import BlazorComponent from "./base/blazor-component.js"
import { getTargetElement, getTransitionDelayDurationFromElement } from "./base/utility.js"

export class Menu extends BlazorComponent {
    _init() {
        this._collapses = this._element.querySelectorAll('[data-bs-toggle="collapse"]')
        this._activeLink()
    }

    _activeLink() {
        let activeLink = this._element.querySelector('.nav-link.active')
        if (activeLink === null) {
            const url = window.location.pathname.substring(1)
            activeLink = this._element.querySelector(`[href="${url}"]`)
            if (activeLink != null) {
                activeLink.classList.add('active')
            }
        }
        while (activeLink !== null) {
            const menu = activeLink.closest('.collapse.submenu')
            if (menu !== null && !menu.classList.contains('show')) {
                const id = menu.getAttribute('id');
                const triggerElement = this._element.querySelector(`[data-bs-toggle="collapse"][data-bs-target="#${id}"]`)
                if (triggerElement !== null) {
                    // expand menu
                    triggerElement.click()
                }
            }
            activeLink = menu
        }
    }

    _execute() {
        const expandAll = this._element.getAttribute('data-bb-expand') === 'true'
        this._collapses.forEach(el => {
            const target = getTargetElement(el)
            const collapse = bootstrap.Collapse.getInstance(target);
            if (collapse !== null) {
                if (expandAll) {
                    if (!collapse._isShown()) {
                        collapse.show()
                    }
                }
                else {
                    this._disposeCollapse(collapse, el)
                }
            }
            else if (expandAll) {
                new bootstrap.Collapse(target, {
                    toggle: true
                });
            }
        });
    }

    _disposeCollapse(collapse, target) {
        if (collapse._isShown()) {
            collapse.hide()

            const duration = getTransitionDelayDurationFromElement(target);
            const handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                collapse.dispose()
            }, duration)
        }
        else {
            collapse.dispose()
        }
    }

    _dispose() {
        this._collapses.forEach(el => {
            const target = getTargetElement(el)
            const collapse = bootstrap.Collapse.getInstance(target)
            if (collapse !== null) {
                this._disposeCollapse(collapse, el)
            }
        })
    }
}
