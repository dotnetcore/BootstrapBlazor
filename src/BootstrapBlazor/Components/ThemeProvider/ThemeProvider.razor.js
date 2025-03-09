import { getPreferredTheme, setTheme, switchTheme, calcCenterPosition } from "../../modules/utility.js"
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
    EventHandler.on(darkModeMediaQuery, 'change', () => changeTheme(id));
    theme.mediaQueryList = darkModeMediaQuery;

    let currentTheme = themeValue;
    if (currentTheme === 'useLocalStorage') {
        currentTheme = getPreferredTheme();
    }
    setTheme(currentTheme, true);
    theme.currentTheme = currentTheme;

    EventHandler.on(el, 'click', '.dropdown-item', e => {
        const activeTheme = e.delegateTarget.getAttribute('data-bb-theme-value');
        theme.currentTheme = activeTheme;

        const rect = calcCenterPosition(el);
        switchTheme(activeTheme, rect.x, rect.y);
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

const changeTheme = id => {
    const theme = Data.get(id);
    if (theme === null) {
        return;
    }

    if (theme.currentTheme === 'auto') {
        switchTheme('auto', window.innerWidth, 0);
    }
}
