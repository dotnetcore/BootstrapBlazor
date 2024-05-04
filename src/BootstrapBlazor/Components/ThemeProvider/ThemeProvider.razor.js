export function init() {
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
