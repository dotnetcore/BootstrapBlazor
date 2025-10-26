import { getTheme, setTheme } from "../../_content/BootstrapBlazor/modules/utility.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

function initTheme() {
    const currentTheme = getTheme();
    setTheme(currentTheme, false);
}

export function doTask(invoke) {
    initTheme();

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
    const handler = setTimeout(async () => {
        clearTimeout(handler);
        await invoke.invokeMethodAsync("ShowVoteToast");
    }, 10000);

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
