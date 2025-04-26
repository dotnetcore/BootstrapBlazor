import EventHandler from '../../modules/event-handler.js'

export function init(id) {
    const el = document.getElementById(id);
    const skipKeys = ['Enter', 'Tab', 'Shift', 'Control', 'Alt'];
    EventHandler.on(el, 'input', 'input', e => {
        const isNumber = e.target.getAttribute('type') === 'number';
        if (isNumber) {
            if (e.target.value.length > 1) {
                e.target.value = e.target.value.slice(1, 2);
            }
        }
    });
    EventHandler.on(el, 'keydown', 'input', e => {
        const isNumber = e.target.getAttribute('type') === 'number';
        if (skipKeys.indexOf(e.key) > -1) {

        }
        else if (e.key === 'Backspace' || e.key === 'ArrowLeft') {
            setPrevFocus(el, e.target);
        }
        else if (e.key === 'ArrowRight') {
            setNextFocus(el, e.target);
        }
        else if (isNumber) {
            if ("0123456789".indexOf(e.key) > -1) {
                setNextFocus(el, e.target);
            }
            else {
                e.preventDefault();
            }
        }
        else if ("abcdefghijklmnopqrstuvwxyzABCDEDFHIJKLMNOPQRSTUVWXYZ0123456789".indexOf(e.key) > -1) {
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
}

const setNextFocus = (el, target) => {
    const inputs = [...el.querySelectorAll('input')];
    let index = inputs.indexOf(target);
    if (index < inputs.length - 1) {
        setFocus(inputs[index + 1]);
    }
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
    EventHandler.off(el, 'input');
    EventHandler.off(el, 'keydown');
    EventHandler.off(el, 'focus');
}
