﻿import { isElement } from "./utility.js"
import Data from "./data.js"

export function init(id) {
    const fs = { toggleElement: null };
    Data.set(id, fs)

    fs.toggle = options => {
        if (options.id) {
            fs.toggleElement = document.getElementById(options.id)
        }
        else if (options.element && isElement(options.element)) {
            fs.toggleElement = el
        }
        else {
            fs.toggleElement = document.documentElement
        }

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

export function execute(id, options) {
    const fs = Data.get(id)
    if (fs) {
        fs.toggle(options);
    }
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
