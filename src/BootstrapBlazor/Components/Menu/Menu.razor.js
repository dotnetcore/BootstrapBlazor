import { getTargetElement, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const menu = {
        element: el,
        collapses: el.querySelectorAll('[data-bs-toggle="collapse"]'),
        activeLink: () => {
            let activeLink = menu.element.querySelector('.nav-link.active')
            if (activeLink === null) {
                const url = window.location.pathname.substring(1)
                activeLink = menu.element.querySelector(`[href="${url}"]`)
                if (activeLink != null) {
                    activeLink.classList.add('active')
                }
            }
            while (activeLink !== null) {
                const menu = activeLink.closest('.collapse.submenu')
                if (menu !== null && !menu.classList.contains('show')) {
                    const id = menu.getAttribute('id');
                    const triggerElement = menu.element.querySelector(`[data-bs-toggle="collapse"][data-bs-target="#${id}"]`)
                    if (triggerElement !== null) {
                        // expand menu
                        triggerElement.click()
                    }
                }
                activeLink = menu
            }
        },
        disposeCollapse: (collapse, target) => {
            if (collapse._isShown()) {
                collapse.hide()

                const duration = getTransitionDelayDurationFromElement(target);
                const handler = setTimeout(() => {
                    clearTimeout(handler)
                    collapse.dispose()
                }, duration)
            }
            else {
                collapse.dispose()
            }
        }
    }
    Data.set(id, menu)

    scrollElementToView(menu.element)
}

export function update(id) {
    const menu = Data.get(id)
    const expandAll = menu.element.getAttribute('data-bb-expand') === 'true'
    menu.collapses.forEach(el => {
        const target = getTargetElement(el)
        const collapse = bootstrap.Collapse.getInstance(target);
        if (collapse !== null) {
            if (expandAll) {
                if (!collapse._isShown()) {
                    collapse.show()
                }
            }
            else {
                menu.disposeCollapse(collapse, el)
            }
        }
        else if (expandAll) {
            new bootstrap.Collapse(target, {
                toggle: true
            });
        }
    });

    scrollElementToView(menu.element)
}

export function scrollToView(id) {
    const menu = Data.get(id)
    if (menu) {
        scrollElementToView(menu.element);
    }
}

export function dispose(id) {
    const menu = Data.get(id)
    Data.remove(id)

    menu.collapses.forEach(el => {
        const target = getTargetElement(el)
        const collapse = bootstrap.Collapse.getInstance(target)
        if (collapse !== null) {
            menu.disposeCollapse(collapse, el)
        }
    })
}

const scrollElementToView = element => {
    const scroll = element.hasAttribute('data-bb-scroll-view')
    if (scroll) {
        var links = [...element.querySelectorAll('.nav-link.active')]
        if (links.length > 0) {
            var link = links.pop()
            link.scrollIntoView({ behavior: 'smooth', block: 'center', inline: 'start' });
        }
    }
}
