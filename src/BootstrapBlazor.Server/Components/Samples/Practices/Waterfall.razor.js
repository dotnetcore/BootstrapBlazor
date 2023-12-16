import { addScript } from "/_content/BootstrapBlazor/modules/utility.js"

let el, msnry, bb, waterfallInvoke, waterfallMethod, lastScrollTop = 0;

const getBlazorAttributeName = (element) => {
    var attrs = element.attributes;
    for (let i = 0; i < attrs.length; i++) {
        if (attrs[i].name.startsWith("b-")) {
            return attrs[i].name;
        }
    }

    return null;
}

const loadImage = (item) => {
    const img = new Image();
    img.src = item.getAttribute("data-wurl");
    img.onload = () => {
        item.src = img.src;
        item.classList.add("waterfall-ok");
        requestAnimationFrame(() => {
            throttle(msnry.layout(), 500, 1000);
        });
    }
    img.onerror = () => {
        item.src = "/lib/waterfall/Fail.svg";
        item.classList.add("waterfall-ok", "waterfall-contain");
        console.info(`Image loading failed：${img.src}`);
    }
    item.removeAttribute("data-wurl");
}

const lazyLoadImages = (element) => {
    const viewHeight = element.offsetHeight;
    const lazyImages = element.querySelectorAll("[data-wurl]");

    lazyImages.forEach((item) => {
        if (item.getAttribute("data-wurl") === "")
            return;

        const rect = item.getBoundingClientRect();

        if (rect.bottom >= 0 && rect.top < viewHeight) {
            if (item.nodeName === "IMG") {
                loadImage(item);
            }
        }
    });
}

const throttle = (func, wait, mustRun) => {
    let timeout;
    let startTime = new Date();

    return function (...args) {
        const curTime = new Date();

        clearTimeout(timeout);
        if (curTime - startTime >= mustRun) {
            func.apply(this, args);
            startTime = curTime;
        } else {
            timeout = setTimeout(() => func.apply(this, args), wait);
        }
    };
};

const isScrollToBottom = (element) => {
    const totalHeight = element.scrollHeight;
    const scrollHeight = element.scrollTop;
    const clientHeight = element.clientHeight;

    const isScrollingUp = scrollHeight < lastScrollTop;
    lastScrollTop = scrollHeight;

    return !isScrollingUp && scrollHeight + clientHeight >= totalHeight - 300;
}

const appendImage = (element) => {
    return new Promise((resolve) => {
        imagesLoaded(element).on("progress", (imgLoad, e) => {
            e.img.parentNode.classList.add("in-loaded");
            msnry.appended(e.img.parentNode);
            msnry.layout();
        }
        ).on("done", () => {
            msnry.once("layoutComplete", () => {
                resolve();
            });
        });
    });
}

const fetchData = async (num) => {
    try {
        return await waterfallInvoke.invokeMethodAsync(waterfallMethod, num);
    } catch (error) {
        console.log(error);
        return null;
    }
}

const onScroll = throttle(async () => {
    lazyLoadImages(el);
    if (isScrollToBottom(el)) {
        console.info("滚动到底部");
        setTimeout(async () => {
            const data = await fetchData(30);
            if (data === null) {

            } else {
                await update(data);
            }
        }, 2000); // 延迟2秒
    }
}, 500, 1000);

const addImage = (parentElement, src) => {
    var item = document.createElement("div");
    item.className = "waterfall-item in-load p-2";
    item.setAttribute(bb, "");

    var img = document.createElement("img");
    img.className = "shadow-sm waterfall-img scrollLoading";
    img.setAttribute(bb, "");
    img.src = "./lib/waterfall/load.gif";
    img.dataset.wurl = src;
    img.referrerPolicy = "no-referrer";

    item.appendChild(img);

    parentElement.appendChild(item);
    return item;
}

const update = async (data) => {
    const element = el.getElementsByClassName("waterfall-container")[0];
    const items = [];

    data.forEach((src, index) => {
        const item = addImage(element, src);
        items.push(item);
    });

    await new Promise((resolve) => {
        appendImage(items).then(resolve);
    });
}

export const init = async (id, invoke, method) => {
    waterfallInvoke = invoke;
    waterfallMethod = method;

    await addScript("./lib/waterfall/masonry.pkgd.min.js")
    await addScript("./lib/waterfall/imagesloaded.pkgd.min.js")

    el = document.getElementById(id);
    bb = getBlazorAttributeName(el);
    const element = el.getElementsByClassName("waterfall-container")[0];
    msnry = new Masonry(element, { itemSelector: ".waterfall-item" });
    const data = await fetchData(30);
    await update(data);

    el.addEventListener("scroll", onScroll);

    lazyLoadImages(el);
}
