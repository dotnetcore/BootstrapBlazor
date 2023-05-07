import { getHeight, getInnerHeight } from "../../modules/utility.js"
import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const split = { el }
    Data.set(id, split)

    let splitWidth = el.offsetWidth
    let splitHeight = el.offsetHeight
    let curVal = 0
    let newVal = 0
    let originX = 0
    let originY = 0
    const splitWrapper = el.firstElementChild
    const isVertical = !splitWrapper.classList.contains('is-horizontal')
    const splitBar = splitWrapper.children[1]
    const splitLeft = splitWrapper.children[0]
    const splitRight = splitWrapper.children[2]
    Drag.drag(splitBar,
        e => {
            splitWidth = el.offsetWidth
            splitHeight = el.offsetHeight
            if (isVertical) {
                originY = e.clientY || e.touches[0].clientY
                curVal = splitLeft.offsetHeight * 100 / splitHeight
            }
            else {
                originX = e.clientX || e.touches[0].clientX
                curVal = splitLeft.offsetWidth * 100 / splitWidth
            }
            el.classList.add('dragging')
        },
        e => {
            if (isVertical) {
                const eventY = e.clientY || e.changedTouches[0].clientY
                newVal = Math.ceil((eventY - originY) * 100 / splitHeight) + curVal
            }
            else {
                const eventX = e.clientX || e.changedTouches[0].clientX
                newVal = Math.ceil((eventX - originX) * 100 / splitWidth) + curVal
            }

            if (newVal <= 0) newVal = 0
            if (newVal >= 100) newVal = 100

            splitLeft.style.flexBasis = `${newVal}%`
            splitRight.style.flexBasis = `${100 - newVal}%`
        },
        () => {
            el.classList.remove('dragging')
        })
}

export function dispose(id) {
    const split = Data.get(id)
    Data.remove(id)

    if (split) {
        Drag.dispose(split.el)
    }
}
