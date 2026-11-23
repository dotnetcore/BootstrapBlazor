import { getOverflowParent, getTargetElement, getTransitionDelayDurationFromElement } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import MenuPopup from "../../modules/menu-popup.js"

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

    MenuPopup.refresh(menu)
    scrollElementToView(menu)
}

export function prepare(id, collapsed) {
    const menu = Data.get(id)
    if (menu) {
        if (collapsed) {
            menu.expandedMenus = new Map([...menu.collapses].map(el => {
                const target = getTargetElement(el)
                return [getMenuKey(menu, target), target.classList.contains('show') || el.getAttribute('aria-expanded') === 'true']
            }))
            menu.expandedLocation = window.location.pathname
            menu.collapses.forEach(el => {
                const target = getTargetElement(el)
                if (target.classList.contains('collapsing')) {
                    target.dispatchEvent(new Event('transitionend'))
                }
                bootstrap.Collapse.getInstance(target)?.dispose()
            })
        }
        MenuPopup.dispose(menu)
    }
}

export function refresh(id) {
    const menu = Data.get(id)
    if (menu) {
        menu.collapses = menu.element.querySelectorAll('[data-bs-toggle="collapse"]')
        MenuPopup.refresh(menu)
        if (!menu.element.classList.contains('is-collapsed') && menu.expandedMenus) {
            const expandAll = menu.element.getAttribute('data-bb-expand') === 'true'
            const locationChanged = menu.expandedLocation !== window.location.pathname
            menu.collapses.forEach(el => {
                const target = getTargetElement(el)
                const expanded = expandAll || locationChanged && target.querySelector('.nav-link.active') !== null
                    || menu.expandedMenus.get(getMenuKey(menu, target))
                if (expanded !== void 0) {
                    target.classList.toggle('show', expanded)
                    el.setAttribute('aria-expanded', expanded ? 'true' : 'false')
                }
            })
            delete menu.expandedMenus
            delete menu.expandedLocation
        }
    }
}

const getMenuKey = (menu, target) => {
    const prefix = `${menu.element.id}_`
    return target.id.startsWith(prefix) ? target.id.substring(prefix.length) : target.id
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

    scrollElementToView(menu)
}

export function scrollToView(id) {
    const menu = Data.get(id)
    if (menu) {
        scrollElementToView(menu);
    }
}

export function dispose(id) {
    const menu = Data.get(id)
    Data.remove(id);

    if (!menu) {
        return
    }
    MenuPopup.dispose(menu)

    if (menu.handler) {
        clearInterval(menu.handler);
        menu.handler = null;
    }

    menu.collapses.forEach(el => {
        const target = getTargetElement(el)
        const collapse = bootstrap.Collapse.getInstance(target)
        if (collapse !== null) {
            menu.disposeCollapse(collapse, el)
        }
    })
}

const scrollElementToView = menu => {
    const { element } = menu;

    if (menu.handler) {
        clearInterval(menu.handler);
        menu.handler = null;
    }

    if (!element.hasAttribute('data-bb-scroll-view') || element.classList.contains('is-collapsed')) {
        return;
    }

    let attempts = 0;
    menu.handler = setInterval(() => {
        const links = element.querySelectorAll('.nav-link.active');
        const link = links[links.length - 1];
        const hasLayout = link?.getClientRects().length > 0;
        if (!hasLayout && ++attempts < 3) {
            return;
        }

        clearInterval(menu.handler);
        menu.handler = null;
        if (!hasLayout) {
            return;
        }

        const container = getOverflowParent(link);
        if (container === window || container === document.body || container === document.documentElement
            || container.scrollHeight <= container.clientHeight) {
            return;
        }

        const viewportTop = container.getBoundingClientRect().top + container.clientTop;
        const viewportBottom = viewportTop + container.clientHeight;
        const { top, bottom } = link.getBoundingClientRect();
        if (top < viewportTop || bottom > viewportBottom) {
            container.scrollBy({ top: (top + bottom - viewportTop - viewportBottom) / 2, behavior: 'smooth' });
        }
    }, 100);
}
