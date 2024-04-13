import { getPreferredTheme, setTheme } from "../../_content/BootstrapBlazor/modules/theme.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init() {
    const scrollTop = () => (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop
    let prevScrollTop = 0;
    EventHandler.on(document, 'scroll', () => {
        const items = document.querySelectorAll('.navbar-header, .coms-search')
        const currentScrollTop = scrollTop()
        if (currentScrollTop > prevScrollTop) {
            items.forEach(item => item.classList.add('hide'))
        } else {
            items.forEach(item => item.classList.remove('hide'))
        }
        prevScrollTop = currentScrollTop
    })

    const themeElements = document.querySelectorAll('.icon-theme');
    if (themeElements) {
        themeElements.forEach(el => {
            EventHandler.on(el, 'click', e => {
                let theme = getPreferredTheme();
                if (theme === 'dark') {
                    theme = 'light';
                }
                else {
                    theme = 'dark';
                }
                setTheme(theme);
            });
        });
    }
}

export function dispose() {
    EventHandler.off(document, 'scroll')
}
