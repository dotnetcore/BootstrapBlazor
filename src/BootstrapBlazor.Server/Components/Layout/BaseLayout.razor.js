import { getTheme, setTheme } from "../../_content/BootstrapBlazor/modules/utility.js"

function initTheme() {
    const currentTheme = getTheme();
    setTheme(currentTheme, false);
}

export function doTask() {
    initTheme();
}
