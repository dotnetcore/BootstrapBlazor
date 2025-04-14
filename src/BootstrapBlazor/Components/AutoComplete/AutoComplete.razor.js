// Original imports
import { debounce, registerBootstrapBlazorModule } from "../../modules/utility.js";
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js";
import Data from "../../modules/data.js";
import EventHandler from "../../modules/event-handler.js";
import Input from "../../modules/input.js";
import Popover from "../../modules/base-popover.js";

export function init(id, invoke) {
    const el = document.getElementById(id);
    if (!el || Data.get(id)) return;

    const menu = el.querySelector('.dropdown-menu');
    const input = document.getElementById(`${id}_input`);
    if (!input || !menu) return;

    const ac = { el, invoke, menu, input };
    Data.set(id, ac);

    // --- Popover/Dropdown Setup ---
    const isPopover = input.getAttribute('data-bs-toggle') === 'bb.dropdown';
    if (isPopover) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    }
    else {
        const extraClass = input.getAttribute('data-bs-custom-class');
        if (extraClass) menu.classList.add(...extraClass.split(' '));
        const offset = input.getAttribute('data-bs-offset');
        if (offset) {
            try {
                const [x, y] = offset.split(',').map(parseFloat);
                if (!isNaN(x) && x !== 0) menu.style.setProperty('margin-left', `${x}px`);
                if (!isNaN(y) && y !== 0) menu.style.setProperty('margin-top', `${y}px`);
            } catch (e) { console.error("Error parsing offset", e); }
        }
    }

    // --- Debounce Setup ---
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');
    const filterDuration = duration > 0 ? duration : 200;
    const filterCallback = debounce(async v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        try {
            await currentAc.invoke.invokeMethodAsync('PerformFilteringAndCommitValue', v);
        } catch (error) {
            if (!error.message || !error.message.includes("instance is already disposed")) {
                console.error(`[AutoComplete JS] Error invoking C# PerformFilteringAndCommitValue for value: '${v}'.`, error);
            }
        } finally {
            if (currentAc.el) currentAc.el.classList.remove('is-loading');
        }
    }, filterDuration);

    // --- Event Listeners ---
    Input.composition(input, v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        if (isPopover === false) currentAc.el.classList.add('show');
        currentAc.el.classList.add('is-loading');
        filterCallback(v);
    });

    const keydownHandler = debounce(e => {
        const currentAc = Data.get(id);
        if (currentAc) handlerKeydown(currentAc, e);
    }, duration, e => {
        return ['ArrowUp', 'ArrowDown', 'Escape', 'Enter', 'NumpadEnter'].indexOf(e.key) > -1
    });
    if (duration > 0) {
        EventHandler.on(input, 'keydown', keydownHandler);
    } else {
        EventHandler.on(input, 'keydown', e => {
            const currentAc = Data.get(id);
            if (currentAc) handlerKeydown(currentAc, e);
        });
    }

    ac.triggerBlur = () => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        setTimeout(() => {
            const currentAcDelayed = Data.get(id);
            if (!currentAcDelayed) return;
            if (!isPopover) currentAcDelayed.el.classList.remove('show');
            const shouldTriggerCsharp = currentAcDelayed.input.getAttribute('data-bb-blur') === 'true';
            if (shouldTriggerCsharp) {
                try {
                    const currentValue = currentAcDelayed.input.value;
                    currentAcDelayed.invoke.invokeMethodAsync('TriggerBlurWithValue', currentValue);
                } catch (error) {
                    if (!error.message || !error.message.includes("instance is already disposed")) {
                        console.error('[AutoComplete JS] triggerBlur: Error invoking C# TriggerBlurWithValue.', error);
                    }
                }
            }
        }, 150);
    };

    EventHandler.on(menu, 'click', '.dropdown-item', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        if (currentAc.popover) currentAc.popover.hide();
        else currentAc.el.classList.remove('show');
    });

    EventHandler.on(input, 'focus', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        const showDropdownOnFocus = currentAc.input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover === false) currentAc.el.classList.add('show');
            else if (currentAc.popover) currentAc.popover.show();
        }
    });

    EventHandler.on(input, 'blur', e => {
        const currentAc = Data.get(id);
        if (currentAc) currentAc.triggerBlur();
    });

    ac.closePopover = e => {
        [...document.querySelectorAll('.auto-complete.show')].forEach(a => {
            const targetAc = e.target.closest('.auto-complete');
            if (targetAc === a) return;
            const elInput = a.querySelector('[data-bs-toggle="bb.dropdown"]');
            if (elInput === null) {
                const idToClose = a.getAttribute('id');
                const d = Data.get(idToClose);
                if (d) {
                    if (d.popover) d.popover.hide();
                    else a.classList.remove('show');
                }
            } else {
                const popoverInstance = Popover.getInstance(elInput);
                popoverInstance?.hide();
            }
        });
    }
    registerBootstrapBlazorModule('AutoComplete', id, () => {
        EventHandler.on(document, 'click', ac.closePopover);
    });
}

