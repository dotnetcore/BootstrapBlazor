import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(invoke, debug) {
    const v = localStorage.getItem('bb-gitee-vote');
    if (v) {
        try {
            const differ = new Date().getTime() - v;
            if (differ < 86400000) {
                return;
            }
        }
        catch {
            localStorage.removeItem('bb-gitee-vote');
        }
    }

    if (debug !== true) {
        const handler = setTimeout(async () => {
            clearTimeout(handler);
            await invoke.invokeMethodAsync("ShowVoteToast");
        }, 10000);
    }

    EventHandler.on(document, 'click', '#bb-gitee-vote', e => {
        const toast = e.delegateTarget.closest('.toast');
        if (toast) {
            toast.classList.remove('show');

            localStorage.setItem('bb-gitee-vote', new Date().getTime());
        }
    });
}

export function dispose() {
    EventHandler.off(document, 'click', '#bb-gitee-vote');
}
