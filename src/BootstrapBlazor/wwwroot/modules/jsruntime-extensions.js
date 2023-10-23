import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version"

export function dispose(guid) {
    const hp = Data.get(guid)
    Data.remove(guid)

    if (hp) {
        EventHandler.off(hp.target, hp.eventName, hp.handler)
    }
}

export function addEventListener(interop, invokeMethodName, eventName, id) {
    const hp = {
        eventName,
        handler: () => {
            interop.invokeMethodAsync(invokeMethodName);
        },
        id,
        target: window,
    }

    Data.set(id, hp)
    EventHandler.on(hp.target, hp.eventName, hp.handler)
}

export function getProperties(obj, tag) {
    try {
        let tags = tag.split('.');
        let tagsCopy = JSON.parse(JSON.stringify(tags));
        var object = obj;
        tagsCopy.map(() => {
            object = object[tags[0]];
            tags.shift();
        })
        return object;
    } catch (e) {
        console.warn("C# Class JSModuleExtensions.Invoke JS Function getProperties Error:" + e);
        return null;
    }
}

export function runEval(code) {
    try {
        return eval(code);
    } catch (e) {
        console.warn("C# Class JSModuleExtensions.Invoke JS Function runEval Error:" + e);
        return e.message;
    }
}

export function getElementProperties(id, tag) {
    var el = document.getElementById(id);
    if (el) {
        return getProperties(el, tag);
    }
}

export function doConsole(type, arg) {
    try {
        const fn = (typeof console[type] === 'function' ? console[type] : console.log);
        fn(...arg);
    } catch (e) {
        console.error("C# Class JSModuleExtensions.Console Invoke JS Function doConsole Error:" + e);
    }
}

export function changeMeta(isAdd, type, rel, href) {
    try {
        var head = document.getElementsByTagName('head')[0];
        var links = head.getElementsByTagName(type);

        // 检查是否存在相同项
        for (var i = 0; i < links.length; i++) {
            if (links[i].rel === rel && links[i].href === href) {
                // 是否执行移除操作
                if (!isAdd) {
                    head.removeChild(links[i])
                }
                return true;
            }
        }

        // 是否执行添加操作
        if (isAdd) {
            // 创建并添加新项
            var link = document.createElement(type);
            link.rel = rel;
            link.href = href;
            head.appendChild(link);
            return true;
        } else {
            return false;
        }

    } catch (e) {
        return false;
    }
}

export function isMobileDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}
