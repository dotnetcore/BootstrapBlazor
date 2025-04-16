import { debounce, registerBootstrapBlazorModule } from "../../modules/utility.js"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    const menu = el.querySelector('.dropdown-menu')
    const input = document.getElementById(`${id}_input`)
    const ac = { el, invoke, menu }
    Data.set(id, ac)

    const isPopover = input.getAttribute('data-bs-toggle') === 'bb.dropdown';
    if (isPopover) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    }
    else {
        const extraClass = input.getAttribute('data-bs-custom-class');
        if (extraClass) {
            menu.classList.add(...extraClass.split(' '))
        }
        const offset = input.getAttribute('data-bs-offset');
        if (offset) {
            const [x, y] = offset.split(',');
            const xValue = parseFloat(x);
            const yValue = parseFloat(y);

            if (xValue > 0) {
                menu.style.setProperty('margin-left', `${xValue}px`);
            }
            if (yValue > 0) {
                menu.style.setProperty('margin-top', `${yValue}px`);
            }
        }
    }

    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');
    if (duration > 0) {
        ac.debounce = true
        EventHandler.on(input, 'keyup', debounce(e => {
            handlerKeyup(ac, e);
        }, duration))
    }
    else {
        EventHandler.on(input, 'keyup', e => {
            handlerKeyup(ac, e);
        })
    }

    EventHandler.on(menu, 'click', '.dropdown-item', e => {
        ac.close();
    });

    EventHandler.on(input, 'focus', e => {
        const showDropdownOnFocus = input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover === false) {
                ac.show();
            }
        }
    });

    let filterDuration = duration;
    if (filterDuration === 0) {
        filterDuration = 200;
    }
    const filterCallback = debounce(async v => {
        await invoke.invokeMethodAsync('TriggerFilter', v);
        el.classList.remove('is-loading');
    }, filterDuration);

    Input.composition(input, v => {
        if (isPopover === false) {
            ac.show();
        }

        el.classList.add('is-loading');
        filterCallback(v);

    });

    ac.show = () => {
        ac.el.classList.add('show');
    }

    ac.close = () => {
        ac.el.classList.remove('show');
    }

    ac.closePopover = e => {
        [...document.querySelectorAll('.auto-complete.show')].forEach(a => {
            const ac = e.target.closest('.auto-complete');
            if (ac === a) {
                return;
            }

            const el = a.querySelector('[data-bs-toggle="bb.dropdown"]');
            if (el === null) {
                const id = a.getAttribute('id');
                const d = Data.get(id);
                if (d) {
                    d.close();
                }
            }
        });
    }
    ac.blur = e => {
        if (e.key === 'Tab') {
            [...document.querySelectorAll('.auto-complete.show')].forEach(a => {
                const id = a.getAttribute('id');
                const d = Data.get(id);
                if (d) {
                    d.close();
                }
            });
        }
    }
    registerBootstrapBlazorModule('AutoComplete', id, () => {
        EventHandler.on(document, 'click', ac.closePopover);
        EventHandler.on(document, 'keyup', ac.blur);
    });
}

const handlerKeyup = (ac, e) => {
    const key = e.key;
    const { el, invoke, menu } = ac;
    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = el.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null) {
                current.click();
            }
            invoke.invokeMethodAsync('EnterCallback');
        }
    }
    else if (key === 'Escape') {
        const skipEsc = el.getAttribute('data-bb-skip-esc') === 'true';
        if (skipEsc === false) {
            invoke.invokeMethodAsync('EscCallback');
        }
    }
    else if (key === 'ArrowUp' || key === 'ArrowDown') {
        ac.show();
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
        current = items[index];
        current.classList.add('active');
        scrollIntoView(el, current);
    }
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
        EventHandler.off(menu, 'click');
        EventHandler.off(input, 'keyup');
        Input.dispose(input);
    }

    const { AutoComplete } = window.BootstrapBlazor;
    AutoComplete.dispose(id, () => {
        EventHandler.off(document, 'click', ac.closePopover);
        EventHandler.off(document, 'keyup', ac.blur);
    });
}

const scrollIntoView = (el, item) => {
    const behavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
