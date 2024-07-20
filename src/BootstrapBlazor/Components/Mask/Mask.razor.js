export function update(id, options) {
    const mask = document.getElementById(id);
    if (mask) {
        const { show, containerId } = options;
        const el = document.querySelector(`[data-bb-mask="${id}"]`);
        if (containerId) {
            const container = document.getElementById(containerId);
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
