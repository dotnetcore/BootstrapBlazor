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

export function getElementPropertiesByTagFromId(id, tag) {
    var el = document.getElementById(id);
    return getProperties(el, tag);
}

export function getDocumentPropertiesByTag(tag) {
    return getProperties(document, tag);
}

export function getElementPropertiesByTag(el, tag) {
    return getProperties(el, tag);
}

export function getProperties(obj, tag) {
    let tags = tag.split('.');
    let tagsCopy = JSON.parse(JSON.stringify(tags));
    var object = obj;
    tagsCopy.map(() => {
        object = object[tags[0]];
        tags.shift();
    })
    return object;
}

export function runJSFileFetch(path) {
    // 使用 fetch() 函数从服务器获取 JavaScript 文件的内容
    fetch(path).then(response => response.text())
        .then(scriptContent => {
            // 将 JavaScript 文件的内容插入到页面中
            eval(scriptContent);
        });
}

export function runJSFileImport(path) {
    // 使用 import() 函数从服务器获取 JavaScript 文件的内容
    import(path).then(module => {
        // 将 JavaScript 文件的内容插入到页面中
        module.default();
    });
}

export function runJSWithEval(js) {
    try {
        return eval(js);
    } catch (error) {
        console.error('执行异常(runJSWithEval)：', error);
    }
}

export function runJSWithFunction(js) {
    try {
        var func = new Function(js);
        return func();
    } catch (error) {
        console.error('执行异常(runJSWithFunction)：', error);
    }
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

export function doAlert(text) {
    alert(text);
}

export function doPrompt(title) {
    return prompt(title);
}
