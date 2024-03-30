import { getClientInfo } from "./client.js?v=$version"
import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version";

export async function init(id, options) {
    const { invoke, method, interval = 3000, url, connectionId } = options;
    const localStorageKey = 'bb_hub_el_id';
    if (localStorage.getItem(localStorageKey) === null) {
        localStorage.setItem(localStorageKey, id);
    }

    const localStorageConnectionIdKey = 'bb_hub_connection_id';
    let clientId = localStorage.getItem(localStorageConnectionIdKey);
    if (clientId === null) {
        localStorage.setItem(localStorageConnectionIdKey, connectionId);
        clientId = connectionId;
    }
    window.addEventListener('unload', () => {
        dispose(id);
    });

    const hubs = [];
    const chanel = new BroadcastChannel('bb_hubs_chanel');
    EventHandler.on(chanel, 'message', e => {
        const { id, type } = e.data;
        if (type === 'ping' && hubs.find(v => v === id) === void 0) {
            hubs.push(id);
        }
        else if (type === 'dispose') {
            const index = hubs.indexOf(v => v === id);
            if (index > -1) {
                hubs.splice(index, 1);
            }
            if (clientId === connectionId) {
                localStorage.removeItem(localStorageConnectionIdKey);
            }
            if (localStorage.getItem(localStorageKey) === id) {
                localStorage.removeItem(localStorageKey);
            }
        }
    });

    const info = await getClientInfo(url);
    info.id = clientId;
    const handler = setInterval(async () => {
        chanel.postMessage({ id, type: 'ping' });
        let hubId = localStorage.getItem(localStorageKey);

        if (hubId === null) {
            localStorage.setItem(localStorageKey, id);
            hubId = id;
        }
        if (hubId === id) {
            await invoke.invokeMethodAsync(method, info);
        }
        else if (hubs.length > 0) {
            const h = hubs.find(v => v === hubId);
            if (h === void 0) {
                localStorage.removeItem(localStorageKey);
            }
        }
        else {
            localStorage.removeItem(localStorageKey);
        }
    }, interval);

    const hub = { handler, chanel, connectionId };
    Data.set(id, hub);
}

export async function dispose(id) {
    const hub = Data.get(id);

    if (hub) {
        clearInterval(hub.handler);
        hub.chanel.postMessage({ id, type: 'dispose' });
        hub.chanel.close();
    }
}
