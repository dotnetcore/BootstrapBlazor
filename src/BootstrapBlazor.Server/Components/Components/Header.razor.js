import { getTheme, switchTheme } from "../../_content/BootstrapBlazor/modules/utility.js"
import Data from "../../_content/BootstrapBlazor/modules/data.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id) {
    const scrollTop = () => (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop
    let prevScrollTop = 0;
    EventHandler.on(document, 'scroll', () => {
        const items = document.querySelectorAll('.navbar-header, .coms-search')
        const currentScrollTop = scrollTop()
        if (currentScrollTop > prevScrollTop) {
            items.forEach(item => item.classList.add('hide'))
        }
        else {
            items.forEach(item => item.classList.remove('hide'))
        }
        prevScrollTop = currentScrollTop
    });

    const themeElement = document.querySelector('.icon-theme');
    if (themeElement) {
        EventHandler.on(themeElement, 'click', e => {
            let theme = getTheme();
            if (theme === 'dark') {
                theme = 'light';
            }
            else {
                theme = 'dark';
            }
            switchTheme(theme, window.innerWidth, window.innerHeight);
        });
    }

    Data.set(id, themeElement);
}

export function dispose(id) {
    EventHandler.off(document, 'scroll');

    const themeElement = Data.get(id);
    Data.remove(id);

    if (themeElement) {
        EventHandler.off(themeElement, 'click');
    }
}
