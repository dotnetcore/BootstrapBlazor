import { getAutoThemeValue, getPreferredTheme, setActiveTheme, setTheme, saveTheme } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el) {
        const currentTheme = getPreferredTheme();
        const activeItem = el.querySelector(`.dropdown-item[data-bb-theme-value="${currentTheme}"]`);
        if (activeItem) {
            setActiveTheme(el, activeItem);
        }

        const items = el.querySelectorAll('.dropdown-item');
        items.forEach(item => {
            EventHandler.on(item, 'click', () => {
                setActiveTheme(el, item);

                let theme = item.getAttribute('data-bb-theme-value');
                if (theme === 'auto') {
                    theme = getAutoThemeValue();
                }
                setTheme(theme, false);
                saveTheme(theme);
            });
        });
    }
}

export function dispose(id) {
    const el = document.getElementById(id);
    const items = el.querySelectorAll('.dropdown-item');
    items.forEach(item => {
        EventHandler.off(item, 'click');
    });
}
