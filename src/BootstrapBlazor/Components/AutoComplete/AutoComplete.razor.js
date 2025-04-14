// Original imports
import { debounce, registerBootstrapBlazorModule } from "../../modules/utility.js"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    // Prevent double init
    if (!el || Data.get(id)) {
        return;
    }

    const menu = el.querySelector('.dropdown-menu')
    const input = document.getElementById(`${id}_input`)
    if (!input || !menu) return;

    const ac = { el, invoke, menu, input }
    Data.set(id, ac)

    // Original popover/style logic
    const isPopover = input.getAttribute('data-bs-toggle') === 'bb.dropdown';
    if (isPopover) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    }
    else {
        const extraClass = input.getAttribute('data-bs-custom-class');
        if (extraClass) menu.classList.add(...extraClass.split(' '));
        const offset = input.getAttribute('data-bs-offset');
        if (offset) {
            try { // Add try/catch for safety
                const [x, y] = offset.split(',');
                const xValue = parseFloat(x);
                const yValue = parseFloat(y);
                if (xValue > 0) menu.style.setProperty('margin-left', `${xValue}px`);
                if (yValue > 0) menu.style.setProperty('margin-top', `${yValue}px`);
            } catch (e) { console.error("Error parsing offset", e); }
        }
    }

    // Original debounce setup
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');

    // Debounced callback for filtering and committing value
    let filterDuration = duration;
    if (filterDuration === 0) {
        filterDuration = 200; // Original default
    }
    const filterCallback = debounce(async v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        try {
            // STUTTER FIX: Call renamed C# method to handle filtering and state commit
            await currentAc.invoke.invokeMethodAsync('PerformFilteringAndCommitValue', v);
        } catch (error) {
            if (!error.message || !error.message.includes("instance is already disposed")) {
                console.error(`[AutoComplete JS] Error invoking C# PerformFilteringAndCommitValue for value: '${v}'.`, error);
            }
        } finally {
            if (currentAc.el) currentAc.el.classList.remove('is-loading');
        }
    }, filterDuration);

    // Original keydown listener setup
    // Note: Debouncing keydown might feel slightly laggy for navigation keys.
    // Consider if debouncing is truly needed here or only for filtering.
    // Keeping original logic for minimal change.
    if (duration > 0) {
        EventHandler.on(input, 'keydown', debounce(e => {
            const currentAc = Data.get(id); // Get context inside handler
            if (currentAc) handlerKeydown(currentAc, e); // Pass context
        }, duration, e => {
            // Debounce only specific keys (original logic)
            return ['ArrowUp', 'ArrowDown', 'Escape', 'Enter', 'NumpadEnter'].indexOf(e.key) > -1
        }))
    }
    else {
        EventHandler.on(input, 'keydown', e => {
            const currentAc = Data.get(id); // Get context
            if (currentAc) handlerKeydown(currentAc, e); // Pass context
        })
    }

    // Original keyup listener setup
    // EventHandler.on(input, 'keyup', e => handlerKeyup(ac, e));
    // Replaced by handlerKeydown for Enter/Esc/Arrows to match original structure better
    // Backspace/Delete now handled by Input.composition

    // Original triggerBlur function definition
    ac.triggerBlur = () => {
        const currentAc = Data.get(id);
        if (!currentAc) return;

        // BLUR FIX: Add delay to allow click events on items to register first
        setTimeout(() => {
            const currentAcDelayed = Data.get(id);
            if (!currentAcDelayed) return;

            if (!isPopover) currentAcDelayed.el.classList.remove('show'); // Use isPopover from outer scope

            const shouldTriggerCsharp = currentAcDelayed.input.getAttribute('data-bb-blur') === 'true';
            if (shouldTriggerCsharp) {
                try {
                    const currentValue = currentAcDelayed.input.value;
                    // BLUR FIX: Call new C# method with the current visual value
                    currentAcDelayed.invoke.invokeMethodAsync('TriggerBlurWithValue', currentValue);
                } catch (error) {
                    if (!error.message || !error.message.includes("instance is already disposed")) {
                        console.error('[AutoComplete JS] triggerBlur: Error invoking C# TriggerBlurWithValue.', error);
                    }
                }
            }
        }, 150); // Adjust delay if needed
    };

    // Original dropdown item click listener
    EventHandler.on(menu, 'click', '.dropdown-item', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        // Original called ac.triggerBlur(). Let the native blur event handle it now.
        if (currentAc.popover) currentAc.popover.hide();
        else currentAc.el.classList.remove('show');
        // C# OnClickItem calls JS setValue
    });

    // Original focus listener
    EventHandler.on(input, 'focus', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        const showDropdownOnFocus = currentAc.input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover === false) {
                currentAc.el.classList.add('show');
            }
            // Show popover on focus if applicable
            else if (currentAc.popover) {
                currentAc.popover.show();
            }
        }
    });

    // REMOVED: Original 'change' listener - Input.composition handles value changes
    // EventHandler.on(input, 'change', e => { ... });

    // ADDED: Standard 'blur' event listener for input
    EventHandler.on(input, 'blur', e => {
        const currentAc = Data.get(id);
        if (currentAc) {
            // Call the (potentially delayed) triggerBlur function
            currentAc.triggerBlur();
        }
    });

    // ADDED: Use Input.composition for reliable input value changes (typing, paste, IME)
    Input.composition(input, v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        if (isPopover === false) {
            currentAc.el.classList.add('show');
        }
        currentAc.el.classList.add('is-loading');
        // STUTTER FIX: Only call filterCallback (which calls C# PerformFilteringAndCommitValue)
        filterCallback(v);
        // STUTTER FIX: Removed call to UpdateInputValue / TriggerChange
    });

    // Original click outside logic
    ac.closePopover = e => {
        [...document.querySelectorAll('.auto-complete.show')].forEach(a => {
            const targetAc = e.target.closest('.auto-complete'); // Renamed variable
            if (targetAc === a) {
                return;
            }
            const elInput = a.querySelector('[data-bs-toggle="bb.dropdown"]'); // Renamed variable
            if (elInput === null) {
                const idToClose = a.getAttribute('id'); // Renamed variable
                const d = Data.get(idToClose);
                if (d) {
                    // Original called d.triggerBlur(). Removed to rely on native blur.
                    if (d.popover) d.popover.hide();
                    else a.classList.remove('show');
                }
            }
            // Handle popover case if needed (original didn't explicitly)
            else {
                const popoverInstance = Popover.getInstance(elInput);
                popoverInstance?.hide();
            }
        });
    }
    registerBootstrapBlazorModule('AutoComplete', id, () => {
        EventHandler.on(document, 'click', ac.closePopover);
    });
}

