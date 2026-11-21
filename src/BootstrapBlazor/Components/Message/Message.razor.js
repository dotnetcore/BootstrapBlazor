import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import { attachOverlay, supportsPopover } from "../../modules/fullscreen.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    const msg = { el, invoke, items: [] }
    if (!supportsPopover(el)) {
        el.removeAttribute('popover')
    }
    msg.detachOverlay = attachOverlay(el)
    Data.set(id, msg)
}

export function show(id, msgId) {
    const el = document.getElementById(msgId)
    if (el === null) {
        return
    }

    const msg = Data.get(id)
    msg.items = msg.items.filter(item => {
        if (item.el.isConnected) {
            return true
        }
        if (item.animationId) {
            cancelAnimationFrame(item.animationId)
        }
        EventHandler.off(item.el, 'click')
        return false
    })
    let msgItem = msg.items.find(i => i.el.id === msgId)
    if (msgItem === void 0) {
        msgItem = { el, animationId: null }
        msg.items.push(msgItem)
    }

    if (msgItem.animationId) {
        return;
    }

    if (msg.el.hasAttribute('popover') && supportsPopover(msg.el) && !msg.el.matches(':popover-open')) {
        msg.el.showPopover()
    }

    const close = method => {
        if (!msg.items.includes(msgItem)) {
            return;
        }

        if (msgItem.animationId) {
            cancelAnimationFrame(msgItem.animationId);
            msgItem.animationId = null;
        }
        EventHandler.off(el, 'click');
        el.classList.add("d-none");
        msg.items = msg.items.filter(i => i !== msgItem);
        if (msg.items.length === 0 && msg.el.hasAttribute('popover') && supportsPopover(msg.el) && msg.el.matches(':popover-open')) {
            msg.el.hidePopover()
        }
        return msg.invoke.invokeMethodAsync(method, msgId);
    };

    const autoHide = el.getAttribute('data-bb-autohide') === 'true';
    if (autoHide) {
        const delay = parseInt(el.getAttribute('data-bb-delay'));
        let start = void 0
        const autoCloseAnimation = ts => {
            if (start === void 0) {
                start = ts
            }

            const elapsed = ts - start;
            if (elapsed > delay) {
                close("Clear");
            }
            else {
                msgItem.animationId = requestAnimationFrame(autoCloseAnimation);
            }
        }
        msgItem.animationId = requestAnimationFrame(autoCloseAnimation)
    }
    el.classList.add('show');

    EventHandler.on(el, 'click', '.btn-close', async e => {
        e.preventDefault();
        e.stopPropagation();

        await close('Dismiss');
    });
}

export function dispose(id) {
    const msg = Data.get(id)
    if (msg) {
        if (msg.el.hasAttribute('popover') && supportsPopover(msg.el) && msg.el.matches(':popover-open')) {
            msg.el.hidePopover()
        }
        msg.items.forEach(item => {
            EventHandler.off(item.el, 'click')
            if (item.animationId) {
                cancelAnimationFrame(item.animationId);
                item.animationId = null;
            }
        });
        msg.detachOverlay?.()
    }
    Data.remove(id)
}
