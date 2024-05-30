import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const setListeners = (viewer, index) => {
    if (viewer.prevList && viewer.prevList.length > 0) {
        EventHandler.on(viewer.img, 'click', () => {
            if (!viewer.previewer) {
                viewer.previewer = Data.get(viewer.previewerId)
            }
            if (viewer.previewer) {
                viewer.previewer.viewer.show(index)
            }
        })
    }
}

export function init(id, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const { url, preList, index, async, previewerId, intersection } = options;
    const viewer = {
        element: el,
        img: el.querySelector('img'),
        async: async,
        prevList: preList || [],
        previewerId: previewerId
    }
    if (url) {
        viewer.prevList.push(url)
    }
    Data.set(id, viewer)

    if (intersection) {
        let observer = new IntersectionObserver(enteries => {
            const entry = enteries[0];
            if (entry.isIntersecting) {
                entry.target.setAttribute('src', url);
                observer.unobserve(entry.target);
                observer = null;
            }
        });
        observer.observe(viewer.img);
    }
    else if (viewer.img && viewer.async) {
        viewer.img.setAttribute('src', url)
    }

    setListeners(viewer, index)
}

export function update(id, prevList, index) {
    const viewer = Data.get(id)
    if (viewer.img) {
        EventHandler.off(viewer.img, 'click')
    }

    viewer.prevList = prevList
    setListeners(viewer, index)
}

export function dispose(id) {
    const viewer = Data.get(id)
    Data.remove(id)

    if (viewer.img) {
        EventHandler.off(viewer.img, 'click')
    }
}
