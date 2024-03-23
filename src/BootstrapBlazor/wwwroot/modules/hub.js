import "./browser.js?v=$version"
import { execute } from "./ajax.js?v=$version"
import { getFingerCode } from "./utility.js?v=$version"

export function init(options) {
    const { invoke, method, interval = 3000 } = options;
    var info = browser()
    var data = {
        Browser: info.browser + ' ' + info.version,
        Device: info.device,
        Language: info.language,
        Engine: info.engine,
        UserAgent: navigator.userAgent,
        Os: info.system + ' ' + info.systemVersion
    }
    setTimeout(() => {
        const code = getFingerCode();
        invoke.invokeMethodAsync(method, code);
    }, interval);
}

export async function ping(url, invoke, method) {

    const result = await execute({
        method: 'GET',
        url
    })
    await invoke.invokeMethodAsync(method, result.Id, result.Ip, data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent)
}
