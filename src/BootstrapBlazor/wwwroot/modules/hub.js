import { getClientInfo } from "./client.js?v=$version"
import { getFingerCode } from "./utility.js?v=$version"
import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version";

export async function init(id, options) {
    const { invoke, method, interval = 3000, url } = options;
    const hubs = [];
    const chanel = new BroadcastChannel('bb_hubs_chanel');
    const localStorageKey = 'bb_hub_id';

    if (localStorage.getItem(localStorageKey) === null) {
        localStorage.setItem(localStorageKey, id);
    }
    window.addEventListener('unload', () => {
        chanel.close();
        localStorage.removeItem(localStorageKey);
    });

    EventHandler.on(chanel, 'message', e => {
        const id = e.data;
        if (hubs.find(v => v === id) === void 0) {
            hubs.push(id);
        }
    });

    const info = await getClientInfo(url);
    info.id = getFingerCode();
    const handler = setInterval(async () => {
        chanel.postMessage(id);
        let hubId = localStorage.getItem(localStorageKey);

        if (hubId === null) {
            localStorage.setItem(localStorageKey, id);
            hubId = id;
        }
        console.log(hubId);
        if (hubId === id) {
            await invoke.invokeMethodAsync(method, info);
        }
        else if (hubs.length > 0) {
            const h = hubs.find(v => v === hubId);
            if (h === void 0) {
                localStorage.removeItem(localStorageKey);
            }
        }
    }, interval);

    const hub = { handler, chanel };
    Data.set(id, hub);
}

export async function dispose(id) {
    const hub = Data.get(id);

    if (hub) {
        hub.chanel.close();
        clearInterval(hub.handler);
        localStorage.removeItem('bootstrapblazor_hub_id');
    }
}
