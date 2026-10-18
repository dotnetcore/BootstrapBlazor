import { getTheme, setTheme } from "../../../BootstrapBlazor/modules/utility.js"

export function initTheme() {
    const currentTheme = getTheme();
    setTheme(currentTheme, false);
}
