import Data from "./base/data.js"
import EventHandler from "./base/event-handler.js"



/**
 * @function 事件注册
 * @param {any} guid 唯一编码
 * @param {any} interop C# 委托对象
 * @param {any} invokeMethodName C# 委托对象的回调方法
 * @param {any} eventName 要注册的事件名称
 * @param {any} id 元素id
 * @param {any} el 元素标识
 */
export function registerEvent(guid, interop, invokeMethodName, eventName, id, el) {
    const hp = {
        eventName: eventName,
        target: window,
        handler: () => {
            interop.invokeMethodAsync(invokeMethodName);
        },
        id: id,
        el: el,
        code: guid
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

export function getIdPropertieByName(id, tag) {
    var el = document.getElementById(id);
    return getPropertie(el, tag);
}

export function getDocumentPropertieByName(tag) {
    return getPropertie(document, tag);
}

export function getElementPropertieByName(el, tag) {
    return getPropertie(el, tag);
}

export function getPropertie(obj, tag) {
    let tags = tag.split('.');
    let tagsCopy = JSON.parse(JSON.stringify(tags));
    var object = obj;
    tagsCopy.map(() => {
        object = object[tags[0]];
        tags.shift();
    })
    return object;
}

export function dispose(code) {
    const hp = Data.get(code)
    Data.remove(code)

    EventHandler.off(hp.target, hp.eventName, hp.handler)
    hp.destroy()
}