const handlerKeydown = (ac, e) => {
    const key = e.key;
    const { el, input, invoke, menu } = ac;

    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = input.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null) {
                current.click();
            } else {
                invoke.invokeMethodAsync('EnterCallback', input.value);
            }
            e.preventDefault();
        }
    }
    else if (key === 'Escape') {
        const skipEsc = input.getAttribute('data-bb-skip-esc') === 'true';
        if (skipEsc === false) {
            invoke.invokeMethodAsync('EscCallback');
            input.blur();
        }
    }
    else if (key === 'ArrowUp' || key === 'ArrowDown') {
        if (!el.classList.contains('show') && !ac.popover?.isShown()) {
            el.classList.add('show');
            ac.popover?.show();
        }
        const items = [...menu.querySelectorAll('.dropdown-item:not(.disabled)')];
        if (!items.length) return;
        e.preventDefault();
        let current = menu.querySelector('.active');
        if (current !== null) current.classList.remove('active');
        let index = current === null ? -1 : items.indexOf(current);
        index = key === 'ArrowUp' ? index - 1 : index + 1;
        if (index < 0) index = items.length - 1;
        else if (index > items.length - 1) index = 0;
        current = items[index];
        if (current) {
            current.classList.add('active');
            scrollIntoView(el, current);
        }
    }
    else if (e.key === 'Tab') {
        // Let blur handler manage
    }
}

// Function called from C# to set the input value visually
export function setValue(id, value) {
    let inputElement = null;
    const ac = Data.get(id);
    if (ac && ac.input) {
        inputElement = ac.input;
    } else {
        inputElement = document.getElementById(`${id}_input`);
    }

    if (inputElement) {
        const valueToSet = value ?? "";
        const hasFocus = document.activeElement === inputElement;

        // *** Corrected Logic ***
        // Only set the value if:
        // 1. The element doesn't have focus OR
        // 2. The value to set is different from the current visual value.
        // This prevents overwriting user input with the initial empty value if they type quickly,
        // and avoids unnecessary value sets (which can affect cursor position) if the value is already correct.
        if (!hasFocus || inputElement.value !== valueToSet) {
            // console.log(`[AutoComplete JS] Setting value for ID ${id} to: '${valueToSet}' (Focus: ${hasFocus}, Current: '${inputElement.value}')`);
            inputElement.value = valueToSet;
        }
        // else {
        // console.log(`[AutoComplete JS] Skipping setValue for ID ${id} as value is same or input has focus.`);
        // }
    }
}


// Original showList function
export function showList(id) {
    const ac = Data.get(id);
    if (ac) {
        if (ac.popover) ac.popover.show();
        else ac.el.classList.add('show');
    }
}

// Original dispose function, adapted for added/removed listeners
export function dispose(id) {
    const ac = Data.get(id);
    Data.remove(id);
    if (ac) {
        const { popover, input, menu, closePopover } = ac;
        if (popover) Popover.dispose(popover);
        EventHandler.off(input, 'focus');
        EventHandler.off(input, 'keydown');
        EventHandler.off(input, 'blur');
        EventHandler.off(menu, 'click');
        EventHandler.off(document, 'click', closePopover);
        Input.dispose(input);

        const bb = window.BootstrapBlazor || {};
        if (bb.AutoComplete && typeof bb.AutoComplete.dispose === 'function') {
            bb.AutoComplete.dispose(id, () => {
                EventHandler.off(document, 'click', closePopover);
            });
        } else {
            EventHandler.off(document, 'click', closePopover);
        }
    }
}

// Original scrollIntoView function
const scrollIntoView = (el, item) => {
    const input = el.querySelector('input');
    const behavior = input?.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
};

// Original exports
export { handleKeyUp, select, selectAllByFocus, selectAllByEnter };
