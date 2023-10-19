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
    console.log();
    const selectedItem = el.querySelector('.selected');
    if (selectedItem) {
        selectedItem.classList.remove('selected');
    }

    let mask = el.querySelector('.segmented-item-mask')
    if (mask === null) {
        mask = document.createElement('div');
        mask.classList.add('segmented-item-mask');
        el.insertBefore(mask, el.children[0]);
    }
    mask.style.setProperty('width', `${selectedItem.offsetWidth}px`);
    mask.style.setProperty('transform', `translateX(${selectedItem.offsetLeft}px)`);

    const handler = setTimeout(() => {
        clearTimeout(handler);
        mask.style.setProperty('width', `${item.offsetWidth}px`);
        mask.style.setProperty('transform', `translateX(${item.offsetLeft}px)`);
    }, 0);
}
