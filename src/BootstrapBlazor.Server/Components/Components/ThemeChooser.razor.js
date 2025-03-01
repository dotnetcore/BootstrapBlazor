import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const themeList = document.querySelector('.theme-list');
    const close = document.querySelector('.theme-list .btn-close');
    EventHandler.on(el, 'click', () => {
        themeList.classList.toggle('is-open');
    });
    EventHandler.on(close, 'click', () => {
        themeList.classList.remove('is-open');
    });
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el) {
        EventHandler.off(el, 'click');
    }

    const close = document.querySelector('.theme-list .btn-close');
    if (close) {
        EventHandler.off(close, 'click');
    }
}
