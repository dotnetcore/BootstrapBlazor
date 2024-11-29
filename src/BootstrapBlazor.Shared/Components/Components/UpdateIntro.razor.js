import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id, version) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const update = {
        el,
        key: `bb_intro_popup:${version}`,
    }
    Data.set(id, update)

    check(update.key, update.el);
    EventHandler.on(el, 'click', '.blazor-intro-button', () => {
        close(update.key, el)
    })
}

export function dispose(id) {
    Data.remove(id)

    const data = Data.get(id)
    if (data) {
        EventHandler.off(data.el, 'click', '.blazor-intro-button');
    }
}

const check = (key, el) => {
    const width = window.innerWidth
    if (width >= 768) {
        const isShown = localStorage.getItem(key)
        if (!isShown) {
            slideToggle(el)

            // clean
            for (let index = localStorage.length; index > 0; index--) {
                const k = localStorage.key(index - 1);
                if (k.indexOf('bb_intro_popup:') > -1) {
                    localStorage.removeItem(k);
                }
            }
        }
    }
}

const slideToggle = el => {
    el.classList.toggle('show')
}

const close = (key, el) => {
    localStorage.setItem(key, 'false')
    slideToggle(el)
}
