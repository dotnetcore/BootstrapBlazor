import Data from "./data.js"
import EventHandler from "./event-handler.js";

export function init(id, options) {
    const { invoke, onlineStateChangedCallback, onNetworkStateChangedCallback } = options;
    navigator.connection.onchange = e => {
        const nt = e.target;
        const { downlink, effectiveType, rtt } = nt;
        invoke.invokeMethodAsync(onNetworkStateChangedCallback, {
            downlink, networkType: effectiveType, rTT: rtt
        });
    }

    const onlineStateChanged = () => {
        invoke.invokeMethodAsync(onlineStateChangedCallback, true);
    }
    const offlineStateChanged = () => {
        invoke.invokeMethodAsync(onlineStateChangedCallback, false);
    }
    EventHandler.on(window, 'online', onlineStateChanged);
    EventHandler.on(window, 'offline', offlineStateChanged);

    Data.set(id, {
        onlineStateChanged,
        offlineStateChanged
    })
}

export async function dispose(id) {
    var nt = Data.get(id);
    Data.remove(id);

    if (nt) {
        const { onlineStateChanged, offlineStateChanged } = nt;
        EventHandler.off(window, 'online', onlineStateChanged);
        EventHandler.off(window, 'offline', offlineStateChanged);
    }
}
