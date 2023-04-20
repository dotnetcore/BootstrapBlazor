import EventHandler from "../../../_content/BootstrapBlazor/modules/event-handler.js"
import Data from "../../../_content/BootstrapBlazor/modules/data.js"

export function init(id, version) {
    const update = {
        el: document.getElementById(id),
        key: `bb_intro_popup:${version}`,
    }
    Data.set(id, update)

    check(update.key, update.el);
    EventHandler.on(update.el, 'click', '.blazor-intro-button', () => {
        close(update.key, update.el)
    })
}

function check(key, el) {
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

function slideToggle(el) {
    el.classList.toggle('show')
}

function close(key, el) {
    localStorage.setItem(key, 'false')
    slideToggle(el)
}

export function dispose(id) {
    const data = Data.get(id)
    EventHandler.off(data.el, 'click', '.blazor-intro-button');
    Data.remove(id)
}

