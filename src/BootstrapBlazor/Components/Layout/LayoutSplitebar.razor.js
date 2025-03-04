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
    if (section === null) {
        log.warning(`LayoutSplitebar: selector ${selector} not found`);
        return;
    }

    const bar = el.querySelector(".layout-splitebar-body");
    let originX = 0;
    let width = 0;
    Drag.drag(bar,
        e => {
            section.classList.add('drag');
            const widthString = getComputedStyle(section).getPropertyValue('--bb-layout-sidebar-width');
            if (widthString === '') {
                section.style.setProperty('--bb-layout-sidebar-width', '0');
                widthString = '0';
            }
            width = parseInt(widthString);
            originX = e.clientX || e.touches[0].clientX;
        },
        e => {
            const eventX = e.clientX || (e.touches.length > 0 && e.touches[0].clientX)
            const moveX = eventX - originX
            let newWidth = width + moveX
            if (min > -1 && newWidth < min) {
                newWidth = min
            }
            if (max > -1 && newWidth > max) {
                newWidth = max
            }
            section.style.setProperty('--bb-layout-sidebar-width', `${newWidth}px`)
        },
        e => {
            section.classList.remove('drag')
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
