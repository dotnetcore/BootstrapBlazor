import Data from "../../modules/data.js"
import Previewer from "../../modules/base-viewer.js"

export function init(id, prevList) {
    const el = document.getElementById(id)

    if (el != null) {
        return
    }

    const baseviewer = Previewer.init(el, prevList)
    const previewer = {
        el,
        baseviewer
    }

    Data.set(id, previewer)
}

export function update(id, prevList) {
    const viewer = Data.get(id)
    viewer.baseviewer.prevList = prevList
}

export function dispose(id) {
    const viewer = Data.get(id)
    if (viewer) {
        Previewer.dispose(viewer.baseviewer)
    }
    Data.remove(id)
}
