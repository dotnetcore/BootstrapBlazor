import Data from "../../modules/data.js"

export function init(id, invoke, callback) {
    const handler = e => {
        invoke.invokeMethodAsync(callback, e.data)
    }
    Data.set(id, handler)

    window.addEventListener('message', handler);
    const frame = document.getElementById(id);

    frame.onload = () => {
        invoke.invokeMethodAsync("TriggerLoaded");
    }
}

export async function execute(id, data) {
    const frame = document.getElementById(id);
    frame.contentWindow.postMessage(data);
}

export function dispose(id) {
    const handler = Data.get(id)
    Data.remove(id)

    if (handler) {
        window.removeEventListener('message', handler);
    }
}
