import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

const setListeners = viewer => {
    if (viewer.prevList.length > 0) {
        EventHandler.on(viewer.img, 'click', () => {
            if (!viewer.previewer) {
                viewer.previewer = Data.get(viewer.previewerId)
            }
            if (viewer.previewer) {
                viewer.previewer.viewer.show()
            }
        })
    }
}

export function init(id, url, preList) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const viewer = {
        element: el,
        img: el.querySelector('img'),
        async: el.getAttribute('data-bb-async'),
        prevList: preList || [],
        previewerId: el.getAttribute('data-bb-previewer-id')
    }
    if (url) {
        viewer.prevList.push(url)
    }
    Data.set(id, viewer)

    if (viewer.img && viewer.async) {
        viewer.img.setAttribute('src', url)
    }

    setListeners(viewer)
}

export function update(id, prevList) {
    const viewer = Data.get(id)
    if (viewer.img) {
        EventHandler.off(viewer.img, 'click')
    }

    viewer.prevList = prevList
    setListeners(viewer)
}

export function dispose(id) {
    const viewer = Data.get(id)
    Data.remove(id)

    if (viewer.img) {
        EventHandler.off(viewer.img, 'click')
    }
}
