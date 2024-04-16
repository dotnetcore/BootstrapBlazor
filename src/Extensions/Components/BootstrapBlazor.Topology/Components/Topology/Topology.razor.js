import '../../meta2d.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export function init(id, options) {
    const el = document.getElementById(id)
    if (el === null) { return }

    const { invoker, data, callback, isFitView, isCenterView } = options;
    const initCanvas = () => {
        const option = { isFitView, isCenterView }
        const meta2d = createMeta2d(id, data, option)
        Data.set(id, {
            el,
            meta2d,
            option
        })

        invoker.invokeMethodAsync(callback)
    }

    // make sure el has height
    if (el.offsetHeight > 0 && el.offsetWidth > 0) {
        initCanvas()
    }
    else {
        const handler = setInterval(() => {
            if (el.offsetHeight > 0 && el.offsetWidth > 0) {
                clearInterval(handler)
                initCanvas()
            }
        }, 200)
    }
}

export function update(id, data) {
    const meta = Data.get(id)
    if (meta) {
        meta.meta2d.doSocket(JSON.stringify(data))
    }
}

export function scale(id, rate) {
    const meta = Data.get(id)
    if (meta) {
        meta.meta2d.scale(rate)
        if (meta.option.isCenter) {
            meta.meta2d.centerView()
        }
    }
}

export function reset(id) {
    const meta = Data.get(id)
    if (meta) {
        meta.meta2d.fitView()
        meta.meta2d.centerView()
    }
}

export function resize(id, width, height) {
    const meta = Data.get(id)
    if (meta) {
        const meta2d = meta.meta2d
        const el = meta.el
        const option = meta.option

        if (el.offsetHeight > 0 && el.offsetWidth > 0) {
            meta2d.canvas.dirty = true
            if (width && height) {
                meta2d.resize(width, height)
            }
            else {
                meta2d.resize()
            }

            if (option.isCenterView) {
                meta2d.centerView()
            }
            if (option.isFitView) {
                meta2d.fitView()
            }
        }
    }
}

export function dispose(id) {
    const meta = Data.get(id)

    Data.remove(id)
    if (meta) {
        meta.meta2d.destroy()
        delete meta.meta2d
    }
}

const createMeta2d = (id, data, option) => {
    const el = document.getElementById(id);
    const meta2d = hackMeta2d(el)
    meta2d.open(JSON.parse(data))
    meta2d.lock(1)

    if (option.isFitView) {
        meta2d.fitView()
    }
    if (option.isCenterView) {
        meta2d.centerView()
    }
    el.observer = new ResizeObserver(() => {
        resize(id)
    })
    el.observer.observe(el)
    return meta2d
}

const hackMeta2d = el => {
    if (Meta2d.isHack === void 0) {
        Meta2d.isHack = true
        Meta2d.prototype.lock = function (status) {
            this.store.data.locked = status
            this.finishDrawLine(!0)
            this.canvas.drawingLineName = ""
            this.stopPencil()
        }
        Meta2d.prototype.connectSocket = function () {

        }
        Meta2d.prototype.doSocket = function (data) {
            this.socketCallback(data)
        }
    }

    const meta2d = new Meta2d(el)

    const originalCanvasResize = meta2d.canvas.resize
    meta2d.canvas.resize = function () {
        const width = this.parentElement.clientWidth
        const height = this.parentElement.clientHeight
        if (width > 0 && height > 0) {
            originalCanvasResize.call(this)
        }
    }
    return meta2d;
}
