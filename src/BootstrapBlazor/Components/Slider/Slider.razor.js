import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"

export function init(id, invoke, callback) {
    const el = document.getElementById(id)
    const slider = {}
    if (el) {
        Data.set(id, slider)
        const isDisabled = el.querySelector('.disabled') !== null
        if (!isDisabled) {
            let originX = 0
            let curVal = 0
            let newVal = 0
            let slider_width = el.offsetWidth
            const bar = el.querySelector('.slider-bar')
            const button = el.querySelector('.slider-button-wrapper')
            if (button) {
                slider.button = button
                Drag.drag(button,
                    e => {
                        slider_width = el.offsetWidth
                        originX = e.clientX || e.touches[0].clientX
                        curVal = parseInt(el.getAttribute('aria-valuetext'))
                        button.classList.add('dragging')
                        button.children[0].classList.add('dragging')
                    },
                    e => {
                        const eventX = e.clientX || e.changedTouches[0].clientX

                        newVal = Math.ceil((eventX - originX) * 100 / slider_width) + curVal
                        if (isNaN(newVal)) newVal = 0
                        if (newVal <= 0) newVal = 0
                        if (newVal >= 100) newVal = 100

                        bar.style.width = `${newVal}%`
                        button.style.left = `${newVal}%`
                        el.setAttribute('aria-valuetext', newVal.toString())
                        invoke.invokeMethodAsync(callback, newVal)
                    },
                    e => {
                        button.classList.remove('dragging')
                        button.children[0].classList.remove('dragging')
                        invoke.invokeMethodAsync(callback, newVal)
                    })
            }
        }
    }

}

export function dispose(id) {
    const slider = Data.get(id)
    Data.remove(id)
    if (slider.button) {
        Drag.dispose(slider.button)
    }
}
