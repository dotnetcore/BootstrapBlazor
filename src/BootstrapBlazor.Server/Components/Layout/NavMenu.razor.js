import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const navbar = document.querySelector('.navbar-toggler');
    const menu = document.querySelector('.sidebar-content');
    Data.set(id, { navbar, menu });

    EventHandler.on(navbar, 'click', () => {
        menu.classList.toggle('show')
    })
    EventHandler.on(menu, 'click', '.nav-link', e => {
        const link = e.delegateTarget
        const url = link.getAttribute('href');
        if (url !== '#') {
            menu.classList.remove('show')
        }
    })
}

export function dispose(id) {
    const data = Data.get(id);
    Data.remove(id);

    const { navbar, menu } = data;
    EventHandler.off(navbar, 'click');
    EventHandler.off(menu, 'click', '.nav-link');
}
