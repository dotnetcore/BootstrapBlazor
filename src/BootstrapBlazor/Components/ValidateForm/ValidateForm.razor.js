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

export function update(id, invalidIds) {
    const el = document.getElementById(id);
    const items = [...el.children];
    const invalidElements = invalidIds.map(cId => {
        const item = document.getElementById(cId);
        let parentEl = item.parentElement;
        while (parentEl !== el) {
            parentEl = parentEl.parentElement;
        }
        return {
            item, order: items.indexOf(item)
        };
    });
    invalidElements.sort((a, b) => b.order - a.order);

    const invalid = invalidElements.pop();
    if (invalid) {
        const handler = setInterval(() => {
            const tip = bootstrap.Tooltip.getInstance(invalid.item)
            if (tip) {
                clearInterval(handler);
                if (!tip._isShown()) {
                    tip.show();
                }
            }
        }, 20);
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    EventHandler.off(el, 'keydown')
}
