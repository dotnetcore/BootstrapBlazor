import { isElement } from "./utility.js"
import Data from "./data.js"

export function init(id) {
    const fs = {}
    Data.set(id, fs)

    fs.toggle = () => {
        if (isFullscreen()) {
            exit()
        }
        else {
            fs.enter()
        }
        fs.toggleElement.classList.toggle('bb-fs-open')
    }

    fs.enter = () => {
        fs.toggleElement.requestFullscreen() ||
            fs.toggleElement.webkitRequestFullscreen ||
            fs.toggleElement.mozRequestFullScreen ||
            fs.toggleElement.msRequestFullscreen
    }
}

export function execute(id, el) {
    const fs = Data.get(id)
    if (el && typeof (el) === 'string' && el.length > 0) {
        fs.toggleElement = document.getElementById(el)
    }
    else if (el && isElement(el)) {
        fs.toggleElement = el
    }
    else {
        fs.toggleElement = document.documentElement
    }
    fs.toggle()
}

export function dispose(id) {
    Data.remove(id)
}

const isFullscreen = () => {
    return document.fullscreen ||
        document.webkitIsFullScreen ||
        document.webkitFullScreen ||
        document.mozFullScreen ||
        document.msFullScreent
}

const exit = () => {
    if (document.exitFullscreen) {
        document.exitFullscreen()
    }
    else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen()
    }
    else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen()
    }
    else if (document.msExitFullscreen) {
        document.msExitFullscreen()
    }
}
