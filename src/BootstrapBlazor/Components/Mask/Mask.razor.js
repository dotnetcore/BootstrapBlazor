export function update(id, options) {
    const mask = document.getElementById(id);
    if (mask) {
        const { show } = options;
        const el = document.querySelector(`[data-bb-mask="${id}"]`);
        const container = getContainerBySelector(options);
        if (container) {
            const position = container.style.getPropertyValue('position');
            if (position === '' || position === 'static') {
                container.style.setProperty('position', 'relative');
            }
            if (show) {
                el.style.setProperty('--bb-mask-position', 'absolute');
                container.appendChild(el);
            }
        }
        else {
            document.body.appendChild(el);
        }

        if (show) {
            el.classList.add('show');
        }
        else {
            el.classList.remove('show');
            el.style.removeProperty('--bb-mask-position');
            mask.appendChild(el);
        }
    }
}

const getContainerBySelector = options => {
    const selector = getContainerById(options.containerId) ?? options.selector;
    return selector ? document.querySelector(selector) : null;
}

const getContainerById = id => id ? `#${id}` : null;
