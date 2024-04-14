import "./browser.js"
import { execute } from "./ajax.js"

export async function ping(url, invoke, method) {
    const data = await getClientInfo(url);
    await invoke.invokeMethodAsync(method, data)
}

export async function getClientInfo(url) {
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
        url
    });
    if (result) {
        data.ip = result.Ip;
        data.id = result.Id;
    }
    return data;
}
