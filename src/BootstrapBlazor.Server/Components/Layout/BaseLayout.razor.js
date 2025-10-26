import { getTheme, setTheme } from "../../_content/BootstrapBlazor/modules/utility.js"

function initTheme() {
    const currentTheme = getTheme();
    setTheme(currentTheme, false);
}

export function doTask(invoke) {
    initTheme();
    const handler = setTimeout(() => {
        clearTimeout(handler);
        invoke.invokeMethodAsync("ShowVoteToast");
    }, 1000);
}

export function dispose() {
    
}
