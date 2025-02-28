import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const navmenu = {
        navbar: document.querySelector('.navbar-toggler'),
        menu: document.querySelector('.sidebar-content')
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
}

export function dispose(id) {
    const data = Data.get(id);
    Data.remove(id);

    EventHandler.off(data.navbar, 'click');
    EventHandler.off(data.menu, 'click', '.nav-link');
}
