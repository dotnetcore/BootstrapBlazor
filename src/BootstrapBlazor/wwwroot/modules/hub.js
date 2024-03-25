import { getFingerCode } from "./utility.js?v=$version"

export async function init(options) {
    const { invoke, method, interval = 3000 } = options;
    const code = getFingerCode();

    setInterval(() => {
        invoke.invokeMethodAsync(method, code);
    }, interval);
}
