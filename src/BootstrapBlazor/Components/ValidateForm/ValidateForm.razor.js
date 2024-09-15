import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)

    EventHandler.on(el, 'keydown', e => {
        if (e.target.nodeName !== 'TEXTAREA') {
            const dissubmit = el.getAttribute('data-bb-dissubmit') === 'true'
            if (e.keyCode === 13 && dissubmit) {
                e.preventDefault()
                e.stopPropagation()
            }
        }
    })
}

export function update(id, eId, title) {
    const el = document.getElementById(eId);
    if (el) {
        const tip = bootstrap.Tooltip.getOrCreateInstance(el, { customClass: 'is-invalid', title })
        if (title !== tip._config.title) {
            tip._config.title = title;
        }

        if (!tip._isShown()) {
            tip.show();
        }
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    EventHandler.off(el, 'keydown')
}
