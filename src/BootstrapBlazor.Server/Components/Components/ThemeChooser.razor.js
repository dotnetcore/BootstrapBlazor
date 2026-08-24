import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const themeList = document.querySelector('.theme-list');
    if (themeList === null) {
        return;
    }

    EventHandler.on(el, 'click', () => {
        themeList.classList.toggle('is-open');
    });

    const close = themeList.querySelector('.btn-close');
    if (close) {
        EventHandler.on(close, 'click', () => {
            themeList.classList.remove('is-open');
        });
    }

    EventHandler.on(themeList, 'click', '.theme-item', () => {
        themeList.classList.remove('is-open');
    });

    const outsideClick = e => {
        if (!el.contains(e.target) && !themeList.contains(e.target)) {
            themeList.classList.remove('is-open');
        }
    };
    EventHandler.on(document, 'click', outsideClick);
    el.themeOutsideClick = outsideClick;
}

export function dispose(id) {
    const el = document.getElementById(id);
    if (el) {
        EventHandler.off(el, 'click');
        if (el.themeOutsideClick) {
            EventHandler.off(document, 'click', el.themeOutsideClick);
            delete el.themeOutsideClick;
        }
    }

    const themeList = document.querySelector('.theme-list');
    if (themeList) {
        EventHandler.off(themeList, 'click');
    }

    const close = document.querySelector('.theme-list .btn-close');
    if (close) {
        EventHandler.off(close, 'click');
    }
}
