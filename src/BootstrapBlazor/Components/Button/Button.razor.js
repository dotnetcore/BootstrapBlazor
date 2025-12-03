import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el) {
        EventHandler.on(el, 'click', e => {
            const isAsync = el.getAttribute('data-bb-async') === 'true';
            if (isAsync) {
                setTimeout(() => {
                    el.setAttribute('disabled', 'disabled');
                }, 0);
            }
        });
    }
}

const showTooltip = (id, title) => {
    const el = document.getElementById(id)

    if (el) {
        const tooltip = bootstrap.Tooltip.getOrCreateInstance(el, {
            title: title
        })
        if (tooltip._config.title !== title) {
            tooltip._config.title = title
        }
    }
}

const removeTooltip = id => {
    const el = document.getElementById(id)

    if (el) {
        const tip = bootstrap.Tooltip.getInstance(el)
        if (tip) {
            tip.dispose()
        }
    }
}

const dispose = id => {
    removeTooltip(id);

    const el = document.getElementById(id);
    if (el) {
        EventHandler.off(el, 'click');
    }
}

const share = context => {
    navigator.share(context)
}

export {
    share,
    showTooltip,
    removeTooltip,
    dispose
}
