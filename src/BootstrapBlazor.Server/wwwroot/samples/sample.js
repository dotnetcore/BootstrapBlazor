import { addLink, addScript, getTheme } from '../../BootstrapBlazor/modules/utility.js'
import Data from '../../BootstrapBlazor/modules/data.js'
import EventHandler from "../../BootstrapBlazor/modules/event-handler.js"

export async function init(id, invoke, options) {
    await addLink('./_content/BootstrapBlazor.JitViewer/jit-viewer.css');
    await addScript('./_content/BootstrapBlazor.JitViewer/lib/jit-viewer.min.js');

    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    if (options.locale.startsWith('en')) {
        options.locale = 'en';
    }

    if (options.theme === 'auto') {
        options.theme = getTheme();
    }

    const { createViewer } = window.JitViewer;
    const viewer = createViewer({
        target: el,
        ...options
    });
    viewer.mount();

    Data.set(id, {
        el,
        invoke,
        viewer
    });

    const updateTheme = e => viewer.setTheme(e.theme);
    EventHandler.on(document, 'changed.bb.theme', updateTheme);
}

export function setFile(id, file, fileName) {
    const data = Data.get(id);
    if (data === null) {
        return;
    }

    const { viewer } = data;
    if (viewer) {
        viewer.setFile(file, fileName);
    }
}

export function dispose(id) {
    const data = Data.get(id);
    if (data === null) {
        return;
    }

    const { viewer } = data;
    if (viewer) {
        viewer.destroy();
    }
}
