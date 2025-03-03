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

    ac.triggerBlur = () => {
        el.classList.remove('show');
        const triggerBlur = input.getAttribute('data-bb-blur') === 'true';
        if (triggerBlur) {
            invoke.invokeMethodAsync('TriggerBlur');
        }
    }

    EventHandler.on(menu, 'click', '.dropdown-item', e => {
        ac.triggerBlur();
    });

    EventHandler.on(input, 'focus', e => {
        const showDropdownOnFocus = input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover === false) {
                el.classList.add('show');
            }
        }
    });

    EventHandler.on(input, 'change', e => {
        invoke.invokeMethodAsync('TriggerChange', e.target.value);
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
            el.classList.add('show');
        }

        el.classList.add('is-loading');
        filterCallback(v);
    });

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
                    d.triggerBlur();
                }
            }
        });
    }
    registerBootstrapBlazorModule('AutoComplete', id, () => {
        EventHandler.on(document, 'click', ac.closePopover);
    });
}

const handlerKeyup = (ac, e) => {
    const key = e.key;
    const { el, input, invoke, menu } = ac;
    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = el.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null) {
                current.click();
                ac.triggerBlur();
            }
            invoke.invokeMethodAsync('EnterCallback', input.value);
        }
    }
    else if (key === 'Escape') {
        const skipEsc = el.getAttribute('data-bb-skip-esc') === 'true';
        if (skipEsc === false) {
            invoke.invokeMethodAsync('EscCallback');
            ac.triggerBlur();
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
        current = items[index];
        current.classList.add('active');
        scrollIntoView(el, current);
    }
}

export function showList(id) {
    const ac = Data.get(id)
    if (ac) {
        if (ac.popover) {
            ac.popover.show();
        }
        else {
            ac.el.classList.add('show');
        }
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
        EventHandler.off(input, 'change');
        EventHandler.off(input, 'keyup');
        EventHandler.off(menu, 'click');
        Input.dispose(input);

        const { AutoComplete } = window.BootstrapBlazor;
        AutoComplete.dispose(id, () => {
            EventHandler.off(document, 'click', ac.closePopover);
        });
    }
}

const scrollIntoView = (el, item) => {
    const behavior = el.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
