export function check(requestPermission) {
    let async = false
    let granted = null

    if ((!window.Notification && !navigator.mozNotification) || !window.FileReader || !window.history.pushState) {
        console.warn("Your browser does not support all features of this API")
        granted = false
    }
    else if (Notification.permission === "granted") {
        granted = true
    }
    else if (requestPermission && (Notification.permission !== 'denied' || Notification.permission === "default")) {
        async = true
        Notification.requestPermission(permission => {
            granted = permission === "granted"

        })
    }
    return new Promise((resolve, reject) => {
        if (async) {
            const handler = setInterval(() => {
                if (granted != null) {
                    clearInterval(handler)
                    resolve(granted)
                }
            }, 20)
        }
        else {
            resolve(granted || false)
        }
    })
}

export async function notify(invoke, callback, model) {
    let ret = false
    if(await check(true)) {
        if (model.title !== null) {
            const options = {}
            if (model.message !== null) options.body = model.message.substring(0, 250)
            if (model.icon !== null) options.icon = model.icon
            if (model.silent !== null) options.silent = model.silent
            if (model.sound !== null) options.sound = model.sound
            const notification = new Notification(model.title.substring(0, 100), options)
            notification.onclick = e => {
                e.preventDefault()
                invoke.invokeMethodAsync(callback, model.id)
            }
            ret = true
        }
    }
    return ret
}
