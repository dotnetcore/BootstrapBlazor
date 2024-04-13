import Data from "../../modules/data.js"
import Viewer from "../../modules/viewer.js"

export function init(id, prevList) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const viewer = {
        el,
        viewer: Viewer.init(el, prevList)
    }
    Data.set(id, viewer)
}

export function update(id, prevList) {
    const viewer = Data.get(id)
    viewer.viewer.prevList = prevList
}

export function show(id, index) {
    const viewer = Data.get(id)
    viewer.viewer.show(index)
}

export function dispose(id) {
    const viewer = Data.get(id)
    Data.remove(id)

    if (viewer) {
        Viewer.dispose(viewer.viewer)
    }
}
