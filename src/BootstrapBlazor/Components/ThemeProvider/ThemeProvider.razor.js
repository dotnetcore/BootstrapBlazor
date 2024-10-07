import { getPreferredTheme, setTheme, switchTheme } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"
import Data from "../../modules/data.js"

export function init(id, invoke, themeValue, callback) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const theme = { el };
    Data.set(id, theme);

    const darkModeMediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
    theme.mediaQueryList = darkModeMediaQuery;
    EventHandler.on(darkModeMediaQuery, 'change', () => {
        if (Data.get('currentTheme') === 'auto') {
            switchTheme('auto');
        }
    });

    let currentTheme = themeValue;
    if (currentTheme === 'useLocalStorage') {
        currentTheme = getPreferredTheme();
    }
    theme.currentTheme = currentTheme;
    setTheme(currentTheme, true);

    EventHandler.on(el, 'click', '.dropdown-item', e => {
        const activeTheme = e.delegateTarget.getAttribute('data-bb-theme-value');
        theme.currentTheme = activeTheme;
        switchTheme(activeTheme, window.innerWidth, 0);
        if (callback) {
            invoke.invokeMethodAsync(callback, activeTheme);
        }
    });
}

export function dispose(id) {
    const theme = Data.get(id);
    if (theme === null) {
        return;
    }
    Data.remove(id);

    const { el, darkModeMediaQuery } = theme;
    EventHandler.off(el, 'click');
    EventHandler.off(darkModeMediaQuery, 'change');
}
