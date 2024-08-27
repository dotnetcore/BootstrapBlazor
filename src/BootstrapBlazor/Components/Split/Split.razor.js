import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const split = { el }
    Data.set(id, split)

    let splitWidth = el.offsetWidth
    let splitHeight = el.offsetHeight
    let curVal = 0
    let newVal = 0
    let originX = 0
    let originY = 0
    const isVertical = el.classList.contains('is-vertical')
    const splitBar = el.querySelector('.split-bar');
    const splitLeft = el.querySelector('.split-left');
    const splitRight = el.querySelector('.split-right');

    split.splitBar = splitBar;
    Drag.drag(splitBar,
        e => {
            splitWidth = el.offsetWidth
            splitHeight = el.offsetHeight
            if (isVertical) {
                originY = e.clientY || e.touches[0].clientY
                curVal = splitLeft.offsetHeight * 100 / splitHeight
            }
            else {
                originX = e.clientX || e.touches[0].clientX
                curVal = splitLeft.offsetWidth * 100 / splitWidth
            }
            el.classList.add('dragging');
            showMask(splitLeft, splitRight);
        },
        e => {
            if (isVertical) {
                const eventY = e.clientY || e.changedTouches[0].clientY
                newVal = Math.ceil((eventY - originY) * 100 / splitHeight) + curVal
            }
            else {
                const eventX = e.clientX || e.changedTouches[0].clientX
                newVal = Math.ceil((eventX - originX) * 100 / splitWidth) + curVal
            }

            if (newVal <= 0) newVal = 0
            if (newVal >= 100) newVal = 100

            splitLeft.style.flexBasis = `${newVal}%`
            splitRight.style.flexBasis = `${100 - newVal}%`
        },
        () => {
            el.classList.remove('dragging');
            removeMask(splitLeft, splitRight);
        }
    );
    EventHandler.on(el, 'click', '.split-bar-arrow', e => {

    });
}

const showMask = (left, right) => {
    const div = document.createElement('div');
    div.style = "position: absolute; inset: 0; opacity: 0; z-index: 5;";
    div.className = "split-mask";

    left.appendChild(div);
    right.appendChild(div.cloneNode(true));
}

const removeMask = (left, right) => {
    deleteMask(left);
    deleteMask(right);
}

const deleteMask = el => {
    let mask = el.querySelector('.split-mask');
    if (mask) {
        mask.remove();
        mask = null;
    }
}

export function dispose(id) {
    const split = Data.get(id)
    Data.remove(id)

    if (split) {
        EventHandler.off(el, 'click', '.split-bar-arrow');
        const { el } = split;
        if (el.splitBar) {
            Drag.dispose(el.splitBar);
        }
    }
}
