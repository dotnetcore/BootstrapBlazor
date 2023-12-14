import Editor from "../../editor/Editor.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init() {
    await addLink("./_content/BootstrapBlazor.SvgEditor/editor/svgedit.css")

    /* for available options see the file `docs/tutorials/ConfigOptions.md */
    const svgEditor = new Editor(document.getElementById('container'))
    svgEditor.setConfig({
        allowInitialUserOverride: true,
        imgPath: "./_content/BootstrapBlazor.SvgEditor/editor/images/",
        extensions: [],
        noDefaultExtensions: false,
        userExtensions: [/* { pathName: '/packages/react-test/dist/react-test.js' } */]
    })
    svgEditor.init()
}
