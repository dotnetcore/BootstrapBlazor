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

export function getElementProperties(id, tag) {
    if (tag === null || tag === undefined || tag === "" || tag.trim() === "") {
        return "wrong tag";
    }

    var el = document.getElementById(id);
    if (el) {
        try {
            return getProperties(el, tag);
        } catch (e) {
            console.warn(e.message);
            return e.message;
        }
    }
}

export function getCSSValue(id, propertyName) {
    try {
        var element = document.getElementById(id);
        if (element) {
            var computedStyle = getComputedStyle(element);
            return computedStyle.getPropertyValue(propertyName);
        }
    } catch (e) {
        console.error(e.message);
        return null;
    }
}

export function getElementRect(id) {
    try {
        var element = document.getElementById(id);
        if (element) {
            var rect = element.getBoundingClientRect();
            return rect;
        }
    } catch (e) {
        console.error(e.message);
        return null;
    }
}

export function runEval(code) {
    try {
        return eval(code);
    } catch (e) {
        console.warn(e.message);
        return e.message;
    }
}

export function doConsole(type, arg) {
    try {
        const fn = (typeof console[type] === 'function' ? console[type] : console.log);
        fn(...arg);
    } catch (e) {
        console.warn(e.message);
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
