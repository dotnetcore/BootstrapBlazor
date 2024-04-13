import { getClientInfo } from "./client.js"
import Data from "./data.js"
import EventHandler from "./event-handler.js";

export async function init(id, options) {
    const { invoke, method, interval = 3000, url, connectionId } = options;
    const elKey = 'bb_hub_el_id';
    if (localStorage.getItem(elKey) === null) {
        localStorage.setItem(elKey, id);
    }

    const connectionIdKey = 'bb_hub_connection_id';
    let clientId = localStorage.getItem(connectionIdKey);
    if (clientId === null) {
        localStorage.setItem(connectionIdKey, connectionId);
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
            if (localStorage.getItem(elKey) === id) {
                localStorage.removeItem(elKey);
            }
        }
    });

    const info = await getClientInfo(url);
    info.id = clientId;

    const callback = async () => {
        chanel.postMessage({ id, type: 'ping' });

        let hubId = localStorage.getItem(elKey);
        if (hubId === null || hubs.length === 0) {
            localStorage.setItem(elKey, id);
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

    const hub = { handler, chanel };
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
