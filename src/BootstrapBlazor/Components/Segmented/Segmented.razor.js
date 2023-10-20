import Data from '../../modules/data.js?v=$version'
import EventHandler from '../../modules/event-handler.js?v=$version'

export function init(id, invoke) {
    const el = document.getElementById(id);
    Data.set(id, { el, invoke });
    EventHandler.on(el, 'click', '.segmented-item', e => {
        move(el, e.delegateTarget);
    });
}

export function dispose(id) {
    const seg = Data.get(id);
    Data.remove(id);

    if (seg) {
        EventHandler.off(seg.el, 'click');
    }
}

const move = (el, item) => {
    const selectedItem = el.querySelector('.selected');
    if (selectedItem === null) {
        return;
    }
    selectedItem.classList.remove('selected');

    const mask = el.querySelector('.mask')
    mask.style.setProperty('width', `${selectedItem.offsetWidth}px`);
    mask.style.setProperty('transform', `translateX(${selectedItem.offsetLeft}px)`);

    const handler = setTimeout(() => {
        clearTimeout(handler);
        mask.style.setProperty('width', `${item.offsetWidth}px`);
        mask.style.setProperty('height', `${item.offsetHeight}px`);
        mask.style.setProperty('transform', `translateX(${item.offsetLeft}px)`);
        mask.style.setProperty('visibility', 'visible');
        requestAnimationFrame(step);
    }, 0);

    let start = void 0;
    const step = ts => {
        if (start === void 0) {
            start = ts
        }
        const elapsed = ts - start;
        if (elapsed < 300) {
            requestAnimationFrame(step);
        }
        else {
            item.classList.add('selected');
            mask.style.removeProperty('visibility');
        }
    }
}
