export function update(id, options) {
    const mask = document.getElementById(id);
    if (mask) {
        const { show, appendToBody } = options;
        const el = document.querySelector(`[data-bb-mask="${id}"]`);
        const container = getContainerBySelector(options);
        if (container) {
            const position = container.style.getPropertyValue('position');
            if (position === '' || position === 'static') {
                container.style.setProperty('position', 'relative');
            }
            reset(el, mask, container, show);
        }
        else if (appendToBody === true) {
            reset(el, mask, document.body, show);
        }
    }
}

const reset = (el, mask, container, status) => {
    if (status) {
        container.appendChild(el);
        el.classList.add('show');
    }
    else {
        el.classList.remove('show');
        mask.appendChild(el);
    }
}

const getContainerBySelector = options => {
    const selector = getContainerById(options.containerId) ?? options.selector;
    return selector ? document.querySelector(selector) : null;
}

const getContainerById = id => id ? `#${id}` : null;
