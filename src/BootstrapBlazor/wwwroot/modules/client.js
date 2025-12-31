import browser from "./browser.min.mjs"
import { execute } from "./ajax.js"

export async function ping(url, invoke, method) {
    const data = await getClientInfo(url);
    await invoke.invokeMethodAsync(method, data)
}

export async function getClientInfo(url) {
    const info = await browser.getInfo(['browser', 'system', 'device', 'language']);
    let data = {
        browser: info.browser + ' ' + info.browserVersion,
        device: info.device,
        language: info.language,
        engine: info.engine,
        userAgent: navigator.userAgent,
        os: info.system + ' ' + info.systemVersion
    }

    const result = await execute({
        method: 'GET',
        url
    });
    if (result) {
        data.ip = result.Ip;
    }
    data.id = localStorage.getItem('bb_hub_connection_id') || result.Id;
    return data;
}
