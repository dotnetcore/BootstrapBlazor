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

export async function runJSFile(path, functionName, args) {
    try {
        const module = await import(path);
        const targetFunction = module[functionName] || module.default;

        if (typeof targetFunction === 'function') {
            return targetFunction(...args);
        } else {
            console.error(`Function '${functionName}' not found in module at path: ${path}`);
        }
    } catch (error) {
        console.error('RunJSFile Execution error:', error);
    }
}

export function runJSWithEval(js) {
    try {
        return eval(js);
    } catch (error) {
        console.error('RunJSWithEval Execution error:', error);
    }
}

export function runJSWithFunction(js) {
    try {
        var func = new Function(js);
        return func();
    } catch (error) {
        console.error('RunJSWithFunction Execution error:', error);
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

export function doPrompt(title, defaultValue) {
    return prompt(title, defaultValue);
}

export function doConsole(type, arg) {
    const fn = (typeof console[type] === 'function' ? console[type] : console.log);
    fn(...arg);
}

export function doConsoleClear() {
    console.clear();
}
