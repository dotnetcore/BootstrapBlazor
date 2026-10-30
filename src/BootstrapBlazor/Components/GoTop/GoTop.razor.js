import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const getScrollElement = el => {
    let ele = el
    while (ele && ele.scrollHeight <= ele.clientHeight + 1) {
        // ele.parentNode 会继续找到 document，导致后续调用不存在的 document.scrollTo() 方法报错
        // 所以这里改为 parentElement，parentElement 到 html 就会返回 null，正确回退到 window
        ele = ele.parentElement
    }
    return ele || window
}

export function init(id, target) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const go = { el, target }
    Data.set(id, go)

    go.tip = new bootstrap.Tooltip(el)
    EventHandler.on(el, 'click', e => {
        e.preventDefault();

        const target = el.getAttribute('data-bb-target');
        const behavior = el.getAttribute('data-bb-behavior') || 'smooth';
        const element = (target && document.querySelector(target)) || getScrollElement(el)
        element.scrollTo({ top: 0, left: 0, behavior: behavior });
        go.tip.hide()
    })
}

export function dispose(id) {
    const go = Data.get(id)
    Data.remove(id)

    if (go) {
        EventHandler.off(go.el, 'click')
        go.tip.dispose()
        delete go.tip
    }
}
