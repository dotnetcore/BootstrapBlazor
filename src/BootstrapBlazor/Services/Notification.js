export function check(invoke, callback, requestPermission) {
    if ((!window.Notification && !navigator.mozNotification) || !window.FileReader || !window.history.pushState) {
        console.warn("Your browser does not support all features of this API")
        if (invoke !== null && callback !== '') invoke.invokeMethodAsync(callback, false)
    }
    else if (Notification.permission === "granted") {
        if (invoke !== null && callback !== '') invoke.invokeMethodAsync(callback, true)
    }
    else if (requestPermission && (Notification.permission !== 'denied' || Notification.permission === "default")) {
        Notification.requestPermission(permission => {
            var granted = permission === "granted"
            if (invoke !== null && callback !== '') invoke.invokeMethodAsync(callback, granted)
        })
    }
}

export function notify(invoke, callback, model) {
    var ret = false
    check(null, null, true)
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
                invoke.invokeMethodAsync(onClickCallback)
            }
        }
        ret = true
    }
    if (invoke !== null && callback !== null) invoke.invokeMethodAsync(callback, ret)
    return ret
}
