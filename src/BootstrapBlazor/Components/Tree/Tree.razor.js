import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const tree = {el}
    Data.set(id, tree)

    EventHandler.on(el, 'mouseenter', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.add('hover')
    })

    EventHandler.on(el, 'mouseleave', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.remove('hover')
    })

    // 支持 Radio
    EventHandler.on(el, 'click', '.tree-node', e => {
        const node = e.delegateTarget
        const prev = node.previousElementSibling;
        const radio = prev.querySelector('[type="radio"]')
        if (radio && radio.getAttribute('disabled') !== 'disabled') {
            radio.click();
        }
    })
}

export function dispose(id) {
    const tree = Data.get(id)
    if (tree) {
        EventHandler.off(tree.el, 'mouseenter')
        EventHandler.off(tree.el, 'mouseleave')
        EventHandler.off(tree.el, 'click', '.tree-node')
    }
}
