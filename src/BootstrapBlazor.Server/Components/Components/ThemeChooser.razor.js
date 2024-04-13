import { insertAfter } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const themeList = el.querySelector('.theme-list')

    const chooser = { el, themeList }
    Data.set(id, chooser);

    EventHandler.on(el, 'click', () => {
        themeList.classList.toggle('is-open')
    })
}

export function updateTheme(args) {
    const links = document.querySelectorAll('link')
    if (links) {
        const link = [].slice.call(links).filter(function (item) {
            const href = item.getAttribute('href')
            return href.indexOf('css/site.css') > -1
        });
        const original = link[0];
        while (original.nextElementSibling && original.nextElementSibling.nodeName === 'LINK') {
            original.nextElementSibling.remove()
        }

        args.forEach(function (c) {
            const link = document.createElement('link')
            link.setAttribute('rel', 'stylesheet')
            link.setAttribute('href', c)

            insertAfter(original, link)
        });
    }
}

export function dispose(id) {
    const chooser = Data.get(id)
    Data.remove(id)

    if (chooser) {
        EventHandler.off(chooser.el, 'click')
    }
}
