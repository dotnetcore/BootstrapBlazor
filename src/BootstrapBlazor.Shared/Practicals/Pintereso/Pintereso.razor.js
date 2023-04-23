import Data from "../../../BootstrapBlazor/modules/data.js"
import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

export function init(id, invoke, callback) {
    const ps = {
        handler: () => {
            //可视区窗口高度
            var windowH = document.documentElement.clientHeight
            //滚动条的上边距
            var scrollH = document.documentElement.scrollTop || document.body.scrollTop
            //滚动条的高度
            var documentH = document.documentElement.scrollHeight || document.body.scrollHeight
            var h1 = windowH + scrollH
            var h2 = documentH
            if (Math.abs(h1 - h2) < 50) {
                invoke.invokeMethodAsync(callback)
                //把滚动条往上滚一点
                window.scrollTo(0, scrollH - 40)
            }
        }
    }
    Data.set(id, ps)

    EventHandler.on(window, 'scroll', ps.handler)
}

export function dispose(id) {
    const ps = Data.get(id)
    Data.remove(id)

    EventHandler.off(window, 'scroll', ps.handler)
}
