import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback) {
    //当页面高度超过设备可见高度时，阻止掉touchmove事件。
    const el = document.getElementById(id)
    const clearEl = el.querySelector('.btn-clear')
    const saveEl = el.querySelector('.btn-save')
    const elBody = el.querySelector('.hw-body')
    const canvas = elBody.querySelector('canvas')

    const hw = {
        el, invoke, callback, clearEl, saveEl, canvas
    }
    Data.set(id, hw)

    canvas.width = elBody.offsetWidth;
    canvas.height = elBody.offsetHeight;
    createCanvas(hw, {
        linewidth: 1,
        color: "#000000",
        background: "#fff"
    })

    //阻止默认的处理方式(阻止下拉滑动的效果)
    hw.preventHandler = e => e.preventDefault()
    //passive 参数不能省略，用来兼容ios和android
    hw.preventEventOption = { passive: false }
    //当页面高度超过设备可见高度时，阻止掉touchmove事件。
    document.body.addEventListener('touchmove', hw.preventHandler, hw.preventEventOption);
}

export function dispose(id) {
    const hw = Data.get(id)
    if (hw !== null) {
        document.body.removeEventListener('touchmove', hw.preventHandler, hw.preventEventOption)

        EventHandler.off(hw.clearEl, 'click')
        EventHandler.off(hw.saveEl, 'click')

        EventHandler.off(hw.canvas, 'mousedown')
        EventHandler.off(hw.canvas, 'mousemove')
        EventHandler.off(hw.canvas, 'mousup')
        EventHandler.off(hw.canvas, 'touchstart')
        EventHandler.off(hw.canvas, 'touchmove')
        EventHandler.off(hw.canvas, 'touchend')
    }
}

const createCanvas = (hw, op) => {
    const canvas = hw.canvas
    const clearEl = hw.clearEl
    const saveEl = hw.saveEl
    const cxt = canvas.getContext("2d", { willReadFrequently: true })

    cxt.fillStyle = op.background
    cxt.fillRect(0, 0, canvas.width, canvas.height)
    cxt.fillStyle = op.background
    cxt.strokeStyle = op.color
    cxt.lineWidth = op.linewidth
    cxt.lineCap = "round"

    var isStart = false

    const getOffsetX = e => {
        let offsetX = e.offsetX
        if (offsetX === undefined) {
            const rect = canvas.getBoundingClientRect()
            offsetX = e.touches[0].clientX - rect.left
        }
        return offsetX
    }

    const getOffsetY = e => {
        let offsetY = e.offsetY
        if (offsetY === undefined) {
            const rect = canvas.getBoundingClientRect()
            offsetY = e.touches[0].clientY - rect.top
        }
        return offsetY
    }

    //开始绘制
    const star = e => {
        isStart = true
        cxt.beginPath()

        const originX = getOffsetX(e)
        const originY = getOffsetY(e)
        cxt.moveTo(originX, originY)
    }

    //绘制中
    const move = e => {
        if (isStart) {
            const originX = getOffsetX(e)
            const originY = getOffsetY(e)
            cxt.lineTo(originX, originY)
            cxt.stroke()
        }
    }

    //结束绘制
    const end = () => {
        isStart = false
        cxt.closePath()
    }

    EventHandler.on(canvas, "touchstart", star)
    EventHandler.on(canvas, "touchmove", move)
    EventHandler.on(canvas, "touchend", end)

    EventHandler.on(canvas, 'mousedown', star)
    EventHandler.on(canvas, 'mousemove', move)
    EventHandler.on(canvas, 'mouseup', end)

    //清除画布
    EventHandler.on(clearEl, "click", () => {
        cxt.clearRect(0, 0, canvas.width, canvas.height)
    })
    //保存图片，直接转base64
    EventHandler.on(saveEl, "click", () => {
        var imgBase64 = canvas.toDataURL()
        hw.invoke.invokeMethodAsync(hw.callback, imgBase64)
    })
}
