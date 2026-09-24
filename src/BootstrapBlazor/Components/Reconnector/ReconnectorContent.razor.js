import EventHandler from "../../modules/event-handler.js"
import Data from "../../modules/data.js"

export function init(id, interval) {
    dispose(id);

    const modal = document.getElementById('components-reconnect-modal');
    if (modal === null) {
        return;
    }

    let reloadHandler;
    let reloadInProgress = false;
    let disposed = false;

    const stopReloading = () => {
        if (reloadHandler !== undefined) {
            clearInterval(reloadHandler);
            reloadHandler = undefined;
        }
    };

    const attemptReload = async () => {
        if (disposed || reloadInProgress) {
            return;
        }

        try {
            const response = await fetch('', { cache: 'no-store' });
            if (!disposed && response.ok) {
                reloadInProgress = true;
                stopReloading();
                location.reload();
            }
        }
        catch {
        }
    };

    const startReloading = () => {
        if (reloadHandler !== undefined) {
            return;
        }

        reloadHandler = setInterval(attemptReload, interval);
        void attemptReload();
    };

    const updateReloading = () => {
        if (modal.classList.length === 0 || modal.classList.contains('components-reconnect-hide')) {
            stopReloading();
        }
        else {
            startReloading();
        }
    };

    const stateChanged = event => {
        if (event.detail?.state === 'hide') {
            stopReloading();
        }
        else {
            startReloading();
        }
    };

    const observer = new MutationObserver(updateReloading);
    observer.observe(modal, { attributes: true, attributeFilter: ['class'] });
    EventHandler.on(modal, 'components-reconnect-state-changed.bb.reconnector', stateChanged);

    Data.set(id, {
        modal,
        observer,
        stateChanged,
        dispose: () => {
            disposed = true;
            stopReloading();
        }
    });
    updateReloading();
}

export function dispose(id) {
    const reconnector = Data.get(id);
    if (reconnector === null) {
        return;
    }

    Data.remove(id);
    reconnector.observer.disconnect();
    EventHandler.off(reconnector.modal, 'components-reconnect-state-changed.bb.reconnector', reconnector.stateChanged);
    reconnector.dispose();
}
