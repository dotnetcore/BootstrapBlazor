// Import necessary modules
import { debounce, registerBootstrapBlazorModule } from "../../modules/utility.js";
// handleKeyUp is imported but not used directly, keep for potential base class usage
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js";
import Data from "../../modules/data.js";
import EventHandler from "../../modules/event-handler.js";
import Input from "../../modules/input.js";
import Popover from "../../modules/base-popover.js";

// Initialization function for the AutoComplete component
export function init(id, invoke) {
    const el = document.getElementById(id);
    if (!el) return;

    // Prevent double initialization
    if (Data.get(id)) {
        return;
    }

    const menu = el.querySelector('.dropdown-menu');
    const input = document.getElementById(`${id}_input`);
    if (!input || !menu) return;

    const ac = { el, invoke, menu, input };
    Data.set(id, ac);

    // --- Popover/Dropdown Setup ---
    const isPopover = input.getAttribute('data-bs-toggle') === 'bb.dropdown';
    if (isPopover) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' });
    } else {
        const extraClass = input.getAttribute('data-bs-custom-class');
        if (extraClass) menu.classList.add(...extraClass.split(' '));
        const offset = input.getAttribute('data-bs-offset');
        if (offset) {
            try {
                const [x, y] = offset.split(',').map(parseFloat);
                if (!isNaN(x) && x !== 0) menu.style.marginLeft = `${x}px`;
                if (!isNaN(y) && y !== 0) menu.style.marginTop = `${y}px`;
            } catch (e) { console.error("Invalid offset format:", offset); }
        }
    }

    // --- Debounce Setup ---
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0', 10);
    const filterDuration = duration > 0 ? duration : 200;

    // Debounced callback for filtering and committing value
    const filterCallback = debounce(async v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        try {
            // Call C# method (renamed in our fix)
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

    // Handles input changes (typing, pasting, IME)
    Input.composition(input, v => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        // Don't push immediate value to C#
        // Trigger debounced C# call
        if (!isPopover) currentAc.el.classList.add('show');
        currentAc.el.classList.add('is-loading');
        filterCallback(v); // Pass current input value 'v'
    });

    // Keydown handler (handles Tab, potentially debounced Arrows/Enter/Esc if duration > 0)
    if (duration > 0) {
        // If debounce is enabled, debounce keydown actions too
        EventHandler.on(input, 'keydown', debounce(e => {
            const currentAc = Data.get(id); // Get context inside debounced function
            if (currentAc) handlerKeydown(currentAc, e); // Pass ac context
        }, duration, e => {
            // Only debounce specific keys if needed (original logic)
            return ['ArrowUp', 'ArrowDown', 'Escape', 'Enter', 'NumpadEnter'].indexOf(e.key) > -1
        }))
    }
    else {
        // If no debounce, handle keydown directly
        EventHandler.on(input, 'keydown', e => {
            const currentAc = Data.get(id);
            if (currentAc) handlerKeydown(currentAc, e); // Pass ac context
        })
    }

    // Keyup handler (handles actions after key is released)
    EventHandler.on(input, 'keyup', e => {
        const currentAc = Data.get(id);
        if (currentAc) handlerKeyup(currentAc, e); // Pass ac context
    });

    // Blur trigger function (with delay)
    ac.triggerBlur = () => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        setTimeout(() => {
            const currentAcDelayed = Data.get(id);
            if (!currentAcDelayed) return;
            // console.log('[AutoComplete JS] triggerBlur: function called (after delay).');
            if (!isPopover) currentAcDelayed.el.classList.remove('show');
            const shouldTriggerCsharp = currentAcDelayed.input.getAttribute('data-bb-blur') === 'true';
            // console.log(`[AutoComplete JS] triggerBlur: Should call C#? ${shouldTriggerCsharp}`);
            if (shouldTriggerCsharp) {
                try {
                    // console.log('[AutoComplete JS] triggerBlur: Attempting invokeMethodAsync("TriggerBlur")...');
                    currentAcDelayed.invoke.invokeMethodAsync('TriggerBlur');
                    // console.log('[AutoComplete JS] triggerBlur: invokeMethodAsync("TriggerBlur") called.');
                } catch (error) {
                    if (!error.message || !error.message.includes("instance is already disposed")) {
                        console.error('[AutoComplete JS] triggerBlur: Error invoking C# TriggerBlur.', error);
                    }
                }
            }
        }, 150);
    };

    // Dropdown item click handler
    EventHandler.on(menu, 'click', '.dropdown-item:not(.disabled)', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        // Let the natural blur event handle calling triggerBlur
        if (currentAc.popover) currentAc.popover.hide();
        else currentAc.el.classList.remove('show');
        // C# OnClickItem handles state update and calls JS setValue
    });

    // Focus handler
    EventHandler.on(input, 'focus', e => {
        const currentAc = Data.get(id);
        if (!currentAc) return;
        const showDropdownOnFocus = currentAc.input.getAttribute('data-bb-auto-dropdown-focus') === 'true';
        if (showDropdownOnFocus) {
            if (isPopover) { if (currentAc.popover) currentAc.popover.show(); }
            else currentAc.el.classList.add('show');
        }
    });

    // Standard blur event listener
    EventHandler.on(input, 'blur', e => {
        const currentAc = Data.get(id);
        if (currentAc) currentAc.triggerBlur();
    });

    // REMOVED: Change handler (Input.composition is used)
    // EventHandler.on(input, 'change', e => { ... });

    // Click outside handler
    ac.closePopover = e => {
        const targetAc = e.target.closest('.auto-complete');
        if (targetAc && targetAc.getAttribute('id') === id) return;
        document.querySelectorAll('.auto-complete.show, .auto-complete .popover.show').forEach(visibleAc => {
            const visibleId = visibleAc.getAttribute('id');
            if (visibleId === id) return;
            if (!targetAc || visibleAc !== targetAc) {
                const data = Data.get(visibleId);
                if (data) {
                    if (data.popover) data.popover.hide();
                    else visibleAc.classList.remove('show');
                }
            }
        });
    };

    // Register module
    registerBootstrapBlazorModule('AutoComplete', id, () => {
        EventHandler.on(document, 'click', ac.closePopover);
    });
}

