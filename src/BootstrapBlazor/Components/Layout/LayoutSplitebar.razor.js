import Drag from "../../modules/drag.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const min = parseFloat(el.getAttribute("data-bb-min") ?? "-1");
    const max = parseFloat(el.getAttribute("data-bb-max") ?? "-1");
    const selector = el.getAttribute("data-bb-selector") ?? ".layout";
    const section = document.querySelector(selector);
    const bar = el.querySelector(".layout-splitebar-body");
    let originX = 0;
    let width = 0;
    Drag.drag(bar,
        e => {
            bar.classList.add('drag')
            width = parseInt(getComputedStyle(section).getPropertyValue('--bb-layout-sidebar-width'))
            originX = e.clientX || e.touches[0].clientX
        },
        e => {
            const eventX = e.clientX || (e.touches.length > 0 && e.touches[0].clientX)
            const moveX = eventX - originX
            const newWidth = width + moveX
            if (min > -1 && newWidth < min) {
                newWidth = min
            }
            if (max > -1 && newWidth > max) {
                newWidth = max
            }
            section.style.setProperty('--bb-layout-sidebar-width', `${newWidth}px`)
        },
        e => {
            bar.classList.remove('drag')
        }
    )
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el) {
        const bar = el.querySelector(".layout-splitebar-body");
        if (bar) {
            Drag.dispose(bar);
        }
    }
}
