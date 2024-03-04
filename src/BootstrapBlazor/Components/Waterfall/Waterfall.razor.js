import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"
import { debounce } from "../../modules/utility.js?v=$version"

const cal = (el, imgWidth) => {
    const containerWidth = el.offsetWidth;
    const columns = Math.floor(containerWidth / imgWidth);
    const spaceNumber = columns + 1;
    const leftSpace = containerWidth - columns * imgWidth;
    const space = leftSpace / spaceNumber;
    return { space, columns }
}

const setPositions = (container, imgWidth) => {
    const info = cal(container, imgWidth);
    const nextTops = new Array(info.columns);
    nextTops.fill(0);
    for (let i = 0; i < container.children.length; i++) {
        const img = container.children[i];
        const minTop = Math.min.apply(null, nextTops);
        img.style.setProperty("top", `${minTop}px`);

        const index = nextTops.indexOf(minTop);
        nextTops[index] += img.offsetHeight + info.space;

        const left = (index + 1) * info.space + index * imgWidth;
        img.style.setProperty("left", `${left}px`);
    }
    const max = Math.max.apply(null, nextTops);
    container.style.setProperty("height", `${max}px`);
}

const createItem = v => {
    const item = document.createElement('div');
    item.classList.add('bb-waterfall-item');
    item.setAttribute('data-bb-waterfall-item-id', v.id);

    const img = document.createElement('img');
    img.setAttribute('src', v.url);
    item.appendChild(img);

    return item;
}

const append = (container, images) => {
    images.forEach(v => {
        const item = createItem(v);
        container.appendChild(item);
    });
}

export function init(id, invoke, method) {
    const el = document.getElementById(id);
    const container = el.querySelector('.bb-waterfall-list');
    const loader = el.querySelector('.bb-wf-loader');
    const imgWidth = parseFloat(container.style.getPropertyValue('--bb-waterfall-item-width'));

    const requestItems = async item => {
        const images = await invoke.invokeMethodAsync(method, item);
        append(container, images);
    }

    EventHandler.on(container, 'load', 'img', () => setPositions(container, imgWidth));
    EventHandler.on(window, 'resize', () => setPositions(container, imgWidth));
    EventHandler.on(window, 'scroll', () => {
        const offsetHeight = (window.innerHeight || document.documentElement.clientHeight) + document.documentElement.scrollTop;
        if (offsetHeight > container.offsetHeight) {
            const last = container.querySelector('.bb-waterfall-item:last-child');
            if (last) {
                var item = { id: last.getAttribute('data-bb-waterfall-item-id'), url: last.querySelector('img').src }
                requestItems(item);
            }
        }
    });

    Data.set(id, {
        container,
        loader,
        invoke
    });

    requestItems(null);
}

export function dispose(id) {
    const wf = Data.get(id);
    Data.remove(id);

    if (wf) {
        EventHandler.off(wf.container, 'load', 'img');
        EventHandler.off(window, 'resize');
        EventHandler.off(window, 'scroll');
    }
}
