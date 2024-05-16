import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import Editor from "../../editor/Editor.js"

export async function init(id, options) {
    await addLink("./_content/BootstrapBlazor.SvgEditor/editor/svgedit.css")

    const { preload, interop, callback } = options;
    /* for available options see the file `docs/tutorials/ConfigOptions.md */
    const svgEditor = new Editor(document.getElementById(id))
    svgEditor.setConfig({
        canvasName: id,
        lang: "en",
        allowInitialUserOverride: false,
        imgPath: "./_content/BootstrapBlazor.SvgEditor/editor/images/",
        showGrid: true,
        extPath: "/_content/BootstrapBlazor.SvgEditor/editor/extensions/",
        noDefaultExtensions: false
    })
    svgEditor.init()
    svgEditor.topPanel.showSourceEditor = async (e, forceSaving) => {
        const origSource = svgEditor.svgCanvas.getSvgString()
        await interop.invokeMethodAsync(callback, origSource);
    }

    if (preload != null) {
        svgEditor.loadFromString(preload)
    }
    Data.set(id, {
        svgEditor,
        interop
    })

    var timer = setTimeout(() => {
        svgEditor.zoomChanged(window, "canvas", true);
        document.querySelector("#zoom").value = "canvas"
        clearTimeout(timer)
    }, 800)
}

export function updateContent(id, contenet) {
    var editor = Data.get(id)
    if (editor) {
        editor.svgEditor.loadFromString(contenet);
    }
}

export function dispose(id) {
    Data.remove(id)
}
