import { debounce, getHeight } from "../../modules/utility.js"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    const menu = el.querySelector('.dropdown-menu')
    const input = document.getElementById(`${id}_input`)
    const ac = { el, invoke, menu, input }
    Data.set(id, ac)

    if (el.querySelector('[data-bs-toggle="bb.dropdown"]')) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    }

    // debounce
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');
    if (duration > 0) {
        ac.debounce = true
        EventHandler.on(input, 'keyup', debounce(async e => {
            await handlerKeyup(ac, e);
        }, duration, e => {
            return ['ArrowUp', 'ArrowDown', 'Escape', 'Enter', 'NumpadEnter'].indexOf(e.key) > -1
        }))
    }
    else {
        EventHandler.on(input, 'keyup', async e => {
            await handlerKeyup(ac, e);
        })
    }

    EventHandler.on(input, 'focus', e => {
        const showDropdownOnFocus = input.getAttribute('data-bb-auto-dropdown-focus') !== 'false';
        if (showDropdownOnFocus) {
            if (ac.popover === void 0) {
                el.classList.add('show');
            }
        }
    });

    EventHandler.on(input, 'blur', e => {
        el.classList.remove('show');
        invoke.invokeMethodAsync('TriggerBlur');
    });

    Input.composition(input, async v => {
        el.classList.add('is-loading');
        el.classList.add('show');
        await invoke.invokeMethodAsync('TriggerOnChange', v);
        el.classList.remove('is-loading');
    });
}

const handlerKeyup = async (ac, e) => {
    const key = e.key;
    const { el, input, menu, invoke } = ac;
    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = el.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            //await invoke.invokeMethodAsync('TriggerOnChange', key);
        }
    }
    else if (key === 'Escape') {
        const skipEsc = el.getAttribute('data-bb-skip-esc') === 'true';
        if (!skipEsc) {
            input.blur();
        }
    }
    else if (key === 'ArrowUp' || key === 'ArrowDown') {
        e.preventDefault();
        e.stopPropagation();

        const items = [...menu.querySelectorAll('.dropdown-item')];
        let current = menu.querySelector('.active');
        if (current !== null) {
            current.classList.remove('active');
        }
        let index = current === null ? -1 : items.indexOf(current);
        index = key === 'ArrowUp' ? index - 1 : index + 1;
        if (index < 0) {
            index = items.length - 1;
        }
        else if (index > items.length - 1) {
            index = 0;
        }
        items[index].classList.add('active');
        const top = getTop(menu, index);
        menu.scrollTo({ top: top, left: 0, behavior: 'smooth' });
    }
}

const getTop = (menu, index) => {
    const styles = getComputedStyle(menu)
    const maxHeight = parseInt(styles.maxHeight) / 2
    const itemHeight = getHeight(menu.querySelector('.dropdown-item'))
    const height = itemHeight * index
    const count = Math.floor(maxHeight / itemHeight);
    let top = 0;
    if (height > maxHeight) {
        top = itemHeight * (index > count ? index - count : index)
    }
    return top;
}

export function triggerFocus(id) {
    const ac = Data.get(id)
    ac.popover?.show();
}

export function dispose(id) {
    const ac = Data.get(id)
    Data.remove(id)

    if (ac) {
        if (ac.popover) {
            Popover.dispose(ac.popover)
            if (ac.input) {
                EventHandler.off(ac.input, 'focus')
            }
        }
        if (ac.input) {
            Input.dispose(ac.input)
            EventHandler.off(ac.input, 'keyup')
        }
    }
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
