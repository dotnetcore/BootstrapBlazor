import Data from "../../modules/data.js"

export function init(id, invoke, options) {
    const { data, triggerPostDataCallback, triggerLoadedCallback } = options;
    const handler = e => {
        invoke.invokeMethodAsync(triggerPostDataCallback, e.data)
    }
    Data.set(id, handler)

    const frame = document.getElementById(id);

    frame.onload = () => {
        invoke.invokeMethodAsync(triggerLoadedCallback);
        window.addEventListener('message', handler);
        if (data) {
            frame.contentWindow.postMessage(data);
        }
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
