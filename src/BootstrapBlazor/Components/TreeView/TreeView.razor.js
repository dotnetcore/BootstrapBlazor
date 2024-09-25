import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const tree = { el };
    Data.set(id, tree)

    EventHandler.on(el, 'mouseenter', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.add('hover')
    })

    EventHandler.on(el, 'mouseleave', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.remove('hover')
    })

    EventHandler.on(el, 'click', '.tree-node', e => {
        const node = e.delegateTarget
        const prev = node.previousElementSibling;
        const radio = prev.querySelector('[type="radio"]')
        if (radio && radio.getAttribute('disabled') !== 'disabled') {
            radio.click();
        }
    })

    EventHandler.on(el, 'keydown', '.tree-root', e => {
        if (e.key === 'ArrowDown' || e.key === 'ArrowUp' || e.key === 'ArrowLeft' || e.key === 'ArrowRight') {
            const v = el.getAttribute('data-bb-keyboard');
            if (v === "true") {
                e.preventDefault();
                invoke.invokeMethodAsync(method, e.key);
            }
        }
        else if (e.keyCode === 32) {
            const v = el.getAttribute('data-bb-keyboard');
            if (v === "true") {
                const checkbox = el.querySelector(".active > .tree-content > .form-check > .form-check-input");
                if (checkbox) {
                    e.preventDefault();
                    checkbox.click();
                }
            }
        }
    });
}

export function scroll(id, options) {
    const tree = Data.get(id)
    if (tree) {
        const { el } = tree;
        const item = el.querySelector(".active .tree-node");
        if (item) {
            item.scrollIntoView(options);
        }
    }
}

export function dispose(id) {
    const tree = Data.get(id)
    Data.remove(id);

    if (tree) {
        const { el } = tree;
        EventHandler.off(el, 'mouseenter');
        EventHandler.off(el, 'mouseleave');
        EventHandler.off(el, 'click', '.tree-node');
        EventHandler.off(el, 'keyup', '.tree-root');
    }
}
