import { isElement } from "./utility.js"

export async function toggle(options) {
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
            await document.exitFullscreen()
        }
        else {
            await enterFullscreen(el);
        }
    }
}

const enterFullscreen = async el => {
    await el.requestFullscreen();

    if (!isFullscreen()) {
        el.classList.remove('bb-fs-open');
        document.documentElement.classList.remove('bb-fs-open');
    }
    else {
        el.classList.add('bb-fs-open')
    }
}

const isFullscreen = () => {
    return document.fullscreenElement !== null;
}
