import { isElement } from "./utility.js"

export async function toggle(options) {
    let el = null;
    options = options || {};
    if (options.id) {
        el = document.getElementById(options.id);
    }
    else if (options.selector) {
        el = document.querySelector(options.selector);
    }
    else if (isElement(options.element)) {
        el = options.element;
    }
    else {
        el = document.documentElement
    }

    if (el !== null) {
        if (isFullscreen()) {
            await exitFullscreen(el);
        }
        else {
            await enterFullscreen(el);
        }
    }
}

const enterFullscreen = async el => {
    await el.requestFullscreen();
}

const exitFullscreen = async el => {
    await document.exitFullscreen()
}

const isFullscreen = () => {
    return document.fullscreenElement !== null;
}
