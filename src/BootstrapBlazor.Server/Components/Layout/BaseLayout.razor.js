import { getPreferredTheme, setTheme } from "../../_content/BootstrapBlazor/modules/utility.js"

export function initTheme() {
    const currentTheme = getPreferredTheme();
    setTheme(currentTheme, false);
}
