import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const msg = { el, invoke, callback, items: [] }
    Data.set(id, msg)
}

export function show(id, msgId) {
    const msg = Data.get(id)
    const el = document.getElementById(msgId)
    if (el === null) {
        return
    }

    const msgItem = { el, animationId: null }
    msg.items.push(msgItem)
    const autoHide = el.getAttribute('data-bb-autohide') === 'true';

    if (autoHide) {
        // auto close
        const delay = parseInt(el.getAttribute('data-bb-delay'));
        let start = void 0
        const autoCloseAnimation = ts => {
            if (start === void 0) {
                start = ts
            }

            const elapsed = ts - start;
            if (elapsed > delay) {
                close();
            }
            else {
                msgItem.animationId = requestAnimationFrame(autoCloseAnimation);
            }
        }
        msgItem.animationId = requestAnimationFrame(autoCloseAnimation)
    }
    el.classList.add('show');

    const close = () => {
        EventHandler.off(el, 'click')
        el.classList.remove('show');
        const hideHandler = setTimeout(function () {
            clearTimeout(hideHandler);

            // remove Id
            msg.items.pop();
            if (msg.items.length === 0) {
                // call server method prepare remove dom
                msg.invoke.invokeMethodAsync(msg.callback);
            }
        }, 500);
    };

    EventHandler.on(el, 'click', '.btn-close', e => {
        e.preventDefault();
        e.stopPropagation();

        // trigger on-dismiss event callback
        const alert = e.delegateTarget.closest('.alert');
        if(alert) {
            const alertId = alert.getAttribute('id');
            msg.invoke.invokeMethodAsync('Dismiss', alertId);
        }
        close();
    });
}

export function dispose(id) {
    const msg = Data.get(id)
    if (msg) {
        msg.items.forEach(item => {
            if (item.animationId) {
                cancelAnimationFrame(item.animationId);
            }
        });
    }
    Data.remove(id)
}
