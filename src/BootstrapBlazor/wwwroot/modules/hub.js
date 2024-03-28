import { getFingerCode } from "./utility.js?v=$version"
import Data from "./data.js?v=$version"

export async function init(id, options) {
    const { invoke, method, interval = 3000 } = options;

    if (localStorage.getItem('bootstrapblazor_hub_id') === null) {
        localStorage.setItem('bootstrapblazor_hub_id', id);
    }

    const handler = setInterval(async () => {
        const hubId = localStorage.getItem('bootstrapblazor_hub_id');

        if (hubId === id) {
            await invoke.invokeMethodAsync(method, hub.code);
        }
    }, interval);

    window.addEventListener('unload', () => localStorage.removeItem('bootstrapblazor_hub_id'));

    const hub = { code: getFingerCode(), handler };
    Data.set(id, hub);
}

export async function dispose(id) {
    const hub = Data.get(id);

    if (hub) {
        clearInterval(hub.handler);
        localStorage.removeItem('bootstrapblazor_hub_id');
    }
}
