import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version"

export function dispose(guid) {
    const hp = Data.get(guid)
    Data.remove(guid)

    if (hp) {
        EventHandler.off(hp.target, hp.eventName, hp.handler)
    }
}

/**
 * @function 事件注册
 * @param {any} interop C# 委托对象
 * @param {any} invokeMethodName C# 委托对象的回调方法
 * @param {any} eventName 要注册的事件名称
 * @param {any} id 元素id
 */
export function addEventListener(interop, invokeMethodName, eventName, id) {
    const hp = {
        eventName,
        handler: () => {
            interop.invokeMethodAsync(invokeMethodName);
        },
        id,
        target: window,
    }

    hp.target = document.getElementById(id);

    Data.set(id, hp)
    EventHandler.on(hp.target, hp.eventName, hp.handler)
}

export function doConsole(type, arg) {
    const fn = (typeof console[type] === 'function' ? console[type] : console.log);
    fn(...arg);
}

export function doConsoleClear() {
    console.clear();
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
