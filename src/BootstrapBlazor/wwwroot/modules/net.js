import EventHandler from "./event-handler.js";
import { registerBootstrapBlazorModule } from './utility.js'

export function init(options) {
    const { invoke, onNetworkStateChangedCallback } = options;
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
        const indicators = [...document.querySelectorAll('.bb-nt-indicator')];
        indicators.forEach(indicator => {
            indicator.classList.remove('offline');
        });
    }
    const offlineStateChanged = () => {
        const indicators = [...document.querySelectorAll('.bb-nt-indicator')];
        indicators.forEach(indicator => {
            indicator.classList.add('offline');
        });
    }

    registerBootstrapBlazorModule("NetworkMonitor", null, () => {
        EventHandler.on(window, 'online', onlineStateChanged);
        EventHandler.on(window, 'offline', offlineStateChanged);
    });

    updateState(navigator.connection);
}
