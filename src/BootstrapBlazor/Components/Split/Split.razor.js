import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, method, option) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    let splitWidth = el.offsetWidth
    let splitHeight = el.offsetHeight
    let curVal = 0
    let newVal = 0
    let originX = 0
    let originY = 0
    const isVertical = el.classList.contains('is-vertical')
    const splitLeft = el.children[0];
    const splitRight = el.children[1];
    const splitBar = el.children[2];

    const split = { el, invoke, method, option, splitLeft, splitRight, splitBar, isVertical };
    Data.set(id, split)
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

            const min = getMin(split);
            let max = getMax(split);

            if (min + 0.01 < 100 - max) {
                max = 100;
            }
            if (newVal <= min) newVal = min
            if (newVal >= max) newVal = max

            splitLeft.style.flexBasis = `${newVal}%`
        },
        () => {
            el.classList.remove('dragging');
            delete option.restoreLeftBasis;
            removeMask(splitLeft, splitRight);
            invoke.invokeMethodAsync(method, splitLeft.style.flexBasis);
        }
    );

    split.initCollapseButton = () => {
        let start = 0;
        const step = ts => {
            if (start === 0) {
                start = ts;
            }
            if (ts - start > 300) {
                splitLeft.classList.remove('is-collapsed');
            }
            requestAnimationFrame(step);
        }

        [...splitBar.querySelectorAll('.split-bar-arrow')].forEach(element => {
            EventHandler.on(element, 'mousedown', e => {
                e.stopPropagation();
                splitLeft.classList.add('is-collapsed');
                const triggerLeft = element.classList.contains("split-bar-arrow-left");
                setLeftBasis(split, triggerLeft);
                start = 0;
                requestAnimationFrame(step);
            });
        });
    };

    const basis = parseFloat(splitLeft.style.flexBasis);
    const min = getMin(split);
    if (basis < min) {
        splitLeft.style.flexBasis = `${min}%`;
    }
    split.initCollapseButton();
}

export function update(id) {
    const split = Data.get(id)

    if (split) {
        const { splitBar, initCollapseButton } = split;
        if (splitBar) {
            disposeCollapseButton(splitBar);
            initCollapseButton();
        }
    }
}

const convertToPercent = (split, minValue) => {
    let ret = 0;
    const { el, isVertical } = split;
    let d = createVirtualDom();
    if (isVertical) {
        d.style.setProperty('height', minValue);
    }
    else {
        d.style.setProperty('width', minValue);
    }
    el.appendChild(d);
    if (isVertical) {
        ret = d.offsetHeight * 100 / el.offsetHeight;
    }
    else {
        ret = d.offsetWidth * 100 / el.offsetWidth;
    }
    d.remove();
    d = null;
    return ret;
}

const createVirtualDom = () => {
    const d = document.createElement('div');
    d.style.setProperty("position", "absolute");
    d.style.setProperty("visibility", "hidden");
    d.style.setProperty("z-index", "-10");
    d.style.setProperty("pointer-events", "none");
    return d;
}

const getMin = split => {
    let ret = 0;
    const { splitLeft } = split;
    const leftMin = splitLeft.getAttribute('data-bb-min');
    if (leftMin) {
        if (leftMin.substring(leftMin.length) === '%') {
            ret = leftMin.substring(0, leftMin.length - 1);
        }
        else {
            ret = convertToPercent(split, leftMin);
        }
    }
    return ret;
}

const getMax = split => {
    let ret = 100;
    const { splitRight } = split;
    const rightMin = splitRight.getAttribute('data-bb-min');
    if (rightMin) {
        if (rightMin.substring(rightMin.length) === '%') {
            ret = 100 - parseFloat(rightMin.substring(0, rightMin.length - 1));
        }
        else {
            ret = 100 - convertToPercent(split, rightMin);
        }
    }
    return ret;
}

const setLeftBasis = (split, triggerLeft) => {
    const { option, splitLeft, invoke, method } = split;
    let leftBasis = splitLeft.style.flexBasis;
    if (option.isKeepOriginalSize) {
        if (option.restoreLeftBasis === void 0) {
            option.restoreLeftBasis = splitLeft.style.flexBasis;
            if (triggerLeft) {
                leftBasis = '0%';
            }
            else {
                leftBasis = '100%';
            }
        }
        else {
            leftBasis = option.restoreLeftBasis;
            delete option.restoreLeftBasis;
        }
    }
    else {
        if (triggerLeft) {
            leftBasis = '0%';
        }
        else {
            leftBasis = '100%';
        }
    }
    splitLeft.style.setProperty('flex-basis', leftBasis);
    invoke.invokeMethodAsync(method, leftBasis);
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

const disposeCollapseButton = splitBar => {
    [...splitBar.querySelectorAll('.split-bar-arrow')].forEach(element => {
        EventHandler.off(element, 'mousedown');
    });
}

export function dispose(id) {
    const split = Data.get(id)
    Data.remove(id)

    if (split) {
        const { splitBar } = split;
        if (splitBar) {
            disposeCollapseButton(splitBar);
            Drag.dispose(splitBar);
        }
    }
}
