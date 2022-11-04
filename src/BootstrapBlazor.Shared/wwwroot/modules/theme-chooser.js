import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"
import { insertAfter } from "../../../_content/BootstrapBlazor/modules/base/utility.js"

export class ThemeChooser extends BlazorComponent {
    _init() {
        this._themeList = this._element.querySelector('.theme-list')

        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', () => {
            this._themeList.classList.toggle('is-open')
        })
    }

    _execute(args) {
        const links = document.querySelectorAll('link')
        if (links) {
            const link = [].slice.call(links).filter(function (item) {
                const href = item.getAttribute('href')
                return href.indexOf('_content/BootstrapBlazor.Shared/css/site.css') > -1
            });
            const original = link[0]
            while (original.nextElementSibling && original.nextElementSibling.nodeName === 'LINK') {
                original.nextElementSibling.remove()
            }

            args[0].forEach(function (c) {
                const link = document.createElement('link')
                link.setAttribute('rel', 'stylesheet')
                link.setAttribute('href', c)

                insertAfter(original, link)
            });
        }
    }

    _dispose() {
        EventHandler.off(this._element, 'click')
    }
}
