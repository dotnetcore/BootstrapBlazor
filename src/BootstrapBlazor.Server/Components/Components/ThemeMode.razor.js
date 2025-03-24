import { getTheme, switchTheme, calcCenterPosition } from "../../_content/BootstrapBlazor/modules/utility.js"
import EventHandler from "../../_content/BootstrapBlazor/modules/event-handler.js"

export function init(id, invoke, method) {
    const el = document.getElementById(id);
    if (el) {
        EventHandler.on(el, 'click', e => {
            let theme = getTheme();
            if (theme === 'dark') {
                theme = 'light';
            }
            else {
                theme = 'dark';
            }

            const rect = calcCenterPosition(el);
            switchTheme(theme, rect.x, rect.y);
            invoke.invokeMethodAsync(method, theme);
        });
    }
}

export function dispose(id) {
    const el = document.getElementById(id);

    if (el) {
        EventHandler.off(el, 'click');
    }
}
