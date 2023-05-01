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
    var ret = false
    await check(true)
    if (model.title !== null) {
        var onClickCallback = model.onClick
        var options = {}
        if (model.message !== null) options.body = model.message.substr(0, 250)
        if (model.icon !== null) options.icon = model.icon
        if (model.silent !== null) options.silent = model.silent
        if (model.sound !== null) options.sound = model.sound
        var notification = new Notification(model.title.substr(0, 100), options)
        if (invoke !== null && onClickCallback !== null) {
            notification.onclick = e => {
                e.preventDefault()
                invoke.invokeMethodAsync(callback, model.id)
            }
            notification.onclick = () => {
                invoke.invokeMethodAsync(callback, model.id)
            }
        }
        ret = true
    }
    return ret
}
