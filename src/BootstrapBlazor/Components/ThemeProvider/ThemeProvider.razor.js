import { setTheme, getPreferredTheme, setActiveTheme, switchTheme } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"
import Data from "../../modules/data.js"

const darkModeMediaQuery = window.matchMedia('(prefers-color-scheme: dark)');

export function init(id, invoke, themeValue, callback) {
    console.log(id)
    const el = document.getElementById(id);
    console.log(el)
    if (el) {
        console.log(themeValue);
        Data.set('theme', el);
        darkModeMediaQuery.addEventListener('change', handleDarkModeChange);
        let currentTheme = themeValue;
        if (currentTheme === 'useLocalStorage'){
            currentTheme = getPreferredTheme();
        }
        Data.set('currentTheme', currentTheme);
        console.log(currentTheme);
        setTheme(currentTheme, false);
        const activeItem = el.querySelector(`.dropdown-item[data-bb-theme-value="${currentTheme}"]`);
        if (activeItem) {
            setActiveTheme(el, activeItem);
        }

        const items = el.querySelectorAll('.dropdown-item');
        items.forEach(item => {
            EventHandler.on(item, 'click', () => {
                setActiveTheme(el, item);

                let theme = item.getAttribute('data-bb-theme-value');
                Data.set('currentTheme', theme);
                switchTheme(theme, window.innerWidth, 0);
                if (callback) {
                    invoke.invokeMethodAsync(callback, theme);
                }
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
    darkModeMediaQuery.removeEventListener('change', handleDarkModeChange);
    Data.remove('theme');
    Data.remove('currentTheme');
}

function handleDarkModeChange(e) {
    console.log(Data.get('currentTheme'))
    if (Data.get('currentTheme') === 'auto') {
        switchTheme('auto');
        if (e.matches) {
            // 现在是暗模式
            console.log('Dark mode is now enabled');

        } else {
            // 现在是明亮模式
            console.log('Light mode is now enabled');
        }
    }
}
