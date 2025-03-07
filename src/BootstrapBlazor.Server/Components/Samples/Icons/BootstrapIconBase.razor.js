import Data from '../../../_content/BootstrapBlazor/modules/data.js';
import EventHandler from '../../../_content/BootstrapBlazor/modules/event-handler.js';
import { copy } from '../../../_content/BootstrapBlazor/modules/utility.js';

export function init(id) {
    const el = document.getElementById(id);
    const tooltips = [];
    [...el.querySelectorAll('svg')].forEach(s => {
        const use = s.querySelector('use');
        const url = use.getAttribute('href');
        const segs = url.split('#');
        if (segs.length == 2) {
            const title = segs[1];
            s.parentElement.setAttribute('data-bs-original-title', title);
            tooltips.push(new bootstrap.Tooltip(s.parentElement));
        }
    });
    EventHandler.on(el, 'click', 'div', e => {
        const div = e.delegateTarget;
        const href = div.querySelector('use').getAttribute('href');
        const text = `<svg xmlns="http://www.w3.org/2000/svg"><use href="${href}"></use></svg>`;
        copy(text);
        const tooltip = bootstrap.Tooltip.getInstance(div);
        tooltip.setContent({ '.tooltip-inner': 'Copy' });
        const handler = setTimeout(() => {
            clearTimeout(handler);
            tooltip.setContent({
                '.tooltip-inner': div.getAttribute('data-bs-original-title')
            });
        }, 1000);
    });
    Data.set(id, {
        el,
        tooltips
    })
}

export function dispose(id) {
    const data = Data.get(id);
    Data.remove(id);

    const { el, tooltips } = data;
    EventHandler.off(el, 'click');
    tooltips.forEach(t => t.dispose());
}
