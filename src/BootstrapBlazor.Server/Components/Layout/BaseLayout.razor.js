import { getTheme, setTheme } from "../../_content/BootstrapBlazor/modules/utility.js"

export function initTheme() {
    const currentTheme = getTheme();
    setTheme(currentTheme, false);
}
