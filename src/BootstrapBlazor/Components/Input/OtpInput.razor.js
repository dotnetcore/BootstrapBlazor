import EventHandler from '../../modules/event-handler.js'

export function init(id) {
    const el = document.getElementById(id);
    EventHandler.on(el, 'keydown', 'input', e => {
        const isNumber = e.target.getAttribute('type') === 'number';
        if (e.key === 'Tab') {

        }
        else if (e.key === 'Backspace' || e.key === 'ArrowLeft') {
            setPrevFocus(el, e.target);
        }
        else if (e.key === 'ArrowRight') {
            setNextFocus(el, e.target);
        }
        else if (isNumber) {
            if (e.key >= '0' && e.key <= '9') {
                setNextFocus(el, e.target);
            }
            else {
                e.preventDefault();
            }
        }
        else if ((e.key >= 'a' && e.key <= 'z') || (e.key >= 'A' && e.key <= 'Z')) {
            setNextFocus(el, e.target);
        }
        else {
            e.preventDefault();
        }
    });
    EventHandler.on(el, 'focus', 'input', e => {
        if (e.target.select) {
            e.target.select();
        }
    });
}

const setPrevFocus = (el, target) => {
    const inputs = [...el.querySelectorAll('input')];
    let index = inputs.indexOf(target);
    if (index > 0) {
        setFocus(inputs[index - 1]);
    }
    else {
        setBlur(target);
    }
}

const setNextFocus = (el, target) => {
    const inputs = [...el.querySelectorAll('input')];
    let index = inputs.indexOf(target);
    if (index < inputs.length - 1) {
        setFocus(inputs[index + 1]);
    }
    else {
        setBlur(target);
    }
}

const setBlur = target => {
    const handler = setTimeout(function () {
        clearTimeout(handler);
        if (target.blur) {
            target.blur();
        }
    }, 0);
}

const setFocus = target => {
    const handler = setTimeout(function () {
        clearTimeout(handler);
        if (target.focus) {
            target.focus();
        }
    }, 10);
}

export function dispose(id) {
    const el = document.getElementById(id);
    EventHandler.off(el, 'keydown');
}
