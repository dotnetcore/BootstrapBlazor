import { debounce, getHeight } from "../../modules/utility.js"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"
import Popover from "../../modules/base-popover.js"

if (window.BootstrapBlazor === void 0) {
    window.BootstrapBlazor = {};
}

export function init(id, invoke) {
    const el = document.getElementById(id)
    const menu = el.querySelector('.dropdown-menu')
    const input = document.getElementById(`${id}_input`)
    const ac = { el, invoke, menu, input }
    Data.set(id, ac)

    const isPopover = input.getAttribute('data-bs-toggle') === 'bb.dropdown';
    if (isPopover) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    }

    // debounce
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');
    if (duration > 0) {
        ac.debounce = true
        EventHandler.on(input, 'keyup', debounce(e => {
            handlerKeyup(ac, e);
        }, duration, e => {
            return ['ArrowUp', 'ArrowDown', 'Escape', 'Enter', 'NumpadEnter'].indexOf(e.key) > -1
        }))
    }
    else {
        EventHandler.on(input, 'keyup', e => {
            handlerKeyup(ac, e);
        })
    }

    EventHandler.on(input, 'focus', e => {
        const showDropdownOnFocus = input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover === false) {
                el.classList.add('show');
            }
        }
    });

    EventHandler.on(menu, 'click', e => {
        el.classList.remove('show');
        if (el.triggerEnter !== true) {
            invoke.invokeMethodAsync('TriggerBlur');
        }
        delete el.triggerEnter;
    });

    Input.composition(input, async v => {
        const useInput = input.getAttribute('data-bb-input') !== 'false';
        if (isPopover === false && useInput) {
            el.classList.add('show');
        }

        el.classList.add('is-loading');
        await invoke.invokeMethodAsync('TriggerOnChange', v, useInput);
        el.classList.remove('is-loading');
    });

    if (window.BootstrapBlazor.AutoComplete === void 0) {
        window.BootstrapBlazor.AutoComplete = {
            hooked: false,
            registerCloseDropdownHandler: function () {
                if (this.hooked === false) {
                    this.hooked = true;

                    EventHandler.on(document, 'click', e => {
                        [...document.querySelectorAll('.auto-complete.show')].forEach(a => {
                            const ac = e.target.closest('.auto-complete');
                            if (ac === a) {
                                return;
                            }

                            const el = a.querySelector('[data-bs-toggle="bb.dropdown"]');
                            if (el === null) {
                                a.classList.remove('show');
                            }
                        });
                    });
                }
            }
        }
    }

    window.BootstrapBlazor.AutoComplete.registerCloseDropdownHandler();
}

const handlerKeyup = (ac, e) => {
    const key = e.key;
    const { el, input, menu } = ac;
    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = el.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null) {
                el.triggerEnter = true;
                current.click();
            }
        }
    }
    else if (key === 'Escape') {
        const skipEsc = el.getAttribute('data-bb-skip-esc') === 'true';
        if (skipEsc === false) {
            EventHandler.trigger(menu, 'click');
        }
    }
    else if (key === 'ArrowUp' || key === 'ArrowDown') {
        el.classList.add('show');
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
        const hehavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
        menu.scrollTo({ top: top, left: 0, behavior: hehavior });
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
        const { popover, input, menu } = ac;
        if (popover) {
            Popover.dispose(popover)
            if (input) {
                EventHandler.off(input, 'focus')
            }
        }
        EventHandler.off(input, 'keyup');
        EventHandler.off(menu, 'click');
        Input.dispose(input);
    }
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
