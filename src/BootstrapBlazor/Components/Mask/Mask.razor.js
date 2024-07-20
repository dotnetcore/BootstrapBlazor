export function update(id, options) {
    console.log(id, options);

    const mask = document.getElementById(id);
    if (mask) {
        const { show, containerId } = options;
        if (containerId) {
            const container = document.getElementById(containerId);
            if (container) {
                const position = container.style.getPropertyValue('position');
                if (position === '' || position === 'static') {
                    container.style.setProperty('position', 'relative');
                }

                if (show) {
                    mask.style.setProperty('--bb-mask-position', 'absolute');
                    container.appendChild(mask);
                }
            }
        }

        if (show) {
            mask.classList.add('show');
        }
        else {
            mask.classList.remove('show');
            mask.style.removeProperty('--bb-mask-position');
            if (mask.parentElement !== document.body) {
                document.body.appendChild(mask);
            }
        }
    }
}
