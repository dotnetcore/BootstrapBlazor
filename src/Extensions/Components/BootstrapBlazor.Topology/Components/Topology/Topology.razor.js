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

    meta._topology = new Topology(el, {}, isSupportTouch)
    meta._topology.connectSocket = function () {
    }
    meta._topology.open(JSON.parse(data))
    meta._topology.lock(1)
    if (isFitView) {
        meta._topology.fitView()
    }
    if (isCenterView) {
        meta._topology.centerView()
    }
    invoker.invokeMethodAsync(callback)
}

export function update(el, data) {
    const meta = Data.get(el)
    meta._topology.doSocket(JSON.stringify(data))
}

export function scale(el, rate) {
    const meta = Data.get(el)
    meta._topology.scale(rate)
    meta._topology.centerView()
}

export function reset(el) {
    const meta = Data.get(el)
    meta._topology.fitView()
    meta._topology.centerView()
}

export function resize(el, width, height) {
    const meta = Data.get(el)
    meta._topology.canvas.dirty = true
    if (width !== null && height !== null) {
        meta._topology.resize(width, height)
    }
    else {
        meta._topology.resize()
    }
    meta._topology.fitView()
    meta._topology.centerView()
}

export function dispose(el) {
    const meta = Data.get(el)
    if (meta._topology) {
        meta._topology.destroy()
        delete meta._topology
    }
    Data.remove(el)
}
