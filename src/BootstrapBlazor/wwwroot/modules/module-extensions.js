import Data from "./data.js?v=$version"
import EventHandler from "./event-handler.js?v=$version"
import * as Utility from "./utility.js?v=$version"

export function dispose(guid) {
    const hp = Data.get(guid)
    Data.remove(guid)

    if (hp) {
        EventHandler.off(hp.target, hp.eventName, hp.handler)
    }
}

/**
 * @function 事件注册
 * @param {any} guid 唯一编码
 * @param {any} interop C# 委托对象
 * @param {any} invokeMethodName C# 委托对象的回调方法
 * @param {any} eventName 要注册的事件名称
 * @param {any} id 元素id
 * @param {any} el 元素标识
 */
export function addEventListener(guid, interop, invokeMethodName, eventName, id, el) {
    const hp = {
        guid,
        eventName,
        handler: () => {
            interop.invokeMethodAsync(invokeMethodName);
        },
        id, el,
        target: window,
    }

    if (id) {
        hp.target = document.getElementById(id);
    }
    if (el) {
        hp.target = el;
    }

    Data.set(guid, hp)
    EventHandler.on(hp.target, hp.eventName, hp.handler)
}

export async function doAddLink(link) {
    await Utility.addLink(link);
}

export async function doAddLinkWithRel(link, rel) {
    await Utility.addLink(link, rel);
}

export async function doRemoveLink(link) {
    await Utility.remove(link);
}

export async function doAddScript(src) {
    await Utility.doAddScript(src);
}

export async function doRemoveScript(src) {
    await Utility.doRemoveScript(src);
}

export function doConsole(type, arg) {
    const fn = (typeof console[type] === 'function' ? console[type] : console.log);
    fn(...arg);
}

export function doConsoleClear() {
    console.clear();
}

export function addMeta(type, rel, href) {
    try {
        var head = document.getElementsByTagName('head')[0];
        var links = head.getElementsByTagName(type);

        // 检查是否存在相同项
        for (var i = 0; i < links.length; i++) {
            if (links[i].rel === rel && links[i].href === href) {
                return true;
            }
        }

        // 创建并添加新项
        var link = document.createElement(type);
        link.rel = rel;
        link.href = href;
        head.appendChild(link);

        return true;
    } catch (e) {
        return false;
    }
}

export function removeMeta(type, rel, href) {
    try {
        var head = document.getElementsByTagName('head')[0];
        var links = head.getElementsByTagName(type);

        // 检查是否存在相同项
        for (var i = 0; i < links.length; i++) {
            if (links[i].rel === rel && links[i].href === href) {
                head.removeChild(links[i]);
                return true;
            }
        }

        return false;
    } catch (e) {
        return false;
    }
}
