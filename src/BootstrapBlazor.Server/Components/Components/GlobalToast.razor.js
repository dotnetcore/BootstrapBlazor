import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(invoke, method, debug) {
    const localstorageKey = 'bb-g-toast'
    const v = localStorage.getItem(localstorageKey);
    if (v) {
        try {
            const differ = new Date().getTime() - v;
            if (differ < 86400000) {
                return;
            }
        }
        catch {
            localStorage.removeItem(localstorageKey);
        }
    }

    if (debug !== true) {
        const handler = setTimeout(async () => {
            clearTimeout(handler);
            await invoke.invokeMethodAsync(method);
        }, 10000);
    }

    EventHandler.on(document, 'click', '#bb-g-toast', e => {
        const toast = e.delegateTarget.closest('.toast');
        if (toast) {
            toast.classList.remove('show');

            localStorage.setItem(localstorageKey, new Date().getTime());
        }
    });
}

export function dispose() {
    EventHandler.off(document, 'click', '#bb-g-toast');
}
