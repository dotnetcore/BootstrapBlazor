import { getClientInfo } from "./client.js?v=$version"
import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version";

export async function init(id, options) {
    const { invoke, method, interval = 3000, url, connectionId } = options;
    const localStorageElKey = 'bb_hub_el_id';
    if (localStorage.getItem(localStorageElKey) === null) {
        localStorage.setItem(localStorageElKey, id);
    }

    const localStorageConnectionIdKey = 'bb_hub_connection_id';
    let clientId = localStorage.getItem(localStorageConnectionIdKey);
    if (clientId === null) {
        localStorage.setItem(localStorageConnectionIdKey, connectionId);
        clientId = connectionId;
    }

    const hubs = [];
    const chanel = new BroadcastChannel('bb_hubs_chanel');
    EventHandler.on(chanel, 'message', e => {
        const { id, type } = e.data;
        if (type === 'ping' && hubs.find(v => v === id) === void 0) {
            hubs.push(id);
        }
        else if (type === 'dispose') {
            const index = hubs.indexOf(id);
            if (index > -1) {
                hubs.splice(index, 1);
            }
            if (localStorage.getItem(localStorageElKey) === id) {
                localStorage.removeItem(localStorageElKey);
            }
        }
    });

    const info = await getClientInfo(url);
    info.id = clientId;

    const callback = async () => {
        chanel.postMessage({ id, type: 'ping' });
        let hubId = localStorage.getItem(localStorageElKey);

        if (hubId === null || hubs.length === 0) {
            localStorage.setItem(localStorageElKey, id);
            hubId = id;
        }
        if (hubId === id) {
            await invoke.invokeMethodAsync(method, info);
        }
    }
    await callback();

    const handler = setInterval(async () => {
        await callback();
    }, interval);

    window.addEventListener('unload', () => {
        dispose(id);
    });

    const hub = { handler, chanel, connectionId, hubs, localStorageConnectionIdKey, localStorageElKey };
    Data.set(id, hub);
}

export async function dispose(id) {
    const hub = Data.get(id);

    if (hub) {
        clearInterval(hub.handler);
        hub.chanel.postMessage({ id, type: 'dispose' });
        hub.chanel.close();

        if (hub.hubs.length === 0) {
            localStorage.removeItem(hub.localStorageConnectionIdKey)
            localStorage.removeItem(hub.localStorageElKey)
        }
    }
}
