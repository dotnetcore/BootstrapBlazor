import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

const initDrag = el => {
    let originX = 0
    let originY = 0;
    let width = 0
    let height = 0;
    let isVertical = false;
    const drawerBody = el.querySelector('.drawer-body');
    const bar = el.querySelector('.drawer-bar');
    Drag.drag(bar,
        e => {
            isVertical = drawerBody.classList.contains("top") || drawerBody.classList.contains("bottom")
            bar.classList.add('drag')
            if (isVertical) {
                height = parseInt(getComputedStyle(drawerBody).getPropertyValue('--bb-drawer-height'))
                originY = e.clientY || e.touches[0].clientY
            }
            else {
                width = parseInt(getComputedStyle(drawerBody).getPropertyValue('--bb-drawer-width'))
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
        body: document.querySelector('body')
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

    let start = void 0
    const show = ts => {
        if (start === void 0) {
            start = ts
        }
        const elapsed = ts - start;
        if (elapsed < 20) {
            requestAnimationFrame(show);
        }
        else {
            showDrawer();
        }
    }
    
    const showDrawer = () => {
        drawerBody.classList.add('show');
        if (drawerBackdrop) {
            drawerBackdrop.classList.add('show');
        }
        el.focus();
    }

    const hide = ts => {
        if (start === void 0) {
            start = ts
        }
        const elapsed = ts - start;
        if (elapsed < 320) {
            requestAnimationFrame(hide);
        }
        else {
            el.classList.remove('show');
        }
    }

    if (open) {
        el.classList.add('show')

        const scroll = el.getAttribute('data-bb-scroll') === "true";
        if (scroll === false) {
            body.classList.add('drawer-overflow-hidden');
        }

        if (animationFrame) {
            requestAnimationFrame(show)
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
        body.classList.remove('drawer-overflow-hidden');
        if (animationFrame) {
            requestAnimationFrame(hide)
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

    const { el, body } = dw;
    EventHandler.off(el, 'keyup');

    if (el.classList.contains('show')) {
        el.classList.remove('show')

        const drawerBody = el.querySelector('.drawer-body')
        if (drawerBody) {
            drawerBody.classList.remove('show')
        }
        body.classList.remove('overflow-hidden')
    }

    const bar = el.querySelector('.drawer-bar');
    if (bar) {
        Drag.dispose(bar)
    }
}
