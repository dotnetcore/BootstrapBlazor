import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke, callback, option) {
    const el = document.getElementById(id)
    const captcha = { el, invoke, callback }
    Data.set(id, captcha)

    const canvas = el.querySelector(".captcha-body-bg").getContext('2d', { willReadFrequently: true })
    const block = el.querySelector(".captcha-body-bar")
    const bar = block.getContext('2d', { willReadFrequently: true })
    const load = el.querySelector(".captcha-load")
    const footer = el.querySelector('.captcha-footer')
    const barLeft = footer.querySelector('.captcha-bar-mask')
    const slider = el.querySelector('.captcha-bar')
    const barTextEl = el.querySelector('.captcha-bar-text')
    const refresh = el.querySelector('.captcha-refresh')

    captcha.canvas = canvas
    captcha.block = block
    captcha.bar = bar
    captcha.load = load
    captcha.footer = footer
    captcha.barLeft = barLeft
    captcha.slider = slider
    captcha.barTextEl = barTextEl
    captcha.refresh = refresh

    option = {
        ...{
            sideLength: 0,
            diameter: 0,
            imageUrl: '',
            barWidth: 0
        },
        ...option
    }

    initImg(captcha, option)

    let originX = 0
    let originY = 0
    const trail = []

    Drag.drag(slider,
        e => {
            barTextEl.classList.add('d-none')
            originX = e.clientX || e.touches[0].clientX
            originY = e.clientY || e.touches[0].clientY
        },
        e => {
            const eventX = e.clientX || e.touches[0].clientX
            const eventY = e.clientY || e.touches[0].clientY
            const moveX = eventX - originX
            const moveY = eventY - originY
            if (moveX < 0 || moveX + 40 > option.width) return false

            slider.style.left = `${moveX - 1}px`
            const blockLeft = (option.width - 40 - 20) / (option.width - 40) * moveX
            block.style.left = `${blockLeft}px`

            footer.classList.add('is-move')
            barLeft.style.width = `${moveX + 4}px`
            trail.push(Math.round(moveY))
        },
        e => {
            const eventX = e.clientX || e.changedTouches[0].clientX
            const offset = Math.ceil((option.width - 40 - 20) / (option.width - 40) * (eventX - originX) + 3)
            invoke.invokeMethodAsync(callback, offset, trail).then(data => {
                if (data) {
                    barTextEl.innerHTML = barTextEl.getAttribute('data-text')
                }
                else {
                    setTimeout(() => {
                        refresh.click()
                    }, 1000)
                }
            })
        }
    )
}

export function reset(id, option) {
    const captcha = Data.get(id)
    if (captcha) {
        resetCanvas(captcha, option)
        initImg(captcha, option)
        resetBar(captcha);
    }
}

export function dispose(id) {
    const captcha = Data.get(id)
    Data.remove(id)

    if (captcha) {
        Drag.dispose(captcha.el)
    }
}

const initImg = (captcha, option) => {
    const drawImg = (ctx, operation) => {
        const l = option.sideLength
        const r = option.diameter
        const PI = Math.PI
        const x = option.offsetX
        const y = option.offsetY
        ctx.beginPath()
        ctx.moveTo(x, y)
        ctx.arc(x + l / 2, y - r + 2, r, 0.72 * PI, 2.26 * PI)
        ctx.lineTo(x + l, y)
        ctx.arc(x + l + r - 2, y + l / 2, r, 1.21 * PI, 2.78 * PI)
        ctx.lineTo(x + l, y + l)
        ctx.lineTo(x, y + l)
        ctx.arc(x + r - 2, y + l / 2, r + 0.4, 2.76 * PI, 1.24 * PI, true)
        ctx.lineTo(x, y)
        ctx.lineWidth = 2
        ctx.fillStyle = 'rgba(255, 255, 255, 0.7)'
        ctx.strokeStyle = 'rgba(255, 255, 255, 0.7)'
        ctx.stroke()
        ctx[operation]()
        ctx.globalCompositeOperation = 'destination-over'
    }

    const img = new Image()
    img.src = option.imageUrl

    img.onload = () => {
        drawImg(captcha.canvas, 'fill')
        drawImg(captcha.bar, 'clip')

        captcha.canvas.drawImage(img, 0, 0, option.width, option.height)
        captcha.bar.drawImage(img, 0, 0, option.width, option.height)

        const y = option.offsetY - option.diameter * 2 - 1
        const ImageData = captcha.bar.getImageData(option.offsetX - 3, y, option.barWidth, option.barWidth)
        captcha.block.width = option.barWidth
        captcha.bar.putImageData(ImageData, 0, y)
    }
    img.onerror = () => {
        captcha.load.innerHTML = captcha.load.getAttribute('data-failed')
        captcha.load.classList.add('text-danger')
    }
}

const resetCanvas = (captcha, option) => {
    captcha.canvas.clearRect(0, 0, option.width, option.height)
    captcha.bar.clearRect(0, 0, option.width, option.height)
    captcha.block.width = option.width
    captcha.block.style.left = '0'
    captcha.load.innerHTML = captcha.load.getAttribute('data-load')
    captcha.load.classList.remove('text-danger')
}

const resetBar = function (captcha) {
    captcha.footer.classList.remove('is-invalid')
    captcha.footer.classList.remove('is-valid')
    captcha.barTextEl.innerHTML = captcha.barTextEl.getAttribute('data-text')
    captcha.barTextEl.classList.remove('d-none')
    captcha.slider.style.left = '0'
    captcha.barLeft.style.width = '0'
    captcha.footer.classList.remove('is-move')
}
