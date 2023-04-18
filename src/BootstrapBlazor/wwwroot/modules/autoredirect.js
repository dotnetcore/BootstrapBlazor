import Data from "./data.js"
import EventHandler from "./event-handler.js"

export function init(id, invoker, interval, callback) {
    var m = { invoker, interval, callback, mousePosition: {}, count: 1000 }
    Data.set(id, m)

    m.fnMouseHandler = e => {
        if (m.mousePosition.screenX !== e.screenX || m.mousePosition.screenY !== e.screenY) {
            m.mousePosition.screenX = e.screenX
            m.mousePosition.screenY = e.screenY
            m.count = 1000
        }
    }

    m.fnKeyHandler = () => {
        m.count = 1000
    }

    EventHandler.on(document, 'mousemove', m.fnMouseHandler)
    EventHandler.on(document, 'keydown', m.fnKeyHandler)

    m.lockHandler = setInterval(() => {
        m.count += 1000
        if (m.count > m.interval) {
            clearInterval(m.lockHandler)
            m.lockHandler = null

            m.invoker.invokeMethodAsync(m.callback)
        }
    }, 1000)
}

export function dispose(id) {
    const m = Data.get(id)
    Data.remove(id)

    EventHandler.off(document, 'mousemove', m.fnMouseHandler)
    EventHandler.off(document, 'keydown', m.fnKeyHandler)

    if (m.lockHandler) {
        clearInterval(m.lockHandler)
    }
}
