import "./browser.js?v=$version"
import { execute } from "./ajax.js?v=$version"

export async function ping(url, invoke, method) {
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
    data.id = result.Id;
    data.ip = result.Ip;
    await invoke.invokeMethodAsync(method, data)
}
