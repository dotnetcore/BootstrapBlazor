import Drag from "../../modules/drag.js"
import Data from "../../modules/data.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const section = document.querySelector('.layout-side');
    let originX = 0;
    let width = 0;
    Drag.drag(layout.bar,
        e => {
            navmenu.bar.classList.add('drag')
            width = parseInt(getComputedStyle(section).getPropertyValue('--bb-layout-sidebar-width'))
            originX = e.clientX || e.touches[0].clientX
        },
        e => {
            const eventX = e.clientX || (e.touches.length > 0 && e.touches[0].clientX)
            const moveX = eventX - originX
            const newWidth = width + moveX
            if (newWidth >= 250 && newWidth <= 380) {
                section.style.setProperty('--bb-layout-sidebar-width', `${newWidth}px`)
            }
        },
        e => {
            navmenu.bar.classList.remove('drag')
        }
    )
}

export function dispose(id) {

    if (layout.bar) {
        Drag.dispose(layout.bar)
    }

}