// Keydown Handler (Adapted from original logic + our fixes)
const handlerKeydown = (ac, e) => {
    const key = e.key;
    const { el, input, invoke, menu } = ac;

    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = input.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null) {
                current.click(); // C# OnClickItem calls JS setValue
            } else {
                invoke.invokeMethodAsync('EnterCallback', input.value); // C# EnterCallback calls JS setValue
            }
            e.preventDefault(); // Prevent default form submission
        }
    }
    else if (key === 'Escape') {
        const skipEsc = input.getAttribute('data-bb-skip-esc') === 'true';
        if (skipEsc === false) {
            invoke.invokeMethodAsync('EscCallback'); // C# EscCallback calls JS setValue to reset
            input.blur(); // Explicitly blur on Esc
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
    else if (key === 'Backspace' || key === 'Delete') {
        // Value change handled by Input.composition -> filterCallback
        // Removed original call to TriggerDeleteCallback
    }
    else if (e.key === 'Tab') {
        // Let blur handler manage the triggerBlur call
    }
}

// REMOVED: handlerKeyup (logic merged into handlerKeydown or covered by composition)

// ADDED: Function called from C# to set the input value visually
export function setValue(id, value) {
    const ac = Data.get(id);
    if (ac && ac.input) {
        ac.input.value = value ?? "";
    } else {
        // Fallback if called before init or after dispose
        const inputElement = document.getElementById(`${id}_input`);
        if (inputElement) inputElement.value = value ?? "";
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
        // Remove all listeners added in init
        EventHandler.off(input, 'focus');
        EventHandler.off(input, 'keydown');
        // EventHandler.off(input, 'keyup'); // Not added in this version
        EventHandler.off(input, 'blur'); // Remove added blur listener
        EventHandler.off(menu, 'click');
        EventHandler.off(document, 'click', closePopover);
        Input.dispose(input); // Ensure this removes composition listeners

        // Original module disposal
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
    const input = el.querySelector('input'); // Find input within el
    const behavior = input?.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
};

// Original exports
export { handleKeyUp, select, selectAllByFocus, selectAllByEnter };

