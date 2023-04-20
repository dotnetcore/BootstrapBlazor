import EventHandler from "../../../_content/BootstrapBlazor/modules/event-handler.js"
import { insertAfter } from "../../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../../_content/BootstrapBlazor/modules/data.js"

export function init(id) {
    const element = document.getElementById(id)
    const themeList = element.querySelector('.theme-list')

    const chooser = {
        el: element,
        themeList: themeList
    }
    Data.set(id, chooser);

    EventHandler.on(chooser.el, 'click', () => {
        chooser.themeList.classList.toggle('is-open')
    })
}

export function addScript(args) {
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

        args.forEach(function (c) {
            const link = document.createElement('link')
            link.setAttribute('rel', 'stylesheet')
            link.setAttribute('href', c)

            insertAfter(original, link)
        });
    }
}

export function dispose(id) {
    const data = Data.get(id)
    EventHandler.off(data.el, 'click')
    Data.remove(id)
}
