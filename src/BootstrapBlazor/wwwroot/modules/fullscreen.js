import { isElement } from "./utility.js"
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
    }

    fs.enter = () => {
        fs.toggleElement.requestFullscreen() ||
            fs.toggleElement.webkitRequestFullscreen ||
            fs.toggleElement.mozRequestFullScreen ||
            fs.toggleElement.msRequestFullscreen

        // 处理 ESC 按键退出全屏
        var handler = setTimeout(() => {
            clearTimeout(handler);

            const fullscreenCheck = () => {
                if (!isFullscreen()) {
                    fs.toggleElement.classList.remove('bb-fs-open');
                    document.documentElement.classList.remove('bb-fs-open');
                }
                else {
                    fs.toggleElement.classList.add('bb-fs-open')
                    requestAnimationFrame(fullscreenCheck);
                }
            }
            requestAnimationFrame(fullscreenCheck);
        }, 200);
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
