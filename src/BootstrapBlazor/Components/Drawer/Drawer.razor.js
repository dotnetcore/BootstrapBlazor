import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"
import { attachOverlay } from "../../modules/fullscreen.js"

const restoreBodyScroll = (el, body) => {
    const hasOpenDrawer = [...document.querySelectorAll('.drawer.show')]
        .some(drawer => drawer !== el && drawer.getAttribute('data-bb-scroll') !== 'true');
    if (!hasOpenDrawer) {
        body.classList.remove('drawer-overflow-hidden');
    }
}

const initDrag = el => {
    let originX = 0
    let originY = 0;
    let width = 0
    let height = 0;
    let isVertical = false;
    const drawerBody = el.querySelector('.drawer-body');
    if (drawerBody === null) {
        return;
    }
    const bar = [...drawerBody.children].find(i => i.classList.contains('drawer-bar'));
    if (bar === null) {
        return;
    }

    Drag.drag(bar,
        e => {
            isVertical = drawerBody.classList.contains("top") || drawerBody.classList.contains("bottom")
            bar.classList.add('drag')
            if (isVertical) {
                height = drawerBody.getBoundingClientRect().height
                originY = e.clientY || e.touches[0].clientY
            }
            else {
                width = drawerBody.getBoundingClientRect().width
                originX = e.clientX || e.touches[0].clientX
            }
        },
        e => {
            if (isVertical) {
                const eventY = e.clientY || (e.touches.length || e.touches.length > 0 && e.touches[0].clientY)
                const moveY = eventY - originY
                let newHeight = 0;
                if (drawerBody.classList.contains("bottom")) {
                    newHeight = height - moveY
                }
                else {
                    newHeight = height + moveY
                }
                const maxHeight = window.innerHeight;
                if (newHeight > 100 && newHeight < maxHeight) {
                    drawerBody.style.setProperty('--bb-drawer-height', `${newHeight}px`)
                }
            }
            else {
                const eventX = e.clientX || (e.touches.length || e.touches.length > 0 && e.touches[0].clientX)
                const moveX = eventX - originX
                let newWidth = 0;
                if (drawerBody.classList.contains("right")) {
                    newWidth = width - moveX
                }
                else {
                    newWidth = width + moveX
                }
                const maxWidth = window.innerWidth;
                if (newWidth > 100 && newWidth < maxWidth) {
                    drawerBody.style.setProperty('--bb-drawer-width', `${newWidth}px`)
                }
            }
        },
        e => {
            bar.classList.remove('drag')
        }
    )
}

export function init(id, invoke, method) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const dw = {
        el,
        body: document.querySelector('body'),
        detachOverlay: el.hasAttribute('data-bb-drawer-service') ? attachOverlay(el) : null,
        open: null,
        animationId: null
    }
    Data.set(id, dw)

    initDrag(el);

    EventHandler.on(el, 'keyup', async e => {
        if (e.key === 'Escape') {
            const supportESC = el.getAttribute('data-bb-keyboard') === 'true';
            if (supportESC) {
                await invoke.invokeMethodAsync(method);
            }
        }
    });
}

export function execute(id, open) {
    const dw = Data.get(id)
    const { el, body } = dw
    const drawerBody = el.querySelector('.drawer-body')
    const drawerBackdrop = el.querySelector('.drawer-backdrop')
    const animationFrame = getComputedStyle(drawerBody).getPropertyValue('transition') !== 'none';

    if (open) {
        const scroll = el.getAttribute('data-bb-scroll') === "true";
        if (scroll === false) {
            body.classList.add('drawer-overflow-hidden');
        }
        else {
            restoreBodyScroll(el, body);
        }
    }

    // Renders in the same state must not restart a pending animation.
    if (dw.animationId && dw.open === open) {
        return animationFrame;
    }
    dw.open = open;

    if (dw.animationId) {
        cancelAnimationFrame(dw.animationId);
        dw.animationId = null;
    }

    let start = void 0
    const show = ts => {
        if (start === void 0) {
            start = ts
        }
        const elapsed = ts - start;
        if (elapsed < 20) {
            dw.animationId = requestAnimationFrame(show);
        }
        else {
            showDrawer();
        }
    }

    const showDrawer = () => {
        dw.animationId = null;
        drawerBody.classList.add('show');
        const backdrop = el.querySelector('.drawer-backdrop');
        if (backdrop) {
            backdrop.classList.add('show');
        }
        el.focus();
    }

    const hide = ts => {
        if (start === void 0) {
            start = ts
        }
        const elapsed = ts - start;
        if (elapsed < 320) {
            dw.animationId = requestAnimationFrame(hide);
        }
        else {
            dw.animationId = null;
            el.classList.remove('show');
            restoreBodyScroll(el, body);
        }
    }

    if (open) {
        el.classList.add('show')

        if (animationFrame) {
            dw.animationId = requestAnimationFrame(show)
        }
        else {
            showDrawer();
        }
    }
    else if (el.classList.contains('show')) {
        drawerBody.classList.remove('show');
        if (drawerBackdrop) {
            drawerBackdrop.classList.remove('show');
        }
        restoreBodyScroll(el, body);
        if (animationFrame) {
            dw.animationId = requestAnimationFrame(hide)
        }
        else {
            el.classList.remove('show');
        }
    }
    return animationFrame;
}

export function dispose(id) {
    const dw = Data.get(id)
    Data.remove(id);

    if (!dw) {
        return;
    }

    const { el, body } = dw;
    if (dw.animationId) {
        cancelAnimationFrame(dw.animationId);
    }
    EventHandler.off(el, 'keyup');

    if (el.classList.contains('show')) {
        el.classList.remove('show')

        const drawerBody = el.querySelector('.drawer-body')
        if (drawerBody) {
            drawerBody.classList.remove('show')
        }
    }
    restoreBodyScroll(el, body);
    dw.detachOverlay?.();

    const drawerBody = el.querySelector('.drawer-body');
    if (drawerBody !== null) {
        const bar = [...drawerBody.children].find(i => i.classList.contains('drawer-bar'));
        if (bar) {
            Drag.dispose(bar)
        }
    }
}
