import '../../topology.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export function init(el, invoker, data, callback) {
    Topology.prototype.lock = function (status) {
        this.store.data.locked = status
        this.finishDrawLine(!0)
        this.canvas.drawingLineName = ""
        this.stopPencil()
    }

    const meta = {}
    Data.set(el, meta)

    const isSupportTouch = el.getAttribute('data-bb-support-touch') === 'true'
    const isFitView = el.getAttribute('data-bb-fit-view') === 'true'
    const isCenterView = el.getAttribute('data-bb-center-view') === 'true'

    const initCanvas = () => {
        meta.topology = new Topology(el, {}, isSupportTouch)
        meta.topology.connectSocket = function () {
        }
        meta.topology.open(JSON.parse(data))
        meta.topology.lock(1)
        if (isFitView) {
            meta.topology.fitView()
        }
        if (isCenterView) {
            meta.topology.centerView()
        }
        invoker.invokeMethodAsync(callback)
    }

    // make sure el has height
    if (el.offsetHeight > 0) {
        initCanvas()
    }
    else {
        let timers = 0;
        const handler = setInterval(() => {
            if (el.offsetHeight > 0) {
                clearInterval(handler)
                initCanvas()
            }
            else {
                timers++
                if (timers > 10) {
                    clearInterval(handler)
                    console.log(el)
                    console.error(`el no height can't init'`)
                }
            }
        }, 200)
    }
}

export function update(el, data) {
    const meta = Data.get(el)
    meta.topology.doSocket(JSON.stringify(data))
}

export function scale(el, rate) {
    const meta = Data.get(el)
    meta.topology.scale(rate)
    meta.topology.centerView()
}

export function reset(el) {
    const meta = Data.get(el)
    meta.topology.fitView()
    meta.topology.centerView()
}

export function resize(el, width, height) {
    const meta = Data.get(el)
    meta.topology.canvas.dirty = true
    if (width !== null && height !== null) {
        meta.topology.resize(width, height)
    }
    else {
        meta.topology.resize()
    }
    meta.topology.fitView()
    meta.topology.centerView()
}

export function dispose(el) {
    const meta = Data.get(el)
    Data.remove(el)

    if (meta.topology) {
        meta.topology.destroy()
        delete meta.topology
    }
}
