import "./browser.js"
import { execute } from "./ajax.js"

export async function ping(url, invoke, method, options) {
    const data = await getClientInfo(url, options);
    await invoke.invokeMethodAsync(method, data)
}

export async function getClientInfo(url, options) {
    const info = browser()
    let data = {
        browser: info.browser + ' ' + info.version,
        device: info.device,
        language: info.language,
        engine: info.engine,
        userAgent: navigator.userAgent,
        os: info.system + ' ' + info.systemVersion
    }

    if (options.enableIpLocator === true) {
        const result = await execute({
            method: 'GET',
            url
        });
        if (result) {
            data.ip = result.Ip;
        }
    }
    data.id = localStorage.getItem('bb_hub_connection_id') ?? result.Id;
    return data;
}
