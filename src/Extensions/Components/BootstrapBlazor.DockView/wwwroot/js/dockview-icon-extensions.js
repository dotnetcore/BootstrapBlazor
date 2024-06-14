import Data from '../../BootstrapBlazor/modules/data.js'

const getIcon = (name, hasTitle = true) => {
    const icons = initIcons();

    let icon = null;
    const control = icons.find(i => i.name === name);
    if (control) {
        icon = control.icon.cloneNode(true);
        if (!hasTitle) {
            icon.removeAttribute('title');
        }
    }
    return icon;
}

const initIcons = () => {
    let icons = Data.get('dockview-v2');
    if (icons === null) {
        icons = ['bar', 'dropdown', 'lock', 'unlock', 'down', 'full', 'restore', 'float', 'dock', 'close'].map(v => {
            return {
                name: v,
                icon: document.querySelector(`template > .bb-dockview-control-icon-${v}`)
            };
        });
        Data.set('dockview-v2', icons);
    }
    return icons;
}

export { getIcon };
