import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

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

export function init(id, invoke, method) {
    const el = document.getElementById(id);
    const container = el.querySelector('.bb-waterfall-list');
    const loader = el.querySelector('.bb-wf-loader');
    let itemWidth = 216;
    const itemWidthString = el.getAttribute('data-bb-item-width');
    if (itemWidthString) {
        const itemWidthValue = parseFloat(itemWidthString);
        if (!isNaN(itemWidthValue)) {
            itemWidth = itemWidthValue;
        }
    }
    EventHandler.on(container, 'load', 'img', () => setPositions(container, itemWidth));
    EventHandler.on(window, 'resize', () => setPositions(container, itemWidth));

    Data.set(id, {
        container,
        loader,
        invoke
    });

    invoke.invokeMethodAsync(method);
}

export function append(id, images) {
    const wf = Data.get(id);
    if (wf) {
        images.forEach(img => {
            const item = document.createElement('div');
            item.classList.add('bb-waterfall-item');
            item.innerHTML = `<img alt="" src="${img}" />`;
            wf.container.appendChild(item);
        });
    }
}

export function dispose(id) {
    const wf = Data.get(id);
    Data.remove(id);

    if (wf) {
        EventHandler.off(wf.container, 'load', 'img');
        EventHandler.off(window, 'resize');
    }
}
