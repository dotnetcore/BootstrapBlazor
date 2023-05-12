import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

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

    msg.items.push(el)
    const autoHide = el.getAttribute('data-bb-autohide') === 'true';
    const delay = parseInt(el.getAttribute('data-bb-delay'));
    let autoHideHandler = null;

    const showHandler = setTimeout(() => {
        clearTimeout(showHandler);
        if (autoHide) {
            // auto close
            autoHideHandler = setTimeout(() => {
                clearTimeout(autoHideHandler);
                close();
            }, delay);
        }
        el.classList.add('show');
    }, 50);

    const close = () => {
        if (autoHideHandler != null) {
            clearTimeout(autoHideHandler);
        }
        el.classList.remove('show');
        const hideHandler = setTimeout(function () {
            clearTimeout(hideHandler);

            // remove Id
            msg.items.remove(el);
            if (msg.items.length === 0) {
                // call server method prepare remove dom
                msg.invoke.invokeMethodAsync(msg.callback);
            }
        }, 500);
    };

    EventHandler.on(el, 'click', '.btn-close', e => {
        e.preventDefault();
        e.stopPropagation();

        close();
    });
}

export function dispose(id) {
    Data.remove(id)
}
