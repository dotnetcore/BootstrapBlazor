import Data from "../../modules/data.js"
import { attachOverlay } from "../../modules/fullscreen.js"

export function update(id, options) {
    const mask = document.getElementById(id);
    if (mask) {
        const { show, appendToBody } = options;
        const state = Data.get(id);
        const el = state?.el ?? document.querySelector(`[data-bb-mask="${id}"]`);
        if (!el) {
            return;
        }

        if (!show) {
            dispose(id);
            el.classList.remove('show', 'bb-mask-viewport');
            mask.appendChild(el);
            return;
        }

        const container = getContainerBySelector(options);
        if (container) {
            const position = container.style.getPropertyValue('position');
            if (position === '' || position === 'static') {
                container.style.setProperty('position', 'relative');
            }
        }

        const target = container ?? (appendToBody === true ? document.body : null);
        if (state?.container !== target) {
            dispose(id);
            el.classList.remove('show', 'bb-mask-viewport');
            if (target) {
                target.appendChild(el);
                el.classList.add('show');
                const viewport = target === document.body;
                el.classList.toggle('bb-mask-viewport', viewport);
                Data.set(id, { el, container: target, detachOverlay: viewport ? attachOverlay(el) : null });
            }
            else {
                mask.appendChild(el);
            }
        }
    }
}

export function dispose(id) {
    const state = Data.get(id);
    if (state) {
        state.detachOverlay?.();
        state.el.remove();
        Data.remove(id);
    }
}

const getContainerBySelector = options => {
    const selector = getContainerById(options.containerId) ?? options.selector;
    return selector ? document.querySelector(selector) : null;
}

const getContainerById = id => id ? `#${id}` : null;
