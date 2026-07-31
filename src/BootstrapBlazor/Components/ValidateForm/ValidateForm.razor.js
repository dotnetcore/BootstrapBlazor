import EventHandler from "../../modules/event-handler.js"
import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id)
    Data.set(id, {
        el
    });

    const dissubmit = el.getAttribute('data-bb-dissubmit') === 'true';
    if (dissubmit) {
        bind(el);
    }
}

export function update(id) {
    const form = Data.get(id);
    if (form) {
        const el = document.getElementById(id);

        if (el === form.el) {
            return;
        }
    }

    dispose(id);
    init(id);
}

export function dispose(id) {
    const form = Data.get(id);
    Data.remove(id);

    if (form) {
        unbind(form.el);
    }
}

const unbind = el => {
    EventHandler.off(el, 'keydown');
}

const bind = el => {
    EventHandler.on(el, 'keydown', e => {
        if (e.key === 'Enter' && e.target.nodeName !== 'TEXTAREA') {
            e.preventDefault()
            e.stopPropagation()
        }
    });
}
