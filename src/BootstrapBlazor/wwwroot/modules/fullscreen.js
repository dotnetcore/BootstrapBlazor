import { isElement } from "./utility.js"

export function toggle(options) {
    let el = null;
    options = options || {};
    if (options.id) {
        el = document.getElementById(options.id);
    }
    else if (options.element && isElement(options.element)) {
        el = options.element;
    }
    else {
        el = document.documentElement
    }

    if (el !== null) {
        if (isFullscreen()) {
            exit();
        }
        else {
            enterFullscreen(el);
        }
    }
}

const enterFullscreen = el => {
    el.requestFullscreen() || el.webkitRequestFullscreen || el.mozRequestFullScreen || el.msRequestFullscreen

    // 处理 ESC 按键退出全屏
    var handler = setTimeout(() => {
        clearTimeout(handler);

        const fullscreenCheck = () => {
            if (!isFullscreen()) {
                el.classList.remove('bb-fs-open');
                document.documentElement.classList.remove('bb-fs-open');
            }
            else {
                el.classList.add('bb-fs-open')
                requestAnimationFrame(fullscreenCheck);
            }
        }
        requestAnimationFrame(fullscreenCheck);
    }, 200);
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
