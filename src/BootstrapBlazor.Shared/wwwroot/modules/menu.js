import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"

export class Menu extends BlazorComponent {
    _init() {
        this._navbar = document.querySelector('.navbar-toggler')
        this._menu = document.querySelector('.sidebar-content')

        EventHandler.on(this._navbar, 'click', () => {
            this._menu.classList.toggle('show')
        })
        EventHandler.on(this._menu, 'click', '.nav-link', e => {
            const link = e.delegateTarget
            const url = link.getAttribute('href');
            if (url !== '#') {
                this._menu.classList.remove('show')
            }
        })
    }

    _dispose() {
        EventHandler.off(this._navbar, 'click');
        EventHandler.off(this._menu, 'click', '.nav-link');
    }
}
