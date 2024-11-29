import Data from "../../_content/BootstrapBlazor/modules/data.js"
import Drag from "../../_content/BootstrapBlazor/modules/drag.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const navmenu = {
        navbar: document.querySelector('.navbar-toggler'),
        menu: document.querySelector('.sidebar-content'),
        bar: document.querySelector('.sidebar-body')
    }
    Data.set(id, navmenu)

    EventHandler.on(navmenu.navbar, 'click', () => {
        navmenu.menu.classList.toggle('show')
    })
    EventHandler.on(navmenu.menu, 'click', '.nav-link', e => {
        const link = e.delegateTarget
        const url = link.getAttribute('href');
        if (url !== '#') {
            navmenu.menu.classList.remove('show')
        }
    })
    let originX = 0
    let section = document.querySelector('section');
    let width = 0
    Drag.drag(navmenu.bar,
        e => {
            navmenu.bar.classList.add('drag')
            width = parseInt(getComputedStyle(section).getPropertyValue('--bb-sidebar-width'))
            originX = e.clientX || e.touches[0].clientX
        },
        e => {
            const eventX = e.clientX || (e.touches.length > 0 && e.touches[0].clientX)
            const moveX = eventX - originX
            const newWidth = width + moveX
            if (newWidth >= 250 && newWidth <= 380) {
                section.style.setProperty('--bb-sidebar-width', `${newWidth}px`)
            }
        },
        e => {
            navmenu.bar.classList.remove('drag')
        }
    )
}

export function dispose(id) {
    const data = Data.get(id);
    Data.remove(id);

    EventHandler.off(data.navbar, 'click');
    EventHandler.off(data.menu, 'click', '.nav-link');
    Drag.dispose(data.bar)
}
