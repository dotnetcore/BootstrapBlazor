import EventHandler from '../../modules/event-handler.js'

export function init(id, invoke, method) {
    const el = document.getElementById(id);

    EventHandler.on(el, 'input', '.bb-otp-item', e => {
        const isNumber = e.target.getAttribute('type') === 'number';
        if (isNumber && e.target.value.length > 1) {
            e.target.value = e.target.value.slice(1, 2);
        }
        setValue(el, invoke, method);
    });
    EventHandler.on(el, 'keydown', '.bb-otp-item', e => {
        if (e.ctrlKey) {
            return;
        }

        processKey(el, e);
    });
    EventHandler.on(el, 'focus', '.bb-otp-item', e => {
        if (e.target.select) {
            e.target.select();
        }
    });

    EventHandler.on(el, 'paste', e => {
        if (e.clipboardData && e.clipboardData.getData) {
            const pastedText = e.clipboardData.getData('text/plain');
            const inputs = [...el.querySelectorAll('.bb-otp-item')];
            if (inputs.find(i => i.getAttribute('disabled') || i.getAttribute('readonly'))) {
                return;
            }
            for (const index in inputs) {
                const input = inputs[index];
                if (index < pastedText.length) {
                    input.value = pastedText[index];
                }
                else {
                    input.value = '';
                }
            }
        }
        e.target.blur();
        setValue(el, invoke, method);
    });
}

const setValue = (el, invoke, method) => {
    const val = [...el.querySelectorAll('.bb-otp-item')].map(input => input.value).join('');
    console.log(val);
    invoke.invokeMethodAsync(method, val);
}

const setPrevFocus = (el, target) => {
    const inputs = [...el.querySelectorAll('.bb-otp-item')];
    let index = inputs.indexOf(target);
    if (index > 0) {
        setFocus(inputs[index - 1]);
    }
}

const setNextFocus = (el, target) => {
    const inputs = [...el.querySelectorAll('.bb-otp-item')];
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
    }, 0);
}

const processKey = (el, event) => {
    const skipKeys = ['Enter', 'Tab', 'Shift', 'Control', 'Alt'];
    const input = event.target;
    const isNumber = input.getAttribute('type') === 'number';

    if (skipKeys.indexOf(event.key) > -1) {

    }
    else if (event.key === 'Backspace' || event.key === 'ArrowLeft') {
        setPrevFocus(el, input);
    }
    else if (event.key === 'ArrowRight') {
        setNextFocus(el, input);
    }
    else if (isNumber) {
        if ("0123456789".indexOf(event.key) > -1) {
            setNextFocus(el, input);
        }
        else {
            event.preventDefault();
        }
    }
    else if ("abcdefghijklmnopqrstuvwxyzABCDEDFGHIJKLMNOPQRSTUVWXYZ0123456789".indexOf(event.key) > -1) {
        setNextFocus(el, input);
    }
    else {
        event.preventDefault();
    }
}

export function dispose(id) {
    const el = document.getElementById(id);
    EventHandler.off(el, 'input');
    EventHandler.off(el, 'keydown');
    EventHandler.off(el, 'focus');
}
