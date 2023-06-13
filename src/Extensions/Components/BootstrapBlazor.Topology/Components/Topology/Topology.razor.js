import '../../topology.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export function init(id, invoker, data, callback) {
    Topology.prototype.lock = function (status) {
        this.store.data.locked = status
        this.finishDrawLine(!0)
        this.canvas.drawingLineName = ""
        this.stopPencil()
    }

    const el = document.getElementById(id)
    const isSupportTouch = el.getAttribute('data-bb-support-touch') === 'true'
    const isFitView = el.getAttribute('data-bb-fit-view') === 'true'
    const isCenterView = el.getAttribute('data-bb-center-view') === 'true'

    const initCanvas = () => {
        const topology = new Topology(el, {}, isSupportTouch)
        topology.connectSocket = function () {
        }
        topology.open(JSON.parse(data))
        topology.lock(1)
        if (isFitView) {
            topology.fitView()
        }
        if (isCenterView) {
            topology.centerView()
        }
        new ResizeObserver(() => {
            resize(id)
        }).observe(el)
        invoker.invokeMethodAsync(callback)
        Data.set(id, { topology })
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

export function update(id, data) {
    const meta = Data.get(id)
    if (meta) {
        meta.topology.doSocket(JSON.stringify(data))
    }
}

export function scale(id, rate) {
    const meta = Data.get(id)
    if (meta) {
        meta.topology.scale(rate)
        meta.topology.centerView()
    }
}

export function reset(id) {
    const meta = Data.get(id)
    if (meta) {
        meta.topology.fitView()
        meta.topology.centerView()
    }
}

export function resize(id, width, height) {
    const meta = Data.get(id)
    if (meta) {
        meta.topology.canvas.dirty = true
        if (width && height) {
            meta.topology.resize(width, height)
        }
        else {
            meta.topology.resize()
        }
        meta.topology.fitView()
        meta.topology.centerView()
    }
}

export function dispose(id) {
    const meta = Data.get(id)

    Data.remove(id)
    if (meta) {
        meta.topology.destroy()
        delete meta.topology
    }
}
