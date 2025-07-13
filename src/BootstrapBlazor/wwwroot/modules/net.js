import Data from "./data.js"
import EventHandler from "./event-handler.js";

export function init(id, options) {
    const { invoke, onlineStateChangedCallback, onNetworkStateChangedCallback, indicators } = options;
    const updateState = nt => {
        const { downlink, effectiveType, rtt } = nt;
        invoke.invokeMethodAsync(onNetworkStateChangedCallback, {
            downlink, networkType: effectiveType, rTT: rtt
        });
    }
    navigator.connection.onchange = e => {
        updateState(e.target);
    }

    const onlineStateChanged = () => {
        if (Array.isArray(indicators)) {
            indicators.forEach(indicator => {
                const el = document.getElementById(indicator);
                if (el) {
                    el.classList.remove('offline');
                }
            });
        }
        invoke.invokeMethodAsync(onlineStateChangedCallback, true);
    }
    const offlineStateChanged = () => {
        if (Array.isArray(indicators)) {
            indicators.forEach(indicator => {
                const el = document.getElementById(indicator);
                if (el) {
                    el.classList.add('offline');
                }
            });
        }
        invoke.invokeMethodAsync(onlineStateChangedCallback, false);
    }
    EventHandler.on(window, 'online', onlineStateChanged);
    EventHandler.on(window, 'offline', offlineStateChanged);

    Data.set(id, {
        onlineStateChanged,
        offlineStateChanged
    });

    updateState(navigator.connection);
}

export async function dispose(id) {
    const nt = Data.get(id);
    Data.remove(id);

    if (nt) {
        const { onlineStateChanged, offlineStateChanged } = nt;
        EventHandler.off(window, 'online', onlineStateChanged);
        EventHandler.off(window, 'offline', offlineStateChanged);
    }
}
