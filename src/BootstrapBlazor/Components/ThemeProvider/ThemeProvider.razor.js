import { getPreferredTheme, getAutoThemeValue, setTheme } from "../../modules/theme.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id);
    if (el) {
        const activeTheme = getPreferredTheme();
        const activeItem = el.querySelector(`.dropdown-item[data-bb-theme-value="${activeTheme}"]`);
        if (activeItem) {
            activeItem.classList.add('active');
            setActiveTheme(el, activeItem);
        }

        const items = el.querySelectorAll('.dropdown-item');
        items.forEach(item => {
            EventHandler.on(item, 'click', () => {
                let theme = item.getAttribute('data-bb-theme-value');
                if (theme === 'auto') {
                    theme = getAutoThemeValue();
                }
                setTheme(theme);

                const activeThemeItem = el.querySelector('.active');
                if (activeThemeItem) {
                    activeThemeItem.classList.remove('active');
                }
                item.classList.add('active');
                setActiveTheme(el, item);
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

const setActiveTheme = (el, activeItem) => {
    const iconItem = activeItem.querySelector('[data-bb-theme-icon]');
    if (iconItem) {
        const icon = iconItem.getAttribute('data-bb-theme-icon');
        if (icon) {
            const toggleIcon = el.querySelector('.bb-theme-mode-active');
            if (toggleIcon) {
                toggleIcon.outerHTML = `<i class="${icon} bb-theme-mode-active"></i>`;
            }
        }
    }
}
