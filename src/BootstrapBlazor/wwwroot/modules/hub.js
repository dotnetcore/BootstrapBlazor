import "./browser.js?v=$version"
import { execute } from "./ajax.js?v=$version"
import { getFingerCode } from "./utility.js?v=$version"

export async function init(options) {
    const { invoke, method, interval = 3000 } = options;
    const info = browser()
    let data = {
        browser: info.browser + ' ' + info.version,
        device: info.device,
        language: info.language,
        engine: info.engine,
        userAgent: navigator.userAgent,
        os: info.system + ' ' + info.systemVersion
    }
    const result = await execute({
        method: 'GET',
        url: './ip.axd'
    });
    data.id = getFingerCode();
    data.ip = result.ip;

    setInterval(() => {
        invoke.invokeMethodAsync(method, data);
    }, interval);
}
