import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import { cerateDockview } from '../js/dockview-utils.js'
import Data from '../../BootstrapBlazor/modules/data.js'

export async function init(id, invoke, options) {
    await addLink("./_content/BootstrapBlazor.DockView/css/dockview-bb.css")
    const el = document.getElementById(id);
    if (!el) {
        return;
    }

    const dockview = cerateDockview(el, options)
    Data.set(id, { el, dockview });

    dockview.on('initialized', () => {
        invoke.invokeMethodAsync(options.initializedCallback);
    })
    dockview.on('lockChanged', ({ title, isLock }) => {
        invoke.invokeMethodAsync(options.lockChangedCallback, title, isLock);
    })
    dockview.on('panelClosed', title => {
        invoke.invokeMethodAsync(options.panelClosedCallback, title);
    })
}

export function update(id, options) {
    const dock = Data.get(id)
    if (dock) {
        const { dockview } = dock;
        dockview.update(options);
    }
}

export function reset(id, options) {
    const dock = Data.get(id)
    if (dock) {
        const { dockview } = dock;
        dockview.reset(options);
    }
}

export function save(id) {
    let ret = '';
    const dock = Data.get(id)
    if (dock) {
        const { dockview } = dock;
        ret = dockview.saveLayout();
    }
    return ret;
}

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id);

    if (dock) {
        const { dockview } = dock;
        if (dockview) {
            dockview.dispose();
        }
    }
}