// Keyup Handler (from latest code)
const handlerKeyup = (ac, e) => {
    const key = e.key;
    const { el, input, invoke, menu } = ac;

    if (key === 'Enter' || key === 'NumpadEnter') {
        const skipEnter = input.getAttribute('data-bb-skip-enter') === 'true';
        if (!skipEnter) {
            const current = menu.querySelector('.active');
            if (current !== null && (el.classList.contains('show') || ac.popover?.isShown())) {
                current.click(); // C# OnClickItem calls JS setValue
            } else {
                // C# EnterCallback calls JS setValue
                invoke.invokeMethodAsync('EnterCallback', input.value);
            }
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
        // Removed explicit TriggerDeleteCallback call
    }
};

// Keydown handler (from latest code - only handles Tab)
const handlerKeydown = (ac, e) => { // Accept ac context
    if (e.key === 'Tab') {
        // Let blur handler manage the triggerBlur call
        // ac.triggerBlur();
    }
};

// *** NEW FUNCTION: Called from C# to set the input value ***
export function setValue(id, value) {
    const ac = Data.get(id);
    if (ac && ac.input) {
        ac.input.value = value ?? "";
    } else {
        const inputElement = document.getElementById(`${id}_input`);
        if (inputElement) inputElement.value = value ?? "";
    }
}

// Function to show the dropdown list programmatically
export function showList(id) {
    const ac = Data.get(id);
    if (ac) {
        if (ac.popover) ac.popover.show();
        else ac.el.classList.add('show');
    }
}

// Dispose function to clean up resources and event listeners
export function dispose(id) {
    const ac = Data.get(id);
    Data.remove(id);
    if (ac) {
        const { popover, input, menu, closePopover } = ac;
        if (popover) Popover.dispose(popover);
        // Remove all listeners added in init
        EventHandler.off(input, 'focus');
        EventHandler.off(input, 'keydown');
        EventHandler.off(input, 'keyup');
        EventHandler.off(input, 'blur'); // Remove blur listener
        EventHandler.off(menu, 'click');
        EventHandler.off(document, 'click', closePopover);
        Input.dispose(input); // Ensure this removes composition listeners
        const bb = window.BootstrapBlazor;
        if (bb && bb.AutoComplete && typeof bb.AutoComplete.dispose === 'function') {
            bb.AutoComplete.dispose(id);
        }
    }
}

// Helper to scroll an item into view
const scrollIntoView = (el, item) => {
    const input = el.querySelector('input');
    const behavior = input?.getAttribute('data-bb-scroll-behavior') ?? 'smooth';
    item.scrollIntoView({ behavior: behavior, block: "nearest", inline: "start" });
};

// Export base input functions if needed
export { handleKeyUp, select, selectAllByFocus, selectAllByEnter };
