import { isElement } from "./utility.js"

export async function toggle(options) {
    let el = null;
    options = options || {};
    if (options.id) {
        el = document.getElementById(options.id);
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

    updateFullscreenState(el);
}

const exitFullscreen = async el => {
    await document.exitFullscreen()

    updateFullscreenState(el);
}

const updateFullscreenState = el => {
    if (isFullscreen()) {
        el.classList.add('bb-fs-open')
    }
    else {
        el.classList.remove('bb-fs-open');
        document.documentElement.classList.remove('bb-fs-open');
    }
}

const isFullscreen = () => {
    return document.fullscreenElement !== null;
}
