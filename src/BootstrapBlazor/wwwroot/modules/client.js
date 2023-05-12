import * as browser from './browser.js?v=$version'
import { execute } from './ajax.js?v=$version'

export async function ping(url, invoke, method) {
    var info = window.browser()
    var data = {
        Browser: info.browser + ' ' + info.version,
        Device: info.device,
        Language: info.language,
        Engine: info.engine,
        UserAgent: navigator.userAgent,
        Os: info.system + ' ' + info.systemVersion
    }

    const result = await execute({
        method: 'GET',
        url
    })
    await invoke.invokeMethodAsync(method, result.Id, result.Ip, data.Os, data.Browser, data.Device, data.Language, data.Engine, data.UserAgent)
}
